using interview.Models.EFModels;
using interview.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace interview.Models.Repositories
{
    public class OrderRepo
    {
        private readonly AppDbContext _db;
        public OrderRepo()
        {
            _db = new AppDbContext();
        }

        public async Task<List<OrderVm>> GetAllOrder(OrderQueryParameters parameters)
        {
            return await _db.Orders
                .Select(o => new OrderVm
                {
                    OrderId = o.Id,
                    OrderName = o.OrderName,
                    TotalAmount = o.TotalAmount,
                    OrderDate = o.OrderDate,
                    UserName = o.Users.Name,
                    TotalQuantity = o.OrderDetails.Sum(n => n.Quantity)

                }).ToListAsync();           
        }
        public async Task<List<OrderVm>> GetOrder(OrderQueryParameters parameters)
        {
            var query = from o in _db.Orders
                        join u in _db.Users on o.UserID equals u.Id
                        select new OrderVm
                        {
                            OrderId = o.Id,
                            OrderName = o.OrderName,
                            TotalAmount = o.TotalAmount,
                            OrderDate = o.OrderDate,
                            UserName = u.Name,
                            TotalQuantity = o.OrderDetails.Sum(n => n.Quantity)
                        };
            // 日期篩選
            if (parameters.StartDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= parameters.StartDate.Value);
            }

            if (parameters.EndDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= parameters.EndDate.Value);
            }

            // 關鍵字過濾
            if (!string.IsNullOrWhiteSpace(parameters.Keyword))
            {
                query = query.Where(o =>
                    o.OrderName.Contains(parameters.Keyword) ||
                    o.UserName.Contains(parameters.Keyword));
            }

            // 金額篩選
            if (parameters.Amount.HasValue && parameters.Amount.Value > 0)
            {
                query = query.Where(o => o.TotalAmount > parameters.Amount.Value);
            }

            // 排序
            if (!string.IsNullOrEmpty(parameters.AmountSort))
            {
                if (parameters.AmountSort.ToLower() == "asc")
                {
                    query = query.OrderBy(o => o.TotalAmount);
                }
                else if (parameters.AmountSort.ToLower() == "desc")
                {
                    query = query.OrderByDescending(o => o.TotalAmount);
                }
            }
            else
            {
                query = query.OrderBy(o => o.OrderDate);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// 取得訂單總筆數
        /// </summary>
        /// <returns></returns>
        public async Task<int> GettotalRecords(OrderQueryParameters parameters)
        {
            var orders = await GetOrder(parameters); 
            return orders.Count; 
        }
    }
   
}