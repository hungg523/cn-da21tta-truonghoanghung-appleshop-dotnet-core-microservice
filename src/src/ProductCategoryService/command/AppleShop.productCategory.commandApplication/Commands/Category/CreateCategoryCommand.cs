using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.productCategory.commandApplication.Commands.Category
{
    public class CreateCategoryCommand : ICommand
    {
        public string? Name { get; set; }
        public string? IconData { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? IsActived { get; set; } = 0;
    }
}