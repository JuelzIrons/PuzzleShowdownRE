namespace Unity.Multiplayer.Tools.NetworkSimulator.Runtime
{
	public abstract class NetworkScenarioBehaviour : global::Unity.Multiplayer.Tools.NetworkSimulator.Runtime.NetworkScenario
	{
		internal void UpdateScenario(float deltaTime)
		{
			if (!base.IsPaused)
			{
				Update(deltaTime);
			}
		}

		protected abstract void Update(float deltaTime);
	}
}
