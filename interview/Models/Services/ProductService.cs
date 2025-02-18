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

        public ProductService(ProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<List<Products>> GetBooks()
        {

            return await _productRepo.GetBooks();
        }
        public async Task<int> CreateBook(Products product)
        {

            if(product==null) throw new System.ArgumentNullException(nameof(product));

            var CreateBook = await _productRepo.GetBookById(product.Id);
            if (CreateBook != null)
            {
                throw new System.ArgumentException("書籍已存在");
            }
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