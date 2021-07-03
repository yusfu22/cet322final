using System;
using System.Collections.Generic;

namespace cet322finaltodo.Models
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
        }
        public string SearchText { get; set; }
        public bool ShowAll { get; set; }
        public List<TodoItem> Result { get; set; }
    }
}
