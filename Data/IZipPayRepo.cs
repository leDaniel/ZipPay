using System.Collections.Generic;
using ZipPay.Models;

namespace ZipPay.Data
{
    public interface IZipPayRepo
    {
        bool SaveChanges();


        IEnumerable<User> GetUsers();
        User GetUser(int id);
        void CreateUser(User user);


        IEnumerable<Account> GetAccounts();
        Account GetAccount(int id);
        void CreateAccount(Account account);
    }
    
}