namespace Fakrny.Application.Interfaces;
public interface IUnitOfWork
{
    int Complete();
    IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class;
}
