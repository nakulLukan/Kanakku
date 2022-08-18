using FluentValidation;
using Kanakku.Shared.Models;
using Kanakku.Shared.Utilities;
using Kanakku.UI.Contracts.Event;
using MediatR;

namespace Kanakku.UI.Impl.Event;

public class AppMediator : IAppMediator
{
    private readonly IMediator mediator;

    public AppMediator(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<ResponseDto<TData>> Send<TData>(IRequest<ResponseDto<TData>> request)
    {
        try
        {
            return await mediator.Send(request);
        }
        catch (ValidationException ex)
        {
            return new ResponseDto<TData>(ex.Errors.Select(x => new FieldErrorDto(x.PropertyName, x.ErrorMessage)));
        }
        catch (AppException ex)
        {
            return new ResponseDto<TData>(new ErrorDto(ex.ErrorMessage));
        }
        catch (Exception ex)
        {
            return new ResponseDto<TData>(new ErrorDto("Oops, something went wrong."));
        }
    }

    public async Task<ResponseDto<TResponse>> Send<TResponse>(IRequest<TResponse> request)
    {
        try
        {
            return new ResponseDto<TResponse>(await mediator.Send(request));
        }
        catch (ValidationException ex)
        {
            return new ResponseDto<TResponse>(ex.Errors.Select(x => new FieldErrorDto(x.PropertyName, x.ErrorMessage)));
        }
        catch (AppException ex)
        {
            return new ResponseDto<TResponse>(new ErrorDto(ex.ErrorMessage));
        }
        catch (Exception ex)
        {
            return new ResponseDto<TResponse>(new ErrorDto("Oops, something went wrong."));
        }
    }
}
