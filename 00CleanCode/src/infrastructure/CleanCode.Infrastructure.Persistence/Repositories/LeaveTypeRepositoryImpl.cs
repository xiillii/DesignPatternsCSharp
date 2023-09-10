using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Domain;
using CleanCode.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infrastructure.Persistence.Repositories;

public class LeaveTypeRepositoryImpl : GenericRepositoryImpl<LeaveType>
    , ILeaveTypeRepository
{
    public LeaveTypeRepositoryImpl(DatabaseContextImpl context) : base(context)
    {
    }

    public async Task<bool> IsLeaveTypeUnique(string name) 
        => !await _context.LeaveTypes.AnyAsync(x => x.Name == name);
}
