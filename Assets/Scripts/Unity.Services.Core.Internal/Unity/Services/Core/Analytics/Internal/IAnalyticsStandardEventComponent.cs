namespace Unity.Services.Core.Analytics.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IAnalyticsStandardEventComponent : global::Unity.Services.Core.Internal.IServiceComponent
	{
		void Record(string eventName, global::System.Collections.Generic.IDictionary<string, object> eventParameters, int eventVersion, string packageName);
	}
}
