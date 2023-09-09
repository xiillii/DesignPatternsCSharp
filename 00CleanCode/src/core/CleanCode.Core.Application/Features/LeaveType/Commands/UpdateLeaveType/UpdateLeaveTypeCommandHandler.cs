using AutoMapper;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
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
        var validator = new UpdateLeaveTypeCommandValidator(_repository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid LeaveType", validationResult);
        }
        // convert to domain entity object
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

        // add to database
        await _repository.CreateAsync(leaveTypeToUpdate);

        // return Unit value
        return Unit.Value;
    }
}
