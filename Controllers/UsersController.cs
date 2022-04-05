#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LastProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{googleId}")]
        public  ActionResult<User> GetUser(string googleId)
        {
            //var us = await _context.User.FindAsync(id);
            // var user = _context.User.Where(user => user.GoogleId == googleId).Include(user => user.Recipes).First();
            //var user = await _context.User.Where(u => u.GoogleId == googleId).Include(user => user.Recipes).FirstOrDefaultAsync();
            
            var user =  (_context.User
                        .Where(u => u.GoogleId == googleId)
                        .Select(u => new User
                        {
                            Name = u.Name,
                            Email = u.Email,
                            GoogleId = u.GoogleId,

                            // add 2nd table
                            Recipes = u.Recipes
                                .Select(r => new Recipe
                                {
                                    idMeal = r.idMeal,
                                    strMeal = r.strMeal,
                                    strMealThumb = r.strMealThumb,
                                    GoogleId = r.GoogleId,
                                }).ToList()
                        })).FirstOrDefault();

            
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{googleId}")]
        public async Task<IActionResult> PutUser(string googleId, User user)
        {
            if (googleId != user.GoogleId)
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
                if (!UserExists(googleId))
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
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(CreateUserDTO user)
        {
            var newUser = new User {
                GoogleId = user.GoogleId,
                Name = user.Name,
                Email = user.Email
            };


            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.GoogleId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.GoogleId == id);
        }
    }
}
