namespace Domain.Repository
{
    using Domain.Models;

    public interface IBasketRepository : IRepository<Basket>
    {
        public Basket UpdateByModel(Basket basket);
    }
}
