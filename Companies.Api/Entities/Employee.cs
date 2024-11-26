namespace Companies.Api.Entities
{
    public class Employee
    {
        public int Id { get; set; }
           public string? Name { get; set; }

        public int Age { get; set; }
        public string? Position { get; set; }

        //Foreign key
        public int CompanyId { get; set; }
        //Navigation property
        public Company? Company { get; set; }

        }
}