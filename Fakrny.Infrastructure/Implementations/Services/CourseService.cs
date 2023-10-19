namespace Fakrny.Infrastructure.Implementations.Services;
public class CourseService : BaseService<Course>, ICourseService
{
    public CourseService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
