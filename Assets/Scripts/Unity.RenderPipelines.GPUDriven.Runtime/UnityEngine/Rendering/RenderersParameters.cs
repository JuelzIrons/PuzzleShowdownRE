namespace UnityEngine.Rendering
{
	internal struct RenderersParameters
	{
		[global::System.Flags]
		public enum Flags
		{
			None = 0,
			UseBoundingSphereParameter = 1
		}

		public static class ParamNames
		{
			public static readonly int _BaseColor;

			public static readonly int unity_SpecCube0_HDR;

			public static readonly int unity_SHCoefficients;

			public static readonly int unity_LightmapST;

			public static readonly int unity_ObjectToWorld;

			public static readonly int unity_WorldToObject;

			public static readonly int unity_MatrixPreviousM;

			public static readonly int unity_MatrixPreviousMI;

			public static readonly int unity_WorldBoundingSphere;

			public static readonly int unity_RendererUserValuesPropertyEntry;

			public static readonly int[] DOTS_ST_WindParams;

			public static readonly int[] DOTS_ST_WindHistoryParams;

			static ParamNames()
			{
				_BaseColor = global::UnityEngine.Shader.PropertyToID("_BaseColor");
				unity_SpecCube0_HDR = global::UnityEngine.Shader.PropertyToID("unity_SpecCube0_HDR");
				unity_SHCoefficients = global::UnityEngine.Shader.PropertyToID("unity_SHCoefficients");
				unity_LightmapST = global::UnityEngine.Shader.PropertyToID("unity_LightmapST");
				unity_ObjectToWorld = global::UnityEngine.Shader.PropertyToID("unity_ObjectToWorld");
				unity_WorldToObject = global::UnityEngine.Shader.PropertyToID("unity_WorldToObject");
				unity_MatrixPreviousM = global::UnityEngine.Shader.PropertyToID("unity_MatrixPreviousM");
				unity_MatrixPreviousMI = global::UnityEngine.Shader.PropertyToID("unity_MatrixPreviousMI");
				unity_WorldBoundingSphere = global::UnityEngine.Shader.PropertyToID("unity_WorldBoundingSphere");
				unity_RendererUserValuesPropertyEntry = global::UnityEngine.Shader.PropertyToID("unity_RendererUserValuesPropertyEntry");
				DOTS_ST_WindParams = new int[16];
				DOTS_ST_WindHistoryParams = new int[16];
				for (int i = 0; i < 16; i++)
				{
					DOTS_ST_WindParams[i] = global::UnityEngine.Shader.PropertyToID($"DOTS_ST_WindParam{i}");
					DOTS_ST_WindHistoryParams[i] = global::UnityEngine.Shader.PropertyToID($"DOTS_ST_WindHistoryParam{i}");
				}
			}
		}

		public struct ParamInfo
		{
			public int index;

			public int gpuAddress;

			public int uintOffset;

			public bool valid => index != 0;
		}

		private static int s_uintSize = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<uint>();

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo lightmapScale;

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo localToWorld;

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo worldToLocal;

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo matrixPreviousM;

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo matrixPreviousMI;

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo shCoefficients;

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo rendererUserValues;

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo boundingSphere;

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo[] windParams;

		public global::UnityEngine.Rendering.RenderersParameters.ParamInfo[] windHistoryParams;

		public static global::UnityEngine.Rendering.GPUInstanceDataBuffer CreateInstanceDataBuffer(global::UnityEngine.Rendering.RenderersParameters.Flags flags, in global::UnityEngine.Rendering.InstanceNumInfo instanceNumInfo)
		{
			using global::UnityEngine.Rendering.GPUInstanceDataBufferBuilder gPUInstanceDataBufferBuilder = default(global::UnityEngine.Rendering.GPUInstanceDataBufferBuilder);
			gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Vector4>(global::UnityEngine.Rendering.RenderersParameters.ParamNames._BaseColor, isOverriden: false, isPerInstance: false, global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Vector4>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_SpecCube0_HDR, isOverriden: false, isPerInstance: false, global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Rendering.SHCoefficients>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_SHCoefficients, isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.MeshRenderer, global::UnityEngine.Rendering.InstanceComponentGroup.LightProbe);
			gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Vector4>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_LightmapST, isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.MeshRenderer, global::UnityEngine.Rendering.InstanceComponentGroup.Lightmap);
			gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Rendering.PackedMatrix>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_ObjectToWorld, isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Rendering.PackedMatrix>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_WorldToObject, isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Rendering.PackedMatrix>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_MatrixPreviousM, isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Rendering.PackedMatrix>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_MatrixPreviousMI, isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			gPUInstanceDataBufferBuilder.AddComponent<uint>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_RendererUserValuesPropertyEntry, isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			if ((flags & global::UnityEngine.Rendering.RenderersParameters.Flags.UseBoundingSphereParameter) != global::UnityEngine.Rendering.RenderersParameters.Flags.None)
			{
				gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Vector4>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_WorldBoundingSphere, isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.MeshRenderer);
			}
			for (int i = 0; i < 16; i++)
			{
				gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Vector4>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.DOTS_ST_WindParams[i], isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.SpeedTree, global::UnityEngine.Rendering.InstanceComponentGroup.Wind);
			}
			for (int j = 0; j < 16; j++)
			{
				gPUInstanceDataBufferBuilder.AddComponent<global::UnityEngine.Vector4>(global::UnityEngine.Rendering.RenderersParameters.ParamNames.DOTS_ST_WindHistoryParams[j], isOverriden: true, isPerInstance: true, global::UnityEngine.Rendering.InstanceType.SpeedTree, global::UnityEngine.Rendering.InstanceComponentGroup.Wind);
			}
			return gPUInstanceDataBufferBuilder.Build(in instanceNumInfo);
		}

		public RenderersParameters(in global::UnityEngine.Rendering.GPUInstanceDataBuffer instanceDataBuffer)
		{
			lightmapScale = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_LightmapST);
			localToWorld = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_ObjectToWorld);
			worldToLocal = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_WorldToObject);
			matrixPreviousM = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_MatrixPreviousM);
			matrixPreviousMI = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_MatrixPreviousMI);
			shCoefficients = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_SHCoefficients);
			rendererUserValues = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_RendererUserValuesPropertyEntry);
			boundingSphere = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.unity_WorldBoundingSphere, assertOnFail: false);
			windParams = new global::UnityEngine.Rendering.RenderersParameters.ParamInfo[16];
			windHistoryParams = new global::UnityEngine.Rendering.RenderersParameters.ParamInfo[16];
			for (int i = 0; i < 16; i++)
			{
				windParams[i] = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.DOTS_ST_WindParams[i]);
				windHistoryParams[i] = GetParamInfo(in instanceDataBuffer, global::UnityEngine.Rendering.RenderersParameters.ParamNames.DOTS_ST_WindHistoryParams[i]);
			}
			static global::UnityEngine.Rendering.RenderersParameters.ParamInfo GetParamInfo(in global::UnityEngine.Rendering.GPUInstanceDataBuffer reference, int paramNameIdx, bool assertOnFail = true)
			{
				int gpuAddress = reference.GetGpuAddress(paramNameIdx, assertOnFail);
				int propertyIndex = reference.GetPropertyIndex(paramNameIdx, assertOnFail);
				return new global::UnityEngine.Rendering.RenderersParameters.ParamInfo
				{
					index = propertyIndex,
					gpuAddress = gpuAddress,
					uintOffset = gpuAddress / s_uintSize
				};
			}
		}
	}
}
