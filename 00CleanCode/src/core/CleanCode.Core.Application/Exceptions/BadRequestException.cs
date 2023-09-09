using FluentValidation.Results;

namespace CleanCode.Core.Application.Exceptions;

public class BadRequestException : Exception
{
    public List<string>? ValidationErrors { get; set; }

    public BadRequestException(string message)
        : base(message)
    {
        
    }

    public BadRequestException(string message, ValidationResult validationResult)
        : base(message)
    {
        ValidationErrors = new();
        ValidationErrors.AddRange(from error in validationResult.Errors
                                  select error.ErrorMessage);
    }
}
