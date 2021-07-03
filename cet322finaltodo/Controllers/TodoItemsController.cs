using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cet322finaltodo.Data;
using cet322finaltodo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace cet322finaltodo.Controllers
{
    [Authorize]
    public class TodoItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<FirmUser> _firmManager;

        public TodoItemsController(ApplicationDbContext context, UserManager<FirmUser> firmManager)
        {
            _context = context;
            _firmManager = firmManager;
        }

        // GET: TodoItems
      
        public async Task<IActionResult> Index(SearchViewModel searchModel)
        {
            var firmUser = await _firmManager.GetUserAsync(HttpContext.User);
            var query = _context.todoItems.Include(t => t.Category).Where(t=>t.FirmUserId == firmUser.Id).AsQueryable();
            if (!searchModel.ShowAll)
            {
                query = query.Where(t => !t.IsCompleted);
            }
            if (!String.IsNullOrWhiteSpace(searchModel.SearchText))
            {
                query = query.Where(t => t.Title.Contains(searchModel.SearchText));
            }
                query = query.OrderBy(t => t.DueDate);
            searchModel.Result = await query.ToListAsync();

            return View(searchModel);
        }

        // GET: TodoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var oldTodo = await _context.todoItems.FindAsync(id);
            var currentUser = await _firmManager.GetUserAsync(HttpContext.User);
            if (oldTodo.FirmUserId != currentUser.Id)
            {
                return Unauthorized();

            }


            var todoItem = await _context.todoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // GET: TodoItems/Create
       
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Title,Description,IsCompleted,DueDate,CategoryId,CreatedDate,FirmUser")] TodoItem todoItem)
        {
            var firmUser = await _firmManager.GetUserAsync(HttpContext.User);

            todoItem.FirmUserId = firmUser.Id;
            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", todoItem.CategoryId);
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.todoItems.FindAsync(id);

           
            var currentUser = await _firmManager.GetUserAsync(HttpContext.User);
            if (todoItem.FirmUserId != currentUser.Id)
            {
                return Unauthorized();

            }

            if (todoItem == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", todoItem.CategoryId);
            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,IsCompleted,DueDate,CategoryId")] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldTodo = await _context.todoItems.FindAsync(id);
                    var currentUser = await _firmManager.GetUserAsync(HttpContext.User);
                    if(oldTodo.FirmUserId != currentUser.Id)
                    {
                        return Unauthorized();

                    }

                    oldTodo.Title = todoItem.Title;
                    oldTodo.CompletedDate = todoItem.CompletedDate;
                    oldTodo.DueDate = todoItem.DueDate;
                    oldTodo.CategoryId = todoItem.CategoryId;
                    oldTodo.Description = todoItem.Description;
                    oldTodo.IsCompleted = todoItem.IsCompleted;
                    _context.Update(oldTodo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", todoItem.CategoryId);
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.todoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoItem = await _context.todoItems.FindAsync(id);
            _context.todoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Complete(int id, bool currentShowAllValue)
        {
            var todoItemItem = _context.todoItems.FirstOrDefault(todo => todo.Id == id);
            if (todoItemItem == null)
            {
                return NotFound();
            }
            todoItemItem.IsCompleted = true;
            todoItemItem.CompletedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { showall = currentShowAllValue});

        }

        

        private bool TodoItemExists(int id)
        {
            return _context.todoItems.Any(e => e.Id == id);
        }
    }
}
