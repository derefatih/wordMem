using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordMem.DataAccess.Abstract
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Categories { get; }
        IWordRepository Words { get; }
        IWordStatisticRepository WordStatistics { get; }
        IUserSettingRepository UserSettings { get; }


        int SaveChanges();
    }
}
