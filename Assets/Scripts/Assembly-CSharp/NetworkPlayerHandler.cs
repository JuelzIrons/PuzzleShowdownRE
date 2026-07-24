public class NetworkPlayerHandler : global::Unity.Netcode.NetworkBehaviour
{
	[global::UnityEngine.SerializeField]
	private PodManager[] PodManagers;

	public void AssignLocalPlayerToController()
	{
	}

	protected override void __initializeVariables()
	{
		base.__initializeVariables();
	}

	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	protected internal override string __getTypeName()
	{
		return "NetworkPlayerHandler";
	}
}
