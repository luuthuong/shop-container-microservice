using System.Linq.Expressions;
using Core.Specifications.Interfaces;

namespace Core.Specifications;

public abstract class SpecificationBase<T>: ISpecification<T>
{
    private Func<T, bool> _compiledExpression;
    public abstract Expression<Func<T, bool>> Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public Expression<Func<T, object>> OrderBy { get; private set; }
    public Expression<Func<T, object>> OrderByDescending { get; private set; }
    public Expression<Func<T, object>> GroupBy { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsEnablePaging { get; private set; }

    protected void ApplyIncludeList(IEnumerable<Expression<Func<T, object>>> includes)
    {
        foreach (var expression in includes)
        {
            AddInclude(expression);
        }
    }
    
    protected void ApplyIncludeList(IEnumerable<string> includes)
    {
        foreach (var include in includes)
        {
            AddInclude(include);
        }   
    }
    
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsEnablePaging = true;
    }

    public bool IsSatisfiedBy(T obj) => CompiledExpression(obj);
    
    private void AddInclude(Expression<Func<T, object>> expression)
    {
        Includes.Add(expression);
    }

    private void AddInclude(string include)
    {
        IncludeStrings.Add(include);
    }

    private Func<T, bool> CompiledExpression => _compiledExpression ??= Criteria.Compile();
}