using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PhotoManager.Common;
using PhotoManager.Common.DTOs;
using PhotoManager.Data;
using PhotoManager.Data.Models;


namespace PhotoManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly PhotoDbContext _context;

        public PhotoController(PhotoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhotos()
        {
            var photos = await _context.Photos.ToListAsync();
            return Ok(photos);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromBody] List<string> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest(new UploadResponse
                {
                    Status = UploadStatus.NoFilesProvided
                });
            }

            var photos = new List<Photo>();
            foreach (var path in files)
            {
                if (!System.IO.File.Exists(path))
                    continue;

                var photo = Utils.Utils.GetImageInfo(path);
                if (photo != null)
                    photos.Add(photo);
            }

            if (photos.Count == 0)
            {
                return BadRequest(new UploadResponse
                {
                    Status = UploadStatus.FilesNotFound
                });
            }

            _context.Photos.AddRange(photos);
            await _context.SaveChangesAsync();

            return Ok(new UploadResponse
            {
                Status = UploadStatus.Success,
                SavedCount = photos.Count
            });
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUser user)
        {
            if (user == null)
                return BadRequest(new AddUserResponse
                {
                    Status = AddUserStatus.Failed
                });

            try
            {
                var appUser = new AppUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    CreatedAt = DateTime.UtcNow
                };

                _context.AppUsers.Add(appUser);
                await _context.SaveChangesAsync();

                return Ok(new AddUserResponse
                {
                    Status = AddUserStatus.Success
                });
            }
            catch
            {
                return BadRequest(new AddUserResponse
                {
                    Status = AddUserStatus.Failed
                });
            }
        }
    }
}