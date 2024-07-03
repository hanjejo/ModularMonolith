namespace SharedKernel
{
    public interface IRepository<T, TId> where T : AggregateRoot<TId>
    {
        T GetById(TId id);
        void Add(T entity);
        void Remove(T entity);
        IEnumerable<T> Find(ISpecification<T> specification);

        // Pagination support
        PagedResult<T> FindWithPagination(ISpecification<T> specification, int pageIndex, int pageSize);
    }
}
