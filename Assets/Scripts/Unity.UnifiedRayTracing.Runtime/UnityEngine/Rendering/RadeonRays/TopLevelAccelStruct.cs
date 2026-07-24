namespace UnityEngine.Rendering.RadeonRays
{
	internal struct TopLevelAccelStruct : global::System.IDisposable
	{
		public const global::UnityEngine.GraphicsBuffer.Target topLevelBvhTarget = global::UnityEngine.GraphicsBuffer.Target.Structured;

		public const global::UnityEngine.GraphicsBuffer.Target instanceInfoTarget = global::UnityEngine.GraphicsBuffer.Target.Structured;

		public global::UnityEngine.GraphicsBuffer topLevelBvh;

		public global::UnityEngine.GraphicsBuffer bottomLevelBvhs;

		public global::UnityEngine.GraphicsBuffer instanceInfos;

		public uint instanceCount;

		public void Dispose()
		{
			topLevelBvh?.Dispose();
			instanceInfos?.Dispose();
		}
	}
}
