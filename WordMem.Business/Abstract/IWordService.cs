using System;
using System.Collections.Generic;
using System.Text;
using WordMem.Entity;

namespace WordMem.Business.Abstract
{
    public interface IWordService
    {
        List<Word> GetAllWordsByCategoryId(int categoryId);
        List<Word> GetWordsByCategoryId(int categoryId);
        List<Word> GetLearnedWordsByCategoryId(int categoryId);
        List<Word> GetStudiedWordsByCategoryId(int categoryId);
    }
}
