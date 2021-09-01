namespace Application.Interfaces
{
    using System.Collections.Generic;

    public interface IService<TEntity, TCreateRequest, TUpdateRequest, TPatchRequest>
        where TCreateRequest : IRequestDto
        where TUpdateRequest : IRequestDto
        where TPatchRequest : IRequestDto
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        TEntity Update(int id, TUpdateRequest item);

        TEntity Patch(int id, TPatchRequest item);

        TEntity Insert(TCreateRequest item);

        void Delete(int id);
    }
}
