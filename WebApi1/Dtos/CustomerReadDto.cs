namespace WebApi1.Dtos
{
    public class CustomerReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AccountNumber { get; set; }
        public string SortCode { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string TownCity { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public decimal Balance { get; set; }
    }
}