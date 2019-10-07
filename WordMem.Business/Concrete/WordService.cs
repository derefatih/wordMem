using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WordMem.Business.Abstract;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace WordMem.Business.Concrete
{
    public class WordService : IWordService
    {
        private IUnitOfWork uow;
        public WordService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public List<Word> GetAllWordsByCategoryId(int categoryId)
        {
            return uow.Words.GetAll()
            .Include(i => i.WordStatistic)
            .Where(i => i.WordCategories.Any(k => k.CategoryId == categoryId)).ToList();

        }

        public List<Word> GetLearnedWordsByCategoryId(int categoryId)
        {
            return GetAllWordsByCategoryId(categoryId).Where(i =>  i.WordStatistic.IsLearned == true).ToList();
        }

        public List<Word> GetStudiedWordsByCategoryId(int categoryId)
        {
            return GetAllWordsByCategoryId(categoryId).Where(i => i.WordStatistic.IsStudied == true && i.WordStatistic.IsLearned==false).ToList();
        }

        public List<Word> GetWordsByCategoryId(int categoryId)
        {
            return GetAllWordsByCategoryId(categoryId).Where(i => i.WordStatistic.IsLearned == false && i.WordStatistic.IsStudied == false).ToList();
        }
    }
}
