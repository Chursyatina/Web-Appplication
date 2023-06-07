namespace Application.Services
{
    using Application.Interfaces.ServicesInterfaces;
    using Domain.Models;
    using Domain.Repository;

    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetModelById(string id)
        {
            var existingUser = _userRepository.GetById(id);

            return existingUser;
        }

        public User InitializaBasket(string id)
        {
            return _userRepository.InitializeBasket(id);
        }

        public User InitializaBasketByPhone(string phone)
        {
            return _userRepository.InitializeBasketByPhone(phone);
        }
    }
}
