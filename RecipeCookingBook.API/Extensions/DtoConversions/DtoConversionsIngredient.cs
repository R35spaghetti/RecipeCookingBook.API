using RecipeCookingBook.API.Models;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.API.Extensions.DtoConversions
{
    public static class DtoConversionsIngredient
    {
        public static IngredientDto ConvertToOneIngredientDto(this Ingredient? ingredient)
        {
            if (ingredient == null)
            {
                throw new ArgumentNullException(nameof(ingredient), "Ingredient is null");
            }

            return new IngredientDto
            {
                Id = ingredient.Id,
                RecipeId = ingredient.RecipeId,
                Name = ingredient.Name,
                Amount = ingredient.Amount,
            };
        }

        public static IngredientToCreateDto ConvertToIngredientCreateDto(this Ingredient? ingredient)
        {
            if (ingredient == null)
            {
                throw new ArgumentNullException(nameof(ingredient), "Ingredient is null");
            }

            return new IngredientToCreateDto
            {
                RecipeId = ingredient.RecipeId,
                Name = ingredient.Name,
                Amount = ingredient.Amount,
            };
        }

        public static IList<IngredientDto> ConvertAllIngredientsDto(this IList<Ingredient> ingredients)
        {
            if (ingredients == null)
            {
                throw new ArgumentNullException(nameof(ingredients), "Ingredients are null");
            }

            return (from ingredient in ingredients
                select new IngredientDto
                {
                    Id = ingredient.Id,
                    RecipeId = ingredient.RecipeId,
                    Name = ingredient.Name,
                    Amount = ingredient.Amount,
                }).ToList();
        }

        public static IList<IngredientToCreateDto> ConvertAllIngredientsToCreateDTOs(this IList<Ingredient> ingredients)
        {
            return (from ingredient in ingredients
                select new IngredientToCreateDto
                {
                    RecipeId = ingredient.RecipeId,
                    Name = ingredient.Name,
                    Amount = ingredient.Amount,
                }).ToList();
        }
    }
}