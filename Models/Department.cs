using System.ComponentModel.DataAnnotations;

namespace API_Test.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        [Required (ErrorMessage ="Please fill department's name area.")]
        public string DepartmentName { get; set; }
    }
}
