namespace UnityEngine.Rendering
{
	public static class CameraCaptureBridge
	{
		private class CameraEntry
		{
			internal global::System.Collections.Generic.HashSet<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>> actions;

			internal global::System.Collections.Generic.IEnumerator<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>> cachedEnumerator;
		}

		private static global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, global::UnityEngine.Rendering.CameraCaptureBridge.CameraEntry> actionDict = new global::System.Collections.Generic.Dictionary<global::UnityEngine.Camera, global::UnityEngine.Rendering.CameraCaptureBridge.CameraEntry>();

		private static bool _enabled;

		public static bool enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				_enabled = value;
			}
		}

		public static global::System.Collections.Generic.IEnumerator<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>> GetCaptureActions(global::UnityEngine.Camera camera)
		{
			if (!actionDict.TryGetValue(camera, out var value) || value.actions.Count == 0)
			{
				return null;
			}
			return value.actions.GetEnumerator();
		}

		internal static global::System.Collections.Generic.IEnumerator<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>> GetCachedCaptureActionsEnumerator(global::UnityEngine.Camera camera)
		{
			if (!actionDict.TryGetValue(camera, out var value) || value.actions.Count == 0)
			{
				return null;
			}
			value.cachedEnumerator.Reset();
			return value.cachedEnumerator;
		}

		public static void AddCaptureAction(global::UnityEngine.Camera camera, global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer> action)
		{
			actionDict.TryGetValue(camera, out var value);
			if (value == null)
			{
				value = new global::UnityEngine.Rendering.CameraCaptureBridge.CameraEntry
				{
					actions = new global::System.Collections.Generic.HashSet<global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer>>()
				};
				actionDict.Add(camera, value);
			}
			value.actions.Add(action);
			value.cachedEnumerator = value.actions.GetEnumerator();
		}

		public static void RemoveCaptureAction(global::UnityEngine.Camera camera, global::System.Action<global::UnityEngine.Rendering.RenderTargetIdentifier, global::UnityEngine.Rendering.CommandBuffer> action)
		{
			if (!(camera == null) && actionDict.TryGetValue(camera, out var value))
			{
				value.actions.Remove(action);
				value.cachedEnumerator = value.actions.GetEnumerator();
			}
		}
	}
}
