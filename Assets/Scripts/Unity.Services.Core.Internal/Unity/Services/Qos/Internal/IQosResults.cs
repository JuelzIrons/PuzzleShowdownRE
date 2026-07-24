namespace Unity.Services.Qos.Internal
{
	[global::UnityEngine.Scripting.RequireImplementors]
	public interface IQosResults : global::Unity.Services.Core.Internal.IServiceComponent
	{
		global::System.Threading.Tasks.Task<global::System.Collections.Generic.IList<global::Unity.Services.Qos.Internal.QosResult>> GetSortedQosResultsAsync(string service, global::System.Collections.Generic.IList<string> regions);
	}
}
