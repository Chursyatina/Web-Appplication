namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IPizzaRepository : IRepository<Pizza>
    {
        public Pizza GetByName(string name);

        public Pizza Update(int id, Pizza item, List<int> ingredientsIds);

        public Pizza Patch(int id, Pizza item, List<int> ingredientsIds);

        public IEnumerable<int> GetIdentificators();

        public Pizza Insert(Pizza item);
    }
}
