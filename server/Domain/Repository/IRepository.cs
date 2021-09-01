namespace Domain.Repository
{
    using System.Collections.Generic;

    public interface IRepository<TItem>
    {
        IEnumerable<TItem> GetAll();

        TItem GetById(int id);

        void Delete(int id);
    }
}
