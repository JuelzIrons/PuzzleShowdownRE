namespace UnityEngine.Rendering
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
	internal struct EditorInstanceDataArrays : global::UnityEngine.Rendering.IDataArrays
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal readonly struct ReadOnly
		{
			public ReadOnly(in global::UnityEngine.Rendering.CPUInstanceData instanceData)
			{
			}
		}

		public void Initialize(int initCapacity)
		{
		}

		public void Dispose()
		{
		}

		public void Grow(int newCapacity)
		{
		}

		public void Remove(int index, int lastIndex)
		{
		}

		public void SetDefault(int index)
		{
		}
	}
}
