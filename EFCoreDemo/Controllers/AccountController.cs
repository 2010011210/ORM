using EFCoreDemo.Entities;
using EFCoreDemo.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService) 
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("GetAccount")]
        public string Get(int id)
        {
            using (var context = new ApplicationContext())
            {
                //var account = context.Accounts.Single(x => x.Id == id);  // id没有会报错
                var account = context.Accounts.FirstOrDefault(x => x.Id == id);  // id没有会报错
                return Newtonsoft.Json.JsonConvert.SerializeObject(account);
            }
        }

        [HttpPost]
        public string Add(string name, string phone) 
        {
            var model = new Account() { Name = name, Phone = phone};
            var model2 = new Account() { Name = name + "1", Phone = phone + "1"};
            using (var context = new ApplicationContext()) 
            {
                context.Add(model);
                context.Add(model2);  // 一次批量添加2个
                var count = context.SaveChanges();
            }
            return "success";
        }

        [HttpPost]
        [Route("UpadteAccount")]
        public string Update(int id, string name, string phone)
        {
            var model = new Account() { Id = id, Name = name, Phone = phone };
            using (var context = new ApplicationContext())
            {
                var account = context.Accounts.Where(i => i.Id == id).ExecuteUpdate(setters =>
                setters.SetProperty(i => i.Name, name)
                .SetProperty(i => i.Phone, phone));
            }
            return "success";
        }

        [HttpPost]
        [Route("Delete")]
        public string Delete(int id)
        {
            using (var context = new ApplicationContext())
            {
                var account = context.Accounts.Where(i => i.Id == id).ExecuteDelete();
            }
            return "success";
        }

    }
}
