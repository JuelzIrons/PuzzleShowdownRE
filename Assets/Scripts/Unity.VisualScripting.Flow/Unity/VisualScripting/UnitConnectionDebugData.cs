namespace Unity.VisualScripting
{
	public class UnitConnectionDebugData : global::Unity.VisualScripting.IUnitConnectionDebugData, global::Unity.VisualScripting.IGraphElementDebugData
	{
		public int lastInvokeFrame { get; set; }

		public float lastInvokeTime { get; set; }

		public global::System.Exception runtimeException { get; set; }
	}
}
