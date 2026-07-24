namespace WebSocketSharp
{
	public class Logger
	{
		private volatile string _file;

		private volatile global::WebSocketSharp.LogLevel _level;

		private global::System.Action<global::WebSocketSharp.LogData, string> _output;

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
					Warn($"The current path to the log file has been changed to {_file}.");
				}
			}
		}

		public global::WebSocketSharp.LogLevel Level
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
					Warn($"The current logging level has been changed to {_level}.");
				}
			}
		}

		public global::System.Action<global::WebSocketSharp.LogData, string> Output
		{
			get
			{
				return _output;
			}
			set
			{
				lock (_sync)
				{
					_output = value ?? new global::System.Action<global::WebSocketSharp.LogData, string>(defaultOutput);
					Warn("The current output action has been changed.");
				}
			}
		}

		public Logger()
			: this(global::WebSocketSharp.LogLevel.Error, null, null)
		{
		}

		public Logger(global::WebSocketSharp.LogLevel level)
			: this(level, null, null)
		{
		}

		public Logger(global::WebSocketSharp.LogLevel level, string file, global::System.Action<global::WebSocketSharp.LogData, string> output)
		{
			_level = level;
			_file = file;
			_output = output ?? new global::System.Action<global::WebSocketSharp.LogData, string>(defaultOutput);
			_sync = new object();
		}

		private static void defaultOutput(global::WebSocketSharp.LogData data, string path)
		{
			string value = data.ToString();
			global::System.Console.WriteLine(value);
			if (path != null && path.Length > 0)
			{
				writeToFile(value, path);
			}
		}

		private void output(string message, global::WebSocketSharp.LogLevel level)
		{
			lock (_sync)
			{
				if (_level > level)
				{
					return;
				}
				global::WebSocketSharp.LogData logData = null;
				try
				{
					logData = new global::WebSocketSharp.LogData(level, new global::System.Diagnostics.StackFrame(2, fNeedFileInfo: true), message);
					_output(logData, _file);
				}
				catch (global::System.Exception ex)
				{
					logData = new global::WebSocketSharp.LogData(global::WebSocketSharp.LogLevel.Fatal, new global::System.Diagnostics.StackFrame(0, fNeedFileInfo: true), ex.Message);
					global::System.Console.WriteLine(logData.ToString());
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
			if (_level <= global::WebSocketSharp.LogLevel.Debug)
			{
				output(message, global::WebSocketSharp.LogLevel.Debug);
			}
		}

		public void Error(string message)
		{
			if (_level <= global::WebSocketSharp.LogLevel.Error)
			{
				output(message, global::WebSocketSharp.LogLevel.Error);
			}
		}

		public void Fatal(string message)
		{
			output(message, global::WebSocketSharp.LogLevel.Fatal);
		}

		public void Info(string message)
		{
			if (_level <= global::WebSocketSharp.LogLevel.Info)
			{
				output(message, global::WebSocketSharp.LogLevel.Info);
			}
		}

		public void Trace(string message)
		{
			if (_level <= global::WebSocketSharp.LogLevel.Trace)
			{
				output(message, global::WebSocketSharp.LogLevel.Trace);
			}
		}

		public void Warn(string message)
		{
			if (_level <= global::WebSocketSharp.LogLevel.Warn)
			{
				output(message, global::WebSocketSharp.LogLevel.Warn);
			}
		}
	}
}
