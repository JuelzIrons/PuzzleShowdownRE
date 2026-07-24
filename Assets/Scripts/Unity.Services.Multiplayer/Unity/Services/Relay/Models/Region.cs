namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "Region")]
	public class Region
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
		public string Id { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "description", IsRequired = true, EmitDefaultValue = true)]
		public string Description { get; }

		[global::UnityEngine.Scripting.Preserve]
		public Region(string id, string description)
		{
			Id = id;
			Description = description;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Id != null)
			{
				text = text + "id," + Id + ",";
			}
			if (Description != null)
			{
				text = text + "description," + Description;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Id != null)
			{
				string value = Id.ToString();
				dictionary.Add("id", value);
			}
			if (Description != null)
			{
				string value2 = Description.ToString();
				dictionary.Add("description", value2);
			}
			return dictionary;
		}
	}
}
