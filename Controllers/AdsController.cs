using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using OfferAndFindAPI.Models;

namespace OfferAndFindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly Offer_And_FindContext _context;

        public AdsController(Offer_And_FindContext context)
        {
            _context = context;
        }

        // GET: api/Ads

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ad>>> AdsGet()
        {
            if (_context.Ads == null)
            {
                return NotFound();
            }
            return await _context.Ads.ToListAsync();
        }


        [HttpPost("GetAds")]
        public async Task<ActionResult<IEnumerable<CollectionDataModel>>> GetAds([FromBody] UserSelectedValue userSV)
        {
            List<Ad> ad = _context.Ads.ToList();
            List<User> user = _context.Users.ToList();
            var ads = (from a in ad
                       join u in user on a.IdUser equals u.IdUser into table1
                       from u in table1.DefaultIfEmpty()
                       select new CollectionDataModel { ads = a, users = u }).ToList();
            if (userSV.type != 0)
            {
                switch (userSV.Value)
                {
                    case "offer": 
                        return ads.Where(x => x.ads.IdType1 == 1 && x.users.IdUser != userSV.idUser && x.ads.IdType == userSV.type).ToList();
                    case "find":
                        return ads.Where(x => x.ads.IdType1 == 2 && x.users.IdUser != userSV.idUser && x.ads.IdType == userSV.type).ToList();
                    case "offerAndFind":
                        return ads.Where(x => x.users.IdUser != userSV.idUser && x.ads.IdType == userSV.type).ToList();
                    case "userAds":
                        return ads.Where(x => x.users.IdUser == userSV.idUser && x.ads.IdType == userSV.type).ToList();
                    default:
                        return NotFound();
                }
            }
            else
            {
                switch (userSV.Value)
                {
                    case "offer":
                        return ads.Where(x => x.ads.IdType1 == 1 && x.users.IdUser != userSV.idUser).ToList();
                    case "find":
                        return ads.Where(x => x.ads.IdType1 == 2 && x.users.IdUser != userSV.idUser).ToList();
                    case "offerAndFind":
                        return ads.Where(x => x.users.IdUser != userSV.idUser).ToList();
                    case "userAds":
                        return ads.Where(x => x.users.IdUser == userSV.idUser).ToList();
                    default:
                        return NotFound();
                }
            }
        }

        [HttpGet("GetAds1")]
        public async Task<ActionResult<IEnumerable<CollectionDataModel>>> GetAds1(UserSelectedValue userSV)
        {
            List<Ad> ad = _context.Ads.ToList();
            List<User> user = _context.Users.ToList();
            var ads = (from a in ad
                       join u in user on a.IdUser equals u.IdUser into table1
                       from u in table1.DefaultIfEmpty()
                       select new CollectionDataModel { ads = a, users = u }).ToList();
            if (userSV.type != 0)
            {
                switch (userSV.Value)
                {
                    case "offer":
                        return ads.Where(x => x.ads.IdType1 == 1 && x.users.IdUser != userSV.idUser && x.ads.IdType == userSV.type).ToList();
                    case "find":
                        return ads.Where(x => x.ads.IdType1 == 2 && x.users.IdUser != userSV.idUser && x.ads.IdType == userSV.type).ToList();
                    case "offerAndFind":
                        return ads.Where(x => x.users.IdUser != userSV.idUser && x.ads.IdType == userSV.type).ToList();
                    case "userAds":
                        return ads.Where(x => x.users.IdUser == userSV.idUser && x.ads.IdType == userSV.type).ToList();
                    default:
                        return NotFound();
                }
            }
            else
            {
                switch (userSV.Value)
                {
                    case "offer":
                        return ads.Where(x => x.ads.IdType1 == 1 && x.users.IdUser != userSV.idUser).ToList();
                    case "find":
                        return ads.Where(x => x.ads.IdType1 == 2 && x.users.IdUser != userSV.idUser).ToList();
                    case "offerAndFind":
                        return ads.Where(x => x.users.IdUser != userSV.idUser).ToList();
                    case "userAds":
                        return ads.Where(x => x.users.IdUser == userSV.idUser).ToList();
                    default:
                        return NotFound();
                }
            }
        }

        [HttpPost("GetAd")]
        public async Task<ActionResult<CollectionDataModel>> GetAd([FromBody] int Id)
        {
            List<Ad> ad = _context.Ads.Where(x => x.IdAd == Id).ToList();
            List<User> user = _context.Users.ToList();
            var ads = (from a in ad
                       join u in user on a.IdUser equals u.IdUser into table1
                       from u in table1.DefaultIfEmpty()
                       select new CollectionDataModel { ads = a, users = u }).ToList();

            return ads.Where(x => x.ads.IdAd == Id).FirstOrDefault();
        }

        // GET: api/Ads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> GetAd(int? id)
        {
          if (_context.Ads == null)
          {
              return NotFound();
          }
            var ad = await _context.Ads.FindAsync(id);

            if (ad == null)
            {
                return NotFound();
            }

            return ad;
        }

        // PUT: api/Ads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAd(int? id, Ad ad)
        {
            if (id != ad.IdAd)
            {
                return BadRequest();
            }

            _context.Entry(ad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdExists(id))
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

        // POST: api/Ads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ad>> PostAd(Ad ad)
        {
          if (_context.Ads == null)
          {
              return Problem("Entity set 'Offer_And_FindContext.Ads'  is null.");
          }
            _context.Ads.Add(ad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAd", new { id = ad.IdAd }, ad);
        }

        // DELETE: api/Ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAd(int? id)
        {
            if (_context.Ads == null)
            {
                return NotFound();
            }
            var ad = await _context.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }

            _context.Ads.Remove(ad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdExists(int? id)
        {
            return (_context.Ads?.Any(e => e.IdAd == id)).GetValueOrDefault();
        }
    }
}
