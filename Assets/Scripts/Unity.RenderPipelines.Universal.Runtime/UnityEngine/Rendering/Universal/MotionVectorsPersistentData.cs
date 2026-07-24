namespace UnityEngine.Rendering.Universal
{
	internal sealed class MotionVectorsPersistentData
	{
		private const int k_MaxViewPerPass = 2;

		private global::UnityEngine.Matrix4x4[] m_stagingMatrixArray = new global::UnityEngine.Matrix4x4[2];

		private const int k_EyeCount = 4;

		private int m_numPreviousViews;

		private readonly global::UnityEngine.Matrix4x4[] m_Projection = new global::UnityEngine.Matrix4x4[4];

		private readonly global::UnityEngine.Matrix4x4[] m_View = new global::UnityEngine.Matrix4x4[4];

		private readonly global::UnityEngine.Matrix4x4[] m_ViewProjection = new global::UnityEngine.Matrix4x4[4];

		private readonly global::UnityEngine.Matrix4x4[] m_PreviousProjection = new global::UnityEngine.Matrix4x4[4];

		private readonly global::UnityEngine.Matrix4x4[] m_PreviousView = new global::UnityEngine.Matrix4x4[4];

		private readonly global::UnityEngine.Matrix4x4[] m_PreviousViewProjection = new global::UnityEngine.Matrix4x4[4];

		private readonly global::UnityEngine.Matrix4x4[] m_PreviousPreviousProjection = new global::UnityEngine.Matrix4x4[4];

		private readonly global::UnityEngine.Matrix4x4[] m_PreviousPreviousView = new global::UnityEngine.Matrix4x4[4];

		private readonly int[] m_LastFrameIndex = new int[4];

		private readonly float[] m_PrevAspectRatio = new float[4];

		private float m_deltaTime;

		private float m_lastDeltaTime;

		private global::UnityEngine.Vector3 m_worldSpaceCameraPos;

		private global::UnityEngine.Vector3 m_previousWorldSpaceCameraPos;

		private global::UnityEngine.Vector3 m_previousPreviousWorldSpaceCameraPos;

		internal int lastFrameIndex => m_LastFrameIndex[0];

		internal global::UnityEngine.Matrix4x4 viewProjection => m_ViewProjection[0];

		internal global::UnityEngine.Matrix4x4 previousViewProjection => m_PreviousViewProjection[0];

		internal global::UnityEngine.Matrix4x4[] viewProjectionStereo => m_ViewProjection;

		internal global::UnityEngine.Matrix4x4[] previousViewProjectionStereo => m_PreviousViewProjection;

		internal global::UnityEngine.Matrix4x4[] stagingMatrixStereo => m_stagingMatrixArray;

		internal global::UnityEngine.Matrix4x4[] projectionStereo => m_Projection;

		internal global::UnityEngine.Matrix4x4[] previousProjectionStereo => m_PreviousProjection;

		internal global::UnityEngine.Matrix4x4[] previousPreviousProjectionStereo => m_PreviousPreviousProjection;

		internal global::UnityEngine.Matrix4x4[] viewStereo => m_View;

		internal global::UnityEngine.Matrix4x4[] previousViewStereo => m_PreviousView;

		internal global::UnityEngine.Matrix4x4[] previousPreviousViewStereo => m_PreviousPreviousView;

		internal float deltaTime => m_deltaTime;

		internal float lastDeltaTime => m_lastDeltaTime;

		internal global::UnityEngine.Vector3 worldSpaceCameraPos => m_worldSpaceCameraPos;

		internal global::UnityEngine.Vector3 previousWorldSpaceCameraPos => m_previousWorldSpaceCameraPos;

		internal global::UnityEngine.Vector3 previousPreviousWorldSpaceCameraPos => m_previousPreviousWorldSpaceCameraPos;

		internal MotionVectorsPersistentData()
		{
			Reset();
		}

		public void Reset()
		{
			for (int i = 0; i < 4; i++)
			{
				m_Projection[i] = global::UnityEngine.Matrix4x4.identity;
				m_View[i] = global::UnityEngine.Matrix4x4.identity;
				m_ViewProjection[i] = global::UnityEngine.Matrix4x4.identity;
				m_PreviousProjection[i] = global::UnityEngine.Matrix4x4.identity;
				m_PreviousView[i] = global::UnityEngine.Matrix4x4.identity;
				m_PreviousViewProjection[i] = global::UnityEngine.Matrix4x4.identity;
				m_PreviousProjection[i] = global::UnityEngine.Matrix4x4.identity;
				m_PreviousView[i] = global::UnityEngine.Matrix4x4.identity;
				m_PreviousViewProjection[i] = global::UnityEngine.Matrix4x4.identity;
				m_LastFrameIndex[i] = -1;
				m_PrevAspectRatio[i] = -1f;
			}
			m_deltaTime = 0f;
			m_lastDeltaTime = 0f;
			m_worldSpaceCameraPos = global::UnityEngine.Vector3.zero;
			m_previousWorldSpaceCameraPos = global::UnityEngine.Vector3.zero;
			m_previousPreviousWorldSpaceCameraPos = global::UnityEngine.Vector3.zero;
		}

		private static int GetXRMultiPassId(global::UnityEngine.Experimental.Rendering.XRPass xr)
		{
			if (!xr.enabled)
			{
				return 0;
			}
			return xr.multipassId;
		}

		public void Update(global::UnityEngine.Rendering.Universal.UniversalCameraData cameraData)
		{
			int xRMultiPassId = GetXRMultiPassId(cameraData.xr);
			bool num = xRMultiPassId == 0;
			int frameCount = global::UnityEngine.Time.frameCount;
			if (num)
			{
				bool num2 = m_LastFrameIndex[0] == -1;
				float num3 = global::UnityEngine.Time.deltaTime;
				global::UnityEngine.Vector3 position = cameraData.camera.transform.position;
				if (num2)
				{
					m_lastDeltaTime = num3;
					m_deltaTime = num3;
					m_previousPreviousWorldSpaceCameraPos = position;
					m_previousWorldSpaceCameraPos = position;
					m_worldSpaceCameraPos = position;
				}
				m_lastDeltaTime = m_deltaTime;
				m_deltaTime = num3;
				m_previousPreviousWorldSpaceCameraPos = m_previousWorldSpaceCameraPos;
				m_previousWorldSpaceCameraPos = m_worldSpaceCameraPos;
				m_worldSpaceCameraPos = position;
				m_numPreviousViews = 0;
			}
			bool flag = m_PrevAspectRatio[xRMultiPassId] != cameraData.aspectRatio;
			if (!(m_LastFrameIndex[xRMultiPassId] != frameCount || flag))
			{
				return;
			}
			bool flag2 = m_LastFrameIndex[xRMultiPassId] == -1 || flag;
			int num4 = ((!cameraData.xr.enabled) ? 1 : cameraData.xr.viewCount);
			for (int i = 0; i < num4; i++)
			{
				int num5 = i + m_numPreviousViews;
				global::UnityEngine.Matrix4x4 gPUProjectionMatrix = global::UnityEngine.GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrixNoJitter(i), renderIntoTexture: true);
				global::UnityEngine.Matrix4x4 viewMatrix = cameraData.GetViewMatrix(i);
				global::UnityEngine.Matrix4x4 matrix4x = gPUProjectionMatrix * viewMatrix;
				if (flag2)
				{
					m_PreviousPreviousProjection[num5] = gPUProjectionMatrix;
					m_PreviousProjection[num5] = gPUProjectionMatrix;
					m_Projection[num5] = gPUProjectionMatrix;
					m_PreviousPreviousView[num5] = viewMatrix;
					m_PreviousView[num5] = viewMatrix;
					m_View[num5] = viewMatrix;
					m_ViewProjection[num5] = matrix4x;
					m_PreviousViewProjection[num5] = matrix4x;
				}
				m_PreviousPreviousProjection[num5] = m_PreviousProjection[num5];
				m_PreviousProjection[num5] = m_Projection[num5];
				m_Projection[num5] = gPUProjectionMatrix;
				m_PreviousPreviousView[num5] = m_PreviousView[num5];
				m_PreviousView[num5] = m_View[num5];
				m_View[num5] = viewMatrix;
				m_PreviousViewProjection[num5] = m_ViewProjection[num5];
				m_ViewProjection[num5] = matrix4x;
			}
			m_LastFrameIndex[xRMultiPassId] = frameCount;
			m_PrevAspectRatio[xRMultiPassId] = cameraData.aspectRatio;
			m_numPreviousViews += num4;
		}

		public void SetGlobalMotionMatrices(global::UnityEngine.Rendering.RasterCommandBuffer cmd, global::UnityEngine.Experimental.Rendering.XRPass xr)
		{
			int xRMultiPassId = GetXRMultiPassId(xr);
			if (xr.enabled && xr.singlePassEnabled)
			{
				int sourceIndex = xr.viewCount * xRMultiPassId;
				global::System.Array.Copy(previousViewProjectionStereo, sourceIndex, m_stagingMatrixArray, 0, xr.viewCount);
				cmd.SetGlobalMatrixArray(global::UnityEngine.Rendering.Universal.ShaderPropertyId.previousViewProjectionNoJitterStereo, m_stagingMatrixArray);
				global::System.Array.Copy(viewProjectionStereo, sourceIndex, m_stagingMatrixArray, 0, xr.viewCount);
				cmd.SetGlobalMatrixArray(global::UnityEngine.Rendering.Universal.ShaderPropertyId.viewProjectionNoJitterStereo, m_stagingMatrixArray);
			}
			else
			{
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.previousViewProjectionNoJitter, previousViewProjectionStereo[xRMultiPassId]);
				cmd.SetGlobalMatrix(global::UnityEngine.Rendering.Universal.ShaderPropertyId.viewProjectionNoJitter, viewProjectionStereo[xRMultiPassId]);
			}
		}
	}
}
