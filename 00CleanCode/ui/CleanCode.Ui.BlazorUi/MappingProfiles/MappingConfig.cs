using AutoMapper;
using CleanCode.Ui.BlazorUi.Models.LeaveTypes;
using CleanCode.Ui.BlazorUi.Services.Base;

namespace CleanCode.Ui.BlazorUi.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
    }
}
