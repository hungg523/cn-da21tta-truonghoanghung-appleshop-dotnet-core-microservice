namespace AssetService.DependencyInjection.Extensions
{
    public static class StringExtensions
    {
        public static string FormatMsg(this string msgText, params object[] args) => string.Format(msgText, args);
    }
}