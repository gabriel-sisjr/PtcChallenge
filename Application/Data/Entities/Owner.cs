using Domain.AuxModels;
using Domain.Enums;

namespace Data.Entities
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Document { get; set; } = "";
        public string Email { get; set; } = "";
        public Address Address { get; set; } = null!;
        public StatusOwner Status { get; set; }
    }
}
