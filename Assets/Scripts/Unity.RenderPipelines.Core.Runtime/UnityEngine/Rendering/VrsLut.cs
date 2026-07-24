namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	public class VrsLut
	{
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.Color[] m_Data = new global::UnityEngine.Color[global::UnityEngine.Rendering.Vrs.shadingRateFragmentSizeCount];

		private const uint Rate1x = 0u;

		private const uint Rate2x = 1u;

		private const uint Rate4x = 2u;

		public global::UnityEngine.Color this[global::UnityEngine.Rendering.ShadingRateFragmentSize fragmentSize]
		{
			get
			{
				return m_Data[(int)fragmentSize];
			}
			set
			{
				m_Data[(int)fragmentSize] = value;
			}
		}

		public static global::UnityEngine.Rendering.VrsLut CreateDefault()
		{
			return new global::UnityEngine.Rendering.VrsLut
			{
				[global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize1x1] = new global::UnityEngine.Color(0.785f, 0.23f, 0.2f, 1f),
				[global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize1x2] = new global::UnityEngine.Color(1f, 0.8f, 0.8f, 1f),
				[global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize2x1] = new global::UnityEngine.Color(0.4f, 0.2f, 0.2f, 1f),
				[global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize2x2] = new global::UnityEngine.Color(0.51f, 0.8f, 0.6f, 1f),
				[global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize1x4] = new global::UnityEngine.Color(0.6f, 0.8f, 1f, 1f),
				[global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize4x1] = new global::UnityEngine.Color(0.2f, 0.4f, 0.6f, 1f),
				[global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize2x4] = new global::UnityEngine.Color(0.8f, 1f, 0.8f, 1f),
				[global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize4x2] = new global::UnityEngine.Color(0.2f, 0.4f, 0.2f, 1f),
				[global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize4x4] = new global::UnityEngine.Color(0.125f, 0.22f, 0.36f, 1f)
			};
		}

		public global::UnityEngine.GraphicsBuffer CreateBuffer(bool forVisualization = false)
		{
			global::UnityEngine.Color[] array;
			if (forVisualization)
			{
				global::System.Array values = global::System.Enum.GetValues(typeof(global::UnityEngine.Rendering.ShadingRateFragmentSize));
				array = new global::UnityEngine.Color[MapFragmentShadingRateToBinary(global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize4x4) + 1];
				for (int num = values.Length - 1; num >= 0; num--)
				{
					global::UnityEngine.Rendering.ShadingRateFragmentSize shadingRateFragmentSize = (global::UnityEngine.Rendering.ShadingRateFragmentSize)values.GetValue(num);
					byte b = global::UnityEngine.Rendering.ShadingRateInfo.QueryNativeValue(shadingRateFragmentSize);
					array[b] = m_Data[(int)shadingRateFragmentSize].linear;
				}
			}
			else
			{
				array = new global::UnityEngine.Color[m_Data.Length];
				for (int i = 0; i < m_Data.Length; i++)
				{
					array[i] = m_Data[i].linear;
				}
			}
			global::UnityEngine.GraphicsBuffer graphicsBuffer = new global::UnityEngine.GraphicsBuffer(global::UnityEngine.GraphicsBuffer.Target.Structured, array.Length, global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::UnityEngine.Color)));
			graphicsBuffer.SetData(array);
			return graphicsBuffer;
		}

		private uint MapFragmentShadingRateToBinary(global::UnityEngine.Rendering.ShadingRateFragmentSize fs)
		{
			return fs switch
			{
				global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize1x2 => EncodeShadingRate(0u, 1u), 
				global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize2x1 => EncodeShadingRate(1u, 0u), 
				global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize2x2 => EncodeShadingRate(1u, 1u), 
				global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize1x4 => EncodeShadingRate(0u, 2u), 
				global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize4x1 => EncodeShadingRate(2u, 0u), 
				global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize2x4 => EncodeShadingRate(1u, 2u), 
				global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize4x2 => EncodeShadingRate(2u, 1u), 
				global::UnityEngine.Rendering.ShadingRateFragmentSize.FragmentSize4x4 => EncodeShadingRate(2u, 2u), 
				_ => EncodeShadingRate(0u, 0u), 
			};
		}

		private uint EncodeShadingRate(uint x, uint y)
		{
			return (x << 2) | y;
		}
	}
}
