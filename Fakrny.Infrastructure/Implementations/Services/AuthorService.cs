namespace Fakrny.Infrastructure.Implementations.Services;
public class AuthorService : BaseService<Author>, IAuthorService
{
    public AuthorService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public IEnumerable<Author> GetAllWithDetails()
    {
        return _unitOfWork.Repository<Author>().GetQueryable()
            .Include(a => a.Courses)
            .Select(a => new Author
            {
                Id = a.Id,
                Name = a.Name,
                Nickname = a.Nickname,
                IsDeleted = a.IsDeleted,
                Courses = a.Courses.Select(a => new Course() { Id = a.Id }).ToList(),
            }).AsNoTracking().ToList();
    }
}
