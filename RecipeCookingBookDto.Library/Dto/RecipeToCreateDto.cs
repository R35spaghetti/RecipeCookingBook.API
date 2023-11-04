using System.ComponentModel.DataAnnotations;

namespace RecipeCookingBookDto.Library.Dto
{
    public record RecipeToCreateDto
    {
        [Required] public string Name { get; set; } = string.Empty;

        public int PreparationTimeInMinutes { get; set; }

        public int Portions { get; set; }

        [Required] public string Category { get; set; } = string.Empty;

        [Required] public string Instructions { get; set; } = string.Empty;

        public virtual IList<IngredientToCreateDto> Ingredients { get; init; } = new List<IngredientToCreateDto>();
    }
}