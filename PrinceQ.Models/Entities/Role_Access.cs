using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrinceQ.Models.Entities
{
    public class Role_Access
    {
        [ForeignKey("Roles")]
        public string? RoleId { get; set; }
        [ValidateNever]
        public Roles? Roles { get; set; }

        public int AccessId { get; set; }
        [ForeignKey("AccessId")]
        [ValidateNever]
        public Access? Access { get; set; }
    }
}
