namespace AssetService.Request
{
    public class UploadFileRequest
    {
        public string Content { get; set; }
        public string FileName { get; set; }
        public int AssetType { get; set; }
    }
}