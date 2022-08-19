using Shop.Models;
using System.ComponentModel.DataAnnotations;

namespace Shop.Dtos
{
    public class GoodsDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Detail { get; set; }
        public decimal? Price { get; set; }
        //public decimal? OriginalPrice { get; set; }
        //public double? DiscountPresert { get; set; }
        public ICollection<PictureDto>? GoodsPictures { get; set; }
    }
}
