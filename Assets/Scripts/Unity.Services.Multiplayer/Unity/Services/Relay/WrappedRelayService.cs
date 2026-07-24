namespace Unity.Services.Relay
{
	internal class WrappedRelayService : global::Unity.Services.Relay.IRelayService, global::Unity.Services.Relay.IRelayServiceSDK, global::Unity.Services.Relay.IRelayServiceSDKConfiguration
	{
		private const string QosRelayServiceName = "relay";

		internal global::Unity.Services.Relay.IRelayServiceSdk m_RelayService { get; set; }

		public bool EnableQos { get; set; } = true;

		internal WrappedRelayService(global::Unity.Services.Relay.IRelayServiceSdk relayService)
		{
			m_RelayService = relayService;
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Models.Allocation> CreateAllocationAsync(int maxConnections, string region = null)
		{
			EnsureSignedIn();
			if (maxConnections <= 0)
			{
				throw new global::System.ArgumentException("Maximum number of connections for an allocation must be greater than 0!");
			}
			if (EnableQos && m_RelayService.QosResults == null)
			{
				throw new global::System.Exception("Qos component should not be null, check that is is properly initialized.");
			}
			if (EnableQos && string.IsNullOrEmpty(region))
			{
				try
				{
					global::System.Collections.Generic.List<string> regions = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(await ListRegionsAsync(), (global::Unity.Services.Relay.Models.Region r) => r.Id));
					global::System.Collections.Generic.IList<global::Unity.Services.Qos.Internal.QosResult> list = await m_RelayService.QosResults.GetSortedQosResultsAsync("relay", regions);
					if (global::System.Linq.Enumerable.Any(list))
					{
						region = list[0].Region;
					}
				}
				catch (global::System.Exception ex)
				{
					global::Unity.Services.Multiplayer.Logger.LogWarning("Could not do Qos region selection. Will use default." + global::System.Environment.NewLine + "QoS failed due to [" + ex.GetType().Name + "]. Reason: " + ex.Message);
				}
			}
			try
			{
				return (await m_RelayService.AllocationsApi.CreateAllocationAsync(new global::Unity.Services.Relay.RelayAllocations.CreateAllocationRequest(new global::Unity.Services.Relay.Models.AllocationRequest(maxConnections, region)), m_RelayService.Configuration)).Result.Data.Allocation;
			}
			catch (global::Unity.Services.Relay.Http.HttpException<global::Unity.Services.Relay.Models.ErrorResponseBody> ex2)
			{
				throw new global::Unity.Services.Relay.RelayServiceException(ex2.ActualError.GetExceptionReason(), ex2.ActualError.GetExceptionMessage(), ex2);
			}
			catch (global::Unity.Services.Relay.Http.HttpException ex3)
			{
				if (ex3.Response.IsHttpError)
				{
					throw new global::Unity.Services.Relay.RelayServiceException(ex3.Response.GetExceptionReason(), ex3.Response.ErrorMessage, ex3);
				}
				if (ex3.Response.IsNetworkError)
				{
					throw new global::Unity.Services.Relay.RelayServiceException(global::Unity.Services.Relay.RelayExceptionReason.NetworkError, ex3.Response.ErrorMessage);
				}
				throw new global::Unity.Services.Core.RequestFailedException(15999, "Something went wrong.", ex3);
			}
		}

		public async global::System.Threading.Tasks.Task<string> GetJoinCodeAsync(global::System.Guid allocationId)
		{
			EnsureSignedIn();
			if (allocationId == global::System.Guid.Empty)
			{
				throw new global::System.ArgumentNullException("AllocationId cannot be null or empty!");
			}
			try
			{
				return (await m_RelayService.AllocationsApi.CreateJoincodeAsync(new global::Unity.Services.Relay.RelayAllocations.CreateJoincodeRequest(new global::Unity.Services.Relay.Models.JoinCodeRequest(allocationId)), m_RelayService.Configuration)).Result.Data.JoinCode;
			}
			catch (global::Unity.Services.Relay.Http.HttpException<global::Unity.Services.Relay.Models.ErrorResponseBody> ex)
			{
				throw new global::Unity.Services.Relay.RelayServiceException(ex.ActualError.GetExceptionReason(), ex.ActualError.GetExceptionMessage(), ex);
			}
			catch (global::Unity.Services.Relay.Http.HttpException ex2)
			{
				if (ex2.Response.IsHttpError)
				{
					throw new global::Unity.Services.Relay.RelayServiceException(ex2.Response.GetExceptionReason(), ex2.Response.ErrorMessage, ex2);
				}
				if (ex2.Response.IsNetworkError)
				{
					throw new global::Unity.Services.Relay.RelayServiceException(global::Unity.Services.Relay.RelayExceptionReason.NetworkError, ex2.Response.ErrorMessage);
				}
				throw new global::Unity.Services.Core.RequestFailedException(15999, "Something went wrong.", ex2);
			}
		}

		public async global::System.Threading.Tasks.Task<global::Unity.Services.Relay.Models.JoinAllocation> JoinAllocationAsync(string joinCode)
		{
			EnsureSignedIn();
			if (string.IsNullOrWhiteSpace(joinCode))
			{
				throw new global::System.ArgumentNullException("JoinCode must be non-null, non-empty, and cannot contain only whitespace!");
			}
			try
			{
				return (await m_RelayService.AllocationsApi.JoinRelayAsync(new global::Unity.Services.Relay.RelayAllocations.JoinRelayRequest(new global::Unity.Services.Relay.Models.JoinRequest(joinCode)), m_RelayService.Configuration)).Result.Data.Allocation;
			}
			catch (global::Unity.Services.Relay.Http.HttpException<global::Unity.Services.Relay.Models.ErrorResponseBody> ex)
			{
				throw new global::Unity.Services.Relay.RelayServiceException(ex.ActualError.GetExceptionReason(), ex.ActualError.GetExceptionMessage(), ex);
			}
			catch (global::Unity.Services.Relay.Http.HttpException ex2)
			{
				if (ex2.Response.IsHttpError)
				{
					throw new global::Unity.Services.Relay.RelayServiceException(ex2.Response.GetExceptionReason(), ex2.Response.ErrorMessage, ex2);
				}
				if (ex2.Response.IsNetworkError)
				{
					throw new global::Unity.Services.Relay.RelayServiceException(global::Unity.Services.Relay.RelayExceptionReason.NetworkError, ex2.Response.ErrorMessage);
				}
				throw new global::Unity.Services.Core.RequestFailedException(15999, "Something went wrong.", ex2);
			}
		}

		public async global::System.Threading.Tasks.Task<global::System.Collections.Generic.List<global::Unity.Services.Relay.Models.Region>> ListRegionsAsync()
		{
			EnsureSignedIn();
			try
			{
				return (await m_RelayService.AllocationsApi.ListRegionsAsync(new global::Unity.Services.Relay.RelayAllocations.ListRegionsRequest(), m_RelayService.Configuration)).Result.Data.Regions;
			}
			catch (global::Unity.Services.Relay.Http.HttpException<global::Unity.Services.Relay.Models.ErrorResponseBody> ex)
			{
				throw new global::Unity.Services.Relay.RelayServiceException(ex.ActualError.GetExceptionReason(), ex.ActualError.GetExceptionMessage(), ex);
			}
			catch (global::Unity.Services.Relay.Http.HttpException ex2)
			{
				if (ex2.Response.IsHttpError)
				{
					throw new global::Unity.Services.Relay.RelayServiceException(ex2.Response.GetExceptionReason(), ex2.Response.ErrorMessage, ex2);
				}
				if (ex2.Response.IsNetworkError)
				{
					throw new global::Unity.Services.Relay.RelayServiceException(global::Unity.Services.Relay.RelayExceptionReason.NetworkError, ex2.Response.ErrorMessage);
				}
				throw new global::Unity.Services.Core.RequestFailedException(15999, "Something went wrong.", ex2);
			}
		}

		public void SetAllocationsServiceBasePath(string allocationsBasePath)
		{
			m_RelayService.Configuration.BasePath = allocationsBasePath;
		}

		private void EnsureSignedIn()
		{
			if (m_RelayService.AccessToken.AccessToken == null)
			{
				throw new global::Unity.Services.Relay.RelayServiceException(global::Unity.Services.Relay.RelayExceptionReason.Unauthorized, "You are not signed in to the Authentication Service. Please sign in.");
			}
		}
	}
}
