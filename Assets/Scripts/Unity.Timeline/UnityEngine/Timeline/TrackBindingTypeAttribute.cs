namespace UnityEngine.Timeline
{
	[global::System.AttributeUsage(global::System.AttributeTargets.Class)]
	public class TrackBindingTypeAttribute : global::System.Attribute
	{
		public readonly global::System.Type type;

		public readonly global::UnityEngine.Timeline.TrackBindingFlags flags;

		public TrackBindingTypeAttribute(global::System.Type type)
		{
			this.type = type;
			flags = global::UnityEngine.Timeline.TrackBindingFlags.AllowCreateComponent;
		}

		public TrackBindingTypeAttribute(global::System.Type type, global::UnityEngine.Timeline.TrackBindingFlags flags)
		{
			this.type = type;
			this.flags = flags;
		}
	}
}
