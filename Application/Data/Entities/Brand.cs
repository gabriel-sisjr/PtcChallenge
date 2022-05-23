using Domain.Enums;

namespace Data.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public StatusBrand Status { get; set; }
    }
}
