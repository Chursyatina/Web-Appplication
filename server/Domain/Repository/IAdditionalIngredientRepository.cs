namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IAdditionalIngredientRepository : IRepository<AdditionalIngredient>
    {
        public AdditionalIngredient GetByName(string name);

        public AdditionalIngredient Patch(string id, AdditionalIngredient item);

        public IEnumerable<string> GetIdentificators();

        public AdditionalIngredient Insert(AdditionalIngredient item);

        public AdditionalIngredient Update(string id, AdditionalIngredient item);
    }
}
