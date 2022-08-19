using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.DataBase
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)//继承父类的options
        {

        }
        //完成模型与数据库中表的映射
        public DbSet<Goods> goods { get; set; }
        public DbSet<Picture> pictures { get; set; }
    }
}
