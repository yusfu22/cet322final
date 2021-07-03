using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace cet322finaltodo.Models
{
    public class FirmUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual List<TodoItem> TodoItems { get; set; }
    }
}
