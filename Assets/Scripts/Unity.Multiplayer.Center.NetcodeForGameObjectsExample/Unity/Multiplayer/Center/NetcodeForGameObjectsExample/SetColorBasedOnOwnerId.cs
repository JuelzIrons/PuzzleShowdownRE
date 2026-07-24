namespace Unity.Multiplayer.Center.NetcodeForGameObjectsExample
{
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Renderer))]
	public class SetColorBasedOnOwnerId : global::Unity.Netcode.NetworkBehaviour
	{
		public override void OnNetworkSpawn()
		{
			base.OnNetworkSpawn();
			SetColorBasedOnOwner();
		}

		protected override void OnOwnershipChanged(ulong previous, ulong current)
		{
			SetColorBasedOnOwner();
		}

		private void SetColorBasedOnOwner()
		{
			global::UnityEngine.Random.InitState((int)base.OwnerClientId);
			GetComponent<global::UnityEngine.Renderer>().material.color = global::UnityEngine.Random.ColorHSV();
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
			return "SetColorBasedOnOwnerId";
		}
	}
}
