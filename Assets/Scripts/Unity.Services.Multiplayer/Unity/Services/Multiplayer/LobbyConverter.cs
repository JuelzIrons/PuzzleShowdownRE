namespace Unity.Services.Multiplayer
{
	internal static class LobbyConverter
	{
		private const string k_MultiplayServerAttemptingToConnectAnExistingSessionErrorMessage = "only players can join lobbies";

		internal static global::Unity.Services.Lobbies.Models.DataObject ToSessionDataObject(global::Unity.Services.Multiplayer.SessionProperty sessionData)
		{
			if (sessionData != null)
			{
				return new global::Unity.Services.Lobbies.Models.DataObject((global::Unity.Services.Lobbies.Models.DataObject.VisibilityOptions)sessionData.Visibility, sessionData.Value, (global::Unity.Services.Lobbies.Models.DataObject.IndexOptions)sessionData.Index);
			}
			return null;
		}

		internal static global::Unity.Services.Multiplayer.SessionProperty ToSessionProperty(global::Unity.Services.Lobbies.Models.DataObject dataObject)
		{
			if (dataObject != null)
			{
				return new global::Unity.Services.Multiplayer.SessionProperty(dataObject.Value, (global::Unity.Services.Multiplayer.VisibilityPropertyOptions)dataObject.Visibility, (global::Unity.Services.Multiplayer.PropertyIndex)dataObject.Index);
			}
			return null;
		}

		internal static global::Unity.Services.Lobbies.Models.PlayerDataObject ToPlayerDataObject(global::Unity.Services.Multiplayer.PlayerProperty memberData)
		{
			return new global::Unity.Services.Lobbies.Models.PlayerDataObject((global::Unity.Services.Lobbies.Models.PlayerDataObject.VisibilityOptions)memberData.Visibility, memberData.Value);
		}

		internal static global::Unity.Services.Lobbies.Models.Player ToLobbyPlayer(global::Unity.Services.Authentication.Internal.IPlayerId playerId, global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Multiplayer.PlayerProperty> playerProperties)
		{
			if (playerId == null)
			{
				return null;
			}
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject> data = global::System.Linq.Enumerable.ToDictionary(playerProperties?, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.PlayerProperty> kvpPlayerProperty) => kvpPlayerProperty.Key, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.PlayerProperty> kvpPlayerProperty) => ToPlayerDataObject(kvpPlayerProperty.Value));
			return new global::Unity.Services.Lobbies.Models.Player(playerId.PlayerId, null, data);
		}

		internal static global::Unity.Services.Lobbies.Models.Player ToLobbyPlayer(global::Unity.Services.Multiplayer.Player player)
		{
			if (player == null)
			{
				return null;
			}
			global::System.Collections.Generic.Dictionary<string, global::Unity.Services.Lobbies.Models.PlayerDataObject> data = global::System.Linq.Enumerable.ToDictionary(player?.Properties?, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.PlayerProperty> kvpPlayerProperty) => kvpPlayerProperty.Key, (global::System.Collections.Generic.KeyValuePair<string, global::Unity.Services.Multiplayer.PlayerProperty> kvpPlayerProperty) => ToPlayerDataObject(kvpPlayerProperty.Value));
			return new global::Unity.Services.Lobbies.Models.Player(player.Id, player.ConnectionInfo, data, player.AllocationId, player.Joined, player.LastUpdated);
		}

		internal static global::Unity.Services.Lobbies.QueryLobbiesOptions ToQueryLobbiesOptions(global::Unity.Services.Multiplayer.QuerySessionsOptions options)
		{
			return new global::Unity.Services.Lobbies.QueryLobbiesOptions
			{
				Filters = ToQueryFilters(options.FilterOptions),
				Order = ToQueryOrders(options.SortOptions),
				Count = options.Count,
				Skip = options.Skip,
				ContinuationToken = options.ContinuationToken
			};
		}

		internal static global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryFilter> ToQueryFilters(global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.FilterOption> filterOptions)
		{
			global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryFilter> list = new global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryFilter>();
			if (filterOptions != null)
			{
				foreach (global::Unity.Services.Multiplayer.FilterOption filterOption in filterOptions)
				{
					if (filterOption != null)
					{
						list.Add(ToFilterOption(filterOption));
					}
				}
			}
			return list;
		}

		private static global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryOrder> ToQueryOrders(global::System.Collections.Generic.List<global::Unity.Services.Multiplayer.SortOption> sortOptions)
		{
			global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryOrder> list = new global::System.Collections.Generic.List<global::Unity.Services.Lobbies.Models.QueryOrder>();
			if (sortOptions != null)
			{
				foreach (global::Unity.Services.Multiplayer.SortOption sortOption in sortOptions)
				{
					if (sortOption != null)
					{
						bool asc = sortOption.Order == global::Unity.Services.Multiplayer.SortOrder.Ascending;
						global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions field = ToFieldOptions(sortOption.Field);
						list.Add(new global::Unity.Services.Lobbies.Models.QueryOrder(asc, field));
					}
				}
			}
			return list;
		}

		private static global::Unity.Services.Lobbies.Models.QueryFilter ToFilterOption(global::Unity.Services.Multiplayer.FilterOption option)
		{
			return new global::Unity.Services.Lobbies.Models.QueryFilter(ToFilterField(option.Field), option.Value, ToFilterOperator(option.Operation));
		}

		private static global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions ToFilterOperator(global::Unity.Services.Multiplayer.FilterOperation filterOperation)
		{
			return filterOperation switch
			{
				global::Unity.Services.Multiplayer.FilterOperation.Contains => global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions.CONTAINS, 
				global::Unity.Services.Multiplayer.FilterOperation.Equal => global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions.EQ, 
				global::Unity.Services.Multiplayer.FilterOperation.NotEqual => global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions.NE, 
				global::Unity.Services.Multiplayer.FilterOperation.Less => global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions.LT, 
				global::Unity.Services.Multiplayer.FilterOperation.LessOrEqual => global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions.LE, 
				global::Unity.Services.Multiplayer.FilterOperation.Greater => global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions.GT, 
				global::Unity.Services.Multiplayer.FilterOperation.GreaterOrEqual => global::Unity.Services.Lobbies.Models.QueryFilter.OpOptions.GE, 
				_ => throw new global::System.Exception("Invalid FilterOperation"), 
			};
		}

		private static global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions ToFilterField(global::Unity.Services.Multiplayer.FilterField filterField)
		{
			return filterField switch
			{
				global::Unity.Services.Multiplayer.FilterField.AvailableSlots => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.AvailableSlots, 
				global::Unity.Services.Multiplayer.FilterField.Name => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.Name, 
				global::Unity.Services.Multiplayer.FilterField.Created => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.Created, 
				global::Unity.Services.Multiplayer.FilterField.LastUpdated => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.LastUpdated, 
				global::Unity.Services.Multiplayer.FilterField.StringIndex1 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.S1, 
				global::Unity.Services.Multiplayer.FilterField.StringIndex2 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.S2, 
				global::Unity.Services.Multiplayer.FilterField.StringIndex3 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.S3, 
				global::Unity.Services.Multiplayer.FilterField.StringIndex4 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.S4, 
				global::Unity.Services.Multiplayer.FilterField.StringIndex5 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.S5, 
				global::Unity.Services.Multiplayer.FilterField.NumberIndex1 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.N1, 
				global::Unity.Services.Multiplayer.FilterField.NumberIndex2 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.N2, 
				global::Unity.Services.Multiplayer.FilterField.NumberIndex3 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.N3, 
				global::Unity.Services.Multiplayer.FilterField.NumberIndex4 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.N4, 
				global::Unity.Services.Multiplayer.FilterField.NumberIndex5 => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.N5, 
				global::Unity.Services.Multiplayer.FilterField.IsLocked => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.IsLocked, 
				global::Unity.Services.Multiplayer.FilterField.HasPassword => global::Unity.Services.Lobbies.Models.QueryFilter.FieldOptions.HasPassword, 
				_ => throw new global::System.Exception("Invalid FilterField"), 
			};
		}

		private static global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions ToFieldOptions(global::Unity.Services.Multiplayer.SortField sortField)
		{
			return sortField switch
			{
				global::Unity.Services.Multiplayer.SortField.Name => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.Name, 
				global::Unity.Services.Multiplayer.SortField.MaxPlayers => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.MaxPlayers, 
				global::Unity.Services.Multiplayer.SortField.AvailableSlots => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.AvailableSlots, 
				global::Unity.Services.Multiplayer.SortField.CreationTime => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.Created, 
				global::Unity.Services.Multiplayer.SortField.LastUpdated => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.LastUpdated, 
				global::Unity.Services.Multiplayer.SortField.Id => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.ID, 
				global::Unity.Services.Multiplayer.SortField.StringIndex1 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.S1, 
				global::Unity.Services.Multiplayer.SortField.StringIndex2 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.S2, 
				global::Unity.Services.Multiplayer.SortField.StringIndex3 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.S3, 
				global::Unity.Services.Multiplayer.SortField.StringIndex4 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.S4, 
				global::Unity.Services.Multiplayer.SortField.StringIndex5 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.S5, 
				global::Unity.Services.Multiplayer.SortField.NumberIndex1 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.N1, 
				global::Unity.Services.Multiplayer.SortField.NumberIndex2 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.N2, 
				global::Unity.Services.Multiplayer.SortField.NumberIndex3 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.N3, 
				global::Unity.Services.Multiplayer.SortField.NumberIndex4 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.N4, 
				global::Unity.Services.Multiplayer.SortField.NumberIndex5 => global::Unity.Services.Lobbies.Models.QueryOrder.FieldOptions.N5, 
				_ => throw new global::System.Exception("Invalid SortField"), 
			};
		}

		internal static global::Unity.Services.Multiplayer.SessionException ToSessionException(global::Unity.Services.Lobbies.LobbyServiceException exception)
		{
			switch (exception.Reason)
			{
			case global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyNotFound:
				return new global::Unity.Services.Multiplayer.SessionException(exception.Message, global::Unity.Services.Multiplayer.SessionError.SessionNotFound);
			case global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyConflict:
				return new global::Unity.Services.Multiplayer.SessionException(exception.Message, global::Unity.Services.Multiplayer.SessionError.SessionConflict);
			case global::Unity.Services.Lobbies.LobbyExceptionReason.RateLimited:
				return new global::Unity.Services.Multiplayer.SessionException(exception.Message, global::Unity.Services.Multiplayer.SessionError.RateLimitExceeded);
			case global::Unity.Services.Lobbies.LobbyExceptionReason.MigrationDataRequestTimeout:
				return new global::Unity.Services.Multiplayer.SessionException(exception.Message, global::Unity.Services.Multiplayer.SessionError.MigrationDataRequestTimeout);
			case global::Unity.Services.Lobbies.LobbyExceptionReason.Forbidden:
				if (exception.Message == "only players can join lobbies")
				{
					return new global::Unity.Services.Multiplayer.SessionException("A Session with the same identifier already exists, you are likely seeing this error message due to a GUID collision.", global::Unity.Services.Multiplayer.SessionError.InvalidSessionIdentifier);
				}
				break;
			case global::Unity.Services.Lobbies.LobbyExceptionReason.LobbyVersionDoesNotMatch:
				return new global::Unity.Services.Multiplayer.SessionException("Unable to update session because the internal local state is outdated. Please retry the operation.", global::Unity.Services.Multiplayer.SessionError.OutOfSyncSession);
			}
			return new global::Unity.Services.Multiplayer.SessionException(exception.Message, global::Unity.Services.Multiplayer.SessionError.Unknown);
		}
	}
}
