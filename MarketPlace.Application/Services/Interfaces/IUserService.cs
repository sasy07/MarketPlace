using System;
using System.Threading.Tasks;
using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.DataLayer.Entities.Account;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IUserService:IAsyncDisposable
    {
        #region account

        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);
        Task<bool> IsUserExistByMobileNumber(string mobile);
        Task<LoginUserResult> GetUserForLogin(LoginUserDTO login);
        Task<User> GetUserByMobile(string mobile);

        #endregion
    }
}