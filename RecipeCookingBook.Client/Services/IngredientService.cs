using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
using RecipeCookingBook.Client.Services.Contracts;
using RecipeCookingBookDto.Library.Dto;

namespace RecipeCookingBook.Client.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly HttpClient _httpClient;

        public IngredientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<IngredientToCreateDto> CreateIngredient(IngredientToCreateDto ingredientToAdd)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Ingredient/", ingredientToAdd);


            return await response.Content.ReadFromJsonAsync<IngredientToCreateDto>();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Ingredient/{id}");
        }

        public async Task<IngredientDto> GetIngredientAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Ingredient/{id}");
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(IngredientDto);
                }

                return await response.Content.ReadFromJsonAsync<IngredientDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }
        }

        public async Task<IngredientDto> UpdateIngredientAsync(int id, IngredientDto ingredientToUpdate)
        {
            var jsonIngredientRequests = JsonConvert.SerializeObject(ingredientToUpdate);
            var content = new StringContent(jsonIngredientRequests, Encoding.UTF8, "application/json-put+json");

            var response = await _httpClient.PutAsync($"api/Ingredient/{ingredientToUpdate.Id}", content);

            return await response.Content.ReadFromJsonAsync<IngredientDto>();
        }
    }
}