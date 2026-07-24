namespace Unity.VisualScripting
{
	public interface IConnectionCollection<TConnection, TSource, TDestination> : global::System.Collections.Generic.ICollection<TConnection>, global::System.Collections.Generic.IEnumerable<TConnection>, global::System.Collections.IEnumerable where TConnection : global::Unity.VisualScripting.IConnection<TSource, TDestination>
	{
		global::System.Collections.Generic.IEnumerable<TConnection> this[TSource source] { get; }

		global::System.Collections.Generic.IEnumerable<TConnection> this[TDestination destination] { get; }

		global::System.Collections.Generic.IEnumerable<TConnection> WithSource(TSource source);

		global::System.Collections.Generic.IEnumerable<TConnection> WithDestination(TDestination destination);
	}
}
