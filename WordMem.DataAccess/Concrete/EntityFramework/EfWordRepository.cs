
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace WordMem.DataAccess.Concrete.EntityFramework
{
    public class EfWordRepository:EfGenericRepository<Word>,IWordRepository
    {
        public EfWordRepository(DBContext context) : base(context)
        {

        }

        public DBContext DBContext
        {
            get { return context as DBContext; }
        }
    }
}
