using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeCookingBook.API.Models;

public class Ingredient
{
    public int Id { get; set; }

    [ForeignKey("In Recipe")] public int RecipeId { get; set; }

    [Required] public string Name { get; set; } = string.Empty;

    [Required] public int Amount { get; set; }
}