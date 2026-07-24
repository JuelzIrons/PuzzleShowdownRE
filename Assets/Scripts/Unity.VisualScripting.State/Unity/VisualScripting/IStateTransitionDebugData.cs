namespace Unity.VisualScripting
{
	public interface IStateTransitionDebugData : global::Unity.VisualScripting.IGraphElementDebugData
	{
		int lastBranchFrame { get; }

		float lastBranchTime { get; }
	}
}
