namespace Application.Interfaces
{
    using System.Collections.Generic;

    public interface IService<TEntity, TCreateRequest, TUpdateRequest, TPatchRequest>
        where TCreateRequest : IRequestDto
        where TUpdateRequest : IRequestDto
        where TPatchRequest : IRequestDto
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(string id);

        TEntity Update(string id, TUpdateRequest item);

        TEntity Patch(string id, TPatchRequest item);

        TEntity Insert(TCreateRequest item);

        void Delete(string id);
    }
}
