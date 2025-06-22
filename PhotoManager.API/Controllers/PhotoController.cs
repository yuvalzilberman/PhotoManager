using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoManager.Data;

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
    }
}
