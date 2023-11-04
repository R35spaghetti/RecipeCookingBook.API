using RecipeCookingBook.API.Models;
using RecipeCookingBookPaging.Library.Paging;

namespace RecipeCookingBook.API.Repositories.Contracts;

public interface ILeftoverRepository
{
    Task<PagedList<Leftover>> GetAllLeftoversAsync(RecipeParameters recipeParameters);
    Task<IList<Leftover>> CreateLeftOverAsync(IList<Leftover> leftOver);
    Task<Leftover> GetLeftOverAsync(int id);
    Task<Leftover> UpdateLeftOverAsync(int id, Leftover leftover);
    Task DeleteLeftOverAsync(int id);
}