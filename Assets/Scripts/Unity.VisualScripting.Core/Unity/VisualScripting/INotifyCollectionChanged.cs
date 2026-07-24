namespace Unity.VisualScripting
{
	public interface INotifyCollectionChanged<T>
	{
		event global::System.Action<T> ItemAdded;

		event global::System.Action<T> ItemRemoved;

		event global::System.Action CollectionChanged;
	}
}
