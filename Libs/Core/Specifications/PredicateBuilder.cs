using Core.Enums;
using System.Linq.Expressions;

namespace Core.Specifications;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> Build<T>(string propertyName, ExpressionBuilderType comparisonType, string value)
    {
        const string parameterName = "x";
        var parameter = Expression.Parameter(typeof(T), parameterName);
        var left = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
        var body = MakeComparison(left, comparisonType, value);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private static Expression MakeComparison(Expression left, ExpressionBuilderType comparison, string value)
    {
        return comparison switch
        {
            ExpressionBuilderType.Equal => MakeBinary(ExpressionType.Equal, left, value),
            ExpressionBuilderType.NotEqual => MakeBinary(ExpressionType.NotEqual, left, value),
            ExpressionBuilderType.GreaterThan => MakeBinary(ExpressionType.GreaterThan, left, value),
            ExpressionBuilderType.GreaterThanOrEqual => MakeBinary(ExpressionType.GreaterThanOrEqual, left, value),
            ExpressionBuilderType.LessThan => MakeBinary(ExpressionType.LessThan, left, value),
            ExpressionBuilderType.LessThanOrEqual => MakeBinary(ExpressionType.LessThanOrEqual, left, value),
            ExpressionBuilderType.Contains or ExpressionBuilderType.StartsWith or ExpressionBuilderType.EndsWith => Expression.Call(MakeString(left), comparison.ToString(), Type.EmptyTypes, Expression.Constant(value, typeof(string))),
            ExpressionBuilderType.In => MakeList(left, value.Split(',')),
            _ => throw new NotSupportedException($"InValid comparison operator {comparison}")
        };
     }

    private static Expression MakeBinary(ExpressionType type, Expression left, string value)
    {
        object typeValue = value;
        if(left.Type != typeof(string))
        {
            if (String.IsNullOrEmpty(value))
            {
                typeValue = null;
                if (Nullable.GetUnderlyingType(left.Type) == null)
                    left = Expression.Convert(left, typeof(Nullable<>).MakeGenericType(left.Type));
            }
            else
            {
                var valueType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
                typeValue = valueType.IsEnum ? Enum.Parse(valueType, value) : valueType == typeof(Guid) ? Guid.Parse(value) : Convert.ChangeType(type, valueType);
            }
        }
        var right = Expression.Constant(typeValue);
        return Expression.MakeBinary(type, left, right);
    }

    private static Expression MakeString(Expression source)
    {
        return source.Type == typeof(string) ? source : Expression.Call(source, "ToString", Type.EmptyTypes);
    }

    private static Expression MakeList(Expression left, IEnumerable<string> codes)
    {
        var objValues = codes.Cast<object>().ToList();
        var type = typeof(List<object>);
        var methodInfo = type.GetMethod("Contains", new[] {typeof(object)});  
        var list = Expression.Constant(objValues);
        var body = Expression.Call(list, methodInfo, left);
        return body;
    }
}