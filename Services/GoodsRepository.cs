using Microsoft.EntityFrameworkCore;
using Shop.DataBase;
using Shop.Models;

namespace Shop.Services
{
    public class GoodsRepository : IGoodsRepository
    {
        private readonly AppDbContext _context;
        public GoodsRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Goods> GetGoods(string? keyword)
        {
            //数据库的延迟执行操作，减少数据库访问次数
            IQueryable<Goods> result = _context
                .goods
                .Include(g => g.GoodsPictures);
            if(!string.IsNullOrWhiteSpace(keyword))//如果keyword不为空或者空字符串
            {
                keyword = keyword.Trim();//删除行首空格
                result = result.Where(g => g.Name.Contains(keyword));//返回包含keyword的数据
            }
            return result.ToList();//聚合操作返回数据
        }
        public Goods GetGoodsById(Guid GoodsId)
        {
            //这里采用了lamda表达式
            return _context.goods.Include(g => g.GoodsPictures).FirstOrDefault(n => n.Id == GoodsId);
        }
        public void AddGoods(Goods goods)
        {
            if(goods == null)
            {
                throw new ArgumentNullException(nameof(goods));//抛出空数据异常
            }
            _context.goods.Add(goods);//添加进入数据库
        }
        public void AddPicture(Guid goodsId, Picture picture)
        {
            if(goodsId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(goodsId));//抛出空数据异常
            }
            if(picture == null)
            {
                throw new ArgumentNullException(nameof(picture));//抛出空数据异常
            }
            picture.GoodsId = goodsId;
            _context.pictures.Add(picture);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
        public bool GoodsExit(Guid goodsId)
        {
            return _context.goods.Any(g => g.Id == goodsId);
        }
        public IEnumerable<Picture> GetPicturesByGoodsId(Guid goodsId)
        {
            return _context.pictures.Where(p => p.GoodsId == goodsId).ToList();
        }
        public Picture GetPicture(int pictureId)
        {
            return _context.pictures.Where(p => p.Id == pictureId).FirstOrDefault();
        }
    }
}
