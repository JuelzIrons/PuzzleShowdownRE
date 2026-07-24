namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	internal readonly struct Name
	{
		public readonly string name;

		public readonly int utf8ByteCount;

		public Name(string name, bool computeUTF8ByteCount = false)
		{
			this.name = name;
			utf8ByteCount = ((name != null && name.Length > 0 && computeUTF8ByteCount) ? global::System.Text.Encoding.UTF8.GetByteCount((global::System.ReadOnlySpan<char>)name) : 0);
		}
	}
}
