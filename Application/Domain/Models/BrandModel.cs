using Domain.Enums;

namespace Domain.Models
{
    public class BrandModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public StatusBrand Status { get; set; }
    }
}
