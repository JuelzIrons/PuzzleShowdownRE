namespace Unity.Services.Qos
{
	public interface IQosAnnotatedResult : global::Unity.Services.Qos.IQosResult
	{
		global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> Annotations { get; }
	}
}
