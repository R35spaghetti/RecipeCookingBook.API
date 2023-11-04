using Microsoft.AspNetCore.Components;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;
using RecipeCookingBookPaging.Library.Paging;

namespace RecipeCookingBook.Client.Pages.GalleryPage
{
    public partial class GalleryBase : ComponentBase
    {
        public List<RecipeDto>? RecipesList { get; set; } = new List<RecipeDto>();
        public MetaData MetaData { get; set; } = new MetaData();

        public RecipeParameters RecipeParameters { get; set; } = new RecipeParameters();

        [Parameter] public string IngredientName { get; set; } = "";


        [Inject] public IRecipeService RecipeService { get; set; }

        public int FilterValue { get; set; }

        [Parameter] public string CurrentFilter { get; set; }

        protected override async Task OnInitializedAsync()
        {
            FilterValue = Int32.Parse(CurrentFilter);

            switch (FilterValue)
            {
                case 0:
                    await GetRecipes();
                    break;

                case 1:
                    await GetRecipesWithLeftOvers();
                    break;
            }
        }

        public async Task SelectedPage(int page)
        {
            FilterValue = Int32.Parse(CurrentFilter);

            switch (FilterValue)
            {
                case 0:
                    RecipeParameters.PageNumber = page;
                    await GetRecipes();
                    break;

                case 1:
                    RecipeParameters.PageNumber = page;
                    await GetRecipesWithLeftOvers();

                    break;
            }
        }

        private async Task GetRecipesWithLeftOvers()
        {
            var pagingResponse = await RecipeService.FindCompatibleRecipesAsync(RecipeParameters);
            RecipesList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        private async Task GetRecipes()
        {
            var pagingResponse = await RecipeService.GetAllRecipesAsync(RecipeParameters);
            RecipesList = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }

        public async Task SearchChanged(string searchTerm)
        {
            FilterValue = Int32.Parse(CurrentFilter);

            switch (FilterValue)
            {
                case 0:

                    RecipeParameters.PageNumber = 1;
                    RecipeParameters.SearchTerm = searchTerm;
                    await GetRecipes();
                    break;

                case 1:
                    RecipeParameters.PageNumber = 1;
                    RecipeParameters.SearchTerm = searchTerm;
                    await GetRecipesWithLeftOvers();

                    break;
            }
        }
    }
}