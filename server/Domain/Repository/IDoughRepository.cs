namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IDoughRepository : IRepository<Dough>
    {
        public Dough GetByName(string name);

        public Dough Patch(int id, Dough item);

        public IEnumerable<int> GetIdentificators();

        public Dough Insert(Dough item);

        public Dough Update(int id, Dough item);
    }
}
