namespace Unity.VisualScripting
{
	public interface IStateDebugData : global::Unity.VisualScripting.IGraphElementDebugData
	{
		int lastEnterFrame { get; }

		float lastExitTime { get; }
	}
}
