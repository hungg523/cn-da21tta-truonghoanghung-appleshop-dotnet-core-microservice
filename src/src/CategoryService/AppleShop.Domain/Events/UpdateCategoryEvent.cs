namespace AppleShop.Domain.Events
{
    public class UpdateCategoryEvent
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? IsActive { get; set; }
    }
}