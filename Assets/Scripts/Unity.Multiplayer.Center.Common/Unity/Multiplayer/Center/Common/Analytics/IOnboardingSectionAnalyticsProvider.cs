namespace Unity.Multiplayer.Center.Common.Analytics
{
	public interface IOnboardingSectionAnalyticsProvider
	{
		void SendInteractionEvent(global::Unity.Multiplayer.Center.Common.Analytics.InteractionDataType type, string displayName);
	}
}
