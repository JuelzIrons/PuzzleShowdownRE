namespace UnityEngine.Rendering.RenderGraphModule
{
	public struct BufferDesc
	{
		public int count;

		public int stride;

		public string name;

		public global::UnityEngine.GraphicsBuffer.Target target;

		public global::UnityEngine.GraphicsBuffer.UsageFlags usageFlags;

		public BufferDesc(int count, int stride)
		{
			this = default(global::UnityEngine.Rendering.RenderGraphModule.BufferDesc);
			this.count = count;
			this.stride = stride;
			target = global::UnityEngine.GraphicsBuffer.Target.Structured;
			usageFlags = global::UnityEngine.GraphicsBuffer.UsageFlags.None;
		}

		public BufferDesc(int count, int stride, global::UnityEngine.GraphicsBuffer.Target target)
		{
			this = default(global::UnityEngine.Rendering.RenderGraphModule.BufferDesc);
			this.count = count;
			this.stride = stride;
			this.target = target;
			usageFlags = global::UnityEngine.GraphicsBuffer.UsageFlags.None;
		}

		public override int GetHashCode()
		{
			global::UnityEngine.Rendering.HashFNV1A32 hashFNV1A = global::UnityEngine.Rendering.HashFNV1A32.Create();
			hashFNV1A.Append(in count);
			hashFNV1A.Append(in stride);
			int input = (int)target;
			hashFNV1A.Append(in input);
			input = (int)usageFlags;
			hashFNV1A.Append(in input);
			return hashFNV1A.value;
		}
	}
}
