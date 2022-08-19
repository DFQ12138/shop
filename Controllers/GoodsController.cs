using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Dtos;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : Controller
    {
        //在控制器中调用数据仓库
        private IGoodsRepository _goodsRepository;
        private readonly IMapper _mapper;
        public GoodsController(
            IGoodsRepository goodsRepository,
            IMapper mapper
            )
        {
            //通过构造函数注入数据仓库服务
            _goodsRepository = goodsRepository;
            _mapper = mapper;
        }
        //实现第一个控制器中的函数
        [HttpGet]//api/Goods?keyword=商品
        [HttpHead]
        public IActionResult GetGoods([FromQuery] string? keyword)
        {
            var goodsFromRepo = _goodsRepository.GetGoods(keyword);
            if(goodsFromRepo == null || goodsFromRepo.Count()<=0)
            {
                return NotFound("没有商品");
            }
            var goodsDto = _mapper.Map<IEnumerable<GoodsDto>>(goodsFromRepo);
            return Ok(goodsDto);
        }
        //api/Goods/{goodsId}
        [HttpGet("{goodsId}",Name = "GetGoodsById")]
        [HttpHead("{goodsId}", Name = "GetGoodsById")]
        public IActionResult GetGoodsById(Guid goodsId)
        {
            var goodsFromRepo = _goodsRepository.GetGoodsById(goodsId);
            if (goodsFromRepo == null)
            {
                return NotFound($"未找到商品{goodsId}");//模板字符串$
            }
            var goodsDto = _mapper.Map<GoodsDto>(goodsFromRepo);
            return Ok(goodsDto);
        }
        [HttpPost]
        public IActionResult CreateGoods([FromBody] GoodsForCreationDto goodsForCreationDto)
        {
            var goodsModel = _mapper.Map<Goods>(goodsForCreationDto);
            _goodsRepository.AddGoods(goodsModel);//添加进入数据库
            _goodsRepository.Save();//保存
            //返回保存后的信息
            var goodsToReturn = _mapper.Map<GoodsDto>(goodsModel);
            //api路径名字 api需要的参数 返回的数据
            return CreatedAtRoute(
                "GetGoodsById",
                new { goodsid = goodsToReturn.Id },
                goodsToReturn
                );

        }
    }
}
