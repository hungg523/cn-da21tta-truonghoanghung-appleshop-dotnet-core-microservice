namespace AssetService.Errors
{
    public class Error
    {
        public List<string>? Details { get; }
        public Error(string stackTrace, params string[]? details)
        {
            Details = new List<string>();
            if (details is not null) Details.AddRange(details);
        }
    }
}