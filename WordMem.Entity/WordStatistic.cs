using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.Entity
{
    public class WordStatistic
    {
        public int WordStatisticId { get; set; }
        public bool IsLearned { get; set; }
        public bool IsStudied { get; set; }
        public int Asked { get; set; }
        public int Answered { get; set; }

        public int WordId { get; set; }
        public Word Word { get; set; }
    }
}
