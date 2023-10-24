namespace Fakrny.Application.Interfaces;
public interface IUnitOfWork
{
    int Complete();
    IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class;
    ISectionRepository SectionRepository();
    IVideoRepository VideoRepository();
    ICourseRepository CourseRepository();
}
