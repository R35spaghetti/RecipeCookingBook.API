using Microsoft.AspNetCore.Components;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.Client.Pages.GalleryPage
{
    public class RecipeDetailsBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] public IRecipeService? RecipeService { get; set; }

        [Inject] public IIngredientService? IngredientService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }

        public RecipeDto? Recipe { get; set; } = new();


        public string? ErrorMessage { get; set; }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                Recipe = await RecipeService.GetRecipeAsync(Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected async Task UpdateRecipeOnValidSubmit()
        {
            await RecipeService.UpdateRecipe(Recipe);

            if (Recipe != null)
            {
                NavigationManager.NavigateTo("/galleryChoice");
            }
        }
    }
}