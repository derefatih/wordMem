using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.CrossCutting.DTOs
{
    public class UserSettingsDTO
    {
        public int TestWordCount { get; set; }
        public bool Compare { get; set; }
        public bool Fill { get; set; }
        public bool Random { get; set; }

    }
}
