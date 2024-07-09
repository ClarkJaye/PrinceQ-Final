using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinceQ.Models.Entities
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Message { get; set; }

        [Column(TypeName = "datetime2(7)")]
        public DateTime Created_At { get; set; }

    }
}
