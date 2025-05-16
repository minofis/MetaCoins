using Amazon.S3;
using Amazon.S3.Model;
using MetaCoins.Core.Interfaces.Services;
using MetaCoins.DAL.Helpers;
using Microsoft.Extensions.Options;

namespace MetaCoins.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly HttpClient _httpClient;
        private readonly IOptions<S3Settings> _s3Settings;
        public ImageService(HttpClient httpClient, IOptions<S3Settings> s3Settings, IAmazonS3 s3Client)
        {
            _httpClient = httpClient;
            _s3Settings = s3Settings;
            _s3Client = s3Client;
        }

        public async Task<string> GenerateAndUploadImageAsync()
        {
            var imageUrl = await GenerateImage();

            var imageBytes = await GetImageBytesAsync(imageUrl);

            var key = Guid.NewGuid();

            var fileName = $"coin-{key}";

            var s3Url = await UploadToAmazonS3Async(imageBytes, fileName);

            return s3Url;
        }

        public async Task<string> GenerateImage()
        {
            var response = await _httpClient.GetAsync("https://picsum.photos/800/800");

            var imageUrl = response.RequestMessage.RequestUri.ToString() ?? string.Empty;

            return imageUrl;
        }

        public async Task<byte[]> GetImageBytesAsync(string imageUrl)
        {
            var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);

            return imageBytes;
        }

        public async Task<string> UploadToAmazonS3Async(byte[] imageData, string fileName)
        {
            using var stream = new MemoryStream(imageData);

            var putRequest = new PutObjectRequest
            {
                BucketName = _s3Settings.Value.BucketName,
                Key = $"coin-images/{fileName}",
                InputStream = stream,
                ContentType = "image/jpeg"
            };

            await _s3Client.PutObjectAsync(putRequest);

            return fileName;
        }

        public string GetPreSignedUrl(string fileName)
        {
            var getRequest = new GetPreSignedUrlRequest
            {
                BucketName = _s3Settings.Value.BucketName,
                Key = $"coin-images/{fileName}",
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddMinutes(15)
            };

            string preSignedUrl = _s3Client.GetPreSignedURL(getRequest);

            return preSignedUrl;
        }
    }
}