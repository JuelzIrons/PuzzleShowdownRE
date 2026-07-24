namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "AbTestingResult")]
	public class AbTestingResult
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "isAbTesting", EmitDefaultValue = true)]
		public bool IsAbTesting { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "variantId", EmitDefaultValue = false)]
		public string VariantId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "poolId", EmitDefaultValue = false)]
		public string PoolId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "basePoolId", EmitDefaultValue = false)]
		public string BasePoolId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public AbTestingResult(bool isAbTesting = false, string variantId = null, string poolId = null, string basePoolId = null)
		{
			IsAbTesting = isAbTesting;
			VariantId = variantId;
			PoolId = poolId;
			BasePoolId = basePoolId;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			text = text + "isAbTesting," + IsAbTesting + ",";
			if (VariantId != null)
			{
				text = text + "variantId," + VariantId + ",";
			}
			if (PoolId != null)
			{
				text = text + "poolId," + PoolId + ",";
			}
			if (BasePoolId != null)
			{
				text = text + "basePoolId," + BasePoolId;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			string value = IsAbTesting.ToString();
			dictionary.Add("isAbTesting", value);
			if (VariantId != null)
			{
				string value2 = VariantId.ToString();
				dictionary.Add("variantId", value2);
			}
			if (PoolId != null)
			{
				string value3 = PoolId.ToString();
				dictionary.Add("poolId", value3);
			}
			if (BasePoolId != null)
			{
				string value4 = BasePoolId.ToString();
				dictionary.Add("basePoolId", value4);
			}
			return dictionary;
		}
	}
}
