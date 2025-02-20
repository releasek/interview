using interview.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}