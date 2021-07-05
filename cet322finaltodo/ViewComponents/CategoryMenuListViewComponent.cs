using System;
using Microsoft.AspNetCore.Mvc;
using cet322finaltodo.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace cet322finaltodo.ViewComponents
{
    public class CategoryMenuListViewComponent : ViewComponent
    {
        public readonly ApplicationDbContext dbContext;
        public CategoryMenuListViewComponent(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool showEmpty = true)
        {
            var items = await dbContext.Categories.Where(c=> showEmpty || c.TodoItems.Any()).ToListAsync();
            return View(items);
        }
    }
}
