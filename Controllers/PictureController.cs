using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Dtos;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    [Route("api/goods/{goodsId}/pictures")]
    [ApiController]
    public class PictureController : Controller
    {
        //在控制器中调用数据仓库
        private IGoodsRepository _goodsRepository;
        private readonly IMapper _mapper;

        public PictureController(
            IGoodsRepository goodsRepository,
            IMapper mapper
        )
        {
            _goodsRepository = goodsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public IActionResult GetPictureListForGoods(Guid goodsId)
        {
            if (!_goodsRepository.GoodsExit(goodsId))
            {
                return NotFound("商品不存在");
            }

            var picturesFromRepo = _goodsRepository.GetPicturesByGoodsId(goodsId);
            if (picturesFromRepo == null || picturesFromRepo.Count() <= 0)
            {
                return NotFound($"{goodsId}对应的图片不存在");
            }

            return Ok(_mapper.Map<IEnumerable<PictureDto>>(picturesFromRepo));
        }

        [HttpGet("{pictureId}", Name = "GetPicture")]
        [HttpHead("{pictureId}", Name = "GetPicture")]
        public IActionResult GetPicture(Guid goodsId, int pictureId)
        {
            if (!_goodsRepository.GoodsExit(goodsId))
            {
                return NotFound("商品不存在");
            }

            var pictureFromRepo = _goodsRepository.GetPicture(pictureId);
            if (pictureFromRepo == null)
            {
                return NotFound($"{goodsId}对应的图片不存在");
            }

            return Ok(_mapper.Map<PictureDto>(pictureFromRepo));
        }

        [HttpPost]
        public IActionResult CreateGoodsPicture(
            [FromRoute] Guid goodsId,
            [FromBody] PictureForCreationDto pictureForCreationDto
        )
        {
            if (!_goodsRepository.GoodsExit(goodsId))
            {
                return NotFound("商品不存在");
            }

            var pictureModel = _mapper.Map<Picture>(pictureForCreationDto);
            _goodsRepository.AddPicture(goodsId, pictureModel);
            _goodsRepository.Save();
            var pictureToReturn = _mapper.Map<PictureDto>(pictureModel);
            return CreatedAtRoute(
                "GetPicture",
                new
                {
                    goodsId = pictureModel.GoodsId,
                    pictureId = pictureModel.Id
                },
                pictureToReturn
            );
        }
    }
}