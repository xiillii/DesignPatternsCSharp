﻿using AutoMapper;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

    public GetLeaveTypesQueryHandler(IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<GetLeaveTypesQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request,
                                           CancellationToken cancellationToken)
    {
        // query the database
        var leaveTypes = await _leaveTypeRepository.GetAsync();


        // convert data objects to DTO objects
        var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        _logger.LogInformation("Leave types were retrieved successfully");

        // return List of DTO object
        return data;
    }
}
