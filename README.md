# ORM 
官方文档：https://learn.microsoft.com/zh-cn/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt
## 1.EFCore  
1. Nuget安装程序包 
~~~
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools

~~~
2. 增加实体类和context  
表明和列属性的修饰，可以用增加特性，如表名[Table("Account")]，列映射到数据的名称[Column("Name")]，最大长度[MaxLength(22)]  
~~~
[Table("Account")]
public class Account
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    //[Column(TypeName = "varchar(200)")]
    //[Comment("The phone number")]  //注释
    //[Required]
    //[MaxLength(22)]  //最大长度
    //[Column(TypeName = "decimal(5, 2)")]
    //[Precision(14, 2)] //decomal类型，保留几位小数
    //[NotMapped] 排除属性
    public string Phone { get; set; }
}

public class ApplicationContext: DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    
    }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=.;Database=Test;User ID=sa;Password=123456;TrustServerCertificate=true");
    }
}

~~~
3. 在appsettings.json增加数据库连接字符串  
~~~
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=Test;"
  },
  "DefaultConnection": "Server=.;Database=Test;User Id=sa;Password=123456;TrustServerCertificate=true"
}
~~~

4. 在启动类program.cs增加相关配置 UseSqlServer把数据库连接上去
~~~
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//string connectionStr = builder.Configuration["ConnectionStrings:DefaultConnection"];
string connectionStr = builder.Configuration["DefaultConnection"];

builder.Services.AddDbContext<ApplicationContext>(builder => builder.UseSqlServer(connectionStr));

~~~

5. 在visual studio中，点击“视图”=>“其他窗口”=>程序包管理器控制台， 输入 Add-Migration FirstMigration ,然后输入Update-database. 数据库相应的表就建成了。

6. 在webpai中的使用    
   1. 增加SaveChanges(), 
   2. 查询content.Account.Where
   3. 更新 ExecuteUpdate
   4. 删除 ExecuteDelete
~~~
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService) 
    {
        _accountService = accountService;
    }

    [HttpPost]
    public string Add(string name, string phone) 
    {
        var model = new Account() { Name = name, Phone = phone};
        using (var context = new ApplicationContext()) 
        {
            context.Add(model);
            context.SaveChanges();
        }
        return "success";
    }
}
~~~