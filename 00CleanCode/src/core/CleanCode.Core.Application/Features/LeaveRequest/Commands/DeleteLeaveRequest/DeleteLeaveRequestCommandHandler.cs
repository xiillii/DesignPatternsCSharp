using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler
    : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _repository;

    public DeleteLeaveRequestCommandHandler(
        ILeaveRequestRepository leaveTypeRepository)
    {

        _repository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        // Get the object from database
        var leaveRequestToDelete = await _repository
            .GetByIdAsync(request.Id) ??
                throw new NotFoundException(nameof(Domain.LeaveRequest)
                    , request.Id);


        // delete from database
        await _repository.DeleteAsync(leaveRequestToDelete);

        // return Unit value
        return Unit.Value;
    }
}
