using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ChatAppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ChatAppDbContext context, DbSet<T> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(PaginationModel paginationModel)
    {
        if (paginationModel != null)
        {
            return await _dbSet
                .Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize)
                .Take(paginationModel.PageSize)
                .ToListAsync();
        }
        return await _dbSet.Take(10).ToListAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
