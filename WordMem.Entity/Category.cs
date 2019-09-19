using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMem.Entity;

namespace WordMem.Entity
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<WordCategory> WordCategories { get; set; }
    }
}
