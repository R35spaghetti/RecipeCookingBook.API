using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeCookingBookDto.Library.Dto
{
    public record IngredientDto
    {
        public int Id { get; init; }

        [ForeignKey("In Recipe")] public int RecipeId { get; set; }

        [Required] public string Name { get; set; } = string.Empty;

        public int Amount { get; set; }
    }
}