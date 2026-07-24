namespace UnityEngine.Timeline
{
	internal class TimeFieldAttribute : global::UnityEngine.PropertyAttribute
	{
		public enum UseEditMode
		{
			None = 0,
			ApplyEditMode = 1
		}

		public global::UnityEngine.Timeline.TimeFieldAttribute.UseEditMode useEditMode { get; }

		public TimeFieldAttribute(global::UnityEngine.Timeline.TimeFieldAttribute.UseEditMode useEditMode = global::UnityEngine.Timeline.TimeFieldAttribute.UseEditMode.ApplyEditMode)
		{
			this.useEditMode = useEditMode;
		}
	}
}
