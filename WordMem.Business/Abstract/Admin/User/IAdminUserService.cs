using System;
using System.Collections.Generic;
using System.Text;
using WordMem.CrossCutting.DTOs.Admin.User;

namespace WordMem.Business.Abstract.Admin.User
{
    public interface IAdminUserService
    {
        UserListDto GetAll();
    }
}
