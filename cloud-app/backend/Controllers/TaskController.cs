using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudApp.Models;
using CloudApp.Models.DTOs;
using CloudBackend.Data; 
namespace CloudApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAll()
{
    var tasks = await _context.Tasks
        .Select(t => new TaskReadDto
        {
            Id = t.Id,
            Name = t.Name,
            IsCompleted = t.IsCompleted,
        })
        .ToListAsync();

    return Ok(tasks);
}

[HttpGet("{id}")]
public async Task<ActionResult<TaskReadDto>> GetById(int id)
{
    var task = await _context.Tasks
        .Where(t => t.Id == id)
        .Select(t => new TaskReadDto
        {
            Id = t.Id,
            Name = t.Name,
            IsCompleted = t.IsCompleted,
        })
        .FirstOrDefaultAsync();

    if (task == null)
        return NotFound();

    return Ok(task);
}
}