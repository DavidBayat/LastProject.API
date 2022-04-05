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
    public class FavoriteRecipesController : ControllerBase
    {
        private readonly UserContext _context;

        public FavoriteRecipesController(UserContext context)
        {
            _context = context;
        }

        // GET: api/FavoriteRecipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipe()
        {
            return await _context.Recipe.ToListAsync();
        }

        // GET: api/FavoriteRecipes/5
        [HttpGet("{idMeal}")]
        public async Task<ActionResult<Recipe>> GetRecipe(string idMeal)
        {
            var recipe = await _context.Recipe.FindAsync(idMeal);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/FavoriteRecipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idMeal}")]
        public async Task<IActionResult> PutRecipe(string idMeal, Recipe recipe)
        {
            if (idMeal != recipe.idMeal)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(idMeal))
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

        // POST: api/FavoriteRecipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(CreatedRecipeDTO recipe)
        {
            var newRecipe = new Recipe {
                idMeal = recipe.idMeal,
                strMeal = recipe.strMeal,
                strMealThumb = recipe.strMealThumb,
                GoogleId = recipe.GoogleId
            };

            _context.Recipe.Add(newRecipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.idMeal }, recipe);
        }

        // DELETE: api/FavoriteRecipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(string idMeal)
        {
            var recipe = await _context.Recipe.FindAsync(idMeal);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(string id)
        {
            return _context.Recipe.Any(e => e.idMeal == id);
        }
    }
}
