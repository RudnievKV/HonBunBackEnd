using System;
using System.Collections.Generic;

namespace HonbunNoAnkiApi.Wrappers
{
    public class PagedResponse<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        //public Uri FirstPage { get; set; }
        //public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        //public Uri NextPage { get; set; }
        //public Uri PreviousPage { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize, int totalRecords)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
            TotalRecords = totalRecords;
            if (totalRecords == 0)
            {
                TotalPages = 1;
            }
            else if (totalRecords % pageSize != 0)
            {
                TotalPages = totalRecords / pageSize + 1;
            }
            else
            {
                TotalPages = totalRecords / pageSize;
            }

            Succeeded = true;
            Errors = null;
        }
    }
}
