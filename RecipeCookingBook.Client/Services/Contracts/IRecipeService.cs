using RecipeCookingBook.Client.Features;
using RecipeCookingBookDto.Library.Dto;
using RecipeCookingBookPaging.Library.Paging;


namespace RecipeCookingBook.Client.Services.Contracts
{
    public interface IRecipeService
    {
        Task<PagingResponse<RecipeDto>> GetAllRecipesAsync(RecipeParameters recipeParameters);
        Task<RecipeDto> GetRecipeAsync(int id);
        Task<RecipeToCreateDto> CreateRecipe(RecipeToCreateDto recipeToAdd);
        Task<RecipeDto> UpdateRecipe(RecipeDto recipeToUpdate);
        Task DeleteRecipeAsync(int id);

        Task<RecipeDto> GetRandomRecipeAsync();

        Task<PagingResponse<RecipeDto>> FindCompatibleRecipesAsync(RecipeParameters recipeParameters);
    }
}