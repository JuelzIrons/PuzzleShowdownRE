namespace Unity.Networking.Transport
{
	internal static class MultiNetworkDriverExtensions
	{
		internal static ref global::Unity.Networking.Transport.NetworkDriver GetDriverRef(this ref global::Unity.Networking.Transport.MultiNetworkDriver multiDriver, int id)
		{
			return id switch
			{
				1 => ref multiDriver.Driver1, 
				2 => ref multiDriver.Driver2, 
				3 => ref multiDriver.Driver3, 
				4 => ref multiDriver.Driver4, 
				_ => throw new global::System.ArgumentException($"Invalid driver ID {id}."), 
			};
		}

		internal static ref global::Unity.Networking.Transport.NetworkDriver.Concurrent GetDriverRef(this ref global::Unity.Networking.Transport.MultiNetworkDriver.Concurrent multiDriver, int id)
		{
			return id switch
			{
				1 => ref multiDriver.Driver1, 
				2 => ref multiDriver.Driver2, 
				3 => ref multiDriver.Driver3, 
				4 => ref multiDriver.Driver4, 
				_ => throw new global::System.ArgumentException($"Invalid driver ID {id}."), 
			};
		}
	}
}
