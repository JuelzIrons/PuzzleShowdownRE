namespace Unity.Netcode
{
	internal interface IAnticipationEventReceiver
	{
		void SetupForUpdate();

		void SetupForRender();
	}
}
