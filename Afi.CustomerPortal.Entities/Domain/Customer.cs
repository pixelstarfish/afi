using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Afi.CustomerPortal.Entities.Domain
{
    [Index(nameof(EmailAddress), IsUnique = true)]
    public class Customer
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string FirstName { get; set; }
        [MaxLength(50)]
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [MaxLength(254)]
        public string? EmailAddress { get; set; }
        public List<CustomerPolicy> Policies { get; set; } = new List<CustomerPolicy>();
    }
}