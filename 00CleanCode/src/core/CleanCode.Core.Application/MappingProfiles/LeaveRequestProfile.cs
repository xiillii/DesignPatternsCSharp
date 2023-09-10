using AutoMapper;
using CleanCode.Core.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using CleanCode.Core.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using CleanCode.Core.Domain;

namespace CleanCode.Core.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequestDto, LeaveRequest>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
    }
}
