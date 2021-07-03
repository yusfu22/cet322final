using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cet322finaltodo.Models;
using cet322finaltodo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace cet322finaltodo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<FirmUser> firmManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, UserManager<FirmUser> firmManager)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this.firmManager = firmManager;
        }

        public async  Task<IActionResult> Index()
        {
            List<TodoItem> result;

            if (User.Identity.IsAuthenticated)
            {
                var firmUser = await firmManager.GetUserAsync(HttpContext.User);
                var query = dbContext.todoItems
                    .Include(t => t.Category)
                    .Where(t => t.FirmUserId == firmUser.Id && !t.IsCompleted)
                    .OrderBy(t => t.DueDate)
                    .Take(3);
                result = await query.ToListAsync();

            }
            else
            {
                result = new List<TodoItem>();
            }
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
