using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using interview.Models.EFModels;
using interview.Models.Repositories;

namespace interview.Models.Services
{
    public class ProductService
    {
        private readonly ProductRepo _productRepo;

        public ProductService()
        {
            _productRepo = new ProductRepo();
        }

        public async Task<List<Products>> GetBooks()
        {

            return await _productRepo.GetBooks();
        }
        public async Task<Products> GetBookById(int id)
        {
            return await _productRepo.GetBookById(id);
        }
        public async Task<int> CreateBook(Products product)
        {

            if(product==null) throw new System.ArgumentNullException(nameof(product));
            var newProduct = await _productRepo.Create(product);
            return newProduct.Id;
        }
        public async Task<Products> UpdateBook(Products product)
        {
            
            if (product == null) throw new System.ArgumentNullException(nameof(product));

            return await _productRepo.Update(product);
        }
        public async Task DeleteBook(int id)
        {
           var product = await _productRepo.GetBookById(id);
            if (product == null)
            {
                throw new KeyNotFoundException("找不到書籍");
            }
            await _productRepo.Delete(id);

        }

    }
}