using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfferAndFindAPI.Models;

namespace OfferAndFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly Offer_And_FindContext _context;

        public ChatsController(Offer_And_FindContext context)
        {
            _context = context;
        }

        // GET: api/Chats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
        {
          if (_context.Chats == null)
          {
              return NotFound();
          }
            return await _context.Chats.ToListAsync();
        }

        // GET: api/Chats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat(int? id)
        {
          if (_context.Chats == null)
          {
              return NotFound();
          }
            var chat = await _context.Chats.FindAsync(id);

            if (chat == null)
            {
                return NotFound();
            }

            return chat;
        }

        // PUT: api/Chats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChat(int? id, Chat chat)
        {
            if (id != chat.IdChat)
            {
                return BadRequest();
            }

            _context.Entry(chat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatExists(id))
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

        // POST: api/Chats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Chat>> PostChat(Chat chat)
        {

            chat.IdChat = null;

          if (_context.Chats == null)
          {
              return Problem("Entity set 'Offer_And_FindContext.Chats'  is null.");
          }
            List<Chat> chats = _context.Chats.ToList();
            foreach (Chat a in chats)
            {
                if (a.IdUser == chat.IdUser && a.User2 == chat.User2 && a.IdAd == chat.IdAd)
                {
                    return NotFound();
                }
            }
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChat", new { id = chat.IdChat }, chat);
        }

        [HttpPost("GetUsersReact")]
        public async Task<ActionResult<IEnumerable<CollectionDataModel2>>> GetUsersReact([FromBody] int id)
        {
            List<Chat> chat = _context.Chats.ToList();
            List<User> user = _context.Users.ToList();
            var users = (from a in chat
                         where a.IdAd == id
                       join u in user on a.User2 equals u.IdUser into table1
                       from u in table1.DefaultIfEmpty()
                       select new CollectionDataModel2 { user = u }).ToList();
            return users;
        }



        // DELETE: api/Chats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(int? id)
        {
            if (_context.Chats == null)
            {
                return NotFound();
            }
            var chat = await _context.Chats.FindAsync(id);
            if (chat == null)
            {
                return NotFound();
            }

            _context.Chats.Remove(chat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatExists(int? id)
        {
            return (_context.Chats?.Any(e => e.IdChat == id)).GetValueOrDefault();
        }
    }
}
