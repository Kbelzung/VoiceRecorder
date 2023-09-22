using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
            if (_context.Memos == null)
            {
                return NotFound();
            }
            return await _context.Memos.ToListAsync();
        }

        // GET: api/Memo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Memo>> GetMemo(int id)
        {
            if (_context.Memos == null)
            {
                return NotFound();
            }
            var memo = await _context.Memos.FindAsync(id);

            if (memo == null)
            {
                return NotFound();
            }

            return memo;
        }

        // PUT: api/Memo/5
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

        // PATCH: api/Memo/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMemo(int id, [FromBody] JsonPatchDocument<Memo> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var memoToUpdate = await _context.Memos.FindAsync(id);

            if (memoToUpdate == null)
            {
                return NotFound();
            }

            try
            {
                patchDocument.ApplyTo(memoToUpdate);
             }
            catch (Exception ex)
            {
                ModelState.AddModelError("PatchError", ex.Message);
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(memoToUpdate))
            {
                return BadRequest(ModelState); // Retourne les erreurs de validation
            }

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
        [HttpPost]
        public async Task<ActionResult<Memo>> PostMemo(Memo memo)
        {
            if (_context.Memos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.memos'  is null.");
            }
            _context.Memos.Add(memo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMemo", new { id = memo.Id }, memo);
        }

        // DELETE: api/Memo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemo(int id)
        {
            if (_context.Memos == null)
            {
                return NotFound();
            }
            var memo = await _context.Memos.FindAsync(id);
            if (memo == null)
            {
                return NotFound();
            }

            _context.Memos.Remove(memo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemoExists(int id)
        {
            return (_context.Memos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
