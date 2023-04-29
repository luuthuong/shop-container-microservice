using System.Linq.Expressions;

namespace Core.Specifications;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> Build<T>(string propertyName, string comparison, string value)
    {
        const string parameterName = "x";
        var parameter = Expression.Parameter(typeof(T), parameterName);
        // return Expression.Lambda<Func<T,bool>>();
        return default;
    }

    // private static Expression MakeComparison(Expression left, string comparison, string value)
    // {
    //     return comparison switch
    //     {
    //         "==" => 
    //     }
    // }
    //
    // private static Expression MakeBinary(ExpressionType type, Expression left, string value)
}