namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface ISizeRepository : IRepository<Size>
    {
        public Size GetByName(string name);

        public Size Patch(int id, Size item);

        public IEnumerable<int> GetIdentificators();

        public Size Insert(Size item);

        public Size Update(int id, Size item);
    }
}
