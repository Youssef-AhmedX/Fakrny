namespace Fakrny.Infrastructure.Implementations.Services;
public class SectionService : BaseService<Section>, ISectionService
{
    public SectionService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public Section? GetByIdWithDetails(int id)
    {
        return _unitOfWork.Repository<Section>().GetQueryable()
            .Include(s => s.Course).Include(s => s.Videos)
            .Select(s => new Section
            {
                Id = s.Id,
                Name = s.Name,
                IsDeleted = s.IsDeleted,
                Description = s.Description,
                Number = s.Number,
                OrderIndex = s.OrderIndex,
                CourseId = s.CourseId,
                Course = new Course { Id = s.Course!.Id, Name = s.Course.Name, },
                Videos = s.Videos.Select(v => new Video
                {
                    Id = v.Id,
                    Name = v.Name,
                    Number = v.Number,
                    DurationInMin = v.DurationInMin,
                    OrderIndex = v.OrderIndex,
                    IsDeleted = v.IsDeleted,
                }).ToList()
            }).AsNoTracking().FirstOrDefault(c => c.Id == id);
    }
}
