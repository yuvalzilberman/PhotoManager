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

                var fileInfo = new FileInfo(path);
                var photo = new Photo
                {
                    Name = fileInfo.Name,
                    Path = path,
                    Date = fileInfo.CreationTimeUtc,
                    Size = (int)fileInfo.Length,
                    Private = false
                };
                photos.Add(photo);
            }

            if (photos.Count == 0)
                return BadRequest(new UploadResponse
                {
                    Status = UploadStatus.FilesNotFound
                });

            _context.Photos.AddRange(photos);
            await _context.SaveChangesAsync();

            return Ok(new UploadResponse
            {
                Status = UploadStatus.Success,
                SavedCount = photos.Count
            });
        }
    }
}