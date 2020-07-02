using System.ComponentModel.DataAnnotations;

namespace ZipPay.Dtos
{
    public class AccountCreateDto 
    {        
        [Required]
        public int UserId { get; set; }        
    }
}