namespace Unity.VisualScripting.Antlr3.Runtime
{
	public class ANTLRInputStream : global::Unity.VisualScripting.Antlr3.Runtime.ANTLRReaderStream
	{
		protected ANTLRInputStream()
		{
		}

		public ANTLRInputStream(global::System.IO.Stream istream)
			: this(istream, null)
		{
		}

		public ANTLRInputStream(global::System.IO.Stream istream, global::System.Text.Encoding encoding)
			: this(istream, global::Unity.VisualScripting.Antlr3.Runtime.ANTLRReaderStream.INITIAL_BUFFER_SIZE, encoding)
		{
		}

		public ANTLRInputStream(global::System.IO.Stream istream, int size)
			: this(istream, size, null)
		{
		}

		public ANTLRInputStream(global::System.IO.Stream istream, int size, global::System.Text.Encoding encoding)
			: this(istream, size, global::Unity.VisualScripting.Antlr3.Runtime.ANTLRReaderStream.READ_BUFFER_SIZE, encoding)
		{
		}

		public ANTLRInputStream(global::System.IO.Stream istream, int size, int readBufferSize, global::System.Text.Encoding encoding)
		{
			Load((encoding == null) ? new global::System.IO.StreamReader(istream) : new global::System.IO.StreamReader(istream, encoding), size, readBufferSize);
		}
	}
}
