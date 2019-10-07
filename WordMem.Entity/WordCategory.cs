
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.Entity
{
    public class WordCategory
    {
        [JsonIgnore]
        public int WordId { get; set; }
        [JsonIgnore]
        public Word Word { get; set; }

        [JsonIgnore]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
