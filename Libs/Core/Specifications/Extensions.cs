using Core.Specifications.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Specifications
{
    public static class Extensions
    {
        public static void ApplySorting(this IRootSpecification gridSpec,
            string sort,
            string orderByMethodName,
            string orderByDescendingMethodName)
        {
            if (string.IsNullOrEmpty(sort))
                return;

            const string descendingSuffix = "Desc";

            var descending = sort.EndsWith(descendingSuffix, StringComparison.Ordinal);
            var propertyName = string.Concat(sort[..1].ToUpperInvariant(), sort.AsSpan(1, sort.Length - 1 - (descending ? descendingSuffix.Length : 0)));

            var specificationType = gridSpec.GetType().BaseType;
            var targetType = specificationType?.GenericTypeArguments[0];
            var property = targetType!.GetRuntimeProperty(propertyName) ??
                           throw new InvalidOperationException($"Because the property {propertyName} does not exist it cannot be sorted.");

            var lambdaParamX = Expression.Parameter(targetType, "x");

            var propertyReturningExpression = Expression.Lambda(
                Expression.Convert(
                    Expression.Property(lambdaParamX, property),
                    typeof(object)),
                lambdaParamX);

            if (descending)
            {
                specificationType?.GetMethod(
                        orderByDescendingMethodName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    ?.Invoke(gridSpec, new object[] { propertyReturningExpression });
            }
            else
            {
                specificationType?.GetMethod(
                        orderByMethodName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    ?.Invoke(gridSpec, new object[] { propertyReturningExpression });
            }
        }
    }
}
