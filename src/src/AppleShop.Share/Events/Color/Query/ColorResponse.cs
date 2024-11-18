namespace AppleShop.Share.Events.Color.Query
{
    public class ColorResponse
    {
        public ICollection<ColorInfo>? Colors { get; set; } = new List<ColorInfo>();
    }
}