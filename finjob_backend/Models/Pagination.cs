﻿namespace finjob_backend.Models
{
    public class Pagination
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
