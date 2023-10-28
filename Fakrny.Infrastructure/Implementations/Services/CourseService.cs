namespace Fakrny.Infrastructure.Implementations.Services;
public class CourseService : BaseService<Course>, ICourseService
{
    public CourseService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public Course? GetByIdWithDetails(int id)
    {
        return _unitOfWork.Repository<Course>().GetQueryable()
            .Include(S => S.Author).Include(s => s.Sections).ThenInclude(x => x.Videos)
            .Select(c => new Course
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Author = new Author { Id = c.Author!.Id, Name = c.Author!.Name },
                IsDeleted = c.IsDeleted,
                IsPaid = c.IsPaid,
                Sections = c.Sections.Select(s => new Section
                {
                    Id = s.Id,
                    Name = s.Name,
                    Number = s.Number,
                    IsDeleted = s.IsDeleted,
                    OrderIndex = s.OrderIndex,
                    Videos = s.Videos.Select(v => new Video { DurationInMin = v.DurationInMin, IsDeleted = v.IsDeleted }).ToList()
                }).ToList()
            }).AsNoTracking().FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Course> GetAllWithDetails()
    {
        return _unitOfWork.Repository<Course>().GetQueryable()
            .Include(S => S.Author).Include(s => s.Sections).ThenInclude(x => x.Videos)
            .Select(c => new Course
            {
                Id = c.Id,
                Name = c.Name,
                Author = new Author { Id = c.Author!.Id, Name = c.Author!.Name },
                IsDeleted = c.IsDeleted,
                IsPaid = c.IsPaid,
                Sections = c.Sections.Select(s => new Section
                {
                    Id = s.Id,
                    Videos = s.Videos.Select(v => new Video { DurationInMin = v.DurationInMin, IsDeleted = v.IsDeleted }).ToList(),
                    IsDeleted = s.IsDeleted
                }).ToList()
            }).AsNoTracking().ToList();
    }
}
