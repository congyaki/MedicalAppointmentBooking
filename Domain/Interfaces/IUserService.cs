using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterVM model);
        Task<AuthenticationVM> GetTokenAsync(TokenRequestVM model);
        Task<string> AddRoleAsync(AddRoleVM model);

        Task<string> GetUserId();
    }
}
