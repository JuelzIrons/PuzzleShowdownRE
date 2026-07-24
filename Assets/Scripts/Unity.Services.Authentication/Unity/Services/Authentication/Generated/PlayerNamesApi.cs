namespace Unity.Services.Authentication.Generated
{
	internal class PlayerNamesApi : global::Unity.Services.Authentication.Generated.IPlayerNamesApi, global::Unity.Services.Authentication.Shared.IApiAccessor
	{
		public global::Unity.Services.Authentication.Shared.IApiClient Client { get; }

		public global::Unity.Services.Authentication.Shared.IApiConfiguration Configuration { get; }

		public PlayerNamesApi(global::Unity.Services.Authentication.Shared.IApiClient apiClient)
		{
			if (apiClient == null)
			{
				throw new global::System.ArgumentNullException("apiClient");
			}
			Client = apiClient;
			Configuration = new global::Unity.Services.Authentication.Shared.ApiConfiguration
			{
				BasePath = "https://social.services.api.unity.com/v1"
			};
		}

		public PlayerNamesApi(global::Unity.Services.Authentication.Shared.IApiClient apiClient, global::Unity.Services.Authentication.Shared.IApiConfiguration apiConfiguration)
		{
			if (apiClient == null)
			{
				throw new global::System.ArgumentNullException("apiClient");
			}
			if (apiConfiguration == null)
			{
				throw new global::System.ArgumentNullException("apiConfiguration");
			}
			Client = apiClient;
			Configuration = apiConfiguration;
		}

		public string GetBasePath()
		{
			return Configuration.BasePath;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<global::Unity.Services.Authentication.Generated.Player>> GetNameAsync(string playerId, bool? autoGenerate = null, bool? showMetadata = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (playerId == null)
			{
				throw new global::Unity.Services.Authentication.Shared.ApiException(global::Unity.Services.Authentication.Shared.ApiExceptionType.InvalidParameters, "Missing required parameter 'playerId' when calling PlayerNamesApi->GetName");
			}
			global::Unity.Services.Authentication.Shared.ApiRequestOptions apiRequestOptions = new global::Unity.Services.Authentication.Shared.ApiRequestOptions();
			string[] contentTypes = new string[0];
			string[] accepts = new string[2] { "application/json", "application/problem+json" };
			string text = global::Unity.Services.Authentication.Shared.ApiUtils.SelectHeaderContentType(contentTypes);
			if (text != null)
			{
				apiRequestOptions.HeaderParameters.Add("Content-Type", text);
			}
			string text2 = global::Unity.Services.Authentication.Shared.ApiUtils.SelectHeaderAccept(accepts);
			if (text2 != null)
			{
				apiRequestOptions.HeaderParameters.Add("Accept", text2);
			}
			apiRequestOptions.PathParameters.Add("playerId", global::Unity.Services.Authentication.Shared.ApiUtils.ParameterToString(Configuration, playerId));
			if (autoGenerate.HasValue)
			{
				apiRequestOptions.QueryParameters.Add(global::Unity.Services.Authentication.Shared.ApiUtils.ParameterToMultiMap(Configuration, "", "autoGenerate", autoGenerate));
			}
			if (showMetadata.HasValue)
			{
				apiRequestOptions.QueryParameters.Add(global::Unity.Services.Authentication.Shared.ApiUtils.ParameterToMultiMap(Configuration, "", "showMetadata", showMetadata));
			}
			apiRequestOptions.Operation = "PlayerNamesApi.GetName";
			if (!string.IsNullOrEmpty(Configuration.AccessToken) && !apiRequestOptions.HeaderParameters.ContainsKey("Authorization"))
			{
				apiRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + Configuration.AccessToken);
			}
			return await Client.GetAsync<global::Unity.Services.Authentication.Generated.Player>("/names/{playerId}", apiRequestOptions, Configuration, cancellationToken);
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Authentication.Shared.ApiResponse<global::Unity.Services.Authentication.Generated.Player>> UpdateNameAsync(string playerId, global::Unity.Services.Authentication.Generated.UpdateNameRequest updateNameRequest, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
		{
			if (playerId == null)
			{
				throw new global::Unity.Services.Authentication.Shared.ApiException(global::Unity.Services.Authentication.Shared.ApiExceptionType.InvalidParameters, "Missing required parameter 'playerId' when calling PlayerNamesApi->UpdateName");
			}
			if (updateNameRequest == null)
			{
				throw new global::Unity.Services.Authentication.Shared.ApiException(global::Unity.Services.Authentication.Shared.ApiExceptionType.InvalidParameters, "Missing required parameter 'updateNameRequest' when calling PlayerNamesApi->UpdateName");
			}
			global::Unity.Services.Authentication.Shared.ApiRequestOptions apiRequestOptions = new global::Unity.Services.Authentication.Shared.ApiRequestOptions();
			string[] contentTypes = new string[1] { "application/json" };
			string[] accepts = new string[2] { "application/json", "application/problem+json" };
			string text = global::Unity.Services.Authentication.Shared.ApiUtils.SelectHeaderContentType(contentTypes);
			if (text != null)
			{
				apiRequestOptions.HeaderParameters.Add("Content-Type", text);
			}
			string text2 = global::Unity.Services.Authentication.Shared.ApiUtils.SelectHeaderAccept(accepts);
			if (text2 != null)
			{
				apiRequestOptions.HeaderParameters.Add("Accept", text2);
			}
			apiRequestOptions.PathParameters.Add("playerId", global::Unity.Services.Authentication.Shared.ApiUtils.ParameterToString(Configuration, playerId));
			apiRequestOptions.Data = updateNameRequest;
			apiRequestOptions.Operation = "PlayerNamesApi.UpdateName";
			if (!string.IsNullOrEmpty(Configuration.AccessToken) && !apiRequestOptions.HeaderParameters.ContainsKey("Authorization"))
			{
				apiRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + Configuration.AccessToken);
			}
			return await Client.PostAsync<global::Unity.Services.Authentication.Generated.Player>("/names/{playerId}", apiRequestOptions, Configuration, cancellationToken);
		}
	}
}
