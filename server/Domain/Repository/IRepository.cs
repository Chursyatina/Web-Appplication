namespace Domain.Repository
{
    using System.Collections.Generic;

    public interface IRepository<TItem>
    {
        IEnumerable<TItem> GetAll();

        TItem GetById(string id);

        void Delete(string id);
    }
}
