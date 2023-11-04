using RecipeCookingBook.API.Models;
using RecipeCookingBookPaging.Library.Paging;

namespace RecipeCookingBook.API.Repositories.Contracts;

public interface IRecipeRepository
{
    Task<PagedList<Recipe>> GetAllRecipesAsync(RecipeParameters recipeParameters);
    Task<Recipe?> GetRecipeAsync(int id);
    Task<Recipe> CreateRecipe(Recipe recipeToAdd);
    Task<Recipe?> UpdateRecipe(Recipe recipeToUpdate);
    Task DeleteRecipeAsync(int id);
    Task<Recipe> GetRandomRecipeAsync();
    Task<PagedList<Recipe>> FindCompatibleRecipesAsync(RecipeParameters recipeParameters);
}