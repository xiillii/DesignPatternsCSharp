using AutoMapper;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Commads.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler
    : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _logger;

    public UpdateLeaveAllocationCommandHandler(IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IAppLogger<UpdateLeaveAllocationCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request
        , CancellationToken cancellationToken)
    {
        // validate incoming data
        var validator = new UpdateLeaveAllocationCommandValidator(
            _leaveTypeRepository,
            _leaveAllocationRepository);    
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation error in update request for {0} - {1}",
                nameof(Domain.LeaveType), request.Id);
            throw new BadRequestException("Invalid LeaveAllocation", validationResult);
        }

        // get the item
        var lAllocationToUpdate = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        if (lAllocationToUpdate is null)
        {
            _logger.LogWarning("LeaveAllocation not found error in update request for {0} - {1}",
                nameof(Domain.LeaveAllocation), request.Id);

            throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);
        }

        // convert to domain entity object.
        // this is most secure, because we are updating a real element
        _mapper.Map(request, lAllocationToUpdate);

        // add to database
        await _leaveAllocationRepository.UpdateAsync(lAllocationToUpdate);

        // return

        return Unit.Value;
    }
}
