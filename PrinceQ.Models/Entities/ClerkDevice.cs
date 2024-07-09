using System.ComponentModel.DataAnnotations;

namespace PrinceQ.Models.Entities
{
    public class ClerkDevice
    {
        [Key]
        public string? DeviceId { get; set; }

        public string? ClerkNumber { get; set; }

    }
}
