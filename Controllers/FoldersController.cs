using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniNotesAPI.Models;

namespace UniNotesAPI.Controllers
{
    [ApiController]
    [Route("api/folders")]
    public class FoldersController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public FoldersController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpGet("{userId}/all")]
        public async Task<ActionResult<IEnumerable<Folder>>> GetAllFromUser(int userId)
        {
            return await _context.Folders.Where(d => d.UserId == userId).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Folder>> Get(int id)
        {
            var folder = await _context.Folders.FirstOrDefaultAsync(d => d.Id == id);
            if (CheckNullity(folder))
            {
                return NotFound();
            }
            return Ok(folder);
        }
        [HttpPost("{userId}")]
        public async Task<ActionResult> Post(int userId, Folder folder)
        {
            if (CheckNullity(folder))
            {
                return BadRequest();
            }
            folder.UserId = userId;
            _context.Folders.Add(folder);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Folder folder)
        {
            if (CheckNullity(folder))
            {
                return BadRequest();
            }
            if (id != folder.Id)
            {
                return BadRequest();
            }
            _context.Entry(folder).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var folder = await _context.Folders.FirstOrDefaultAsync(d => d.Id == id);
            if (CheckNullity(folder))
            {
                return NotFound();
            }
            _context.Folders.Remove(folder);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private static bool CheckNullity(object? obj)
        {
            return Equals(obj, null);
        }
    }
}