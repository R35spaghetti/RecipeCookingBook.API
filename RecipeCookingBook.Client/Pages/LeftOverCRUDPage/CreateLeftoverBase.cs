using Microsoft.AspNetCore.Components;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.Client.Pages.LeftOverCRUDPage;

public class CreateLeftoverBase : ComponentBase
{
    [Inject] public ILeftOverService LeftoverService { get; set; }

    protected List<LeftOverToCreateDto> CreateLeftover { get; set; } = new();
}