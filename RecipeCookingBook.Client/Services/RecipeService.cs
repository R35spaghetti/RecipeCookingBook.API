using System.Net.Http.Json;
using System.Text;
using RecipeCookingBook.Client.Features;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using Newtonsoft.Json;
using RecipeCookingBookPaging.Library.Paging;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace RecipeCookingBook.Client.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;


        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }


        public async Task<RecipeToCreateDto> CreateRecipe(RecipeToCreateDto recipeToAdd)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Recipe/CreateRecipe", recipeToAdd);
            var feedback = await response.Content.ReadFromJsonAsync<RecipeToCreateDto>();

            return feedback;
        }

        public async Task DeleteRecipeAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Recipe/{id}");
        }

        public async Task<PagingResponse<RecipeDto>> FindCompatibleRecipesAsync(RecipeParameters recipeParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = recipeParameters.PageNumber.ToString(),
                ["searchTerm"] = recipeParameters.SearchTerm == null ? "" : recipeParameters.SearchTerm
            };

            var response =
                await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/Recipe/FindCompatibleRecipesAsync",
                    queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<RecipeDto>
            {
                Items = JsonSerializer.Deserialize<List<RecipeDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            return pagingResponse;
        }


        public async Task<PagingResponse<RecipeDto>> GetAllRecipesAsync(RecipeParameters recipeParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = recipeParameters.PageNumber.ToString(),
                ["searchTerm"] = recipeParameters.SearchTerm == null ? "" : recipeParameters.SearchTerm
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/Recipe", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<RecipeDto>
            {
                Items = JsonSerializer.Deserialize<List<RecipeDto>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options)
            };
            return pagingResponse;
        }

        public async Task<RecipeDto> GetRandomRecipeAsync()
        {
            var randomRecipe = await _httpClient.GetAsync("api/Recipe/GetRandomRecipeAsync");

            if (randomRecipe.IsSuccessStatusCode)
            {
                if (randomRecipe.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return (RecipeDto)Enumerable.Empty<RecipeDto>();
                }

                return await randomRecipe.Content.ReadFromJsonAsync<RecipeDto>();
            }
            else
            {
                var message = await randomRecipe.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<RecipeDto> GetRecipeAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Recipe/{id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(RecipeDto);
                }

                return await response.Content.ReadFromJsonAsync<RecipeDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<RecipeDto> UpdateRecipe(RecipeDto recipeToUpdate)
        {
            var jsonRecipeRequests = JsonConvert.SerializeObject(recipeToUpdate);
            var content = new StringContent(jsonRecipeRequests, Encoding.UTF8, "application/json-put+json");

            var response = await _httpClient.PutAsync($"api/Recipe/", content);

            return await response.Content.ReadFromJsonAsync<RecipeDto>();
        }
    }
}