using Microsoft.EntityFrameworkCore;
using ZipPay.Models;

namespace ZipPay.Data
{
    public class ZipPayContext : DbContext
    {
        public ZipPayContext(DbContextOptions<ZipPayContext> opt) : base(opt)
        {            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

    }
}