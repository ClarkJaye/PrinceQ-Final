using System.ComponentModel.DataAnnotations;

namespace PrinceQ.Models.Entities
{
    public class Access
    {
        [Key]
        public int AccessId { get; set; }
        public string? AccessName { get; set; }

        public DateTime? Created_At { get; set; }
    }
}
