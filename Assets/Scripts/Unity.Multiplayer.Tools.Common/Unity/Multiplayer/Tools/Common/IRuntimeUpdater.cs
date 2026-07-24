namespace Unity.Multiplayer.Tools.Common
{
	public interface IRuntimeUpdater
	{
		event global::System.Action OnStart;

		event global::System.Action OnAwake;

		event global::System.Action OnUpdate;

		event global::System.Action OnFixedUpdate;

		event global::System.Action OnLateUpdate;

		event global::System.Action OnDestroyed;
	}
}
