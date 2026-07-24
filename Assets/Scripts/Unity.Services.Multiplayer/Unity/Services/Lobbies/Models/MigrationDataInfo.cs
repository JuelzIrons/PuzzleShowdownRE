namespace Unity.Services.Lobbies.Models
{
	[global::UnityEngine.Scripting.Preserve]
	[global::System.Runtime.Serialization.DataContract(Name = "MigrationDataInfo")]
	public class MigrationDataInfo
	{
		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "write", EmitDefaultValue = false)]
		public string Write { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "read", EmitDefaultValue = false)]
		public string Read { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "expires", EmitDefaultValue = false)]
		public global::System.DateTime Expires { get; }

		[global::UnityEngine.Scripting.Preserve]
		[global::System.Runtime.Serialization.DataMember(Name = "maxSize", EmitDefaultValue = false)]
		public int MaxSize { get; }

		[global::UnityEngine.Scripting.Preserve]
		public MigrationDataInfo(string write = null, string read = null, global::System.DateTime expires = default(global::System.DateTime), int maxSize = 0)
		{
			Write = write;
			Read = read;
			Expires = expires;
			MaxSize = maxSize;
		}

		internal string SerializeAsPathParam()
		{
			string text = "";
			if (Write != null)
			{
				text = text + "write," + Write + ",";
			}
			if (Read != null)
			{
				text = text + "read," + Read + ",";
			}
			_ = Expires;
			text = text + "expires," + Expires.ToString() + ",";
			return text + "maxSize," + MaxSize;
		}

		internal global::System.Collections.Generic.Dictionary<string, string> GetAsQueryParam()
		{
			global::System.Collections.Generic.Dictionary<string, string> dictionary = new global::System.Collections.Generic.Dictionary<string, string>();
			if (Write != null)
			{
				string value = Write.ToString();
				dictionary.Add("write", value);
			}
			if (Read != null)
			{
				string value2 = Read.ToString();
				dictionary.Add("read", value2);
			}
			_ = Expires;
			string value3 = Expires.ToString();
			dictionary.Add("expires", value3);
			string value4 = MaxSize.ToString();
			dictionary.Add("maxSize", value4);
			return dictionary;
		}
	}
}
