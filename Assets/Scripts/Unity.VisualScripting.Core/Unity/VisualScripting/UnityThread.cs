namespace Unity.VisualScripting
{
	public static class UnityThread
	{
		public static global::System.Threading.Thread thread = global::System.Threading.Thread.CurrentThread;

		public static global::System.Action<global::System.Action> editorAsync;

		public static global::System.Collections.Concurrent.ConcurrentQueue<global::System.Action> pendingQueue = new global::System.Collections.Concurrent.ConcurrentQueue<global::System.Action>();

		public static bool allowsAPI
		{
			get
			{
				if (!global::Unity.VisualScripting.Serialization.isUnitySerializing)
				{
					return global::System.Threading.Thread.CurrentThread == thread;
				}
				return false;
			}
		}

		internal static void RuntimeInitialize()
		{
			thread = global::System.Threading.Thread.CurrentThread;
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		public static void EditorAsync(global::System.Action action)
		{
			if (editorAsync == null)
			{
				pendingQueue.Enqueue(action);
			}
			else
			{
				editorAsync(action);
			}
		}
	}
}
