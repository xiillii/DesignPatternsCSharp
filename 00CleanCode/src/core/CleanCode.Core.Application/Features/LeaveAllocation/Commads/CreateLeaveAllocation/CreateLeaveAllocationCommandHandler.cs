using AutoMapper;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Commads.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler
    : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger;

    public CreateLeaveAllocationCommandHandler(IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<CreateLeaveAllocationCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // validate incoming data
        var validator = new CreateLeaveAllocationCommandValidator(_leaveAllocationRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation error in create request for {0} - {1}",
                nameof(Domain.LeaveAllocation), request.LeaveTypeId);
            throw new BadRequestException("Invalid LeaveAllocation", validationResult);
        }

        // get Leave Type for allocations
        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        // Get employees

        // Get period

        // convert to domain entity object
        var leaveAllocationToCreate = _mapper.Map<Domain.LeaveAllocation>(request);

        // add to database
        await _leaveAllocationRepository.CreateAsync(leaveAllocationToCreate);

        // return
        return Unit.Value;
    }
}
