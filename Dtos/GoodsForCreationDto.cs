using Shop.Models;
using Shop.ValidationAtrributes;
using System.ComponentModel.DataAnnotations;

namespace Shop.Dtos
{
    [GoodsNameMustDiffrentFromDetail]
    public class GoodsForCreationDto //: IValidatableObject
    {
        [Required(ErrorMessage = "名字（Name）不可为空")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "详情（Detail）不可为空")]
        [MaxLength(1500)]
        public string? Detail { get; set; }
        public decimal? OriginalPrice { get; set; }
        [Range(0.0, 1.0)]//限制数据范围
        public double? DiscountPresent { get; set; }
        public ICollection<PictureForCreationDto>? GoodsPictures { get; set; }
            = new List<PictureForCreationDto>();

        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == Detail)
            {
                yield return new ValidationResult(
                    "商品名称必须与商品描述不同",
                    new[] { "GoodsForCreationDto" }
                );
            }
        }*/
    }
}
