using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace interview.Models.Infra
{
    public class Pagination
    {
        // 構造函數，用於初始化分頁數據
        public Pagination(int pageNumber, int pageSize, int totalRecords, string selectedOption)
        {
            PageNumber = pageNumber; // 當前頁碼
            PageSize = pageSize; // 每頁記錄數量
            TotalRecords = totalRecords; // 總記錄數量
            this.selectedOption = selectedOption; // 當前選中的篩選條件
        }
        public string selectedOption { get; } // 當前選中的篩選條件
        public int PageNumber { get; } // 當前頁碼
        public int PageSize { get; } // 每頁記錄數量
        public int TotalRecords { get; } // 總記錄數量

        public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize); // 總頁數

        public bool ShowFirstPage => PageNumber > 1; // 是否顯示第一頁
        public bool ShowPrevPage => PageNumber > 1; // 是否顯示上一頁
        public bool ShowLastPage => PageNumber < TotalPages; // 是否顯示最末頁
        public bool ShowNextPage => PageNumber < TotalPages; // 是否顯示下一頁
    }
}