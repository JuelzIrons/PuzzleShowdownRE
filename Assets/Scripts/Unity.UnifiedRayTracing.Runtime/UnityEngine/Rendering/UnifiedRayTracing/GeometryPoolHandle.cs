namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal struct GeometryPoolHandle : global::System.IEquatable<global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle>
	{
		public int index;

		public static readonly global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle Invalid = new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle
		{
			index = -1
		};

		public readonly bool valid => index != -1;

		public bool Equals(global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolHandle other)
		{
			return index == other.index;
		}
	}
}
