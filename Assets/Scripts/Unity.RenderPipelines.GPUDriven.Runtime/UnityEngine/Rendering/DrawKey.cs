namespace UnityEngine.Rendering
{
	internal struct DrawKey : global::System.IEquatable<global::UnityEngine.Rendering.DrawKey>
	{
		public global::UnityEngine.Rendering.BatchMeshID meshID;

		public int submeshIndex;

		public int activeMeshLod;

		public global::UnityEngine.Rendering.BatchMaterialID materialID;

		public global::UnityEngine.Rendering.BatchDrawCommandFlags flags;

		public int transparentInstanceId;

		public uint overridenComponents;

		public global::UnityEngine.Rendering.RangeKey range;

		public int lightmapIndex;

		public bool Equals(global::UnityEngine.Rendering.DrawKey other)
		{
			if (meshID == other.meshID && submeshIndex == other.submeshIndex && activeMeshLod == other.activeMeshLod && materialID == other.materialID && flags == other.flags && transparentInstanceId == other.transparentInstanceId && overridenComponents == other.overridenComponents && range.Equals(other.range))
			{
				return lightmapIndex == other.lightmapIndex;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return ((((int)(((((int)((13 * 23 + meshID.value) * 23) + submeshIndex) * 23 + activeMeshLod) * 23 + (int)materialID.value) * 23 + flags) * 23 + transparentInstanceId) * 23 + range.GetHashCode()) * 23 + (int)overridenComponents) * 23 + lightmapIndex;
		}
	}
}
