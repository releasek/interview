using interview.Models.Infra;
using interview.Models.Services;
using interview.Models.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;



namespace interview.Controllers.Api
{
    [RoutePrefix("api/order")]
    public class OrderApiController : ApiController
    {
        private readonly OrderService _orderService;
        public OrderApiController()
        {
            _orderService = new OrderService();
        }
        public async Task<IHttpActionResult> Getorder([FromUri] OrderQueryParameters parameters)
        {
            try
            {
                if (parameters == null ||
                    (parameters.StartDate == null &&
                    parameters.EndDate == null &&
                   (!parameters.Amount.HasValue || parameters.Amount <= 0) &&
                    string.IsNullOrWhiteSpace(parameters.Keyword) &&
                    string.IsNullOrWhiteSpace(parameters.AmountSort)))
                {
                    var data = await _orderService.GetAllPageOrder(parameters);
                    return Ok(new
                    {
                        message = "成功獲取訂單資料",
                        data = data.Data,
                        pagination = new
                        {
                            pageNumber = data.Pagination.PageNumber,
                            pageSize = data.Pagination.PageSize,
                            totalPages = data.Pagination.TotalPages
                        }
                    });
                }
                // 調用服務層獲取分頁數據
                var pagedOrders = await _orderService.GetPageOrder(parameters);
                // 如果分頁數據為空，回傳空結果（防止資料錯誤）
                if (parameters == null || pagedOrders.Data == null || !pagedOrders.Data.Any())
                {
                    return Ok(new
                    {
                        message = "查詢結果為空。",
                        data = new List<OrderVm>(),
                        pagination = new
                        {
                            pageNumber = parameters.PageNumber,
                            pageSize = parameters.PageSize,
                            totalRecords = 0,
                            totalPages = 0
                        }
                    });
                }
                return Ok(new
                {
                    message = "成功獲取訂單資料。",
                    data = pagedOrders.Data,
                    pagination = new
                    {
                        pageNumber = pagedOrders.Pagination.PageNumber,
                        pageSize = pagedOrders.Pagination.PageSize,
                        totalRecords = pagedOrders.Pagination.TotalRecords,
                        totalPages = pagedOrders.Pagination.TotalPages
                    }
                });
            }
            catch(Exception ex)
            {
                // 建立 HTTP 500 回應並包含錯誤訊息
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = ex.Message });
                return ResponseMessage(response);
            }
            
        }

    }
}
