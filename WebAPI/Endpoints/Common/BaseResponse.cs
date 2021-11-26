namespace WebAPI.Endpoints.Common
{
    public class BaseResponse
    {
        public string Message { get; set; }
        
    }
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }
}