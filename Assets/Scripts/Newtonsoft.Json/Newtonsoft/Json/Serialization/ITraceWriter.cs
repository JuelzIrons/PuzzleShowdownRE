namespace Newtonsoft.Json.Serialization
{
	public interface ITraceWriter
	{
		global::System.Diagnostics.TraceLevel LevelFilter { get; }

		void Trace(global::System.Diagnostics.TraceLevel level, string message, global::System.Exception? ex);
	}
}
