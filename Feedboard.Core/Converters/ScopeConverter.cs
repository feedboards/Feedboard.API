namespace Feedboard.Core.Converters;

public class ScopeConverter<T>
	where T : class
{
	public IReadOnlyList<T> Scope { get; private set; }

	public ScopeConverter(IReadOnlyList<T> scope)
	{
		Scope = scope;
	}

	public string ConvertScopeToString()
	{
		if (Scope == null || !Scope.Any())
		{
			return string.Empty;
		}

		return string.Join(", ", Scope.Select(scope => scope.ToString()));
	}
}
