namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "JoinCodeRequest")]
	public class JoinCodeRequest
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "allocationId", IsRequired = true, EmitDefaultValue = true)]
		public global::System.Guid AllocationId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public JoinCodeRequest(global::System.Guid allocationId)
		{
			AllocationId = allocationId;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			_ = AllocationId;
			return text + "allocationId," + AllocationId.ToString();
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			_ = AllocationId;
			string value = AllocationId.ToString();
			dictionary.Add("allocationId", value);
			return dictionary;
		}
	}
}
