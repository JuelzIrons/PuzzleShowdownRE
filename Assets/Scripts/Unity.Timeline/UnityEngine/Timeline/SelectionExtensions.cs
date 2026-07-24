namespace UnityEngine.Timeline
{
	internal static class SelectionExtensions
	{
		public static global::UnityEngine.Timeline.ObjectId GetObjectId(this global::UnityEngine.Object obj)
		{
			return obj.GetEntityId();
		}
	}
}
