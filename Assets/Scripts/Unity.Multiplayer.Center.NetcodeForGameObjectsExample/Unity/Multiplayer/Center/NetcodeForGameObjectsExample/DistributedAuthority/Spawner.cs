namespace Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority
{
	public class Spawner : global::Unity.Netcode.NetworkBehaviour
	{
		public global::UnityEngine.GameObject PrefabToSpawn;

		private void Update()
		{
			if (base.IsSpawned && base.HasAuthority && global::UnityEngine.InputSystem.Keyboard.current.spaceKey.wasReleasedThisFrame)
			{
				SpawnSphere();
			}
		}

		private void SpawnSphere()
		{
			global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(PrefabToSpawn);
			obj.transform.position = base.transform.position;
			obj.GetComponent<global::Unity.Netcode.NetworkObject>().Spawn();
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
			return "Spawner";
		}
	}
}
