using System.ComponentModel.DataAnnotations;

namespace WebApi1.Dtos
{
    public class CustomerCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public string SortCode { get; set; }
        [Required]
        public string HouseNumber { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string TownCity { get; set; }
        [Required]
        public string County { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}