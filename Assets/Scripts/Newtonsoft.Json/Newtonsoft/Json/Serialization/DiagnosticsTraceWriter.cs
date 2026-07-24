namespace Newtonsoft.Json.Serialization
{
	public class DiagnosticsTraceWriter : global::Newtonsoft.Json.Serialization.ITraceWriter
	{
		public global::System.Diagnostics.TraceLevel LevelFilter { get; set; }

		private global::System.Diagnostics.TraceEventType GetTraceEventType(global::System.Diagnostics.TraceLevel level)
		{
			return level switch
			{
				global::System.Diagnostics.TraceLevel.Error => global::System.Diagnostics.TraceEventType.Error, 
				global::System.Diagnostics.TraceLevel.Warning => global::System.Diagnostics.TraceEventType.Warning, 
				global::System.Diagnostics.TraceLevel.Info => global::System.Diagnostics.TraceEventType.Information, 
				global::System.Diagnostics.TraceLevel.Verbose => global::System.Diagnostics.TraceEventType.Verbose, 
				_ => throw new global::System.ArgumentOutOfRangeException("level"), 
			};
		}

		public void Trace(global::System.Diagnostics.TraceLevel level, string message, global::System.Exception? ex)
		{
			if (level == global::System.Diagnostics.TraceLevel.Off)
			{
				return;
			}
			global::System.Diagnostics.TraceEventCache eventCache = new global::System.Diagnostics.TraceEventCache();
			global::System.Diagnostics.TraceEventType traceEventType = GetTraceEventType(level);
			foreach (global::System.Diagnostics.TraceListener listener in global::System.Diagnostics.Trace.Listeners)
			{
				if (!listener.IsThreadSafe)
				{
					lock (listener)
					{
						listener.TraceEvent(eventCache, "Newtonsoft.Json", traceEventType, 0, message);
					}
				}
				else
				{
					listener.TraceEvent(eventCache, "Newtonsoft.Json", traceEventType, 0, message);
				}
				if (global::System.Diagnostics.Trace.AutoFlush)
				{
					listener.Flush();
				}
			}
		}
	}
}
