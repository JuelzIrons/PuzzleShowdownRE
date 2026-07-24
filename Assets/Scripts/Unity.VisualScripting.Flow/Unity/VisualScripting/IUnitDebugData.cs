namespace Unity.VisualScripting
{
	public interface IUnitDebugData : global::Unity.VisualScripting.IGraphElementDebugData
	{
		int lastInvokeFrame { get; set; }

		float lastInvokeTime { get; set; }
	}
}
