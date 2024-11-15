namespace AppleShop.Domain.Events
{
    public class CreateCategoryEvent
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? IsActive { get; set; }
    }
}