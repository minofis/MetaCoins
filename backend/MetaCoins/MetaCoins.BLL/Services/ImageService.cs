using MetaCoins.Core.Interfaces.Services;

namespace MetaCoins.BLL.Services
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;
        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GenerateImage()
        {
            var response = await _httpClient.GetAsync("https://picsum.photos/800/800");

            var imageUrl = response.RequestMessage?.RequestUri?.ToString() ?? string.Empty;

            var image = await _httpClient.GetAsync(imageUrl);

            var imageBytes = await image.Content.ReadAsByteArrayAsync();

            var saveDirectory = "wwwroot/images";
            
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }

            var random = new Random();
            
            var coinNumber = random.Next();

            var fileName = $"Coin{coinNumber}.jpg";

            var filePath = Path.Combine(saveDirectory, fileName);

            await File.WriteAllBytesAsync(filePath, imageBytes);

            return $"images/{fileName}";
        }
    }
}