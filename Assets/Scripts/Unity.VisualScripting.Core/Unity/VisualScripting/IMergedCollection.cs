namespace Unity.VisualScripting
{
	public interface IMergedCollection<T> : global::System.Collections.Generic.ICollection<T>, global::System.Collections.Generic.IEnumerable<T>, global::System.Collections.IEnumerable
	{
		bool Includes<TI>() where TI : T;

		bool Includes(global::System.Type elementType);
	}
}
