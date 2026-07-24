namespace Unity.Services.Core.Environments.Internal
{
	internal class Environments : global::Unity.Services.Core.Environments.Internal.IEnvironments, global::Unity.Services.Core.Internal.IServiceComponent
	{
		private string m_Current;

		public string Current
		{
			get
			{
				return m_Current;
			}
			internal set
			{
				m_Current = value;
			}
		}
	}
}
