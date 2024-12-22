namespace CustomLoggingMiddleware.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string Position { get; set; } = string.Empty;
        public double? Salary { get; set; }
        public bool IsManager { get; set; }
    }
}
