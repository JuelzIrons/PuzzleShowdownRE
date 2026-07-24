namespace UnityEngine.Rendering
{
	internal struct InstanceAllocators
	{
		private global::UnityEngine.Rendering.InstanceAllocator m_InstanceAlloc_MeshRenderer;

		private global::UnityEngine.Rendering.InstanceAllocator m_InstanceAlloc_SpeedTree;

		private global::UnityEngine.Rendering.InstanceAllocator m_SharedInstanceAlloc;

		public void Initialize()
		{
			m_InstanceAlloc_MeshRenderer = default(global::UnityEngine.Rendering.InstanceAllocator);
			m_InstanceAlloc_SpeedTree = default(global::UnityEngine.Rendering.InstanceAllocator);
			m_InstanceAlloc_MeshRenderer.Initialize(0, 2);
			m_InstanceAlloc_SpeedTree.Initialize(1, 2);
			m_SharedInstanceAlloc = default(global::UnityEngine.Rendering.InstanceAllocator);
			m_SharedInstanceAlloc.Initialize();
		}

		public void Dispose()
		{
			m_InstanceAlloc_MeshRenderer.Dispose();
			m_InstanceAlloc_SpeedTree.Dispose();
			m_SharedInstanceAlloc.Dispose();
		}

		private global::UnityEngine.Rendering.InstanceAllocator GetInstanceAllocator(global::UnityEngine.Rendering.InstanceType type)
		{
			return type switch
			{
				global::UnityEngine.Rendering.InstanceType.MeshRenderer => m_InstanceAlloc_MeshRenderer, 
				global::UnityEngine.Rendering.InstanceType.SpeedTree => m_InstanceAlloc_SpeedTree, 
				_ => throw new global::System.ArgumentException("Allocator for this type is not created."), 
			};
		}

		public int GetInstanceHandlesLength(global::UnityEngine.Rendering.InstanceType type)
		{
			return GetInstanceAllocator(type).length;
		}

		public int GetInstancesLength(global::UnityEngine.Rendering.InstanceType type)
		{
			return GetInstanceAllocator(type).GetNumAllocated();
		}

		public global::UnityEngine.Rendering.InstanceHandle AllocateInstance(global::UnityEngine.Rendering.InstanceType type)
		{
			return global::UnityEngine.Rendering.InstanceHandle.FromInt(GetInstanceAllocator(type).AllocateInstance());
		}

		public void FreeInstance(global::UnityEngine.Rendering.InstanceHandle instance)
		{
			GetInstanceAllocator(instance.type).FreeInstance(instance.index);
		}

		public global::UnityEngine.Rendering.SharedInstanceHandle AllocateSharedInstance()
		{
			return new global::UnityEngine.Rendering.SharedInstanceHandle
			{
				index = m_SharedInstanceAlloc.AllocateInstance()
			};
		}

		public void FreeSharedInstance(global::UnityEngine.Rendering.SharedInstanceHandle instance)
		{
			m_SharedInstanceAlloc.FreeInstance(instance.index);
		}
	}
}
