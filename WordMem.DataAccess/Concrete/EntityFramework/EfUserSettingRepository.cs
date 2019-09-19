
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordMem.DataAccess.Abstract;
using WordMem.Entity;

namespace WordMem.DataAccess.Concrete.EntityFramework
{
    public class EfUserSettingRepository:EfGenericRepository<UserSetting>,IUserSettingRepository
    {
        public EfUserSettingRepository(DBContext context) : base(context)
        {

        }
    }
}
