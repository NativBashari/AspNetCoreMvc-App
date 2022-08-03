using System.ComponentModel.DataAnnotations;

namespace API_Test.Models
{
    public class Salary
    {
        public int SalaryId { get; set; }
        [Required(ErrorMessage = "Please fill amount area.")]
        public int Amount { get; set; }
    }
}
