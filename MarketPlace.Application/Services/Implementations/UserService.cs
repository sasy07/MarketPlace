using System.Threading.Tasks;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Repository;

namespace MarketPlace.Application.Services.Implementations
{
    public class UserService:IUserService
    {
        #region constructor

        private readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
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