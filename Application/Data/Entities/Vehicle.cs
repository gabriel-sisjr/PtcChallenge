using Domain.Enums;

namespace Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public virtual Owner Owner { get; set; } = null!;
        public int OwnerId { get; set; }
        public string Renavam { get; set; } = "";
        public virtual Brand Brand { get; set; } = null!;
        public int BrandId { get; set; }
        public string Model { get; set; } = "";
        public int YearCreation { get; set; }
        public int YearModel { get; set; }
        public int Quilometers { get; set; }
        public float Value { get; set; }
        public StatusVehicle Status { get; set; }
    }
}
