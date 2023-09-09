using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Domain;
using CleanCode.Infrastructure.Persistence.DatabaseContext;

namespace CleanCode.Infrastructure.Persistence.Repositories;

public class LeaveRequestRepositoryImpl : GenericRepositoryImpl<LeaveRequest>
    , ILeaveRequestRepository
{
    public LeaveRequestRepositoryImpl(DatabaseContextImpl context) : base(context)
    {
    }
}
