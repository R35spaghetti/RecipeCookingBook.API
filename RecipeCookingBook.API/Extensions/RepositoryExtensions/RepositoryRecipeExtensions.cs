using RecipeCookingBook.API.Models;

namespace RecipeCookingBook.API.Extensions.RepositoryExtensions;

public static class RepositoryRecipeExtensions
{
    public static IQueryable<Recipe> Search(this IQueryable<Recipe> recipes, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return recipes;
        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
        return recipes.Where(x => x.Name.ToLower().Contains(lowerCaseSearchTerm));
    }

    public static IQueryable<Leftover> SearchLeftover(this IQueryable<Leftover> leftovers, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return leftovers;
        var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
        return leftovers.Where(x => x.Name.ToLower().Contains(lowerCaseSearchTerm));
    }
}