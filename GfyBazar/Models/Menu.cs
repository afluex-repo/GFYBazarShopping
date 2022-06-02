using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingPortal.Models
{
    public class Menu
    {
        public List<Main> Main { get; set; }


        
    }
    public class Main
    {
        public string MainCategory { get; set; }
        public string MainCategoryId { get; set; }
    }
}