namespace Unity.Collections.LowLevel.Unsafe
{
	internal sealed class UnsafeBitArrayDebugView
	{
		private global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray Data;

		public bool[] Bits
		{
			get
			{
				bool[] array = new bool[Data.Length];
				for (int i = 0; i < Data.Length; i++)
				{
					array[i] = Data.IsSet(i);
				}
				return array;
			}
		}

		public UnsafeBitArrayDebugView(global::Unity.Collections.LowLevel.Unsafe.UnsafeBitArray data)
		{
			Data = data;
		}
	}
}
