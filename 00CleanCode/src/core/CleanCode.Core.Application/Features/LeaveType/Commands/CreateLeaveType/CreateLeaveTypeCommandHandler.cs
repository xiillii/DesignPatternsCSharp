using AutoMapper;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _repository;
    private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;

    public CreateLeaveTypeCommandHandler(IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<CreateLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _repository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request
        , CancellationToken cancellationToken)
    {
        // validate incoming data
        var validator = new CreateLeaveTypeCommandValidator(_repository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);   

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation error in create request for {0} - {1}",
                nameof(LeaveType), request.Name);
            throw new BadRequestException("Invalid LeaveType", validationResult);
        }

        // convert to domain entity object
        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

        // add to database
        await _repository.CreateAsync(leaveTypeToCreate);

        // return record id
        return leaveTypeToCreate.Id;
    }
}
