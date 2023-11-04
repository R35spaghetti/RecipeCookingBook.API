using Microsoft.AspNetCore.Components;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.Client.Components.RecipeTable;

public partial class RecipeTable
{
    [Parameter]
    public List<RecipeDto> Recipes { get; set; }
}