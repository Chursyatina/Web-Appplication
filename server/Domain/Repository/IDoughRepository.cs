namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IDoughRepository : IRepository<Dough>
    {
        public Dough GetByName(string name);

        public Dough Patch(string id, Dough item);

        public IEnumerable<string> GetIdentificators();

        public Dough Insert(Dough item);

        public Dough Update(string id, Dough item);
    }
}
