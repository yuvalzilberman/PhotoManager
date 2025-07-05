using System.ComponentModel.DataAnnotations;

namespace PhotoManager.Data.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public long SizeInBytes { get; set; }
        public string MimeType { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
        public DateTime? OriginalDateTaken { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string StorageUrl { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public int UserId { get; set; }// Foreign keys
        public int AlbumId { get; set; }
        public AppUser User { get; set; } = new AppUser();
        public Album Album { get; set; } = new Album();
    }
}