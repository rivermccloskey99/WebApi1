using System.ComponentModel.DataAnnotations;

namespace WebApi1.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public string? SortCode { get; set; }
        public string? HouseNumber { get; set; }
        public string? StreetName { get; set; }
        public string? TownCity { get; set; }
        public string? County { get; set; }
        [Required]
        public string? PostCode { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}