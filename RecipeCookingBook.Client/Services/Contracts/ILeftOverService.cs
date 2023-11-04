using RecipeCookingBook.Client.Features;
using RecipeCookingBookDto.Library.Dto;
using RecipeCookingBookPaging.Library.Paging;

namespace RecipeCookingBook.Client.Services.Contracts
{
    public interface ILeftOverService
    {
        Task<PagingResponse<LeftoverDto>> GetAllLeftoversAsync(RecipeParameters recipeParameters);
        Task<LeftOverToCreateDto> CreateLeftOverAsync(IList<LeftOverToCreateDto> leftOverToAdd);
        Task<LeftoverDto> GetLeftOverAsync(int ID);
        Task<LeftoverDto> UpdateLeftOverAsync(LeftoverDto leftoverToUpdate);
        Task DeleteLeftOverAsync(int ID);
    }
}