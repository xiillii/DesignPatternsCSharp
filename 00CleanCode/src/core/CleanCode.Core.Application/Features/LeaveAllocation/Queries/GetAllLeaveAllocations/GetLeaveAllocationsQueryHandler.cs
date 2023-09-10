using AutoMapper;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class GetLeaveAllocationsQueryHandler 
    : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _repository;
    private readonly IAppLogger<GetLeaveAllocationsQueryHandler> _logger;

    public GetLeaveAllocationsQueryHandler(IMapper mapper,
        ILeaveAllocationRepository leaveTypeRepository,
        IAppLogger<GetLeaveAllocationsQueryHandler> logger)
    {
        _mapper = mapper;
        _repository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request,
                                                 CancellationToken cancellationToken)
    {
        // query database
        var lAllocations = await _repository.GetAsync();

        // convert data objects to DTO objects
        var data = _mapper.Map<List<LeaveAllocationDto>>(lAllocations);

        _logger.LogInformation("Leave Allocations were retrieved successfully");

        // return list of DTO object
        return data;
    }
}
