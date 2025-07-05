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
                FileName = fileInfo.Name,
                StorageUrl = imagePath,
                OriginalDateTaken = fileInfo.CreationTimeUtc,
                SizeInBytes = (int)fileInfo.Length,
                Width = width,
                Height = height
            };
        }
    }
}