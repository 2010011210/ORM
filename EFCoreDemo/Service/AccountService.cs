using EFCoreDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Service
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext _context;

        public AccountService(ApplicationContext applicationContext) 
        {
            _context = applicationContext;
        }

        public void Run() 
        {
            var entity = _context.Model.FindEntityType(typeof(Account));
            var tableName = entity.GetTableName();

        }

        public void Add(Account account) 
        {
            var accounts= _context.Accounts;
            _context.Add(account);
            _context.SaveChanges();


        }



    }
}
