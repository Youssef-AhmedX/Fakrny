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
    public ActionResult<IEnumerable<CourseGetDto>> GetAllWithDetails()
    {
        var courses = _courseService.GetAllWithDetails();

        var coursesDto = new List<CourseGetDto>();

        foreach (var course in courses)
        {
            coursesDto.Add(new CourseGetDto
            {
                Id = course.Id,
                Name = course.Name,
                Author = new LookupDto
                {
                    Id = course.AuthorId,
                    Name = course.Author!.Name,
                },
                IsDeleted = course.IsDeleted,
                Duration = course.Sections.Select(s => s.Videos.Sum(v => v.DurationInMin)).Sum(),
            });
        }

        return Ok(coursesDto);
    }

    [HttpGet("WithDetails/{id}")]
    public ActionResult<CourseGetDetailsDto> GetByIdWithDetails(int id)
    {
        var course = _courseService.GetByIdWithDetails(id);

        if (course is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        var CourseDto = new CourseGetDetailsDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            IsDeleted = course.IsDeleted,
            Author = new LookupDto
            {
                Id = course.AuthorId,
                Name = course.Author!.Name,
            },
            Sections = course.Sections.Select(s => new LookupDto { Id = s.Id, Name = s.Name })
        };

        return Ok(CourseDto);
    }

    [HttpGet("{id}")]
    public ActionResult<CoursePostDto> GetById(int id)
    {
        var course = _courseService.GetById(id);

        if (course is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        var CourseDto = new CoursePostDto
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            AuthorId = course.AuthorId,
            IsDeleted = course.IsDeleted,
        };

        return Ok(CourseDto);
    }

    [HttpPost("Add")]
    public ActionResult<CoursePostDto> Add(CoursePostDto courseDto)
    {
        var course = MapToEntity(courseDto);

        _courseService.Add(course);

        return Ok(MapToDto(course));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<CoursePostDto> Update(CoursePostDto courseDto, int id)
    {
        var course = _courseService.GetById(id);

        if (course is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        course.Name = courseDto.Name;
        course.Description = courseDto.Description;
        course.AuthorId = courseDto.AuthorId;
        course.IsDeleted = courseDto.IsDeleted;

        _courseService.Update(course);

        return Ok(MapToDto(course));
    }

    [HttpPut("ToggleStatus/{id}")]
    public ActionResult ToggleStatus(int id)
    {
        var course = _courseService.GetById(id);

        if (course is null)
            return BadRequest($"Can not find {_entityName} with this Id");

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
            IsDeleted = course.IsDeleted
        };

        return courseDto;
    }

    private Course MapToEntity(CoursePostDto courseDto)
    {
        var course = new Course
        {
            Id = courseDto.Id,
            Name = courseDto.Name,
            Description = courseDto.Description,
            AuthorId = courseDto.AuthorId,
            IsDeleted = courseDto.IsDeleted
        };

        return course;
    }
}
