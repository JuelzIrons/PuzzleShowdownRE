namespace UnityEngine.Rendering
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	public struct ProfilingScope : global::System.IDisposable
	{
		public ProfilingScope(global::UnityEngine.Rendering.ProfilingSampler sampler)
		{
		}

		public ProfilingScope(global::UnityEngine.Rendering.CommandBuffer cmd, global::UnityEngine.Rendering.ProfilingSampler sampler)
		{
		}

		public ProfilingScope(global::UnityEngine.Rendering.BaseCommandBuffer cmd, global::UnityEngine.Rendering.ProfilingSampler sampler)
		{
		}

		public void Dispose()
		{
		}
	}
}
