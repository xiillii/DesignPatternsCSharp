using AutoMapper;
using CleanCode.Core.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using CleanCode.Core.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using CleanCode.Core.Domain;

namespace CleanCode.Core.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();

    }
}
