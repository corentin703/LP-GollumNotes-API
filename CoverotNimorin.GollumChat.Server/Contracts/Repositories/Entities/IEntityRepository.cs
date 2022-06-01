using CoverotNimorin.GollumChat.Server.Entities;

namespace CoverotNimorin.GollumChat.Server.Contracts.Repositories.Entities;

public interface IEntityRepository<TEntity> where TEntity : BaseEntity
{
    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(string id);

    public List<TEntity> GetAll();
    public Task<List<TEntity>> GetAllAsync();

    public TEntity? GetById(string id);
    public Task<TEntity?> GetByIdAsync(string id);

    public Task SaveChangesAsync();
}