namespace Afi.CustomerPortal.Entities.Dto
{
    public class CustomerRegistration
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? EmailAddress { get; set; }
        public required string PolicyNumber { get; set; }
    }
}