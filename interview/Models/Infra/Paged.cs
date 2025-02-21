using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace interview.Models.Infra
{
    public class Paged<T>
    {
        public Paged(IEnumerable<T> data, Pagination pagination, string selected, IEnumerable<string> options)
        {
            Data = data;
            Pagination = pagination;
            Selected = selected;
            Options = options;
        }

        public IEnumerable<T> Data { get; } // 分頁後的資料
        public Pagination Pagination { get; } // 分頁資訊

        public string Selected { get; } // 當前選中的篩選條件
        public IEnumerable<string> Options { get; } // 可選的篩選條件
    }
}