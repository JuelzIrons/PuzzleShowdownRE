namespace UnityEngine.Rendering.RenderGraphModule
{
	internal readonly struct ResourceHandle : global::System.IEquatable<global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle>
	{
		private const uint kValidityMask = 4294901760u;

		private const uint kIndexMask = 65535u;

		private readonly uint m_Value;

		private readonly int m_Version;

		private readonly global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType m_Type;

		private static uint s_CurrentValidBit = 65536u;

		private static uint s_SharedResourceValidBit = 2147418112u;

		public int index
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return (int)(m_Value & 0xFFFF);
			}
		}

		public int iType
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return (int)type;
			}
		}

		public int version
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Version;
			}
		}

		public global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType type
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Type;
			}
		}

		public bool IsVersioned
		{
			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			get
			{
				return m_Version >= 0;
			}
		}

		internal ResourceHandle(int value, global::UnityEngine.Rendering.RenderGraphModule.RenderGraphResourceType type, bool shared)
		{
			m_Value = (uint)(value & 0xFFFF) | (shared ? s_SharedResourceValidBit : s_CurrentValidBit);
			m_Type = type;
			m_Version = -1;
		}

		internal ResourceHandle(in global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle h, int version)
		{
			m_Value = h.m_Value;
			m_Type = h.type;
			m_Version = version;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool IsValid()
		{
			uint num = m_Value & 0xFFFF0000u;
			if (num != 0)
			{
				if (num != s_CurrentValidBit)
				{
					return num == s_SharedResourceValidBit;
				}
				return true;
			}
			return false;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool IsNull()
		{
			if (index == 0)
			{
				return true;
			}
			return false;
		}

		public static void NewFrame(int executionIndex)
		{
			uint num = s_CurrentValidBit;
			s_CurrentValidBit = (uint)(((executionIndex >> 16) ^ ((executionIndex & 0xFFFF) * 58546883)) << 16);
			if (s_CurrentValidBit == 0 || s_CurrentValidBit == s_SharedResourceValidBit)
			{
				uint num2;
				for (num2 = 1u; num == num2 << 16; num2++)
				{
				}
				s_CurrentValidBit = num2 << 16;
			}
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public bool Equals(global::UnityEngine.Rendering.RenderGraphModule.ResourceHandle hdl)
		{
			if (hdl.m_Value == m_Value && hdl.m_Version == m_Version)
			{
				return hdl.type == type;
			}
			return false;
		}
	}
}
