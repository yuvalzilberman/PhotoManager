using System.IO;
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

        public async Task<bool> UploadPhotoAsync(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            using var content = new MultipartFormDataContent();
            using var stream = File.OpenRead(filePath);

            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");

            content.Add(fileContent, "file", Path.GetFileName(filePath));

            var response = await _http.PostAsync("https://localhost:5001/api/photo/upload", content);

            return response.IsSuccessStatusCode;
        }
    }
}