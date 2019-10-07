using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.CrossCutting.DTOs
{
    public class WordListDTO
    {

        public WordListDTO()
        {
            Words = new List<WordList>();
            LearnedWords = new List<WordList>();
            StudiedWords = new List<WordList>();
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<WordList> Words { get; set; }
        public int WordCount { get; set; }

        public List<WordList> LearnedWords { get; set; }
        public int LearnedWordCount { get; set; }

        public List<WordList> StudiedWords { get; set; }
        public int StudiedWordCount { get; set; }
    }

    public class WordList
    {
        public int WordId { get; set; }
        public string OwnLang { get; set; }
        public string ForeignLang { get; set; }
        public string Image { get; set; }
        public int Asked { get; set; }
        public int Answered { get; set; }
    }
}
