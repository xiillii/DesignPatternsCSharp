using CleanCode.Core.Domain;
using CleanCode.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace CleanCode.Persistence.IntegrationTests;

public class DatabaseContextTests
{
    private readonly DatabaseContextImpl _databaseContext;

    public DatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<DatabaseContextImpl>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _databaseContext = new DatabaseContextImpl(dbOptions);
    }

    [Fact]
    public async Task Save_SetDateCreatedValue()
    {
        // arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation",
        };

        // act
        await _databaseContext.LeaveTypes.AddAsync(leaveType);
        await _databaseContext.SaveChangesAsync();

        // assert
        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task Save_SetDateModifiedValue()
    {
        // arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Vacation",
        };

        // act
        await _databaseContext.LeaveTypes.AddAsync(leaveType);
        await _databaseContext.SaveChangesAsync();

        // assert
        leaveType.DateModified.ShouldNotBeNull();
    }
}
