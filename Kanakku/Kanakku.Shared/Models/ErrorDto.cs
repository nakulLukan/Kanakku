namespace Kanakku.Shared.Models;

public class ErrorDto
{
    public string Message { get; set; }
    public ErrorType Type { get; set; }

    public ErrorDto(string message, ErrorType type = ErrorType.Error)
    {
        Message = message;
        Type = type;
    }
}
