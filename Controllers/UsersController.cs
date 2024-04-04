using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using OfferAndFindAPI.Models;

namespace OfferAndFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Offer_And_FindContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(Offer_And_FindContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogCritical("Тестовый критический лог");
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        [HttpGet("GetActiveUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetActiveUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.Where(x => x.IdStatus == 2).ToListAsync();
        }

        [HttpGet("GetDeletedUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetDeletedUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.Where(x => x.IdStatus == 1).ToListAsync();
        }


        [HttpPost("Auth")]
        public async Task<ActionResult<User>> Autorization([FromBody] User userGet)
        {
            User user = await _context.Users.Where(x => x.Login == userGet.Login && x.Password == userGet.Password).FirstOrDefaultAsync();
            if (user == null) return null;
            else return Ok(user);
        }

        [HttpPost("Admin")]
        public async Task<ActionResult<User>> Admin([FromBody] User userGet)
        {
            User user = await _context.Users.Where(x => x.Login == userGet.Login && x.Password == userGet.Password).FirstOrDefaultAsync();
            if (user == null) return NotFound();
            else if (user.IdRole == 2) return NotFound(); 
            else return user;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int? id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Reg")]
        public async Task<ActionResult<User>> PostUser(User userGet)
        {
            User user = await _context.Users.Where(x => x.Login == userGet.Login).FirstOrDefaultAsync();
            if (user != null) return NotFound();
            else
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'Offer_And_FindContext.Users'  is null.");
                }
                _context.Users.Add(userGet);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUser", new { id = userGet.IdUser }, userGet);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int? id)
        {
            return (_context.Users?.Any(e => e.IdUser == id)).GetValueOrDefault();
        }
    }
}
