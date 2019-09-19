
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace WordMem.DataAccess.Concrete.EntityFramework
{
    public class EfWordStatisticRepository: EfGenericRepository<WordStatistic>, IWordStatisticRepository
    {
        public EfWordStatisticRepository(DBContext context) : base(context)
        {

        }
    }
}
