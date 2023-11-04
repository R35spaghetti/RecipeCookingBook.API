using Microsoft.AspNetCore.Components;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;
using RecipeCookingBookPaging.Library.Paging;

namespace RecipeCookingBook.Client.Pages.LeftOverCRUDPage;

public class CurrentLeftoversBase : ComponentBase
{
    [Inject] public IRecipeService RecipeService { get; set; }

    [Inject] public ILeftOverService LeftOverService { get; set; }
    [Inject] public NavigationManager NavManager { get; set; }

    public RecipeParameters RecipeParameters { get; set; } = new RecipeParameters();

    public List<LeftoverDto>? LeftoversList { get; set; } = new List<LeftoverDto>();

    public MetaData MetaData { get; set; } = new MetaData();


    protected override async Task OnInitializedAsync()
    {
        await GetLeftovers();
    }

    private async Task GetLeftovers()
    {
        var pagingResponse = await LeftOverService.GetAllLeftoversAsync(RecipeParameters);
        LeftoversList = pagingResponse.Items;
        MetaData = pagingResponse.MetaData;
    }

    public async Task SearchChanged(string searchTerm)
    {
        RecipeParameters.PageNumber = 1;
        RecipeParameters.SearchTerm = searchTerm;
        await GetLeftovers();
    }

    public async Task SelectedPage(int page)
    {
        RecipeParameters.PageNumber = page;
        await GetLeftovers();
    }
}