using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.Entity
{
    public class Word
    {
        public int WordId { get; set; }
        public string OwnLang { get; set; }
        public string ForeignLang { get; set; }
        public string SampleSentence { get; set; }
        public string Image { get; set; }

        public List<WordCategory> WordCategories { get; set; }

        public WordStatistic WordStatistic { get; set; }

    }
}
