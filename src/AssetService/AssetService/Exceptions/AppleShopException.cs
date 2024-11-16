using AssetService.Constants;
using AssetService.DependencyInjection.Extensions;
using System.Net;

namespace AssetService.Exceptions
{
    public class AppleShopException : Exception
    {
        public int StatusCode { get; set; }
        public List<string> Details { get; set; } = new();

        public static void NotFound<TEntity>(string? message = null)
        {
            message ??= MsgConst.NOT_FOUND_FIND_KEY.FormatMsg(typeof(TEntity).Name);
            var nhaThuocException = new AppleShopException
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Details = [message]
            };
            throw nhaThuocException;
        }

        public static void ThrowIfValidation(params string[] details)
        {
            var nhaThuocException = new AppleShopException
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Details = details.ToList()
            };
            throw nhaThuocException;
        }
    }
}