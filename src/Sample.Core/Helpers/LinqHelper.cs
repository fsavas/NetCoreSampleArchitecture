using System;
using System.Linq;
using System.Linq.Expressions;

namespace Sample.Core.Helpers
{
    public static class LinqHelper<T>
    {
        public static IQueryable<T> OrderBy(IQueryable<T> source, string orderMember, bool orderByAsc)
        {
            var sortExpression = GetExpression<T, object>(orderMember); //GetExpression(orderMember);

            switch (orderByAsc)
            {
                case true:
                    return source.OrderBy<T, object>(sortExpression);

                default:
                    return source.OrderByDescending<T, object>(sortExpression);
            }
        }

        public static Expression<Func<T, object>> GetExpression(string name)
        {
            var param = Expression.Parameter(typeof(T), name);

            return Expression.Lambda<Func<T, object>>
                (Expression.Convert(Expression.Property(param, name), typeof(object)), param);
        }

        public static Expression<Func<TEntity, TResult>> GetExpression<TEntity, TResult>(string prop)
        {
            var param = Expression.Parameter(typeof(TEntity), prop);
            var parts = prop.Split('_');

            Expression parent = parts.Aggregate<string, Expression>(param, Expression.Property);
            Expression conversion = Expression.Convert(parent, typeof(object));

            //var tryExpression = Expression.TryCatch(Expression.Block(typeof(object), conversion),
            //                                        Expression.Catch(typeof(object), Expression.Constant(null)));

            return Expression.Lambda<Func<TEntity, TResult>>(conversion, param);
        }
    }
}