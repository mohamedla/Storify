using Entities.DataTransferObjects.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Material
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserAuthDTO authUser);
        Task<string> CreateToken();
    }
}
