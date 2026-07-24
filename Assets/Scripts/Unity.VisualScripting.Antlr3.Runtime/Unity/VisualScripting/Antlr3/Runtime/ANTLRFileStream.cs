namespace Unity.VisualScripting.Antlr3.Runtime
{
	public class ANTLRFileStream : global::Unity.VisualScripting.Antlr3.Runtime.ANTLRStringStream
	{
		protected string fileName;

		public override string SourceName => fileName;

		protected ANTLRFileStream()
		{
		}

		public ANTLRFileStream(string fileName)
			: this(fileName, global::System.Text.Encoding.Default)
		{
		}

		public ANTLRFileStream(string fileName, global::System.Text.Encoding encoding)
		{
			this.fileName = fileName;
			Load(fileName, encoding);
		}

		public virtual void Load(string fileName, global::System.Text.Encoding encoding)
		{
			if (fileName == null)
			{
				return;
			}
			global::System.IO.StreamReader streamReader = null;
			try
			{
				global::System.IO.FileInfo file = new global::System.IO.FileInfo(fileName);
				int num = (int)GetFileLength(file);
				data = new char[num];
				streamReader = ((encoding == null) ? new global::System.IO.StreamReader(fileName, global::System.Text.Encoding.Default) : new global::System.IO.StreamReader(fileName, encoding));
				n = streamReader.Read(data, 0, data.Length);
			}
			finally
			{
				streamReader?.Close();
			}
		}

		private long GetFileLength(global::System.IO.FileInfo file)
		{
			if (file.Exists)
			{
				return file.Length;
			}
			return 0L;
		}
	}
}
