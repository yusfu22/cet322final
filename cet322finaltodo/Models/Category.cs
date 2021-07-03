using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cet322finaltodo.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(50)]
        
        public string Name { get; set; }
        public virtual List<TodoItem> TodoItems { get; set; }
    }
}
