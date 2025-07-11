using System.ComponentModel.DataAnnotations;

namespace PhotoManager.Data.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public ICollection<Album> Albums { get; set; } = new List<Album>();
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();

        public AppUser()
        {
        }

        public AppUser(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
            CreatedAt = DateTime.Now.AddMilliseconds(-DateTime.Now.Millisecond);
        }
    }
} 