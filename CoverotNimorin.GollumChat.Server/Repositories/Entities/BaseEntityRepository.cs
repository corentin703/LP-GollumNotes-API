using CoverotNimorin.GollumChat.Server.Contexts;
using CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumChat.Server.Entities;
using CoverotNimorin.GollumChat.Server.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumChat.Server.Repositories.Entities;

public abstract class BaseEntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly GollumNotesContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    public BaseEntityRepository(GollumNotesContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        TEntity? existingEntity = GetById(entity.Id);

        if (existingEntity == null)
            throw new EntityNotFoundException(entity.Id);

        DbSet.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(string id)
    {
        TEntity? existingEntity = GetById(id);

        if (existingEntity == null)
            throw new EntityNotFoundException(id);

        if (DbContext.Entry(existingEntity).State == EntityState.Detached)
            DbSet.Attach(existingEntity);

        DbSet.Remove(existingEntity);
    }

    public virtual List<TEntity> GetAll()
    {
        return DbSet.ToList();
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual TEntity? GetById(string id)
    {
        return DbSet.FirstOrDefault(entity => entity.Id == id);
    }
    
    public virtual async Task<TEntity?> GetByIdAsync(string id)
    {
        return await DbSet.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}