using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordMem.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using WordMem.DataAccess.Abstract;

namespace WordMem.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WordsController : ControllerBase
    {
        private IUnitOfWork uow;
        private UserManager<ApplicationUser> userManager;

        public WordsController(IUnitOfWork _uow, UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
            uow = _uow;
        }

        // GET: api/Words
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Word>>> GetWords()
        {
            return await uow.Words.GetAll().ToListAsync();
        }

        //// GET: api/Words/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Word>> GetWord(int id)
        //{
        //    var word = await _context.Words.FindAsync(id);

        //    if (word == null)
        //    {
        //        return NotFound();
        //    }

        //    return word;
        //}

        //// PUT: api/Words/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWord(int id, Word word)
        //{
        //    if (id != word.WordId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(word).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WordExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Words
        //[HttpPost]
        //public async Task<ActionResult<Word>> PostWord(Word word)
        //{
        //    _context.Words.Add(word);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetWord", new { id = word.WordId }, word);
        //}

        //// DELETE: api/Words/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Word>> DeleteWord(int id)
        //{
        //    var word = await _context.Words.FindAsync(id);
        //    if (word == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Words.Remove(word);
        //    await _context.SaveChangesAsync();

        //    return word;
        //}

        //private bool WordExists(int id)
        //{
        //    return _context.Words.Any(e => e.WordId == id);
        //}
    }
}
