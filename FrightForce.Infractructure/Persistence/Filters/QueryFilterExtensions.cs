using System.Linq.Expressions;
using System.Reflection;
using FrightForce.Domain.Base;
using FrightForce.Domain.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;

namespace FrightForce.Infractructure.Persistence.Filters;

public static class QueryFilterExtensions
{
    public static void FilterSoftDeletedRecords(this IMutableEntityType entityData, ICompanyScoped<int> companyScoped)
    {
        if (typeof(ISoftDeletable).IsAssignableFrom(entityData.ClrType))
        {
            Expression<Func<ISoftDeletable, bool>> filterExpr = e => !e.IsDeleted;

            var parameter = Expression.Parameter(entityData.ClrType);
            var body = ReplacingExpressionVisitor
                .Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
            var lambdaExpression = Expression.Lambda(body, parameter);

            entityData.SetQueryFilter(lambdaExpression);
        }
    }

    public static void AddQueryFilters(this IMutableEntityType entityData, ICompanyScoped<int> companyScoped)
    {
        var methodToCall = typeof(QueryFilterExtensions)
            .GetMethod(nameof(GetFilterExpression), BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entityData.ClrType);
        var filter = methodToCall.Invoke(null, new object[] { companyScoped });
        entityData.SetQueryFilter((LambdaExpression)filter);
        entityData.AddIndex(entityData.FindProperty(nameof(ISoftDeletable.IsDeleted)));
    }

    private static LambdaExpression GetFilterExpression<TEntity>(ICompanyScoped<int> scope)
        where TEntity : class, ISoftDeletable, ICompanyScoped<int>
    {
        Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted && x.CompanyId == scope.CompanyId;
        return filter;
    }
}