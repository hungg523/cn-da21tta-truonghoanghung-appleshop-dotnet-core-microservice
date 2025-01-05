using AssetService.Errors;
using System.Net;

namespace AssetService.Shared
{
    public class Result<TModel> where TModel : class
    {
        public int StatusCode { get; set; }

        public bool IsSuccess { get; set; }

        public Error? Error { get; set; }

        public static implicit operator Result<TModel>(Result<object> result) => new()
        {
            StatusCode = result.StatusCode,
            IsSuccess = result.IsSuccess,
            Error = result.Error
        };

        public static implicit operator Result<TModel>(TModel model) => new()
        {
            StatusCode = (int)HttpStatusCode.OK,
            IsSuccess = true
        };
    }
}