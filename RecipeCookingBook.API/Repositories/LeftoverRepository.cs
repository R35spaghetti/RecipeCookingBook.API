using RecipeCookingBook.API.Repositories.Contracts;
using RecipeCookingBook.API.Data;
using RecipeCookingBook.API.Models;
using Microsoft.EntityFrameworkCore;
using RecipeCookingBook.API.Extensions.RepositoryExtensions;
using RecipeCookingBookPaging.Library.Paging;


namespace RecipeCookingBook.API.Repositories;

public class LeftoverRepository : ILeftoverRepository
{
    private readonly RecipeCookBookDbContext _dbContext;

    public LeftoverRepository(RecipeCookBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Leftover>> CreateLeftOverAsync(IList<Leftover> leftOverToAdd)
    {
        await _dbContext.LeftOvers.AddRangeAsync(leftOverToAdd);
        await _dbContext.SaveChangesAsync();

        return leftOverToAdd;
    }

    public async Task DeleteLeftOverAsync(int id)
    {
        var leftOver = await _dbContext.LeftOvers.FindAsync(id);
        if (leftOver != null)
        {
            _dbContext.LeftOvers.Remove(leftOver);
            await _dbContext.SaveChangesAsync();
        }
    }


    public async Task<Leftover> GetLeftOverAsync(int id)
    {
        var leftover = await _dbContext.LeftOvers.FindAsync(id);
        return leftover ?? throw new InvalidOperationException();
    }

    public async Task<Leftover> UpdateLeftOverAsync(int id, Leftover leftoverToUpdate)
    {
        var leftover = await _dbContext.LeftOvers.FindAsync(id);
        
        leftover.Name = leftoverToUpdate.Name;
        leftover.Amount = leftoverToUpdate.Amount;
        await _dbContext.SaveChangesAsync();

        return leftover;
    }

    public async Task<PagedList<Leftover>> GetAllLeftoversAsync(RecipeParameters recipeParameters)
    {
        var leftOvers = await _dbContext.LeftOvers
            .SearchLeftover(recipeParameters.SearchTerm ?? string.Empty)
            .ToListAsync();
        return PagedList<Leftover>
            .ToPagedList(leftOvers, recipeParameters.PageNumber, recipeParameters.PageSize);
    }
}