using AutoMapper;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using CleanCode.Core.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler
    : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _repository;
    private readonly IAppLogger<GetLeaveRequestDetailsQueryHandler> _logger;

    public GetLeaveRequestDetailsQueryHandler(IMapper mapper,
        ILeaveRequestRepository repository,
        IAppLogger<GetLeaveRequestDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        // query database
        var leaveRequest = await _repository
            .GetByIdAsync(request.Id) ??
                throw new NotFoundException(nameof(Domain.LeaveRequest)
                    , request.Id);

        // convert data object to DTO object
        var data = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

        // TODO: Add employee details as needed

        // return DTO object
        return data;
    }
}
