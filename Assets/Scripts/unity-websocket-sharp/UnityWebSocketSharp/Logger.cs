namespace UnityWebSocketSharp
{
	internal class Logger
	{
		private volatile string _file;

		private volatile global::UnityWebSocketSharp.LogLevel _level;

		private global::System.Action<global::UnityWebSocketSharp.LogData, string> _output;

		private object _sync;

		public string File
		{
			get
			{
				return _file;
			}
			set
			{
				lock (_sync)
				{
					_file = value;
				}
			}
		}

		public global::UnityWebSocketSharp.LogLevel Level
		{
			get
			{
				return _level;
			}
			set
			{
				lock (_sync)
				{
					_level = value;
				}
			}
		}

		public global::System.Action<global::UnityWebSocketSharp.LogData, string> Output
		{
			get
			{
				return _output;
			}
			set
			{
				lock (_sync)
				{
					_output = value ?? new global::System.Action<global::UnityWebSocketSharp.LogData, string>(defaultOutput);
				}
			}
		}

		public Logger()
			: this(global::UnityWebSocketSharp.LogLevel.Error, null, null)
		{
		}

		public Logger(global::UnityWebSocketSharp.LogLevel level)
			: this(level, null, null)
		{
		}

		public Logger(global::UnityWebSocketSharp.LogLevel level, string file, global::System.Action<global::UnityWebSocketSharp.LogData, string> output)
		{
			_level = level;
			_file = file;
			_output = output ?? new global::System.Action<global::UnityWebSocketSharp.LogData, string>(defaultOutput);
			_sync = new object();
		}

		private static void defaultOutput(global::UnityWebSocketSharp.LogData data, string path)
		{
			string value = data.ToString();
			global::System.Console.WriteLine(value);
			if (path != null && path.Length > 0)
			{
				writeToFile(value, path);
			}
		}

		private void output(string message, global::UnityWebSocketSharp.LogLevel level)
		{
			lock (_sync)
			{
				if (_level > level)
				{
					return;
				}
				try
				{
					global::UnityWebSocketSharp.LogData arg = new global::UnityWebSocketSharp.LogData(level, new global::System.Diagnostics.StackFrame(2, fNeedFileInfo: true), message);
					_output(arg, _file);
				}
				catch (global::System.Exception ex)
				{
					global::System.Console.WriteLine(new global::UnityWebSocketSharp.LogData(global::UnityWebSocketSharp.LogLevel.Fatal, new global::System.Diagnostics.StackFrame(0, fNeedFileInfo: true), ex.Message).ToString());
				}
			}
		}

		private static void writeToFile(string value, string path)
		{
			using global::System.IO.StreamWriter writer = new global::System.IO.StreamWriter(path, append: true);
			using global::System.IO.TextWriter textWriter = global::System.IO.TextWriter.Synchronized(writer);
			textWriter.WriteLine(value);
		}

		public void Debug(string message)
		{
			if (_level <= global::UnityWebSocketSharp.LogLevel.Debug)
			{
				output(message, global::UnityWebSocketSharp.LogLevel.Debug);
			}
		}

		public void Error(string message)
		{
			if (_level <= global::UnityWebSocketSharp.LogLevel.Error)
			{
				output(message, global::UnityWebSocketSharp.LogLevel.Error);
			}
		}

		public void Fatal(string message)
		{
			if (_level <= global::UnityWebSocketSharp.LogLevel.Fatal)
			{
				output(message, global::UnityWebSocketSharp.LogLevel.Fatal);
			}
		}

		public void Info(string message)
		{
			if (_level <= global::UnityWebSocketSharp.LogLevel.Info)
			{
				output(message, global::UnityWebSocketSharp.LogLevel.Info);
			}
		}

		public void Trace(string message)
		{
			if (_level <= global::UnityWebSocketSharp.LogLevel.Trace)
			{
				output(message, global::UnityWebSocketSharp.LogLevel.Trace);
			}
		}

		public void Warn(string message)
		{
			if (_level <= global::UnityWebSocketSharp.LogLevel.Warn)
			{
				output(message, global::UnityWebSocketSharp.LogLevel.Warn);
			}
		}
	}
}
