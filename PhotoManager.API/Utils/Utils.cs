using PhotoManager.Data.Models;

namespace PhotoManager.API.Utils
{
    internal class Utils
    {
        internal static Photo? GetImageInfo(string imagePath)
        {
            if (!File.Exists(imagePath))
                return null;

            using Image image = Image.FromFile(imagePath);
            var width = image.Width;
            var height = image.Height;

            var fileInfo = new FileInfo(imagePath);
            return new Photo
            {
                Path = imagePath,
                Name = fileInfo.Name,
                Date = fileInfo.CreationTimeUtc,
                Size = (int)fileInfo.Length,
                Width = width,
                Height = height
            };
        }
    }
}