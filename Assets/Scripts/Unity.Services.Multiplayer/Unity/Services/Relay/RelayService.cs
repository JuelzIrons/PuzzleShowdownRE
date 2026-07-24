namespace Unity.Services.Relay
{
	public static class RelayService
	{
		private static global::Unity.Services.Relay.IRelayService m_Service;

		public static global::Unity.Services.Relay.IRelayService Instance
		{
			get
			{
				if (m_Service == null)
				{
					throw new global::System.InvalidOperationException("Attempting to call Relay Services requires initializing Core Registry. Call 'UnityServices.InitializeAsync' first!");
				}
				return m_Service;
			}
			internal set
			{
				m_Service = value;
			}
		}
	}
}
