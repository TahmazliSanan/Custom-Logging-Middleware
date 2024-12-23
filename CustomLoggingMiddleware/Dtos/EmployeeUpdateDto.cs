namespace CustomLoggingMiddleware.Dtos
{
    public class EmployeeUpdateDto
    {
        public string Position { get; set; } = string.Empty;
        public double? Salary { get; set; }
        public bool IsManager { get; set; }
    }
}
