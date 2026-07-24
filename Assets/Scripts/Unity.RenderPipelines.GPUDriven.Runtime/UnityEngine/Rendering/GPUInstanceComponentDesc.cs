namespace UnityEngine.Rendering
{
	internal struct GPUInstanceComponentDesc
	{
		public int propertyID;

		public int byteSize;

		public bool isOverriden;

		public bool isPerInstance;

		public global::UnityEngine.Rendering.InstanceType instanceType;

		public global::UnityEngine.Rendering.InstanceComponentGroup componentGroup;

		public GPUInstanceComponentDesc(int inPropertyID, int inByteSize, bool inIsOverriden, bool inPerInstance, global::UnityEngine.Rendering.InstanceType inInstanceType, global::UnityEngine.Rendering.InstanceComponentGroup inComponentType)
		{
			propertyID = inPropertyID;
			byteSize = inByteSize;
			isOverriden = inIsOverriden;
			isPerInstance = inPerInstance;
			instanceType = inInstanceType;
			componentGroup = inComponentType;
		}
	}
}
