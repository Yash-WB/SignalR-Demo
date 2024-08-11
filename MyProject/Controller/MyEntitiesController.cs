using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Model;

[ApiController]
[Route("api/[controller]")]
public class MyEntitiesController : ControllerBase
{
    private readonly MyDbContext _context;

    public MyEntitiesController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MyEntity>>> GetMyEntities()
    {
        return await _context.MyEntities.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<MyEntity>> PostMyEntity(MyEntity myEntity)
    {
        _context.MyEntities.Add(myEntity);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMyEntity", new { id = myEntity.Id }, myEntity);
    }
}
