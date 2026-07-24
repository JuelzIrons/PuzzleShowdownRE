namespace Unity.Multiplayer.Center.NetcodeForGameObjectsExample.DistributedAuthority
{
	public class SphereMovement : global::Unity.Netcode.NetworkBehaviour
	{
		public float Frequency = 1f;

		public float Amplitude = 1.5f;

		public float BaseHeight = 1.5f;

		private global::Unity.Netcode.NetworkVariable<int> m_SpawnedTick = new global::Unity.Netcode.NetworkVariable<int>(0);

		public override void OnNetworkSpawn()
		{
			base.OnNetworkSpawn();
			base.name = $"Sphere-{base.NetworkObjectId}";
		}

		protected override void OnNetworkPostSpawn()
		{
			base.OnNetworkPostSpawn();
			if (base.IsSpawned && base.HasAuthority)
			{
				m_SpawnedTick.Value = base.NetworkManager.ServerTime.Tick;
				global::UnityEngine.Debug.Log($"{base.name} Spawned! Tick: {m_SpawnedTick.Value}");
			}
		}

		private void Update()
		{
			if (base.IsSpawned && base.HasAuthority)
			{
				UpdatePosition();
			}
		}

		private void UpdatePosition()
		{
			global::UnityEngine.Vector3 position = base.transform.position;
			global::Unity.Netcode.NetworkTime networkTime = base.NetworkManager.ServerTime.TimeTicksAgo(base.NetworkManager.ServerTime.Tick - m_SpawnedTick.Value);
			float num = global::UnityEngine.Mathf.Sin((base.NetworkManager.ServerTime.TimeAsFloat - networkTime.TimeAsFloat) % (global::System.MathF.PI * 2f) * Frequency);
			position.y = BaseHeight + num * Amplitude;
			base.transform.position = global::UnityEngine.Vector3.Lerp(base.transform.position, position, global::UnityEngine.Time.deltaTime);
		}

		protected override void __initializeVariables()
		{
			if (m_SpawnedTick == null)
			{
				throw new global::System.Exception("SphereMovement.m_SpawnedTick cannot be null. All NetworkVariableBase instances must be initialized.");
			}
			m_SpawnedTick.Initialize(this);
			__nameNetworkVariable(m_SpawnedTick, "m_SpawnedTick");
			NetworkVariableFields.Add(m_SpawnedTick);
			base.__initializeVariables();
		}

		protected override void __initializeRpcs()
		{
			base.__initializeRpcs();
		}

		protected internal override string __getTypeName()
		{
			return "SphereMovement";
		}
	}
}
