using System.ComponentModel.DataAnnotations;

namespace PhotoManager.Data.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }// Foreign key
        public AppUser User { get; set; } = new AppUser();
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
} 