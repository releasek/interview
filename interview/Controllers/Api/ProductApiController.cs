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
    public class ProductApiController : ApiController
    {
        private readonly ProductService _productService;
        public ProductApiController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllbooks()
        {
            var books = await _productService.GetBooks();
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetBookById(int id)
        {
            var book = await _productService.GetBooks();
            return Ok(book);
        }
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateBook([FromBody] Products product)
        {
            if (product == null) return BadRequest("請輸入商品");
            try
            {
                var newBookId = await _productService.CreateBook(product);
                return Ok(newBookId);
            }
            catch (ArgumentNullException)
            {
                //400 Bad Request
                return BadRequest("書籍資料不可為空");
            }
        }
    }
}
