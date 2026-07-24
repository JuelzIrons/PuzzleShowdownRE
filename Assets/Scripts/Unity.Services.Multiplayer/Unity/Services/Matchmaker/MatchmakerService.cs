namespace Unity.Services.Matchmaker
{
	public static class MatchmakerService
	{
		private static global::Unity.Services.Matchmaker.IMatchmakerService m_Service;

		private static readonly global::Unity.Services.Matchmaker.Configuration m_Configuration;

		public static global::Unity.Services.Matchmaker.IMatchmakerService Instance
		{
			get
			{
				if (m_Service == null)
				{
					throw new global::System.InvalidOperationException("Attempting to call Matchmaker Services requires initializing Core Registry. Call 'UnityServices.InitializeAsync' first!");
				}
				return m_Service;
			}
			internal set
			{
				m_Service = value;
			}
		}

		static MatchmakerService()
		{
			m_Configuration = new global::Unity.Services.Matchmaker.Configuration("https://matchmaker.services.api.unity.com", 10, 4, null);
		}
	}
}
