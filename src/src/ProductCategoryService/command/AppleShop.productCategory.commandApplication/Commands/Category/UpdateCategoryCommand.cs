using AppleShop.Share.Abstractions;
using System.Text.Json.Serialization;

namespace AppleShop.productCategory.commandApplication.Commands.Category
{
    public class UpdateCategoryCommand : ICommand
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? IconData { get; set; }
        public string? Description { get; set; }
        public int? IsActived { get; set; }
    }
}