using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZipPay.Models
{
    public class Account 
    {
        [Key]
        public int Id { get; set; }
        
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}