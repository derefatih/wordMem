
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace WordMem.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryRepository : EfGenericRepository<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DBContext context):base(context)
        {

        }

        public DBContext DBContext
        {
            get { return context as DBContext; }
        }

        public Category GetByName(string name)
        {
           return DBContext.Categories.Where(i => i.CategoryName == name).FirstOrDefault();
        }
    }
}
