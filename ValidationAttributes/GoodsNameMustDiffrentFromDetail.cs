using Shop.Dtos;
using System.ComponentModel.DataAnnotations;
 
namespace Shop.ValidationAtrributes
{
    public class GoodsNameMustDiffrentFromDetail:ValidationAttribute//继承数据验证
    {
        //重写IsValid函数
        protected override ValidationResult? IsValid(
            object? value, //输入的数据对象
            ValidationContext validationContext //验证的上下文关系对象
        )
        {
            var GoodsDto = (GoodsForCreationDto)validationContext.ObjectInstance;//获取验证的数据对象
            if (GoodsDto.Name == GoodsDto.Detail)
            {
                return new ValidationResult(
                    "商品名称必须与商品描述不同",
                    new[] { "GoodsForCreationDto" }
                );
            }
            return ValidationResult.Success;
        }
    }
}
