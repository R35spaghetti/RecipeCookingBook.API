using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeCookingBookDto.Library.Dto
{
    public record IngredientToCreateDto
    {
        [ForeignKey("In Recipe")] public int RecipeId { get; init; }

        [Required] public string Name { get; set; } = string.Empty;

        public int Amount { get; set; }
    }
}