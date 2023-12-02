using Kanakku.Shared.Models;

namespace Kanakku.App.Extensions
{
    public static class AppMediatorExtension
    {
        public static async Task<ResponseDto<TData>> OnSuccess<TData>(this Task<ResponseDto<TData>> response,
                Action<TData> callback)
        {
            var res = await response.ConfigureAwait(false);
            if (res.HasErrors || res.HasError || res.HasFormError)
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

        public static async Task<ResponseDto<TData>> OnFormError<TData>(this Task<ResponseDto<TData>> response,
            Action<FormError> callback)
        {
            var res = await response.ConfigureAwait(false);
            if (!res.HasFormError)
            {
                return res;
            }

            callback(res.FormError);
            return res;
        }
    }
}
