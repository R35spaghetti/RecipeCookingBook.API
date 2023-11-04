using Microsoft.AspNetCore.Components;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.Client.Pages.FrontPage
{
    public class IndexBase : ComponentBase
    {
        [Inject] public IRecipeService? RecipeService { get; set; }

        protected RecipeDto? RandomRecipe { get; set; } = new();


        protected async Task<RecipeDto> GetRandom()
        {
            if (RecipeService != null) RandomRecipe = await RecipeService.GetRandomRecipeAsync();
            return RandomRecipe ?? new RecipeDto();
        }
    }
}