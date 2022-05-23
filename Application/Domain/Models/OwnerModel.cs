using Domain.Enums;

namespace Domain.Models
{
    public class OwnerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Document { get; set; } = "";
        public string Email { get; set; } = "";
        public string Cep { get; set; } = "";
        public StatusOwner Status { get; set; }
    }
}
