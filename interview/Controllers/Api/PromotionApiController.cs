using interview.Models.EFModels;
using interview.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace interview.Controllers.Api
{
    [RoutePrefix("api/promotion")]
    public class PromotionApiController : ApiController
    {
        private readonly PromotionService _promotionService;
        public PromotionApiController()
        {
            _promotionService = new PromotionService();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllPromotion()
        {
            var promotions = await _promotionService.GetPromotions();
            return Ok(promotions);
        }
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreatePromotion([FromBody] Promotions promotion)
        {
            if (promotion == null) return BadRequest("請輸入優惠");
            try
            {
                await _promotionService.AddPromotions(promotion);
                return Ok(new { Message = "新增成功", Promotion = promotion.PromotionName });
            }
            catch (ArgumentException)
            {
                return BadRequest("優惠名稱不可為空");
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeletePromotiom(int id)
        {
            try
            {
                await _promotionService.DeletePromotions(id);
                return Ok("刪除成功");
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("找不到促銷活動");
            }
           
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdatePromotion(int id, [FromBody] Promotions promotion)
        {
            if (promotion == null) return BadRequest("請輸入優惠");
            try
            {
                await _promotionService.UpdatePrmotions(promotion);
                return Ok("更新成功");
            }
            catch (KeyNotFoundException)
            {
                return BadRequest("找不到優惠");
            }
        }
    }
}
