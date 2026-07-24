namespace Unity.VisualScripting
{
	public interface IGraphElementCollection<T> : global::Unity.VisualScripting.IKeyedCollection<global::System.Guid, T>, global::System.Collections.Generic.ICollection<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable, global::Unity.VisualScripting.INotifyCollectionChanged<T> where T : global::Unity.VisualScripting.IGraphElement
	{
	}
}
