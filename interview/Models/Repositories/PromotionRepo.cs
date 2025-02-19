using interview.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;


namespace interview.Models.Repositories
{
    public class PromotionRepo
    {
        private readonly AppDbContext _db;
        public PromotionRepo()
        {
            _db = new AppDbContext();
        }
        public async Task<List<Promotions>> GetPromotions()
        {
            return await _db.Promotions.ToListAsync();
        }
        public async Task Create(Promotions promotion)
        {
            if (promotion == null)
            {
                throw new ArgumentNullException(nameof(promotion));
            }
            _db.Promotions.Add(promotion);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var promotion = await _db.Promotions.FindAsync(id);
            if (promotion == null)
            {
                throw new KeyNotFoundException("找不到促銷活動");
            }
            _db.Promotions.Remove(promotion);
            await _db.SaveChangesAsync();
        }
        public async Task Update(Promotions promotion)
        {
            if (promotion == null)
            {
                throw new ArgumentNullException(nameof(promotion));
            }
            _db.Entry(promotion).State=EntityState.Modified;
            await _db.SaveChangesAsync();           
        }
    }
}