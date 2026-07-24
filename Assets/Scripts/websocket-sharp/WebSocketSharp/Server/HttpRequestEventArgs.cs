namespace WebSocketSharp.Server
{
	public class HttpRequestEventArgs : global::System.EventArgs
	{
		private global::WebSocketSharp.Net.HttpListenerContext _context;

		private string _docRootPath;

		public global::WebSocketSharp.Net.HttpListenerRequest Request => _context.Request;

		public global::WebSocketSharp.Net.HttpListenerResponse Response => _context.Response;

		public global::System.Security.Principal.IPrincipal User => _context.User;

		internal HttpRequestEventArgs(global::WebSocketSharp.Net.HttpListenerContext context, string documentRootPath)
		{
			_context = context;
			_docRootPath = documentRootPath;
		}

		private string createFilePath(string childPath)
		{
			childPath = childPath.TrimStart('/', '\\');
			return new global::System.Text.StringBuilder(_docRootPath, 32).AppendFormat("/{0}", childPath).ToString().Replace('\\', '/');
		}

		private static bool tryReadFile(string path, out byte[] contents)
		{
			contents = null;
			if (!global::System.IO.File.Exists(path))
			{
				return false;
			}
			try
			{
				contents = global::System.IO.File.ReadAllBytes(path);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public byte[] ReadFile(string path)
		{
			if (path == null)
			{
				throw new global::System.ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "path");
			}
			if (path.IndexOf("..") > -1)
			{
				throw new global::System.ArgumentException("It contains '..'.", "path");
			}
			path = createFilePath(path);
			tryReadFile(path, out var contents);
			return contents;
		}

		public bool TryReadFile(string path, out byte[] contents)
		{
			if (path == null)
			{
				throw new global::System.ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new global::System.ArgumentException("An empty string.", "path");
			}
			if (path.IndexOf("..") > -1)
			{
				throw new global::System.ArgumentException("It contains '..'.", "path");
			}
			path = createFilePath(path);
			return tryReadFile(path, out contents);
		}
	}
}
