[global::Unity.Jobs.DOTSCompilerGenerated]
internal class __JobReflectionRegistrationOutput__12582928334401220093
{
	public static void CreateJobReflectionData()
	{
		try
		{
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.IPCNetworkInterface.SendUpdate>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.IPCNetworkInterface.ReceiveJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.BottomLayer.ConnectionListCleanup>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.BottomLayer.ClearJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.DTLSLayer.ReceiveJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.DTLSLayer.SendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.LogLayer.LogJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.RelayLayer.SendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.SimpleConnectionLayer.SendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.SimulatorLayer.SimulatorReceiveJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.SimulatorLayer.SimulatorSendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.StreamSegmentationLayer.SendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.StreamToDatagramLayer.SendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.StreamToDatagramLayer.ReceiveJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.TLSLayer.ReceiveJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.TLSLayer.SendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.TopLayer.CompleteReceiveJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.WebSocketLayer.SendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.WebSocketLayer.ReceiveJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.NetworkDriver.UpdateJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.NetworkDriver.ClearEventQueue>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.NetworkDriverSender.DequeuePacketsJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.PacketsQueue.FillBufferPointers>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.TCPNetworkInterface.ReceiveJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.TCPNetworkInterface.SendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.UDPNetworkInterface.FlushSendJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.UDPNetworkInterface.ReceiveJob>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.SimpleConnectionLayer.ReceiveJob<global::Unity.Networking.Transport.UnderlyingConnectionList>>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.SimpleConnectionLayer.ReceiveJob<global::Unity.Networking.Transport.NullUnderlyingConnectionList>>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.RelayLayer.ReceiveJob<global::Unity.Networking.Transport.UnderlyingConnectionList>>();
			global::Unity.Jobs.IJobExtensions.EarlyJobInit<global::Unity.Networking.Transport.RelayLayer.ReceiveJob<global::Unity.Networking.Transport.NullUnderlyingConnectionList>>();
		}
		catch (global::System.Exception ex)
		{
			global::Unity.Jobs.EarlyInitHelpers.JobReflectionDataCreationFailed(ex);
		}
	}

	[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	public static void EarlyInit()
	{
		CreateJobReflectionData();
	}
}
