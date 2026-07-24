namespace UnityEngine.U2D.Animation
{
	internal class NativeByteArray
	{
		public int Length => array.Length;

		public bool IsCreated => array.IsCreated;

		public byte this[int index] => array[index];

		public global::Unity.Collections.NativeArray<byte> array { get; }

		public NativeByteArray(global::Unity.Collections.NativeArray<byte> array)
		{
			this.array = array;
		}

		public void Dispose()
		{
			array.Dispose();
		}
	}
}
