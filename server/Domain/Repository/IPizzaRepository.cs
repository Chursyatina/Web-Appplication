namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IPizzaRepository : IRepository<Pizza>
    {
        public Pizza GetByName(string name);

        public Pizza Update(string id, Pizza item, List<string> ingredientsIds);

        public Pizza Patch(string id, Pizza item, List<string> ingredientsIds);

        public IEnumerable<string> GetIdentificators();

        public Pizza Insert(Pizza item, List<string> ingredientsIds);
    }
}
