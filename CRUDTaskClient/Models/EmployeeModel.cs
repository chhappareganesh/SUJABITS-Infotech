using System.Text.Json.Serialization;

namespace CRUDTaskClient.Models
{
    public class EmployeeModel
    {
        [JsonPropertyName("employeeId")]
        public int EmployeeId { get; set; }

        [JsonPropertyName("employeeName")]
        public string EmployeeName { get; set; }

        [JsonPropertyName("designation")]
        public string Designation { get; set; }

        [JsonPropertyName("salary")]
        public int Salary { get; set; }
    }
}
