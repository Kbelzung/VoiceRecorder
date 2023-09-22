using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MemoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Memo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Memo>>> Getmemos()
        {
            if (_context.memos == null)
            {
                return NotFound();
            }
            return await _context.memos.ToListAsync();
        }

        // GET: api/Memo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Memo>> GetMemo(int id)
        {
            if (_context.memos == null)
            {
                return NotFound();
            }
            var memo = await _context.memos.FindAsync(id);

            if (memo == null)
            {
                return NotFound();
            }

            return memo;
        }

        // PUT: api/Memo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemo(int id, Memo memo)
        {
            if (id != memo.Id)
            {
                return BadRequest();
            }

            _context.Entry(memo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Memo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Memo>> PostMemo(Memo memo)
        {
            if (_context.memos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.memos'  is null.");
            }
            _context.memos.Add(memo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemo", new { id = memo.Id }, memo);
        }

        // DELETE: api/Memo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemo(int id)
        {
            if (_context.memos == null)
            {
                return NotFound();
            }
            var memo = await _context.memos.FindAsync(id);
            if (memo == null)
            {
                return NotFound();
            }

            _context.memos.Remove(memo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemoExists(int id)
        {
            return (_context.memos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
