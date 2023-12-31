﻿using CleanCode.Core.Application.Contracts.Persistence;
using CleanCode.Core.Domain.Common;
using CleanCode.Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanCode.Infrastructure.Persistence.Repositories;

public class GenericRepositoryImpl<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly DatabaseContextImpl _context;

    public GenericRepositoryImpl(DatabaseContextImpl context)
    {
        _context = context;
    }



    public async Task CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync() 
        => await _context.Set<T>().AsNoTracking().ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
        => await _context.Set<T>().AsNoTracking()
                        .FirstOrDefaultAsync(q => q.Id == id);

    public async Task UpdateAsync(T entity)
    {
        
        _context.Entry(entity).State = EntityState.Modified; ;
        await _context.SaveChangesAsync();
    }

}
