namespace PhotoManager.Common.DTOs
{
    public class UploadResponse
    {
        public UploadStatus Status { get; set; }
        public int? SavedCount { get; set; }
    }
}