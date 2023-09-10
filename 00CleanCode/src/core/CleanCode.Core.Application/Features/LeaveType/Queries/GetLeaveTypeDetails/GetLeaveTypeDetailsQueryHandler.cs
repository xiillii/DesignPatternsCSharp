using AutoMapper;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler 
    : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _repository;
    private readonly IAppLogger<GetLeaveTypeDetailsQueryHandler> _logger;

    public GetLeaveTypeDetailsQueryHandler(IMapper mapper, 
        ILeaveTypeRepository repository,
        IAppLogger<GetLeaveTypeDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request,
                                                  CancellationToken cancellationToken)
    {
        // query database
        var leaveTypes = await _repository
            .GetByIdAsync(request.Id) ?? 
                throw new NotFoundException(nameof(Domain.LeaveType)
                    , request.Id);

        // convert data object to DTO object
        var data = _mapper.Map<LeaveTypeDetailsDto>(leaveTypes);

        _logger.LogInformation("Leave types details were retrieved successfully");

        // return DTO object
        return data;
    }
}
