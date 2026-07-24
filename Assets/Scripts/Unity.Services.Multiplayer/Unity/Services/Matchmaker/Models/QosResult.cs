namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "QosResult")]
	public class QosResult
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "regionId", IsRequired = true, EmitDefaultValue = true)]
		public string RegionId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "packetLoss", IsRequired = true, EmitDefaultValue = true)]
		public double? PacketLoss { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "latency", IsRequired = true, EmitDefaultValue = true)]
		public double? Latency { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "annotations", EmitDefaultValue = false)]
		public global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> Annotations { get; }

		[global::UnityEngine.Scripting.Preserve]
		public QosResult(string regionId, double? packetLoss, double? latency, global::System.Collections.Generic.Dictionary<string, global::System.Collections.Generic.List<string>> annotations = null)
		{
			RegionId = regionId;
			PacketLoss = packetLoss;
			Latency = latency;
			Annotations = annotations;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (RegionId != null)
			{
				text = text + "regionId," + RegionId + ",";
			}
			if (PacketLoss.HasValue)
			{
				text = text + "packetLoss," + PacketLoss + ",";
			}
			if (Latency.HasValue)
			{
				text = text + "latency," + Latency + ",";
			}
			if (Annotations != null)
			{
				text = text + "annotations," + Annotations.ToString();
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (RegionId != null)
			{
				string value = RegionId.ToString();
				dictionary.Add("regionId", value);
			}
			if (PacketLoss.HasValue)
			{
				string value2 = PacketLoss.ToString();
				dictionary.Add("packetLoss", value2);
			}
			if (Latency.HasValue)
			{
				string value3 = Latency.ToString();
				dictionary.Add("latency", value3);
			}
			if (Annotations != null)
			{
				string value4 = Annotations.ToString();
				dictionary.Add("annotations", value4);
			}
			return dictionary;
		}
	}
}
