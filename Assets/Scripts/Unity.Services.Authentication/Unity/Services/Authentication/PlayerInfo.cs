namespace Unity.Services.Authentication
{
	public sealed class PlayerInfo
	{
		private const string k_OpenIdConnectPrefix = "oidc-";

		private const string k_IdProviderNameRegex = "^oidc-[a-z0-9-_\\.]{1,15}$";

		private string m_Username;

		private global::System.DateTime? m_LastPasswordUpdate;

		public string Id { get; }

		public global::System.DateTime? CreatedAt { get; }

		public global::System.Collections.Generic.List<global::Unity.Services.Authentication.Identity> Identities { get; }

		[global::JetBrains.Annotations.CanBeNull]
		public string Username
		{
			get
			{
				return m_Username;
			}
			internal set
			{
				if (m_Username != value)
				{
					m_Username = value;
					this.InfoChanged?.Invoke(this);
				}
			}
		}

		[global::JetBrains.Annotations.CanBeNull]
		public global::System.DateTime? LastPasswordUpdate
		{
			get
			{
				return m_LastPasswordUpdate;
			}
			internal set
			{
				if (m_LastPasswordUpdate != value)
				{
					m_LastPasswordUpdate = value;
					this.InfoChanged?.Invoke(this);
				}
			}
		}

		public event global::System.Action<global::Unity.Services.Authentication.PlayerInfo> InfoChanged;

		internal PlayerInfo(string playerId)
		{
			Id = playerId;
			Identities = new global::System.Collections.Generic.List<global::Unity.Services.Authentication.Identity>();
		}

		internal PlayerInfo(global::Unity.Services.Authentication.PlayerInfoResponse response)
			: this(response.Id, response.CreatedAt, response.ExternalIds, response.UsernamePassword?.Username, response.UsernamePassword?.PasswordUpdatedAt)
		{
		}

		internal PlayerInfo(global::Unity.Services.Authentication.User user)
			: this(user.Id, user.CreatedAt, user.ExternalIds, user.UsernameInfo?.Username ?? user.Username, user.UsernameInfo?.PasswordUpdatedAt)
		{
		}

		internal PlayerInfo(string playerId, string createdAt, global::System.Collections.Generic.List<global::Unity.Services.Authentication.ExternalIdentity> externalIdentities, string username, string lastPasswordUpdate)
		{
			Id = playerId;
			Identities = new global::System.Collections.Generic.List<global::Unity.Services.Authentication.Identity>();
			if (double.TryParse(createdAt, out var result))
			{
				CreatedAt = new global::System.DateTime(1970, 1, 1, 0, 0, 0, global::System.DateTimeKind.Utc).AddSeconds(result);
			}
			if (externalIdentities != null)
			{
				foreach (global::Unity.Services.Authentication.ExternalIdentity externalIdentity in externalIdentities)
				{
					Identities.Add(new global::Unity.Services.Authentication.Identity(externalIdentity));
				}
			}
			Username = username;
			if (double.TryParse(lastPasswordUpdate, out var result2))
			{
				LastPasswordUpdate = new global::System.DateTime(1970, 1, 1, 0, 0, 0, global::System.DateTimeKind.Utc).AddSeconds(result2);
			}
		}

		public string GetFacebookId()
		{
			return GetIdentityId("facebook.com");
		}

		public string GetSteamId()
		{
			return GetIdentityId("steampowered.com");
		}

		public string GetGoogleId()
		{
			return GetIdentityId("google.com");
		}

		public string GetGooglePlayGamesId()
		{
			return GetIdentityId("google-play-games");
		}

		public string GetAppleId()
		{
			return GetIdentityId("apple.com");
		}

		public string GetAppleGameCenterId()
		{
			return GetIdentityId("apple-game-center");
		}

		public string GetOculusId()
		{
			return GetIdentityId("oculus");
		}

		public string GetOpenIdConnectId(string idProviderName)
		{
			if (!ValidateOpenIdConnectIdProviderName(idProviderName))
			{
				return null;
			}
			return GetIdentityId(idProviderName);
		}

		public string GetUnityId()
		{
			return GetIdentityId("unity");
		}

		public string GetCustomId()
		{
			return GetIdentityId("custom");
		}

		public global::System.Collections.Generic.List<global::Unity.Services.Authentication.Identity> GetOpenIdConnectIdProviders()
		{
			return Identities?.FindAll((global::Unity.Services.Authentication.Identity id) => id.TypeId.StartsWith("oidc-"));
		}

		internal string GetIdentityId(string typeId)
		{
			return global::System.Linq.Enumerable.FirstOrDefault(Identities?, (global::Unity.Services.Authentication.Identity x) => x.TypeId == typeId)?.UserId;
		}

		internal void AddExternalIdentity(global::Unity.Services.Authentication.ExternalIdentity externalId)
		{
			if (externalId != null)
			{
				Identities.Add(new global::Unity.Services.Authentication.Identity(externalId));
				this.InfoChanged?.Invoke(this);
			}
		}

		internal void RemoveIdentity(string typeId)
		{
			int? num = Identities?.RemoveAll((global::Unity.Services.Authentication.Identity x) => x.TypeId == typeId);
			if (num.HasValue && num.Value > 0)
			{
				this.InfoChanged?.Invoke(this);
			}
		}

		private bool ValidateOpenIdConnectIdProviderName(string idProviderName)
		{
			if (!string.IsNullOrEmpty(idProviderName))
			{
				return global::System.Text.RegularExpressions.Regex.Match(idProviderName, "^oidc-[a-z0-9-_\\.]{1,15}$").Success;
			}
			return false;
		}
	}
}
