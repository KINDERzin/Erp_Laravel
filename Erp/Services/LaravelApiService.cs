using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using NetworkExtension;

namespace Erp.Services;

public class LaravelApiService
{
   private readonly HttpClient _httpClient;
   private readonly JsonSerializerOptions _jsonOptions;

   public LaravelApiService(HttpClient httpClient)
   {
      _httpClient = httpClient;

      // Configura os headers padrão
      _httpClient.DefaultRequestHeaders.Accept.Clear();
      _httpClient.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json")
      );

      _jsonOptions = new JsonSerializerOptions
      {
         PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
         WriteIndented = true
      };
   }

   // Get
   public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
   {
      try
      {
         var response = await _httpClient.GetAsync(endpoint);
         var jsonResponse = await response.Content.ReadAsStringAsync();

         if (response.IsSuccessStatusCode)
         {
            var data = JsonSerializer.Deserialize<T>(jsonResponse, _jsonOptions);

            return new ApiResponse<T>
            {
               Success = true,
               Data = data
            };
         }
         else
            return new ApiResponse<T>
            {
               Success = false,
               Message = $"Erro {response.StatusCode}: {jsonResponse}"
            };
      }
      catch (Exception ex)
      {
         return new ApiResponse<T>
         {
            Success = false,
            Message = ex.Message
         };
      }
   }

   //Post
   public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object data)
   {
      try
      {
         var json = JsonSerializer.Serialize(data, _jsonOptions);
         var content = new StringContent(json, Encoding.UTF8, "application/json");

         var response = await _httpClient.PostAsync(endpoint, content);
         var jsonResponse = await response.Content.ReadAsStringAsync();

         if (response.IsSuccessStatusCode)
         {
            var responseData = JsonSerializer.Deserialize<T>(jsonResponse, _jsonOptions);

            return new ApiResponse<T>
            {
               Success = true,
               Data = responseData
            };
         }
         else
            return new ApiResponse<T>
            {
               Success = false,
               Message = $"Erro {response.StatusCode}: {jsonResponse}"
            };
      }
      catch (Exception ex)
      {
         return new ApiResponse<T>
         {
            Success = false,
            Message = ex.Message
         };
      }
   }

   //Put
   public async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object data)
   {
      try
      {
         var json = JsonSerializer.Serialize(data, _jsonOptions);
         var content = new StringContent(json, Encoding.UTF8, "application/json");

         var response = await _httpClient.PutAsync(endpoint, content);
         var jsonResponse = await response.Content.ReadAsStringAsync();

         if (response.IsSuccessStatusCode)
         {
            var responseData = JsonSerializer.Deserialize<T>(jsonResponse, _jsonOptions);

            return new ApiResponse<T>
            {
               Success = true,
               Data = responseData
            };
         }
         else
            return new ApiResponse<T>
            {
               Success = false,
               Message = $"Erro {response.StatusCode}: {jsonResponse}"
            };
      }
      catch (Exception ex)
      {
         return new ApiResponse<T>
         {
            Success = false,
            Message = ex.Message
         };
      }
   }

   //Delete
   public async Task<ApiResponse<bool>> DeleteAsync<T>(string endpoint)
   {
      try
      {
         var response = await _httpClient.DeleteAsync(endpoint);
         return new ApiResponse<bool>
         {
            Success = response.IsSuccessStatusCode,
            Data = response.IsSuccessStatusCode,
            Message = response.IsSuccessStatusCode ? "Deletado com sucesso!" : "Erro ao deletar"
         };
      }
      catch (Exception ex)
      {
         return new ApiResponse<bool>
         {
            Success = false,
            Message = ex.Message
         };
      }
   }

   //Adiciona o token de verificação se necessário
   public void SetAuthToken(string token)
   {
      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
   }
}