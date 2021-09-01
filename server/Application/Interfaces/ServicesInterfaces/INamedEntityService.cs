namespace Application.Interfaces.ServicesInterfaces
{
    public interface INamedEntityService<TEntity>
    {
        public TEntity GetByName(string name);
    }
}
