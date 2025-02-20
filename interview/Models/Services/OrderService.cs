using interview.Models.Infra;
using interview.Models.Repositories;
using interview.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace interview.Models.Services
{
    public class OrderService
    {
        private readonly OrderRepo _orderRepo;
        public OrderService()
        {
            _orderRepo = new OrderRepo();
        }

        public async Task<Paged<OrderVm>> GetAllPageOrder(OrderQueryParameters parameters)
        {
            // 獲取篩選後的訂單數據(非同步)
            var query = await _orderRepo.GetAllOrder(parameters);

            // 獲取總記錄數 (非同步)
            var totalRecords = await _orderRepo.GettotalRecords(parameters);

            // 分頁處理
            var data = query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();

            // 構造 Pagination 物件
            var pagination = new Pagination(
                parameters.PageNumber,
                parameters.PageSize,
                totalRecords,
                parameters.Keyword);

            return new Paged<OrderVm>(data, pagination, parameters.Keyword, null);
        }

        public async Task<List<OrderVm>> GetOrder(OrderQueryParameters parameters)
        {
            try
            {
                // 從 Repository 獲取查詢結果
                var query = await _orderRepo.GetOrder(parameters);

                // 將查詢轉換為 List 並返回
                return query.ToList();
            }
            catch (Exception ex)
            {
                // 記錄日誌或處理錯誤
                Console.WriteLine($"Error in Service Layer: {ex.Message}");
                throw; // 將例外重新拋出
            }
        }

        public async Task<Paged<OrderVm>> GetPageOrder(OrderQueryParameters parameters)
        {
            // 獲取篩選後的訂單數據
            var query = await _orderRepo.GetOrder(parameters);

            // 獲取分頁數據
            var totalRecords = await _orderRepo.GettotalRecords(parameters);

            //分頁處裡
            var data = query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();
            // 構造 Pagination 對象
            var pagination = new Pagination(
                parameters.PageNumber,
                parameters.PageSize,
                totalRecords,
                parameters.Keyword);
            return new Paged<OrderVm>(data, pagination, parameters.Keyword, null);
        }
    }
}