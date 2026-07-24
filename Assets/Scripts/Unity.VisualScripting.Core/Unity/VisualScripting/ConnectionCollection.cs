namespace Unity.VisualScripting
{
	public class ConnectionCollection<TConnection, TSource, TDestination> : global::Unity.VisualScripting.ConnectionCollectionBase<TConnection, TSource, TDestination, global::System.Collections.Generic.List<TConnection>> where TConnection : global::Unity.VisualScripting.IConnection<TSource, TDestination>
	{
		public ConnectionCollection()
			: base(new global::System.Collections.Generic.List<TConnection>())
		{
		}
	}
}
