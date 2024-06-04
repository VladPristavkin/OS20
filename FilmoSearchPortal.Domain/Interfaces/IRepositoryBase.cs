using System.Linq.Expressions;

namespace FilmoSearchPortal.Domain.Interfaces;

public interface IRepositoryBase<T> where T : class
{
    public void Create(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public IQueryable<T> FindAll(bool trackChanges);
    public IQueryable<T> FindAllByExpression(Expression<Func<T, bool>> expression, bool trackChanges);
}
