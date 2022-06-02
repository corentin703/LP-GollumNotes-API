using CoverotNimorin.GollumNotes.Server.Contexts;
using CoverotNimorin.GollumNotes.Server.Contracts.Repositories.Entities;
using CoverotNimorin.GollumNotes.Server.Entities;
using CoverotNimorin.GollumNotes.Server.Exceptions.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoverotNimorin.GollumNotes.Server.Repositories.Entities;

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

    public virtual TEntity GetById(string id)
    {
        TEntity? entity = DbSet.FirstOrDefault(entity => entity.Id == id);

        if (entity == null)
            throw new EntityNotFoundException(id);

        return entity;
    }
    
    public virtual async Task<TEntity> GetByIdAsync(string id)
    {
        TEntity? entity = await DbSet.FirstOrDefaultAsync(entity => entity.Id == id);

        if (entity == null)
            throw new EntityNotFoundException(id);

        return entity;
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }
}