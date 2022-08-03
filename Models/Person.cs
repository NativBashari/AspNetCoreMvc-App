using System.ComponentModel.DataAnnotations;

namespace API_Test.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SureName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Adress { get; set; }
       
        public int PositionId { get; set; }
      
        public int SalaryId { get; set; }
    }
}
