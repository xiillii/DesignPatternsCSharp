using AutoMapper;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Exceptions;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler 
    : IRequestHandler<GetLeaveAllocationDetailsQuery
        , LeaveAllocationDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _repository;
    private readonly IAppLogger<GetLeaveAllocationDetailsQueryHandler> _logger;

    public GetLeaveAllocationDetailsQueryHandler(IMapper mapper,
        ILeaveAllocationRepository repository,
        IAppLogger<GetLeaveAllocationDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request,
                                                  CancellationToken cancellationToken)
    {
        // query database
        var lAllocations = await _repository
            .GetByIdAsync(request.Id) ??
            throw new NotFoundException(nameof(Domain.LeaveAllocation),
            request.Id);

        // convert data object to DTO object
        var data = _mapper.Map<LeaveAllocationDetailsDto>(lAllocations);

        _logger.LogInformation("Leave Allocations details were retrieved successfully");

        // return DTO object
        return data;
    }
}
