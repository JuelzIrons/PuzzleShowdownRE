namespace Unity.Services.Authentication
{
	internal class EnvironmentIdComponent : global::Unity.Services.Authentication.Internal.IEnvironmentId, global::Unity.Services.Core.Internal.IServiceComponent
	{
		private string m_EnvironmentId;

		public string EnvironmentId
		{
			get
			{
				return m_EnvironmentId;
			}
			internal set
			{
				m_EnvironmentId = value;
			}
		}

		internal EnvironmentIdComponent()
		{
		}
	}
}
