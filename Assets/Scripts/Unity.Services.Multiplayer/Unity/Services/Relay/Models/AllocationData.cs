namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "AllocationData")]
	public class AllocationData
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "allocation", IsRequired = true, EmitDefaultValue = true)]
		public global::Unity.Services.Relay.Models.Allocation Allocation { get; }

		[global::UnityEngine.Scripting.Preserve]
		public AllocationData(global::Unity.Services.Relay.Models.Allocation allocation)
		{
			Allocation = allocation;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Allocation != null)
			{
				text = text + "allocation," + Allocation.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			return new global::System.Collections.Generic.Dictionary<string, string>();
		}
	}
}
