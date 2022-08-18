using Kanakku.Shared.Models;
using MediatR;

namespace Kanakku.UI.Contracts.Event
{
    public interface IAppMediator
    {
        public Task<ResponseDto<TData>> Send<TData>(IRequest<ResponseDto<TData>> request);
        public Task<ResponseDto<TResponse>> Send<TResponse>(IRequest<TResponse> request);
    }
}
