using AutoMapper;
using CleanCode.Core.Application.Contracts.Persistence;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _repository;

    public UpdateLeaveTypeCommandHandler(IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _repository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request
        , CancellationToken cancellationToken)
    {
        // validate incoming data

        // convert to domain entity object
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

        // add to database
        await _repository.CreateAsync(leaveTypeToUpdate);

        // return Unit value
        return Unit.Value;
    }
}
