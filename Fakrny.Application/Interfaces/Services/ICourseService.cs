namespace Fakrny.Application.Interfaces.Services;
public interface ICourseService : IBaseService<Course>
{
    Course? GetByIdWithDetails(int id);
    IEnumerable<Course> GetAllWithDetails();
}
