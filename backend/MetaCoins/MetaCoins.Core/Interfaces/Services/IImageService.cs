namespace MetaCoins.Core.Interfaces.Services
{
    public interface IImageService
    {
        Task<string> GenerateAndUploadImageAsync();
        Task<string> GenerateImage();
        Task<byte[]> GetImageBytesAsync(string imageUrl);
        Task<string> UploadToAmazonS3Async(byte[] imageData, string fileName);
        string GetPreSignedUrl(string fileName);
    }
}