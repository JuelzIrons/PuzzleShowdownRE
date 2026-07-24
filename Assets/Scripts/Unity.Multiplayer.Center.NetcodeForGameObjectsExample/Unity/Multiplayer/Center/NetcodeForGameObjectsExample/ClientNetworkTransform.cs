namespace Unity.Multiplayer.Center.NetcodeForGameObjectsExample
{
	[global::UnityEngine.DisallowMultipleComponent]
	public class ClientNetworkTransform : global::Unity.Netcode.Components.NetworkTransform
	{
		protected override bool OnIsServerAuthoritative()
		{
			return false;
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
			return "ClientNetworkTransform";
		}
	}
}
