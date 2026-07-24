namespace Newtonsoft.Json.Serialization
{
	public class MemoryTraceWriter : global::Newtonsoft.Json.Serialization.ITraceWriter
	{
		private readonly global::System.Collections.Generic.Queue<string> _traceMessages;

		private readonly object _lock;

		public global::System.Diagnostics.TraceLevel LevelFilter { get; set; }

		public MemoryTraceWriter()
		{
			LevelFilter = global::System.Diagnostics.TraceLevel.Verbose;
			_traceMessages = new global::System.Collections.Generic.Queue<string>();
			_lock = new object();
		}

		public void Trace(global::System.Diagnostics.TraceLevel level, string message, global::System.Exception? ex)
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append(global::System.DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", global::System.Globalization.CultureInfo.InvariantCulture));
			stringBuilder.Append(" ");
			stringBuilder.Append(level.ToString("g"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			string item = stringBuilder.ToString();
			lock (_lock)
			{
				if (_traceMessages.Count >= 1000)
				{
					_traceMessages.Dequeue();
				}
				_traceMessages.Enqueue(item);
			}
		}

		public global::System.Collections.Generic.IEnumerable<string> GetTraceMessages()
		{
			return _traceMessages;
		}

		public override string ToString()
		{
			lock (_lock)
			{
				global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
				foreach (string traceMessage in _traceMessages)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.Append(traceMessage);
				}
				return stringBuilder.ToString();
			}
		}
	}
}
