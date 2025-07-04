using System.ComponentModel.DataAnnotations;

namespace PhotoManager.Data.Models
{
    public class Photo
    {
        [Key] // Primary key
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}