using System.Linq.Expressions;

namespace SharedKernel
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        ISpecification<T> And(ISpecification<T> specification);
        ISpecification<T> Or(ISpecification<T> specification);
        ISpecification<T> Not();
    }

    public class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }

        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new Specification<T>(Criteria.And(specification.Criteria));
        }

        public ISpecification<T> Or(ISpecification<T> specification)
        {
            return new Specification<T>(Criteria.Or(specification.Criteria));
        }

        public ISpecification<T> Not()
        {
            return new Specification<T>(Criteria.Not());
        }
    }

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(left, parameter),
                Expression.Invoke(right, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.OrElse(
                Expression.Invoke(left, parameter),
                Expression.Invoke(right, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var parameter = Expression.Parameter(typeof(T));

            var body = Expression.Not(
                Expression.Invoke(expression, parameter)
            );

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
