namespace Fakrny.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;
    private const string _entityName = "Course";

    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CourseDto>> GetAllWithDetails()
    {
        var courses = _courseService.GetAllWithDetails();

        var coursesDto = new List<CourseDto>();

        foreach (var course in courses)
        {
            coursesDto.Add(new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                IsDeleted = course.IsDeleted,
                IsPaid = course.IsPaid,
                DurationInMin = course.Sections.Where(s => !s.IsDeleted).Select(s => s.Videos.Where(v => !v.IsDeleted).Sum(v => v.DurationInMin)).Sum(),
                Author = new LookupDto
                {
                    Id = course.AuthorId,
                    Name = course.Author!.Name,
                },
            });
        }

        return Ok(coursesDto);
    }

    [HttpGet("WithDetails/{id}")]
    public ActionResult<CourseDetailsDto> GetByIdWithDetails(int id)
    {
        var course = _courseService.GetByIdWithDetails(id);

        if (course is null)
            return NotFound($"Can not find {_entityName} with Id equal {id}");

        var CourseDto = new CourseDetailsDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            IsDeleted = course.IsDeleted,
            IsPaid = course.IsPaid,
            DurationInMin = course.Sections.Where(s => !s.IsDeleted).Select(s => s.Videos.Where(v => !v.IsDeleted).Sum(v => v.DurationInMin)).Sum(),
            Author = new LookupDto
            {
                Id = course.AuthorId,
                Name = course.Author!.Name
            },
            Sections = course.Sections.Select(s => new SectionDto
            {
                Id = s.Id,
                Name = s.Name,
                Number = s.Number,
                OrderIndex = s.OrderIndex,
                IsDeleted = s.IsDeleted,
                DurationInMin = s.Videos.Where(v => !v.IsDeleted).Select(v => v.DurationInMin).Sum()
            }),
        };

        return Ok(CourseDto);
    }

    [HttpGet("{id}")]
    public ActionResult<CoursePostDto> GetById(int id)
    {
        var course = _courseService.GetById(id);

        if (course is null)
            return NotFound($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(course));
    }

    [HttpPost("Add")]
    public ActionResult<CoursePostDto> Add(CoursePostDto courseDto)
    {
        var course = new Course
        {
            Id = courseDto.Id,
            Name = courseDto.Name,
            Description = courseDto.Description,
            AuthorId = courseDto.AuthorId,
            IsDeleted = courseDto.IsDeleted
        };

        _courseService.Add(course);

        return Ok(MapToDto(course));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<CoursePostDto> Update(CoursePostDto courseDto, int id)
    {
        var course = _courseService.GetById(id);

        if (course is null)
            return NotFound($"Can not find {_entityName} with Id equal {id}");

        course.Name = courseDto.Name;
        course.Description = courseDto.Description;
        course.AuthorId = courseDto.AuthorId;
        course.IsDeleted = courseDto.IsDeleted;
        course.IsPaid = courseDto.IsPaid;

        _courseService.Update(course);

        return Ok(MapToDto(course));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var course = _courseService.GetById(id);

        if (course is null)
            return NotFound($"Can not find {_entityName} with this Id");

        course.IsDeleted = !course.IsDeleted;

        _courseService.Update(course);

        return Ok($"Status Toggled Successfully for {_entityName} with Id equal {course.Id}");
    }

    private CoursePostDto MapToDto(Course course)
    {
        var courseDto = new CoursePostDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            AuthorId = course.AuthorId,
            IsDeleted = course.IsDeleted,
            IsPaid = course.IsPaid,
        };

        return courseDto;
    }
}
