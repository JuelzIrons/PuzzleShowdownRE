namespace Unity.Multiplayer.Tools.Adapters
{
	internal interface INetworkAdapter
	{
		global::Unity.Multiplayer.Tools.Adapters.AdapterMetadata Metadata { get; }

		[global::JetBrains.Annotations.CanBeNull]
		T GetComponent<T>() where T : class, global::Unity.Multiplayer.Tools.Adapters.IAdapterComponent;
	}
}
