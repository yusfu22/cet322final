using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using cet322finaltodo.Models;

namespace cet322finaltodo.Data
{
    public class ApplicationDbContext : IdentityDbContext<FirmUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<TodoItem> todoItems { get; set; } 
    }
}
