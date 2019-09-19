
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.Entity
{
    public class WordCategory
    {
        public int WordId { get; set; }
        public Word Word { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
