using AutoMapper;
using CleanCode.Core.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using CleanCode.Core.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using CleanCode.Core.Domain;

namespace CleanCode.Core.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDetailsDto>();
    }
}
