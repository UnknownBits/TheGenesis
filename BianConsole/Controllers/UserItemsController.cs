using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BianConsole.Data;
using BianConsole.Models;

namespace BianConsole
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserItemsController : ControllerBase
    {
        private readonly UserContext _context;

        public UserItemsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/UserItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItem>>> GetUserItem()
        {
          if (_context.UserItem == null)
          {
              return NotFound();
          }
            return await _context.UserItem.ToListAsync();
        }

        // GET: api/UserItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserItem>> GetUserItem(int id)
        {
          if (_context.UserItem == null)
          {
              return NotFound();
          }
            var userItem = await _context.UserItem.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return userItem;
        }

        // PUT: api/UserItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserItem(int id, UserItem userItem)
        {
            if (id != userItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(userItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserItemExists(id))
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

        // POST: api/UserItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserItem>> PostUserItem(UserItem userItem)
        {
          if (_context.UserItem == null)
          {
              return Problem("Entity set 'BianConsoleContext.UserItem'  is null.");
          }
            _context.UserItem.Add(userItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserItem", new { id = userItem.Id }, userItem);
        }

        // DELETE: api/UserItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserItem(int id)
        {
            if (_context.UserItem == null)
            {
                return NotFound();
            }
            var userItem = await _context.UserItem.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }

            _context.UserItem.Remove(userItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserItemExists(int id)
        {
            return (_context.UserItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
