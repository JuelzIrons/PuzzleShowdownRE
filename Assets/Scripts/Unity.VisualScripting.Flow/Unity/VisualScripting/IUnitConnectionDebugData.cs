namespace Unity.VisualScripting
{
	public interface IUnitConnectionDebugData : global::Unity.VisualScripting.IGraphElementDebugData
	{
		int lastInvokeFrame { get; set; }

		float lastInvokeTime { get; set; }
	}
}
