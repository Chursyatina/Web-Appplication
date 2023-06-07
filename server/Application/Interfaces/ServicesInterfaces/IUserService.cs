namespace Application.Interfaces.ServicesInterfaces
{
    using Domain.Models;

    public interface IUserService
    {
        public User GetModelById(string id);

        public User InitializaBasket(string id);

        public User InitializaBasketByPhone(string phone);
    }
}
