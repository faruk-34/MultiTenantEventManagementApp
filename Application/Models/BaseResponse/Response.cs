namespace Application.Models.BaseResponse
{
    public class Response<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
    public class BaseResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string MessageTitle { get; set; }
        public string ErrorMessage { get; set; }
   
    }
}
