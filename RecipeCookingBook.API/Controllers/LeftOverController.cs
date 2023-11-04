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
    public class LeftOverController : ControllerBase
    {
        private readonly ILeftoverRepository _leftoverRepository;

        public LeftOverController(ILeftoverRepository leftoverRepository)
        {
            _leftoverRepository = leftoverRepository;
        }

        [HttpGet]
        public async Task<ActionResult<LeftoverDto>> GetAllLeftoversAsync([FromQuery] RecipeParameters recipeParameters)
        {
            try
            {
                var leftovers = await _leftoverRepository.GetAllLeftoversAsync(recipeParameters);

                {
                    var leftoverDto = leftovers.ConvertAllLeftoverDto();


                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(leftovers.MetaData));
                    return Ok(leftoverDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpPost]
        public async Task<ActionResult<LeftOverToCreateDto>> CreateLeftOver([FromBody] IList<Leftover> createLeftOver)
        {
            var newLeftOver = await _leftoverRepository.CreateLeftOverAsync(createLeftOver);

            var newLeftOverDto = newLeftOver.ConvertAllLeftoverDto();

            return CreatedAtAction(nameof(CreateLeftOver), newLeftOverDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeftoverDto>> GetOneLeftOverAsync(int id)
        {
            try
            {
                var leftOver = await _leftoverRepository.GetLeftOverAsync(id);

                {
                    var leftOverDto = leftOver.ConvertToOneLeftOverDto();
                    return Ok(leftOverDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error 500 leftover not found");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<LeftoverDto>> UpdateLeftOverAsync([FromBody] Leftover leftoverToUpdate)
        {
            try
            {
                var leftOver = await _leftoverRepository.UpdateLeftOverAsync(leftoverToUpdate.Id, leftoverToUpdate);

                var leftOverDto = leftOver.ConvertToOneLeftOverDto();

                return Ok(leftOverDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteLeftOverAsync(int id)
        {
            try
            {
                await _leftoverRepository.DeleteLeftOverAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}