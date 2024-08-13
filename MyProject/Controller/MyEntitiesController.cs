using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Model;
using Microsoft.AspNetCore.SignalR;
using MyProject.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyEntitiesController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IHubContext<MyHub> _hubContext;

        public MyEntitiesController(MyDbContext context, IHubContext<MyHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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

            // Notify all clients of the new entity
            await _hubContext.Clients.All.SendAsync("ReceiveItemUpdate", myEntity.Name);

            return CreatedAtAction(nameof(GetMyEntity), new { id = myEntity.Id }, myEntity);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MyEntity>> GetMyEntity(int id)
        {
            var entity = await _context.MyEntities.FindAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMyEntity(int id, MyEntity myEntity)
        {
            if (id != myEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(myEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Notify all clients of the updated entity
            await _hubContext.Clients.All.SendAsync("ReceiveItemUpdate", myEntity.Name);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyEntity(int id)
        {
            var entity = await _context.MyEntities.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.MyEntities.Remove(entity);
            await _context.SaveChangesAsync();

            // Notify all clients of the deleted entity
            await _hubContext.Clients.All.SendAsync("ReceiveItemUpdate", entity.Name);

            return NoContent();
        }

        private bool MyEntityExists(int id)
        {
            return _context.MyEntities.Any(e => e.Id == id);
        }
    }
}
