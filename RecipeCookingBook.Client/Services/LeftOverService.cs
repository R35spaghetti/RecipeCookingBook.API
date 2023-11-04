using System.Net.Http.Json;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RecipeCookingBook.Client.Features;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Text.Json;
using RecipeCookingBookPaging.Library.Paging;


namespace RecipeCookingBook.Client.Services
{
    public class LeftOverService : ILeftOverService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;


        public LeftOverService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }


        public async Task<PagingResponse<LeftoverDto>> GetAllLeftoversAsync(RecipeParameters recipeParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = recipeParameters.PageNumber.ToString(),
                ["searchTerm"] = recipeParameters.SearchTerm == null ? "" : recipeParameters.SearchTerm
            };
            var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString("api/Leftover", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<LeftoverDto>
            {
                Items = JsonSerializer.Deserialize<List<LeftoverDto>>(content, _options) ??
                        throw new InvalidOperationException(),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(),
                    _options) ?? throw new InvalidOperationException()
            };
            return pagingResponse;
        }

        public async Task<LeftOverToCreateDto> CreateLeftOverAsync(IList<LeftOverToCreateDto> leftOverToAdd)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/LeftOver/", leftOverToAdd);


            return (await response.Content.ReadFromJsonAsync<List<LeftOverToCreateDto>>()).FirstOrDefault();
        }

        public async Task DeleteLeftOverAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/LeftOver/{id}");
        }

        public async Task<LeftoverDto> GetLeftOverAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/LeftOver/{id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(LeftoverDto);
                }

                return await response.Content.ReadFromJsonAsync<LeftoverDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<LeftoverDto> UpdateLeftOverAsync(LeftoverDto leftoverToUpdate)
        {
            var jsonLeftOverRequests = JsonConvert.SerializeObject(leftoverToUpdate);
            var content = new StringContent(jsonLeftOverRequests, Encoding.UTF8, "application/json-put+json");

            var response = await _httpClient.PutAsync($"api/LeftOver/{leftoverToUpdate.Id}", content);

            return await response.Content.ReadFromJsonAsync<LeftoverDto>();
        }
    }
}