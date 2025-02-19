using interview.Models.EFModels;
using interview.Models.Services;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Collections.Generic;


namespace interview.Controllers.Api
{
    [RoutePrefix("api/products")]
    public class ProductApiController : ApiController
    {
        private readonly ProductService _productService;

        public ProductApiController()
        {
            _productService = new ProductService();
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
                return Ok(new { Message = "新增成功", CreateId = newBookId });
            }
            catch (ArgumentNullException)
            {
                return BadRequest("書籍資料不可為空");
            }
            catch (Exception ex)  // 捕捉所有異常
            {
                return InternalServerError(new Exception($"伺服器錯誤: {ex.Message}"));
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> UpdateBook(int id, [FromBody] Products product)
        {
            if (product == null) return BadRequest("請輸入商品");
            try
            {
                var newBook = await _productService.UpdateBook(product);
                return Ok(new { Message = "修改成功", UpdateId = id });
            }
            catch (ArgumentNullException)
            {
                //400 Bad Request
                return BadRequest("書籍資料不可為空");
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            try
            {
                await _productService.DeleteBook(id);
                return Ok(new { Message="刪除成功",DeleteId=id});
            }
            catch (KeyNotFoundException)
            {
                //404 Not Found
                return NotFound();
            }
        }

    }
}
