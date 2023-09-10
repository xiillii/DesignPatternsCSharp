using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Features.LeaveRequest.Shared;
using FluentValidation;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator
    : AbstractValidator<CreateLeaveRequestCommand>
{
    

    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository repository)
    {
        Include(new BaseLeaveRequestValidator(repository));
    }
}
