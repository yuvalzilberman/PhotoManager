using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using PhotoManager.Data.Models;
using PhotoManager.Common.DTOs;
using PhotoManager.Common;


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

        internal async Task<List<Photo>> GetPhotosAsync()
        {
            return await _http.GetFromJsonAsync<List<Photo>>("api/photo") ?? new List<Photo>();
        }        

        internal async Task<(bool,string)> UploadPhotoAsync(string[] filePaths)
        {
            if (filePaths == null || filePaths.Length == 0)
                return (false, "No files hae been upload");

            try
            {
                var json = JsonSerializer.Serialize(filePaths);                
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _http.PostAsync("api/photo/upload", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<UploadResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return result == null ? (false, "Unknown Error") :(true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}