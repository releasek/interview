using interview.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace interview.Models.Repositories
{
    public class ProductRepo
    {
        private readonly AppDbContext _db;
        public ProductRepo(AppDbContext context)
        {
            _db = context;
        }
        public async Task<List<Products>> GetBooks()
        {
            return await _db.Products.Take(6).ToListAsync();
        }
        public async Task<Products> GetBookById(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
        /// <summary>
        /// 新增商品   
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Products> Create(Products product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }
        /// <summary>
        /// 編輯書籍
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<Products> Update(Products product)
        {
            //標記修改物件
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// 刪除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }


    }
}