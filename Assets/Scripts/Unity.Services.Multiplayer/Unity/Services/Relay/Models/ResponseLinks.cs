namespace Unity.Services.Relay.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "ResponseLinks")]
	public class ResponseLinks
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "next", EmitDefaultValue = false)]
		public string Next { get; }

		[global::UnityEngine.Scripting.Preserve]
		public ResponseLinks(string next = null)
		{
			Next = next;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Next != null)
			{
				text = text + "next," + Next;
			}
			return text;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Next != null)
			{
				string value = Next.ToString();
				dictionary.Add("next", value);
			}
			return dictionary;
		}
	}
}
