namespace UnityEngine.Timeline
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class, AllowMultiple = true)]
	public class TrackClipTypeAttribute : global::System.Attribute
	{
		public readonly global::System.Type inspectedType;

		public readonly bool allowAutoCreate;

		public TrackClipTypeAttribute(global::System.Type clipClass)
		{
			inspectedType = clipClass;
			allowAutoCreate = true;
		}

		public TrackClipTypeAttribute(global::System.Type clipClass, bool allowAutoCreate)
		{
			inspectedType = clipClass;
			allowAutoCreate = false;
		}
	}
}
