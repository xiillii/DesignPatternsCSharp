using AutoMapper;
using CleanCode.Application.UnitTests.Mocks;
using CleanCode.Core.Application.Contracts.Logging;
using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using CleanCode.Core.Application.MappingProfiles;
using Moq;
using Shouldly;

namespace CleanCode.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypesQueryHandlerTests
{
    private readonly Mock<ILeaveTypeRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly Mock<IAppLogger<GetLeaveTypesQueryHandler>> _appLogger;

    public GetLeaveTypesQueryHandlerTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<LeaveTypeProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _appLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
    }

    [Fact]
    public async Task GetLeaveTypeListTest()
    {
        // 
        var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _appLogger.Object);

        // act
        var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

        // assert
        result.ShouldBeOfType<List<LeaveTypeDto>>();
        result.Count.ShouldBe(3);
    }
}
