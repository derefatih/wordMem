using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.CrossCutting.DTOs
{
    public class CategoryListDTO
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public int TotalCount { get; set; }
        public int LearnedCount { get; set; }
        public int Percentage { get; set; }
    }
}
