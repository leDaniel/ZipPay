using System.ComponentModel.DataAnnotations;

namespace ZipPay.Dtos
{
    public class UserCreateDto 
    {        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Salary field must be greater than 0")]
        public int Salary { get; set; }        
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Expenses field must be greater than 0")]
        public int Expenses { get; set; }
    }
}