
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMem.DataAccess.Abstract;

namespace WordMem.DataAccess.Concrete.EntityFramework
{
    public class EfUnitOfWorks : IUnitOfWork
    {
        private readonly DBContext dbContext;

        public EfUnitOfWorks(DBContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException("dbcontext cannot be null");
        }
        
        private ICategoryRepository _categories;
        private IWordRepository _words;
        private IWordStatisticRepository _wordStatistics;
        private IUserSettingRepository _userSettings;



        public ICategoryRepository Categories {
            get
            {
                return _categories ?? (_categories = new EfCategoryRepository(dbContext));
            }
        }

        public IWordRepository Words {
            get
            {
                return _words ?? (_words = new EfWordRepository(dbContext));
            }
        }

        public IWordStatisticRepository WordStatistics
        {
            get
            {
                return _wordStatistics ?? (_wordStatistics = new EfWordStatisticRepository(dbContext));
            }
        }


        public IUserSettingRepository UserSettings
        {
            get
            {
                return _userSettings ?? (_userSettings = new EfUserSettingRepository(dbContext));
            }
        }


        public int SaveChanges()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }


    }
}
