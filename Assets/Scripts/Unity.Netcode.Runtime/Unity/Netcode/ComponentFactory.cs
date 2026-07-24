namespace Unity.Netcode
{
	internal static class ComponentFactory
	{
		internal delegate object CreateObjectDelegate(global::Unity.Netcode.NetworkManager networkManager);

		private static global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Netcode.ComponentFactory.CreateObjectDelegate> s_Delegates = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.Netcode.ComponentFactory.CreateObjectDelegate>();

		public static T Create<T>(global::Unity.Netcode.NetworkManager networkManager)
		{
			return (T)s_Delegates[typeof(T)](networkManager);
		}

		public static void Register<T>(global::Unity.Netcode.ComponentFactory.CreateObjectDelegate creator)
		{
			s_Delegates[typeof(T)] = creator;
		}

		public static void Deregister<T>()
		{
			s_Delegates.Remove(typeof(T));
			SetDefaults();
		}

		public static void SetDefaults()
		{
			SetDefault<global::Unity.Netcode.IDeferredNetworkMessageManager>((global::Unity.Netcode.NetworkManager networkManager) => new global::Unity.Netcode.DeferredMessageManager(networkManager));
			SetDefault<global::Unity.Netcode.IRealTimeProvider>((global::Unity.Netcode.NetworkManager networkManager) => new global::Unity.Netcode.RealTimeProvider());
		}

		private static void SetDefault<T>(global::Unity.Netcode.ComponentFactory.CreateObjectDelegate creator)
		{
			if (!s_Delegates.ContainsKey(typeof(T)))
			{
				s_Delegates[typeof(T)] = creator;
			}
		}
	}
}
