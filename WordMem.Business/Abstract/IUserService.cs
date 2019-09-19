using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordMem.Entity;

namespace WordMem.Business.Abstract
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
