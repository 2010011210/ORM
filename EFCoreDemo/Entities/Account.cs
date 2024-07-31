using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreDemo.Entities
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Column("Name")]  // 列名称，可以配置
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
}
