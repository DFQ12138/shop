using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Goods
    {
        //主键
        [Key]
        public Guid Id { get; set; }
        //名字
        [Required(ErrorMessage = "名字（Name）不可为空")]//必须填写的字段
        public string? Name { get; set; }
        //详情
        [Required(ErrorMessage = "详情（Detail）不可为空")]//必须填写的字段
        [MaxLength(1500)]//最大长度
        public string? Detail { get; set; }
        //原价
        [Column(TypeName = "decimal(18,2)")]//限制数据类型
        public decimal? OriginalPrice { get; set; }
        //折扣
        [Range(0.0,1.0)]//限制数据范围
        public double? DiscountPresent { get; set; }
        //商品图片
        public ICollection<Picture>? GoodsPictures { get; set; }


    }
}
