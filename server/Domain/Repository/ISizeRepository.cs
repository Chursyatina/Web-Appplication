namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface ISizeRepository : IRepository<Size>
    {
        public Size GetByName(string name);

        public Size Patch(string id, Size item);

        public IEnumerable<string> GetIdentificators();

        public Size Insert(Size item);

        public Size Update(string id, Size item);
    }
}
