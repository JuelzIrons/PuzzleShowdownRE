namespace UnityEngine.Rendering.Universal
{
	internal struct BuddyAllocation
	{
		public int level;

		public int index;

		public global::Unity.Mathematics.uint2 index2D => global::UnityEngine.Rendering.Universal.SpaceFillingCurves.DecodeMorton2D((uint)index);

		public BuddyAllocation(int level, int index)
		{
			this.level = level;
			this.index = index;
		}
	}
}
