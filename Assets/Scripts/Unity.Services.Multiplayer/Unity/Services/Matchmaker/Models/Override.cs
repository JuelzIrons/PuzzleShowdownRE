namespace Unity.Services.Matchmaker.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "Override")]
	public class Override
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "basePoolId", EmitDefaultValue = false)]
		public string BasePoolId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "variantId", EmitDefaultValue = false)]
		public string VariantId { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "poolOverrideId", EmitDefaultValue = false)]
		public string PoolOverrideId { get; }

		[global::UnityEngine.Scripting.Preserve]
		public Override(string basePoolId = null, string variantId = null, string poolOverrideId = null)
		{
			BasePoolId = basePoolId;
			VariantId = variantId;
			PoolOverrideId = poolOverrideId;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (BasePoolId != null)
			{
				text = text + "basePoolId," + BasePoolId + ",";
			}
			if (VariantId != null)
			{
				text = text + "variantId," + VariantId + ",";
			}
			if (PoolOverrideId != null)
			{
				text = text + "poolOverrideId," + PoolOverrideId;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (BasePoolId != null)
			{
				string value = BasePoolId.ToString();
				dictionary.Add("basePoolId", value);
			}
			if (VariantId != null)
			{
				string value2 = VariantId.ToString();
				dictionary.Add("variantId", value2);
			}
			if (PoolOverrideId != null)
			{
				string value3 = PoolOverrideId.ToString();
				dictionary.Add("poolOverrideId", value3);
			}
			return dictionary;
		}
	}
}
