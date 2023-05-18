namespace Pie.Data.Services
{
    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Value { get; set; }
    }
}
