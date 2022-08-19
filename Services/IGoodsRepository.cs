using Shop.Models;

namespace Shop.Services
{
    public interface IGoodsRepository
    {
        //返回所有商品信息
        IEnumerable<Goods> GetGoods(string? keyword);
        //根据Id返回单一商品
        Goods GetGoodsById(Guid GoodsId);
        //创建商品
        void AddGoods(Goods goods);
        //添加图片
        void AddPicture(Guid goodsId,Picture picture);
        //保存进入数据库
        bool Save();
        //判断商品是否存在
        bool GoodsExit(Guid goodsId);
        //根据商品id获取图片
        IEnumerable<Picture> GetPicturesByGoodsId(Guid goodsId);
        Picture GetPicture(int pictureId);
    }
}
