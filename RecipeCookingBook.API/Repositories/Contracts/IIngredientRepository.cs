using RecipeCookingBook.API.Models;

namespace RecipeCookingBook.API.Repositories.Contracts;

public interface IIngredientRepository
{
    Task<Ingredient> CreateIngredient(Ingredient ingredientToAdd);
    Task<Ingredient> UpdateIngredientAsync(int id, Ingredient ingredientToAdd);
    Task DeleteIngredientAsync(int id);
}