using System.Net.Http;
using System.Net.Http.Json;
using PhotoManager.Data.Models;

namespace PhotoManager.Wpf.Services
{
    public class PhotoService
    {
        private readonly HttpClient _http;

        public PhotoService()
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri("https://localhost:7104");
        }

        public async Task<List<Photo>> GetPhotosAsync()
        {
            return await _http.GetFromJsonAsync<List<Photo>>("api/photo") ?? new List<Photo>();
        }
    }
}