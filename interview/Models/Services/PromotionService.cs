using interview.Models.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using interview.Models.EFModels;
using System;

namespace interview.Models.Services
{
    public class PromotionService
    {
        private readonly PromotionRepo _promotionRepo;
        public PromotionService()
        {
            _promotionRepo = new PromotionRepo();
        }
        public async Task<List<Promotions>> GetPromotions()
        {
            return await _promotionRepo.GetPromotions();
        }
        public async Task AddPromotions(Promotions promotion)
        {
            if (string.IsNullOrWhiteSpace("優惠名稱不可為空"))
            {
                throw new ArgumentException("優惠名稱不可為空");
            }
            await _promotionRepo.Create(promotion);
        }
        public async Task DeletePromotions(int id)
        {
            await _promotionRepo.Delete(id);
        }
        public async Task UpdatePrmotions(Promotions promotion)
        {
            if (string.IsNullOrWhiteSpace(promotion.PromotionName))
            {
                throw new KeyNotFoundException("找不到優惠");
            }
            await _promotionRepo.Update(promotion);
        }
    }
}