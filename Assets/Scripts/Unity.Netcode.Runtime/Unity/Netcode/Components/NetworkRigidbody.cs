namespace Unity.Netcode.Components
{
	[global::UnityEngine.RequireComponent(typeof(global::Unity.Netcode.Components.NetworkTransform))]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Rigidbody))]
	[global::UnityEngine.AddComponentMenu("Netcode/Network Rigidbody")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/manual/components/helper/networkrigidbody.html")]
	public class NetworkRigidbody : global::Unity.Netcode.Components.NetworkRigidbodyBase
	{
		public global::UnityEngine.Rigidbody Rigidbody => base.m_InternalRigidbody;

		protected virtual void Awake()
		{
			Initialize(global::Unity.Netcode.Components.NetworkRigidbodyBase.RigidbodyTypes.Rigidbody);
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
			return "NetworkRigidbody";
		}
	}
}
