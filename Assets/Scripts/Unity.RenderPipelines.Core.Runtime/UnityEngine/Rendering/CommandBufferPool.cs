namespace UnityEngine.Rendering
{
	public static class CommandBufferPool
	{
		private static global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.CommandBuffer> s_BufferPool = new global::UnityEngine.Rendering.ObjectPool<global::UnityEngine.Rendering.CommandBuffer>(null, delegate(global::UnityEngine.Rendering.CommandBuffer x)
		{
			x.Clear();
		});

		public static global::UnityEngine.Rendering.CommandBuffer Get()
		{
			global::UnityEngine.Rendering.CommandBuffer commandBuffer = s_BufferPool.Get();
			commandBuffer.name = "";
			return commandBuffer;
		}

		public static global::UnityEngine.Rendering.CommandBuffer Get(string name)
		{
			global::UnityEngine.Rendering.CommandBuffer commandBuffer = s_BufferPool.Get();
			commandBuffer.name = name;
			return commandBuffer;
		}

		public static void Release(global::UnityEngine.Rendering.CommandBuffer buffer)
		{
			s_BufferPool.Release(buffer);
		}
	}
}
