namespace Unity.Services.Multiplayer
{
	internal class LobbySessionInfo : global::Unity.Services.Multiplayer.ISessionInfo
	{
		private global::Unity.Services.Lobbies.Models.Lobby Lobby;

		public global::System.Collections.Generic.IReadOnlyDictionary<string, global::Unity.Services.Multiplayer.SessionProperty> Properties { get; }

		public string Name
		{
			get
			{
				return Lobby.Name;
			}
			set
			{
				Lobby.Name = value;
			}
		}

		public string Id
		{
			get
			{
				return Lobby.Id;
			}
			set
			{
				Lobby.Id = value;
			}
		}

		public string Upid
		{
			get
			{
				return Lobby.Upid;
			}
			set
			{
				Lobby.Upid = value;
			}
		}

		public int MaxPlayers
		{
			get
			{
				return Lobby.MaxPlayers;
			}
			set
			{
				Lobby.MaxPlayers = value;
			}
		}

		public int AvailableSlots
		{
			get
			{
				return Lobby.AvailableSlots;
			}
			set
			{
				Lobby.AvailableSlots = value;
			}
		}

		public bool IsLocked
		{
			get
			{
				return Lobby.IsLocked;
			}
			set
			{
				Lobby.IsLocked = value;
			}
		}

		public bool HasPassword
		{
			get
			{
				return Lobby.HasPassword;
			}
			set
			{
				Lobby.HasPassword = value;
			}
		}

		public global::System.DateTime LastUpdated
		{
			get
			{
				return Lobby.LastUpdated;
			}
			set
			{
				Lobby.LastUpdated = value;
			}
		}

		public string HostId
		{
			get
			{
				return Lobby.HostId;
			}
			set
			{
				Lobby.HostId = value;
			}
		}

		public global::System.DateTime Created
		{
			get
			{
				return Lobby.Created;
			}
			set
			{
				Lobby.Created = value;
			}
		}

		internal LobbySessionInfo(global::Unity.Services.Lobbies.Models.Lobby lobby)
		{
			Lobby = lobby;
			Properties = ((lobby.Data != null) ? ConvertProperty(lobby.Data, global::Unity.Services.Multiplayer.LobbyConverter.ToSessionProperty) : null);
		}

		private static global::System.Collections.Generic.Dictionary<string, TResult> ConvertProperty<TSource, TResult>(global::System.Collections.Generic.Dictionary<string, TSource> source, global::System.Func<TSource, TResult> func)
		{
			global::System.Collections.Generic.Dictionary<string, TResult> dictionary = new global::System.Collections.Generic.Dictionary<string, TResult>(source.Count);
			foreach (var (key, arg) in source)
			{
				dictionary.Add(key, func(arg));
			}
			return dictionary;
		}
	}
}
