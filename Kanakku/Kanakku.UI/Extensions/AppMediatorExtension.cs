using Kanakku.Shared.Models;

namespace Kanakku.UI.Extensions
{
    public static class AppMediatorExtension
    {
        public static async Task<ResponseDto<TData>> OnSuccess<TData>(this Task<ResponseDto<TData>> response,
                Action<TData> callback)
        {
            var res = await response.ConfigureAwait(false);
            if (res.HasErrors || res.HasError)
            {
                return res;
            }

            callback(res.Data);
            return res;
        }

        public static async Task<ResponseDto<TData>> OnError<TData>(this Task<ResponseDto<TData>> response,
            Action<ErrorDto> callback)
        {
            var res = await response.ConfigureAwait(false);
            if (!res.HasError)
            {
                return res;
            }

            callback(res.Error);
            return res;
        }

        public static async Task<ResponseDto<TData>> OnErrors<TData>(this Task<ResponseDto<TData>> response,
            Action<List<FieldErrorDto>> callback)
        {
            var res = await response.ConfigureAwait(false);
            if (!res.HasErrors)
            {
                return res;
            }

            callback(res.Errors);
            return res;
        }
    }

}
