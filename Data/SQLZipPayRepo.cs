using System;
using System.Collections.Generic;
using System.Linq;
using ZipPay.Models;

namespace ZipPay.Data
{
    public class SQLZipPayRepo : IZipPayRepo
    {
        private readonly ZipPayContext _context;

        public SQLZipPayRepo(ZipPayContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            if (user == null) 
            {
                throw new ArgumentNullException(nameof(user));
            }
            //check if another user with same email exists
            if ( _context.Users.Any(u => u.Email == user.Email))
            {            
                throw new ArgumentException("Email already exists: " + user.Email);            
            }
            _context.Users.Add(user);
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public void CreateAccount(Account account)
        {
            if (account == null) 
            {
                throw new ArgumentNullException(nameof(account));
            }
            //check that user exists
            if (! _context.Users.Any(u => u.Id == account.UserId))
            {            
                throw new ArgumentException("User does not exist");            
            }
            //check if user already has an account
            if ( _context.Accounts.Any(a => a.UserId == account.UserId))
            {            
                throw new ArgumentException("User already has an account");            
            }

            //check if user's salary-expenses<1000
            User user = _context.Users.FirstOrDefault(u => u.Id == account.UserId);            
            if (user.Salary - user.Expenses < 1000)
            {
                throw new ArgumentException("User's Saving has to be > $1000");
            }

            _context.Accounts.Add(account);
        }

        public Account GetAccount(int id)
        {
            return _context.Accounts.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()) >= 0;
        }
    }

}