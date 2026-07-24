namespace UnityEngine.Rendering.Universal
{
	internal sealed class StpHistory : global::UnityEngine.Rendering.CameraHistoryItem
	{
		private global::UnityEngine.Rendering.STP.HistoryContext[] m_historyContexts = new global::UnityEngine.Rendering.STP.HistoryContext[2];

		public override void OnCreate(global::UnityEngine.Rendering.BufferedRTHandleSystem owner, uint typeId)
		{
			base.OnCreate(owner, typeId);
			for (int i = 0; i < 2; i++)
			{
				m_historyContexts[i] = new global::UnityEngine.Rendering.STP.HistoryContext();
			}
		}

		public override void Reset()
		{
			for (int i = 0; i < 2; i++)
			{
				m_historyContexts[i].Dispose();
			}
		}

		internal global::UnityEngine.Rendering.STP.HistoryContext GetHistoryContext(int eyeIndex)
		{
			return m_historyContexts[eyeIndex];
		}

		internal bool Update(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			global::UnityEngine.Rendering.STP.HistoryUpdateInfo info = default(global::UnityEngine.Rendering.STP.HistoryUpdateInfo);
			info.preUpscaleSize = new global::UnityEngine.Vector2Int(cameraData.cameraTargetDescriptor.width, cameraData.cameraTargetDescriptor.height);
			info.postUpscaleSize = new global::UnityEngine.Vector2Int(cameraData.pixelWidth, cameraData.pixelHeight);
			info.useHwDrs = false;
			info.useTexArray = cameraData.xr.enabled && cameraData.xr.singlePassEnabled;
			int eyeIndex = ((cameraData.xr.enabled && !cameraData.xr.singlePassEnabled) ? cameraData.xr.multipassId : 0);
			return !GetHistoryContext(eyeIndex).Update(ref info);
		}
	}
}
