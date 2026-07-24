namespace Unity.Netcode
{
	internal interface IAnticipatedObject
	{
		global::Unity.Netcode.NetworkObject OwnerObject { get; }

		void Update();

		void ResetAnticipation();
	}
}
