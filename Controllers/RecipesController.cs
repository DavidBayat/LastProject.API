#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LastProject.API;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using RestSharp;
using System.Text.Json;

namespace LastProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> GetRecepeById (string id)
        {
            var client = new RestClient($"https://www.themealdb.com/api/json/v1/1/lookup.php?i={id}");

            var request = new RestRequest();

            var response = await client.ExecuteAsync(request);

            var mealList = JsonSerializer.Deserialize<ListSearchIdDTO>(response.Content);

            if(string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }
            
            return Ok(mealList);
        }

        // , string searchByIngredient2, string searchByIngredient3

        [HttpGet("ingredients")]
        public async Task<ActionResult> GetRecipeByIngredient (string searchByIngredient, string searchByIngredient2, string searchByIngredient3) //use param later
        {
            var client = new RestClient("https://www.themealdb.com/api/json/v1/1/search.php?s=");

            var request = new RestRequest();

            var response = await client.ExecuteAsync(request);
            
            var mealList = JsonSerializer.Deserialize<RecipesResponse>(response.Content);
            
            // fix it with always full list in case invalid string 
            // if(string.IsNullOrEmpty(searchByIngredient) && string.IsNullOrEmpty(searchByIngredient2) && string.IsNullOrEmpty(searchByIngredient3) || string.IsNullOrEmpty(searchByIngredient))
               
            if(string.IsNullOrEmpty(searchByIngredient))
            {
                return Ok(mealList);
            }

            var result = mealList.ListOfRecipes;
                
            var finalDishIng1 = from r in result
                        where r.Ingredients.Contains(searchByIngredient)
                        select new SearchResultDTO {
                            idMeal = r.idMeal,
                            strMeal = r.strMeal,
                            strMealThumb = r.strMealThumb
                        };

            var finalDishIng2 = from r in result
                        where r.Ingredients.Contains(searchByIngredient2)
                        select new SearchResultDTO {
                            idMeal = r.idMeal,
                            strMeal = r.strMeal,
                            strMealThumb = r.strMealThumb
                        };

            var finalDishIng3 = (from r in result
                        where r.Ingredients.Contains(searchByIngredient3)
                        select new SearchResultDTO {
                            idMeal = r.idMeal,
                            strMeal = r.strMeal,
                            strMealThumb = r.strMealThumb
                        }).ToHashSet();

            List<SearchResultDTO> Joining(IEnumerable<SearchResultDTO> param1, IEnumerable<SearchResultDTO> param2)
            {
                return (from f in param1
                    join g in param2 on f.strMeal equals g.strMeal
                    select new SearchResultDTO
                    {
                        idMeal = f.idMeal,
                        strMeal = f.strMeal,
                        strMealThumb = f.strMealThumb 
                    }).ToList();
            }

            var twoIngredients = Joining(finalDishIng1, finalDishIng2);

            var threeIngredients = Joining(twoIngredients, finalDishIng3);

            if(threeIngredients.Count != 0)
            {
                return Ok(new ListSearchDTO {
                    ListOfRecipes = threeIngredients
                });
            }
            else if (twoIngredients.Count != 0)
            {
                return Ok(new ListSearchDTO {
                    ListOfRecipes = twoIngredients
                });
            }
            else
            {
                return Ok(new ListSearchDTO {
                    ListOfRecipes = finalDishIng1.ToList()
                });
            }
        }
    }

}
            // var makeItWork = (from f in finalDishIng1
            //                 join g in finalDishIng2 on f.strMeal equals g.strMeal
            //                 select new SearchResultDTO
            //                 {
            //                     idMeal = f.idMeal,
            //                     strMeal = f.strMeal,
            //                     strMealThumb = f.strMealThumb 
            //                 }).ToList();