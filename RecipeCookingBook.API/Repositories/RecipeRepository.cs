using Microsoft.EntityFrameworkCore;
using RecipeCookingBook.API.Repositories.Contracts;
using RecipeCookingBook.API.Data;
using RecipeCookingBook.API.Extensions.RepositoryExtensions;
using RecipeCookingBook.API.Models;
using RecipeCookingBookPaging.Library.Paging;


namespace RecipeCookingBook.API.Repositories;

public class RecipeRepository : IRecipeRepository
{
    private readonly RecipeCookBookDbContext _dbContext;

    public RecipeRepository(RecipeCookBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Recipe> CreateRecipe(Recipe recipeToAdd)
    {
        var result = await _dbContext.Recipes.AddAsync(recipeToAdd);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task DeleteRecipeAsync(int id)
    {
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe != null)
        {
            _dbContext.Recipes.Remove(recipe);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<PagedList<Recipe>> GetAllRecipesAsync(RecipeParameters recipeParameters)
    {
        var recipes = await _dbContext.Recipes.Include(x => x.Ingredients)
            .Search(recipeParameters.SearchTerm ?? string.Empty)
            .ToListAsync();
        return PagedList<Recipe>
            .ToPagedList(recipes, recipeParameters.PageNumber, recipeParameters.PageSize);
    }


    public async Task<Recipe> GetRandomRecipeAsync()
    {
        List<Recipe> allRecipeIDs = new List<Recipe>();


        allRecipeIDs = await AddIDsToRecipeListAsync(allRecipeIDs);

        var getRandomRecipe = await GetOneRandomRecipe(allRecipeIDs);

        return getRandomRecipe;
    }

    private async Task<Recipe> GetOneRandomRecipe(List<Recipe> allRecipeIDs)
    {
        var random = new Random();

        var allRecipes = (random.Next(allRecipeIDs.Count));

        var getRandomRecipe = allRecipeIDs[allRecipes];

        return await Task.FromResult(getRandomRecipe);
    }

    private async Task<List<Recipe>> AddIDsToRecipeListAsync(List<Recipe> allRecipeIDs)
    {
        var highestNumber = _dbContext.Recipes.Count();

        for (int i = 0; i <= highestNumber; i++)
        {
            var recipe = await _dbContext.Recipes.Include(z => z.Ingredients).Skip(i).FirstOrDefaultAsync();

            if (recipe is null)
            {
            }

            else
            {
                allRecipeIDs.Add(recipe);
            }
        }

        return allRecipeIDs;
    }

    public async Task<Recipe?> GetRecipeAsync(int id)
    {
        var recipe = await _dbContext.Recipes.Include(x => x.Ingredients).Where(z => z.Id == id).FirstOrDefaultAsync();

        return recipe;
    }

    public async Task<Recipe?> UpdateRecipe(Recipe recipeToUpdate)
    {
        var recipe = await _dbContext.Recipes.FindAsync(recipeToUpdate.Id);

        if (recipe != null)
        {
            recipe.Name = recipeToUpdate.Name;
            recipe.PreparationTimeInMinutes = recipeToUpdate.PreparationTimeInMinutes;
            recipe.Portions = recipeToUpdate.Portions;
            recipe.Instructions = recipeToUpdate.Instructions;
            recipe.Ingredients = recipeToUpdate.Ingredients;
            await _dbContext.SaveChangesAsync();
            return recipe;
        }

        return null;
    }

    public async Task<PagedList<Recipe>> FindCompatibleRecipesAsync(RecipeParameters recipeParameters)
    {
        int amountOfRecipes = await GetAmountOfRecipes();
        var recipesThatUserCanDo = await CompareLeftOversWithRecipe(amountOfRecipes);
        return PagedList<Recipe>.ToPagedList(recipesThatUserCanDo, recipeParameters.PageNumber,
            recipeParameters.PageSize);
    }


    private async Task<int> GetAmountOfRecipes()
    {
        int amount = await _dbContext.Recipes.CountAsync();
        return amount;
    }

    private async Task<IList<Recipe>> CompareLeftOversWithRecipe(int amountOfRecipesToCheck)
    {
        IList<Recipe> recipesThatUserCanDo = new List<Recipe>();


        for (int i = 0; i < amountOfRecipesToCheck; i++)
        {
            var checkThisRecipe = await _dbContext.Recipes.Include(x => x.Ingredients).Skip(i).FirstOrDefaultAsync();
            var recipeIsDoable = checkThisRecipe != null && await IsRecipeDoable(checkThisRecipe);

            if (recipeIsDoable)
            {
                if (checkThisRecipe != null) recipesThatUserCanDo.Add(checkThisRecipe);
            }
        }

        return recipesThatUserCanDo;
    }


    private async Task<bool> IsRecipeDoable(Recipe checkThisRecipe)
    {
        IList<Leftover> findIngredientInLeftOverList = new List<Leftover>();
        if (checkThisRecipe.Ingredients != null)
        {
            int amountOfIngredients = checkThisRecipe.Ingredients.Count;


            if (checkThisRecipe.Ingredients != null)
                foreach (var currentIngredient in checkThisRecipe.Ingredients)
                {
                    var leftoverIngredientExist = _dbContext.LeftOvers.FirstOrDefault(x =>
                        x.Name == currentIngredient.Name && x.Amount >= currentIngredient.Amount);


                    if (leftoverIngredientExist != null)
                    {
                        findIngredientInLeftOverList.Add(leftoverIngredientExist);
                    }
                }

            var enoughIngredients = await CountLeftOvers(findIngredientInLeftOverList);


            if (enoughIngredients == amountOfIngredients)
            {
                return true;
            }
        }


        return false;
    }

    private Task<int> CountLeftOvers(IList<Leftover> currentList)
    {
        int amount = currentList.Count();

        return Task.FromResult(amount);
    }
}