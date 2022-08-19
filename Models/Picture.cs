using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Picture
    {
        //图片Id
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//数据库的自增数据类型
        public int Id { get; set; }
        //图片路径
        [Required]
        public string? Url { get; set; }
        //商品Id
        [ForeignKey("GoodsId")]//外键
        public Guid? GoodsId { get; set; }
        //商品
        public Goods? Goods { get; set; }


    }
}
