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
        public ProductRepo()
        {
            _db = new AppDbContext();
        }
        public async Task<List<Products>> GetBooks()
        {
            return await _db.Products.Take(6).ToListAsync();
        }
        public async Task<Products> GetBookById(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Products> Create(Products product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Products> Update(Products product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            //標記修改物件
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return product;
        }


        public async Task Delete(int id)
        {
           
            var product = await _db.Products.FindAsync(id);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }


    }
}