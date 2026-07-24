namespace UnityEngine.Timeline
{
	internal static class TimelineUndo
	{
		internal static bool undoEnabled => false;

		public static void PushDestroyUndo(global::UnityEngine.Timeline.TimelineAsset timeline, global::UnityEngine.Object thingToDirty, global::UnityEngine.Object objectToDestroy)
		{
			if (objectToDestroy != null)
			{
				global::UnityEngine.Object.Destroy(objectToDestroy);
			}
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void PushUndo(global::UnityEngine.Object[] thingsToDirty, string operation)
		{
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void PushUndo(global::UnityEngine.Object thingToDirty, string operation)
		{
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void RegisterCreatedObjectUndo(global::UnityEngine.Object thingCreated, string operation)
		{
		}

		internal static string UndoName(string name)
		{
			return "Timeline " + name;
		}
	}
}
