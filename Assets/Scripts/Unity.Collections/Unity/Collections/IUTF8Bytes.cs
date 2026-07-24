namespace Unity.Collections
{
	public interface IUTF8Bytes
	{
		bool IsEmpty { get; }

		unsafe byte* GetUnsafePtr();

		bool TryResize(int newLength, global::Unity.Collections.NativeArrayOptions clearOptions = global::Unity.Collections.NativeArrayOptions.ClearMemory);
	}
}
