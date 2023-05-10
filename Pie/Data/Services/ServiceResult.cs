﻿namespace Pie.Data.Services
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Value { get; set; }
    }
}
