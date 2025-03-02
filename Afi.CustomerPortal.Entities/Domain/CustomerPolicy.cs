using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Afi.CustomerPortal.Entities.Domain
{
    [Index(nameof(PolicyNumber), IsUnique = true)]
    public class CustomerPolicy
    {
        public int Id { get; set; }
        [MaxLength(9)]
        public required string PolicyNumber { get; set; }
    }
}