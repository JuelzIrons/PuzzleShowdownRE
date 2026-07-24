namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "PlayerProfile")]
	public class PlayerProfile
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; }

		[global::UnityEngine.Scripting.Preserve]
		public PlayerProfile(string name = null)
		{
			Name = name;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Name != null)
			{
				text = text + "name," + Name;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Name != null)
			{
				string value = Name.ToString();
				dictionary.Add("name", value);
			}
			return dictionary;
		}
	}
}
