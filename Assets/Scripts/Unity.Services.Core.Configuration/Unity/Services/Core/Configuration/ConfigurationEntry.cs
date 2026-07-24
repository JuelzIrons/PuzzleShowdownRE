namespace Unity.Services.Core.Configuration
{
	[global::System.Serializable]
	internal class ConfigurationEntry
	{
		[global::Newtonsoft.Json.JsonRequired]
		[global::UnityEngine.SerializeField]
		private string m_Value;

		[global::Newtonsoft.Json.JsonRequired]
		[global::UnityEngine.SerializeField]
		private bool m_IsReadOnly;

		[global::Newtonsoft.Json.JsonIgnore]
		public string Value => m_Value;

		[global::Newtonsoft.Json.JsonIgnore]
		public bool IsReadOnly
		{
			get
			{
				return m_IsReadOnly;
			}
			internal set
			{
				m_IsReadOnly = value;
			}
		}

		public ConfigurationEntry()
		{
		}

		public ConfigurationEntry(string value, bool isReadOnly = false)
		{
			m_Value = value;
			m_IsReadOnly = isReadOnly;
		}

		public bool TrySetValue(string value)
		{
			if (IsReadOnly)
			{
				return false;
			}
			m_Value = value;
			return true;
		}

		public static implicit operator string(global::Unity.Services.Core.Configuration.ConfigurationEntry entry)
		{
			return entry.Value;
		}

		public static implicit operator global::Unity.Services.Core.Configuration.ConfigurationEntry(string value)
		{
			return new global::Unity.Services.Core.Configuration.ConfigurationEntry(value);
		}
	}
}
