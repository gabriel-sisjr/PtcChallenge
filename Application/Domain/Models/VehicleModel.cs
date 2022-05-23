using Domain.Enums;

namespace Domain.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public OwnerModel Owner { get; set; } = null!;
        public string Renavam { get; set; } = "";
        public BrandModel Brand { get; set; } = null!;
        public string Model { get; set; } = "";
        public int YearCreation { get; set; }
        public int YearModel { get; set; }
        public int Quilometers { get; set; }
        public float Value { get; set; }
        public StatusVehicle Status { get; set; }
    }
}
