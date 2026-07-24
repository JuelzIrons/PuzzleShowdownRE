namespace Unity.Services.Authentication.PlayerAccounts
{
	internal class UnityPlayerAccountSettings : global::UnityEngine.ScriptableObject
	{
		[global::System.Flags]
		public enum SupportedScopesEnum
		{
			OpenId = 1,
			Email = 2,
			OfflineAccess = 4
		}

		private const string k_DeepLinkUriScheme = "unitydl";

		private const string k_DeepLinkUriHostPrefix = "com.unityplayeraccounts.";

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.Tooltip("Unity Player Account Client ID.")]
		internal string clientId;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		internal int scopeMask = (1 << global::System.Enum.GetNames(typeof(global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum)).Length) - 1;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Override the default redirect uri")]
		internal bool useCustomDeepLinkUri;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Custom Deep Link URI Scheme")]
		internal string customScheme;

		[global::UnityEngine.HideInInspector]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Custom Deep Link URI Host Prefix")]
		internal string customHost;

		private static readonly global::System.Collections.Generic.Dictionary<global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum, string> k_SupportedScopesDictionary = new global::System.Collections.Generic.Dictionary<global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum, string>
		{
			{
				global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum.OpenId,
				"openid"
			},
			{
				global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum.Email,
				"email"
			},
			{
				global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum.OfflineAccess,
				"offline_access"
			}
		};

		public global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum ScopeFlags
		{
			get
			{
				return (global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum)scopeMask;
			}
			set
			{
				scopeMask = (int)value;
			}
		}

		public string ClientId
		{
			get
			{
				string text = clientId?.Trim();
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				return null;
			}
			set
			{
				clientId = value.Trim();
			}
		}

		public string Scope
		{
			get
			{
				string text = "";
				global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum scopeFlags = ScopeFlags;
				foreach (global::System.Collections.Generic.KeyValuePair<global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings.SupportedScopesEnum, string> item in k_SupportedScopesDictionary)
				{
					if (scopeFlags.HasFlag(item.Key))
					{
						text = text + item.Value + ";";
					}
				}
				return text.TrimEnd(';');
			}
		}

		public bool UseCustomUri => useCustomDeepLinkUri;

		public string DeepLinkUriScheme
		{
			get
			{
				if (!useCustomDeepLinkUri)
				{
					return "unitydl";
				}
				return customScheme;
			}
		}

		public string DeepLinkUriHostPrefix
		{
			get
			{
				if (!useCustomDeepLinkUri)
				{
					return "com.unityplayeraccounts.";
				}
				return customHost;
			}
		}

		public static global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings Load()
		{
			return global::UnityEngine.Resources.Load<global::Unity.Services.Authentication.PlayerAccounts.UnityPlayerAccountSettings>("UnityPlayerAccountSettings");
		}
	}
}
