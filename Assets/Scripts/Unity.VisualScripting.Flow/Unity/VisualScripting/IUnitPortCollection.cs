namespace Unity.VisualScripting
{
	public interface IUnitPortCollection<TPort> : global::Unity.VisualScripting.IKeyedCollection<string, TPort>, global::System.Collections.Generic.ICollection<TPort>, global::System.Collections.Generic.IEnumerable<TPort>, global::System.Collections.IEnumerable where TPort : global::Unity.VisualScripting.IUnitPort
	{
		TPort Single();
	}
}
