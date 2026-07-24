namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "JoinAllocation_allOf")]
	public class JoinAllocationAllOf
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "hostConnectionData", IsRequired = true, EmitDefaultValue = true)]
		public byte[] HostConnectionData { get; }

		[global::UnityEngine.Scripting.Preserve]
		public JoinAllocationAllOf(byte[] hostConnectionData)
		{
			HostConnectionData = hostConnectionData;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (HostConnectionData != null)
			{
				text = text + "hostConnectionData," + HostConnectionData.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (HostConnectionData != null)
			{
				string value = HostConnectionData.ToString();
				dictionary.Add("hostConnectionData", value);
			}
			return dictionary;
		}
	}
}
