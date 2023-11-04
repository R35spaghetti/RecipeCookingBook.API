using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Components;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.Client.Pages.AddRecipesPage
{
    public class CreateRecipeWithIngredientsBase : ComponentBase
    {
        [Inject] public IRecipeService RecipeService { get; set; }

        public RecipeToCreateDto CreateRecipe { get; set; } = new();
    }
}