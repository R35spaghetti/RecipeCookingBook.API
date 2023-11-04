using RecipeCookingBook.API.Repositories.Contracts;
using RecipeCookingBook.API.Data;
using RecipeCookingBook.API.Models;


namespace RecipeCookingBook.API.Repositories;

public class IngredientRepository : IIngredientRepository
{
    private readonly RecipeCookBookDbContext _dbContext;

    public IngredientRepository(RecipeCookBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Ingredient> CreateIngredient(Ingredient? ingredientToAdd)
    {
     
            await _dbContext.Ingredients.AddAsync(ingredientToAdd ?? throw new ArgumentNullException(nameof(ingredientToAdd)));
            await _dbContext.SaveChangesAsync();
        
             return ingredientToAdd;
    }

    public async Task DeleteIngredientAsync(int id)
    {
        var ingredient = await _dbContext.Ingredients.FindAsync(id);
        if (ingredient != null)
        {
            _dbContext.Ingredients.Remove(ingredient);
            await _dbContext.SaveChangesAsync();
        }
    }


    public async Task<Ingredient> UpdateIngredientAsync(int id, Ingredient ingredientToUpdate)
    {
        var ingredient = await _dbContext.Ingredients.FindAsync(id);

        if (ingredient != null)
        {
            ingredient.Name = ingredientToUpdate.Name;
            ingredient.RecipeId = ingredientToUpdate.RecipeId;
            ingredient.Amount = ingredientToUpdate.Amount;
            await _dbContext.SaveChangesAsync();
        }

        return ingredientToUpdate;
    }
}