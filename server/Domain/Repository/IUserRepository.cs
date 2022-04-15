namespace Domain.Repository
{
    using Domain.Models;

    public interface IUserRepository : IRepository<User>
    {
        public User InitializeBasket(string id);

        public User InitializeBasketByPhone(string phone);
    }
}
