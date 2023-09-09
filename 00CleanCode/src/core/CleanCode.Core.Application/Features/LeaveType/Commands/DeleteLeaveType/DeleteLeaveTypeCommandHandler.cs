using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
  
    private readonly ILeaveTypeRepository _repository;

    public DeleteLeaveTypeCommandHandler(
        ILeaveTypeRepository leaveTypeRepository)
    {
        
        _repository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveTypeCommand request
        , CancellationToken cancellationToken)
    {
        // Get the object from database
        var leaveTypeToDelete = await _repository
            .GetByIdAsync(request.Id) ?? 
                throw new NotFoundException(nameof(Domain.LeaveType)
                    , request.Id);


        // delete from database
        await _repository.DeleteAsync(leaveTypeToDelete);

        // return Unit value
        return Unit.Value;
    }
}
