﻿using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using PhotoManager.Data.Models;
using PhotoManager.Common.DTOs;
using PhotoManager.Common;
using PhotoManager.Wpf.Resources;
using System.Windows.Navigation;


namespace PhotoManager.Wpf.Services
{
    public class PhotoService
    {
        private readonly HttpClient _http;

        public PhotoService()
        {
            _http = new HttpClient();
            _http.BaseAddress = new Uri("https://localhost:7104");
            
            // Add timeout and additional headers for debugging
            _http.Timeout = TimeSpan.FromSeconds(30);
            _http.DefaultRequestHeaders.Add("User-Agent", "PhotoManager-WPF-Client");
        }

        internal async Task<List<Photo>> GetPhotosAsync()
        {
            return await _http.GetFromJsonAsync<List<Photo>>("api/photo") ?? new List<Photo>();
        }

        internal async Task<bool> TestConnectionAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/photo");
                System.Diagnostics.Debug.WriteLine($"Test connection response: {response.StatusCode}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Test connection failed: {ex.Message}");
                return false;
            }
        }

        private async Task<T?> PostAsync<T>(string endpoint, object requestData)
        {
            try
            {
                var json = JsonSerializer.Serialize(requestData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _http.PostAsync(endpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();
                
                return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PostAsync failed for {endpoint}: {ex.Message}");
                return default(T);
            }
        }        

        internal async Task<(bool,string)> UploadPhotoAsync(string[] filePaths)
        {
            if (filePaths == null || filePaths.Length == 0)
                return (false, "No files hae been upload");

            try
            {
                var result = await PostAsync<UploadResponse>("api/photo/upload", filePaths);
                return result == null ? (false, "Unknown Error") : (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        internal async Task<(bool, string)> AddUserAsync(AppUser addUser)
        {
            if (addUser == null)
                return (false, "User cannot be null");

            var user = new AddUser
            {
                UserName = addUser.UserName,
                Email = addUser.Email,
                Password = addUser.Password, //Utils.Encrypt(addUser.Password),
                CreatedAt = addUser.CreatedAt
            };

            try
            {
                var result = await PostAsync<AddUserResponse>("api/Photo/AddUser", user);
                
                return result == null || result.Status == Common.Status.Failed ?
                    (false, StringResourceManager.Registration_AddUserFailure) :
                    (true, StringResourceManager.Registration_AddUser);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in AddUserAsync: {ex}");
                return (false, $"Exception occurred: {ex.Message}");
            }
        }

        internal async Task<(bool, string, AppUser?)> GetUserAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return (false, "Username and password cannot be empty", null);

            var user = new GetUser
            {
                UserName = userName,
                Password = password
            };

            try
            {
                var result = await PostAsync<GetUserResponse>("api/photo/GetUser", user);

                try
                {
                   return result == null || result.Status == Common.Status.Failed ?
                        (true, "", null) :
                        (true, StringResourceManager.Registration_AddUser, result.User);
                }
                catch
                {
                    return (true, "", null);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception in GetUserAsync: {ex}");
                return (false, $"Exception occurred: {ex.Message}", null);
            }
        }
        }
}