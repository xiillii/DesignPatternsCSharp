using AutoMapper;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class GetLeaveRequestsQueryHandler 
    : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _repository;
    private readonly IAppLogger<GetLeaveRequestsQueryHandler> _logger;

    public GetLeaveRequestsQueryHandler(IMapper mapper,
        ILeaveRequestRepository leaveTypeRepository,
        IAppLogger<GetLeaveRequestsQueryHandler> logger)
    {
        _mapper = mapper;
        _repository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<List<LeaveRequestDto>> Handle(
        GetLeaveRequestsQuery request,
        CancellationToken cancellationToken)
    {
        // TODO: check if it is logged in employee

        // query database
        var leaveRequests = await _repository.GetLeaveRequestsWithDetails();

        // convert data object to DTO object
        var data = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

        // TODO: Fill requests with employee information

        // return list of DTO object
        return data;
    }
}
