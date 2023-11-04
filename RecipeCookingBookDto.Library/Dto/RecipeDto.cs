using System.ComponentModel.DataAnnotations;

namespace RecipeCookingBookDto.Library.Dto
{
    public record RecipeDto
    {
        public int Id { get; init; }

        [Required] public string Name { get; set; } = string.Empty;

        public int PreparationTimeInMinutes { get; set; }

        public int Portions { get; set; }

        [Required] public string Category { get; set; } = string.Empty;

        [Required] public string Instructions { get; set; } = string.Empty;

        public IList<IngredientDto>? Ingredients { get; set; } = new List<IngredientDto>();
    }
}