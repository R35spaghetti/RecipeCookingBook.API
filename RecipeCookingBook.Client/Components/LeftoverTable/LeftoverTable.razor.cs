using Microsoft.AspNetCore.Components;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.Client.Components.LeftoverTable;

public partial class LeftoverTable
{
    [Parameter]
    public List<LeftoverDto> Leftover { get; set; }
}