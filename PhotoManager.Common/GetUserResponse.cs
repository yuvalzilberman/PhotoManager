using PhotoManager.Data.Models;

namespace PhotoManager.Common
{
    public class GetUserResponse
    {
        public Status Status { get; set; }
        public AppUser User { get; set; } = new AppUser();
    }
}