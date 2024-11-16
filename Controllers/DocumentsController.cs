using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniNotesAPI.Models;

namespace UniNotesAPI.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public DocumentsController(ApiDbContext context)
        {
            _context = context;
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Document>>> Get(int userId)
        {
            return await _context.Documents.Where(d => d.UserId == userId).ToListAsync();
        }
        [HttpGet("{userId}/{id}")]
        public async Task<ActionResult<Document>> Get(int userId, int id)
        {
            var document = await _context.Documents.Where(d => d.UserId == userId).FirstOrDefaultAsync(d => d.Id == id);
            if (CheckNullity(document))
            {
                return NotFound();
            }
            return Ok(document);
        }
        [HttpPost("{userId}")]
        public async Task<ActionResult> Post(int userId, Document document)
        {
            if (CheckNullity(document))
            {
                return BadRequest();
            }
            document.UserId = userId;
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{userId}/{id}")]
        public async Task<ActionResult> Put(int userId, int id, Document document)
        {
            if (CheckNullity(document))
            {
                return BadRequest();
            }
            if (id != document.Id)
            {
                return BadRequest();
            }
            _context.Entry(document).State = EntityState.Modified;
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
        [HttpDelete("{userId}/{id}")]
        public async Task<ActionResult> Delete(int userId, int id)
        {
            var document = await _context.Documents.Where(d => d.UserId == userId).FirstOrDefaultAsync(d => d.Id == id);
            if (CheckNullity(document))
            {
                return NotFound();
            }
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private static bool CheckNullity(object? obj)
        {
            return Equals(obj, null);
        }
    }
}