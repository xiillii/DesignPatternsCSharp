using AutoMapper;
using CleanCode.Core.Application.Contracts.Persistence;
using MediatR;

namespace CleanCode.Core.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryHandler 
    : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _repository;

    public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository repository)
    {
        this._mapper = mapper;
        this._repository = repository;
    }

    public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request,
                                                  CancellationToken cancellationToken)
    {
        // query database
        var leaveTypes = await _repository.GetByIdAsync(request.Id);

        // convert data object to DTO object
        var data = _mapper.Map<LeaveTypeDetailsDto>(leaveTypes);

        // return DTO object
        return data;
    }
}
