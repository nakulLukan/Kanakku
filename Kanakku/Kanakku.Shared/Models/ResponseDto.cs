namespace Kanakku.Shared.Models;

public class ResponseDto<TData>
{
    public TData Data { get; set; }

    public List<FieldErrorDto> Errors { get; set; }

    public ErrorDto Error { get; set; }

    public ResponseDto(TData data)
    {
        Data = data;
    }

    public ResponseDto(IEnumerable<FieldErrorDto> errors)
    {
        Errors = errors.ToList();
    }

    public ResponseDto(ErrorDto error)
    {
        Error = error;
    }

    public bool HasData()
    {
        return Data is not null;
    }

    public bool HasErrors => (Errors is not null && Errors.Any());
    public bool HasError => Error is not null;

    public static ResponseDto<TData> ShowOopsError(string message)
    {
        return new ResponseDto<TData>(new ErrorDto($"Error: {message}", ErrorType.Error));
    }
}
