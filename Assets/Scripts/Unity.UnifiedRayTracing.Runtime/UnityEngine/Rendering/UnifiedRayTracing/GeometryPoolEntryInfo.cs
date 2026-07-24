namespace UnityEngine.Rendering.UnifiedRayTracing
{
	internal struct GeometryPoolEntryInfo
	{
		public bool valid;

		public uint refCount;

		public static global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolEntryInfo NewDefault()
		{
			return new global::UnityEngine.Rendering.UnifiedRayTracing.GeometryPoolEntryInfo
			{
				valid = false,
				refCount = 0u
			};
		}
	}
}
