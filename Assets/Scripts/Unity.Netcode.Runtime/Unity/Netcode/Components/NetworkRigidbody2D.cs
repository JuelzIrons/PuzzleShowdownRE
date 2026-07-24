namespace Unity.Netcode.Components
{
	[global::UnityEngine.RequireComponent(typeof(global::Unity.Netcode.Components.NetworkTransform))]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.Rigidbody2D))]
	[global::UnityEngine.AddComponentMenu("Netcode/Network Rigidbody 2D")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.netcode.gameobjects@latest/?subfolder=/manual/components/helper/networkrigidbody.html#networkrigidbody2d")]
	public class NetworkRigidbody2D : global::Unity.Netcode.Components.NetworkRigidbodyBase
	{
		public global::UnityEngine.Rigidbody2D Rigidbody2D => base.m_InternalRigidbody2D;

		protected virtual void Awake()
		{
			Initialize(global::Unity.Netcode.Components.NetworkRigidbodyBase.RigidbodyTypes.Rigidbody2D);
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
			return "NetworkRigidbody2D";
		}
	}
}
