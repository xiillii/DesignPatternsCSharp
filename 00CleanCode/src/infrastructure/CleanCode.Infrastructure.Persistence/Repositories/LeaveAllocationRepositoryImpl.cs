using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Domain;
using CleanCode.Infrastructure.Persistence.DatabaseContext;

namespace CleanCode.Infrastructure.Persistence.Repositories;

public class LeaveAllocationRepositoryImpl : GenericRepositoryImpl<LeaveAllocation>
    , ILeaveAllocationRepository
{
    public LeaveAllocationRepositoryImpl(DatabaseContextImpl context) : base(context)
    {
    }    
}
