using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecipeCookingBook.API.Extensions.DtoConversions;
using RecipeCookingBook.API.Models;
using RecipeCookingBook.API.Repositories.Contracts;
using RecipeCookingBookDto.Library.Dto;
using RecipeCookingBookPaging.Library.Paging;

namespace RecipeCookingBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<RecipeDto>> GetAllRecipesAsync([FromQuery] RecipeParameters recipeParameters)
        {
            try
            {
                var recipes = await _recipeRepository.GetAllRecipesAsync(recipeParameters);

                {
                    var recipesDto = recipes.ConvertAllRecipesDto();


                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(recipes.MetaData));
                    return Ok(recipesDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<RecipeDto>> GetOneRecipeAsync(int id)
        {
            try
            {
                var recipe = await _recipeRepository.GetRecipeAsync(id);

                {
                    var recipeDto = recipe.ConvertToOneRecipeDto();
                    return Ok(recipeDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error 500 recipe not found");
            }
        }


        [HttpPost("CreateRecipe")]
        public async Task<ActionResult<RecipeToCreateDto>> CreateRecipe([FromBody] Recipe createRecipe)
        {
            var newRecipe = await _recipeRepository.CreateRecipe(createRecipe);

            var newRecipeDto = newRecipe.ConvertToRecipeCreateDto();

            return CreatedAtAction(nameof(CreateRecipe),
                new
                {
                    newRecipeDto.Name,
                    newRecipeDto.PreparationTimeInMinutes,
                    newRecipeDto.Portions,
                    newRecipeDto.Category,
                    newRecipeDto.Instructions,
                    newRecipeDto.Ingredients,
                },
                newRecipeDto);
        }

        [HttpGet]
        [Route(nameof(GetRandomRecipeAsync))]
        public async Task<ActionResult<RecipeDto>> GetRandomRecipeAsync()
        {
            try
            {
                var recipe = await _recipeRepository.GetRandomRecipeAsync();

                {
                    var recipeDto = recipe.ConvertToOneRecipeDto();
                    return Ok(recipeDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteRecipeAsync(int id)
        {
            try
            {
                await _recipeRepository.DeleteRecipeAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<RecipeDto>> UpdateRecipeAsync([FromBody] Recipe recipeToUpdate)
        {
            try
            {
                var recipe = await _recipeRepository.UpdateRecipe(recipeToUpdate);

                var recipeDto = recipe.ConvertToOneRecipeDto();

                return Ok(recipeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("FindCompatibleRecipesAsync")]
        public async Task<ActionResult<IList<RecipeDto>>> FindCompatibleRecipesAsync(
            [FromQuery] RecipeParameters recipeParameters)
        {
            try
            {
                var recipesUserCanComplete = await _recipeRepository.FindCompatibleRecipesAsync(recipeParameters);

                {
                    var recipesDto = recipesUserCanComplete.ConvertAllRecipesDto();
                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(recipesUserCanComplete.MetaData));
                    return Ok(recipesDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}