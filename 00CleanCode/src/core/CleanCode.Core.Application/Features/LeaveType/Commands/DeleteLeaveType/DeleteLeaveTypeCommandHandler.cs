using CleanCode.Core.Application.Contracts.Persistence;
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
        var leaveTypeToDelete = await _repository.GetByIdAsync(request.Id);

        // convert to domain entity object
        

        // delete from database
        await _repository.DeleteAsync(leaveTypeToDelete);

        // return Unit value
        return Unit.Value;
    }
}
