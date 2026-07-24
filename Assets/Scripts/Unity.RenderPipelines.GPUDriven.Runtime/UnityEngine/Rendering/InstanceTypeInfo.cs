namespace UnityEngine.Rendering
{
	internal static class InstanceTypeInfo
	{
		public const int kInstanceTypeBitCount = 1;

		public const int kMaxInstanceTypesCount = 2;

		public const uint kInstanceTypeMask = 1u;

		private static global::UnityEngine.Rendering.InstanceType[] s_ParentTypes;

		private static global::System.Collections.Generic.List<global::UnityEngine.Rendering.InstanceType>[] s_ChildTypes;

		static InstanceTypeInfo()
		{
			InitParentTypes();
			InitChildTypes();
			ValidateTypeRelationsAreCorrectlySorted();
		}

		private static void InitParentTypes()
		{
			s_ParentTypes = new global::UnityEngine.Rendering.InstanceType[2];
			s_ParentTypes[0] = global::UnityEngine.Rendering.InstanceType.MeshRenderer;
			s_ParentTypes[1] = global::UnityEngine.Rendering.InstanceType.MeshRenderer;
		}

		private static void InitChildTypes()
		{
			s_ChildTypes = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.InstanceType>[2];
			for (int i = 0; i < 2; i++)
			{
				s_ChildTypes[i] = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.InstanceType>();
			}
			for (int j = 0; j < 2; j++)
			{
				global::UnityEngine.Rendering.InstanceType instanceType = (global::UnityEngine.Rendering.InstanceType)j;
				global::UnityEngine.Rendering.InstanceType instanceType2 = s_ParentTypes[(int)instanceType];
				if (instanceType != instanceType2)
				{
					s_ChildTypes[(int)instanceType2].Add(instanceType);
				}
			}
		}

		private static global::UnityEngine.Rendering.InstanceType GetMaxChildTypeRecursively(global::UnityEngine.Rendering.InstanceType type)
		{
			global::UnityEngine.Rendering.InstanceType instanceType = type;
			foreach (global::UnityEngine.Rendering.InstanceType item in s_ChildTypes[(int)type])
			{
				instanceType = (global::UnityEngine.Rendering.InstanceType)global::UnityEngine.Mathf.Max((int)instanceType, (int)GetMaxChildTypeRecursively(item));
			}
			return instanceType;
		}

		private static void FlattenChildInstanceTypes(global::UnityEngine.Rendering.InstanceType instanceType, global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceType> instanceTypes)
		{
			instanceTypes.Add(in instanceType);
			foreach (global::UnityEngine.Rendering.InstanceType item in s_ChildTypes[(int)instanceType])
			{
				FlattenChildInstanceTypes(item, instanceTypes);
			}
		}

		private static void ValidateTypeRelationsAreCorrectlySorted()
		{
			global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceType> instanceTypes = new global::Unity.Collections.NativeList<global::UnityEngine.Rendering.InstanceType>(2, global::Unity.Collections.Allocator.Temp);
			for (int i = 0; i < 2; i++)
			{
				global::UnityEngine.Rendering.InstanceType instanceType = (global::UnityEngine.Rendering.InstanceType)i;
				if (instanceType == s_ParentTypes[i])
				{
					FlattenChildInstanceTypes(instanceType, instanceTypes);
				}
			}
			for (int j = 0; j < instanceTypes.Length; j++)
			{
			}
		}

		public static global::UnityEngine.Rendering.InstanceType GetParentType(global::UnityEngine.Rendering.InstanceType type)
		{
			return s_ParentTypes[(int)type];
		}

		public static global::System.Collections.Generic.List<global::UnityEngine.Rendering.InstanceType> GetChildTypes(global::UnityEngine.Rendering.InstanceType type)
		{
			return s_ChildTypes[(int)type];
		}
	}
}
