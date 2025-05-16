namespace MetaCoins.DAL.Helpers
{
    public class S3Settings
    {
        public string Region { get; init; } = string.Empty;
        public string BucketName { get; init; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
    }
}