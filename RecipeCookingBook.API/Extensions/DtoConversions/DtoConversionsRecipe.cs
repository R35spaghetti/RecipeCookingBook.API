using RecipeCookingBook.API.Models;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.API.Extensions.DtoConversions
{
    public static class DtoConversionsRecipe
    {
        public static RecipeToCreateDto ConvertToRecipeCreateDto(this Recipe recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe is null");
            }

            return new RecipeToCreateDto
            {
                Name = recipe.Name,
                PreparationTimeInMinutes = recipe.PreparationTimeInMinutes,
                Portions = recipe.Portions,
                Category = recipe.Category,
                Instructions = recipe.Instructions,
                Ingredients = GetCreatedIngredientsConverted(recipe.Ingredients),
            };
        }

        public static RecipeDto ConvertToOneRecipeDto(this Recipe? recipe)
        {
            if (recipe == null)
            {
                throw new ArgumentNullException(nameof(recipe), "Recipe is null");
            }

            return new RecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                PreparationTimeInMinutes = recipe.PreparationTimeInMinutes,
                Portions = recipe.Portions,
                Category = recipe.Category,
                Instructions = recipe.Instructions,
                Ingredients = GetIngredientsConverted(recipe.Ingredients),
            };
        }

        public static IList<RecipeDto> ConvertAllRecipesDto(this IList<Recipe> recipes)
        {
            if (recipes == null)
            {
                throw new ArgumentNullException(nameof(recipes), "Recipes are null");
            }

            return (from recipe in recipes
                select new RecipeDto
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    PreparationTimeInMinutes = recipe.PreparationTimeInMinutes,
                    Portions = recipe.Portions,
                    Category = recipe.Category,
                    Instructions = recipe.Instructions,
                    Ingredients = GetIngredientsConverted(recipe.Ingredients),
                }).ToList();
        }

        private static IList<IngredientDto> GetIngredientsConverted(IList<Ingredient>? ingredients)
        {
            if (ingredients == null)
            {
                throw new ArgumentNullException(nameof(ingredients), "Ingredients are null");
            }

            var ingredientDTOs = ingredients.ConvertAllIngredientsDto().ToList();

            return ingredientDTOs;
        }

        private static IList<IngredientToCreateDto> GetCreatedIngredientsConverted(IList<Ingredient>? ingredients)
        {
            if (ingredients == null)
            {
                throw new ArgumentNullException(nameof(ingredients), "Ingredients are null");
            }

            var ingredientDTOs = ingredients.ConvertAllIngredientsToCreateDTOs().ToList();

            return ingredientDTOs;
        }
    }
}