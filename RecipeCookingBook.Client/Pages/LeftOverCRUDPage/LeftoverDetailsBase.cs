using Microsoft.AspNetCore.Components;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.Client.Pages.LeftOverCRUDPage;

public class LeftoverDetailsBase : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] public ILeftOverService? LeftOverService { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    public LeftoverDto? LeftOver { get; set; } = new();


    public string? ErrorMessage { get; set; }


    protected override async Task OnInitializedAsync()
    {
        try
        {
            LeftOver = await LeftOverService.GetLeftOverAsync(Id);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    protected async Task UpdateLeftoverOnValidSubmit()
    {
        await LeftOverService.UpdateLeftOverAsync(LeftOver);

        if (LeftOver != null)
        {
            NavigationManager.NavigateTo("/galleryChoice");
        }
    }
}