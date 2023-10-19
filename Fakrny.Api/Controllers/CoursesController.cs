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
    public ActionResult<IEnumerable<CourseDto>> GetAll(bool IsGetLookup = false)
    {
        var courses = _courseService.GetAll();

        var coursesDto = new List<CourseDto>();

        foreach (var course in courses)
        {
            coursesDto.Add(MapToDto(course));
        }

        return Ok(coursesDto);
    }

    [HttpGet("{id}")]
    public ActionResult<CourseDto> GetById(int id, bool IsGetLookup = false)
    {
        var course = _courseService.GetById(id);

        if (course is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        return Ok(MapToDto(course));
    }

    [HttpPost("Add")]
    public ActionResult<CourseDto> Add(CourseDto courseDto)
    {
        var course = MapToEntity(courseDto);

        _courseService.Add(course);

        return Ok(MapToDto(course));
    }

    [HttpPut("Update/{id}")]
    public ActionResult<CourseDto> Update(CourseDto courseDto, int id)
    {
        var course = _courseService.GetById(id);

        if (course is null)
            return BadRequest($"Can not find {_entityName} with Id equal {id}");

        course.Name = courseDto.Name;

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

    private CourseDto MapToDto(Course course)
    {
        var courseDto = new CourseDto
        {
            Id = course.Id,
            Name = course.Name,
            IsDeleted = course.IsDeleted
        };

        return courseDto;
    }

    private Course MapToEntity(CourseDto courseDto)
    {
        var course = new Course
        {
            Id = courseDto.Id,
            Name = courseDto.Name,
            IsDeleted = courseDto.IsDeleted
        };

        return course;
    }
}
