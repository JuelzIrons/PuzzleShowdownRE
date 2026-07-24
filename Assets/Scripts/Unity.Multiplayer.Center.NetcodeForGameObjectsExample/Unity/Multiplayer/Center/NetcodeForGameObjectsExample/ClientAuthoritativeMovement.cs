namespace Unity.Multiplayer.Center.NetcodeForGameObjectsExample
{
	public class ClientAuthoritativeMovement : global::Unity.Netcode.NetworkBehaviour
	{
		public float Speed = 5f;

		private void Update()
		{
			if (base.IsOwner && base.IsSpawned)
			{
				float num = Speed * global::UnityEngine.Time.deltaTime;
				if (global::UnityEngine.InputSystem.Keyboard.current.aKey.isPressed)
				{
					base.transform.position += new global::UnityEngine.Vector3(0f - num, 0f, 0f);
				}
				else if (global::UnityEngine.InputSystem.Keyboard.current.dKey.isPressed)
				{
					base.transform.position += new global::UnityEngine.Vector3(num, 0f, 0f);
				}
				else if (global::UnityEngine.InputSystem.Keyboard.current.wKey.isPressed)
				{
					base.transform.position += new global::UnityEngine.Vector3(0f, 0f, num);
				}
				else if (global::UnityEngine.InputSystem.Keyboard.current.sKey.isPressed)
				{
					base.transform.position += new global::UnityEngine.Vector3(0f, 0f, 0f - num);
				}
			}
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
			return "ClientAuthoritativeMovement";
		}
	}
}
