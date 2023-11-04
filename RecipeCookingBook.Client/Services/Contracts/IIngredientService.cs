using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.Client.Services.Contracts
{
    public interface IIngredientService
    {
        Task<IngredientDto> GetIngredientAsync(int id);

        Task<IngredientToCreateDto> CreateIngredient(IngredientToCreateDto ingredientToAdd);
        Task<IngredientDto> UpdateIngredientAsync(int id, IngredientDto ingredientToUpdate);
        Task DeleteIngredientAsync(int id);
    }
}