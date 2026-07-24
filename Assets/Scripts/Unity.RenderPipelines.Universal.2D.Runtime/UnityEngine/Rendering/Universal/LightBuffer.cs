namespace UnityEngine.Rendering.Universal
{
	internal class LightBuffer
	{
		internal static readonly int kMax = 16384;

		internal static readonly int kCount = 1;

		internal static readonly int kLightMod = 64;

		internal static readonly int kBatchMax = 256;

		private global::UnityEngine.GraphicsBuffer m_GraphicsBuffer;

		private global::Unity.Collections.NativeArray<int> m_Markers = new global::Unity.Collections.NativeArray<int>(kBatchMax, global::Unity.Collections.Allocator.Persistent);

		private global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.PerLight2D> m_NativeBuffer = new global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.PerLight2D>(kMax, global::Unity.Collections.Allocator.Persistent);

		internal global::UnityEngine.GraphicsBuffer graphicsBuffer
		{
			get
			{
				if (m_GraphicsBuffer == null)
				{
					m_GraphicsBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, kMax, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Rendering.Universal.PerLight2D>());
				}
				return m_GraphicsBuffer;
			}
		}

		internal global::Unity.Collections.NativeArray<int> lightMarkers => m_Markers;

		internal global::Unity.Collections.NativeArray<global::UnityEngine.Rendering.Universal.PerLight2D> nativeBuffer => m_NativeBuffer;

		internal void Release()
		{
			m_GraphicsBuffer.Release();
			m_GraphicsBuffer = null;
		}

		internal unsafe void Reset()
		{
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_Markers), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<int>() * kBatchMax);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(global::Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.GetUnsafePtr(m_NativeBuffer), global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<global::UnityEngine.Rendering.Universal.PerLight2D>() * kMax);
		}
	}
}
