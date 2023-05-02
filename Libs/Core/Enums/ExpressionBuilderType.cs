using System.Linq.Expressions;

namespace Core.Enums
{
    public enum ExpressionBuilderType
    {
        Equal = ExpressionType.Equal,
        GreaterThan = ExpressionType.GreaterThan,
        GreaterThanOrEqual = ExpressionType.GreaterThanOrEqual,
        LessThan = ExpressionType.LessThan,
        LessThanOrEqual = ExpressionType.LessThanOrEqual,
        NotEqual = ExpressionType.NotEqual,
        Contains,
        StartsWith,
        EndsWith,
        In
    }
}
