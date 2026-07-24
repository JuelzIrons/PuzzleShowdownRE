namespace Unity.VisualScripting
{
	public interface IUnitControlPort : global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IGraphItem
	{
		bool isPredictable { get; }

		bool couldBeEntered { get; }
	}
}
