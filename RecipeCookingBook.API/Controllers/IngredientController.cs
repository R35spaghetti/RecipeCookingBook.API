using Microsoft.AspNetCore.Mvc;
using RecipeCookingBook.API.Extensions.DtoConversions;
using RecipeCookingBook.API.Models;
using RecipeCookingBook.API.Repositories.Contracts;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        [HttpPost]
        public async Task<ActionResult<IngredientToCreateDto>> CreateIngredient([FromBody] Ingredient createIngredient)
        {
            var newIngredient = await _ingredientRepository.CreateIngredient(createIngredient);

            var newIngredientDto = newIngredient.ConvertToIngredientCreateDto();

            return CreatedAtAction(nameof(CreateIngredient),
                new
                {
                    RecipeID = newIngredientDto.RecipeId,
                    newIngredientDto.Name,
                    newIngredientDto.Amount,
                },
                createIngredient);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<IngredientDto>> UpdateIngredientAsync([FromBody] Ingredient ingredientToUpdate)
        {
            try
            {
                var ingredient =
                    await _ingredientRepository.UpdateIngredientAsync(ingredientToUpdate.Id, ingredientToUpdate);

                var ingredientDto = ingredient.ConvertToOneIngredientDto();

                return Ok(ingredientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteIngredientAsync(int id)
        {
            try
            {
                await _ingredientRepository.DeleteIngredientAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}