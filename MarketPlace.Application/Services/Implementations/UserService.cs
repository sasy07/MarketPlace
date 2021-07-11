using System;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Account;
using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        #region constructor

        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHelper _passwordHelper;

        public UserService(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
        }

        #endregion

        #region account

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {
            if (!await IsUserExistByMobileNumber(register.Mobile))
            {
                User user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Mobile = register.Mobile,
                    Password = _passwordHelper.EncodePasswordMd5(register.Password),
                    EmailActiveCode = Guid.NewGuid().ToString("N"),
                    MobileActiveCode = new Random().Next(100000, 999999).ToString()
                };
                _userRepository.AddEntity(user);
                _userRepository.SaveChanges();
                //TODO Send Activation SMS
                return RegisterUserResult.Success;
            }

            return RegisterUserResult.MobileExists;
        }

        public async Task<bool> IsUserExistByMobileNumber(string mobile)
        {
            return await _userRepository.GetQuery()
                .AsQueryable()
                .AnyAsync(u => u.Mobile == mobile);
        }

        public async Task<LoginUserResult> GetUserForLogin(LoginUserDTO login)
        {
            var user = await GetUserByMobile(login.Mobile);
            if (user == null) return LoginUserResult.NotFound;
            if (!user.IsMobileActive) return LoginUserResult.NotActivated;
            if (user.Password != _passwordHelper.EncodePasswordMd5(login.Password)) return LoginUserResult.NotFound;
            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByMobile(string mobile)
        {
            return await _userRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(u => u.Mobile == mobile);
        }

        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            _userRepository.DisposeAsync();
        }

        #endregion
    }
}