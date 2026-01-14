using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Proiect_Cafenea.Models;
namespace Proiect_Cafenea.Services
{
    public class RecomandareService : IRecomandareService
    {
        private readonly HttpClient _httpClient;

        
        public RecomandareService(HttpClient httpClient)
        {
            _httpClient = httpClient; 
        }

        public async Task<float> PredictScoreAsync(RecomandareInput input)
        {
            
            var response = await _httpClient.PostAsJsonAsync("/predict", input);

            
            response.EnsureSuccessStatusCode();

           
            var result = await response.Content.ReadFromJsonAsync<RecomandareApiResponse>();

            
            return result?.Score ?? 0;
        }

       
        private class RecomandareApiResponse
        {
            
            public float Score { get; set; }
        }

    }
}
