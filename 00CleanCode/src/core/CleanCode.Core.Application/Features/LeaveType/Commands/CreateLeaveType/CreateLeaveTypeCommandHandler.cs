﻿using AutoMapper;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Domain;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _repository;

    public CreateLeaveTypeCommandHandler(IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _repository = leaveTypeRepository;
    }

    public async Task<int> Handle(CreateLeaveTypeCommand request
        , CancellationToken cancellationToken)
    {
        // validate incoming data

        // convert to domain entity object
        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

        // add to database
        await _repository.CreateAsync(leaveTypeToCreate);

        // return record id
        return leaveTypeToCreate.Id;
    }
}