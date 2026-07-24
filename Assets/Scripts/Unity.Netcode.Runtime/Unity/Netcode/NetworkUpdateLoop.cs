namespace Unity.Netcode
{
	public static class NetworkUpdateLoop
	{
		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct NetworkInitialization
		{
			public static global::UnityEngine.LowLevel.PlayerLoopSystem CreateLoopSystem()
			{
				return new global::UnityEngine.LowLevel.PlayerLoopSystem
				{
					type = typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkInitialization),
					updateDelegate = delegate
					{
						RunNetworkUpdateStage(global::Unity.Netcode.NetworkUpdateStage.Initialization);
					}
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct NetworkEarlyUpdate
		{
			public static global::UnityEngine.LowLevel.PlayerLoopSystem CreateLoopSystem()
			{
				return new global::UnityEngine.LowLevel.PlayerLoopSystem
				{
					type = typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkEarlyUpdate),
					updateDelegate = delegate
					{
						RunNetworkUpdateStage(global::Unity.Netcode.NetworkUpdateStage.EarlyUpdate);
					}
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct NetworkFixedUpdate
		{
			public static global::UnityEngine.LowLevel.PlayerLoopSystem CreateLoopSystem()
			{
				return new global::UnityEngine.LowLevel.PlayerLoopSystem
				{
					type = typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkFixedUpdate),
					updateDelegate = delegate
					{
						RunNetworkUpdateStage(global::Unity.Netcode.NetworkUpdateStage.FixedUpdate);
					}
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct NetworkPreUpdate
		{
			public static global::UnityEngine.LowLevel.PlayerLoopSystem CreateLoopSystem()
			{
				return new global::UnityEngine.LowLevel.PlayerLoopSystem
				{
					type = typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkPreUpdate),
					updateDelegate = delegate
					{
						RunNetworkUpdateStage(global::Unity.Netcode.NetworkUpdateStage.PreUpdate);
					}
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct NetworkUpdate
		{
			public static global::UnityEngine.LowLevel.PlayerLoopSystem CreateLoopSystem()
			{
				return new global::UnityEngine.LowLevel.PlayerLoopSystem
				{
					type = typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkUpdate),
					updateDelegate = delegate
					{
						RunNetworkUpdateStage(global::Unity.Netcode.NetworkUpdateStage.Update);
					}
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct NetworkPreLateUpdate
		{
			public static global::UnityEngine.LowLevel.PlayerLoopSystem CreateLoopSystem()
			{
				return new global::UnityEngine.LowLevel.PlayerLoopSystem
				{
					type = typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkPreLateUpdate),
					updateDelegate = delegate
					{
						RunNetworkUpdateStage(global::Unity.Netcode.NetworkUpdateStage.PreLateUpdate);
					}
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct NetworkPostScriptLateUpdate
		{
			public static global::UnityEngine.LowLevel.PlayerLoopSystem CreateLoopSystem()
			{
				return new global::UnityEngine.LowLevel.PlayerLoopSystem
				{
					type = typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkPostScriptLateUpdate),
					updateDelegate = delegate
					{
						RunNetworkUpdateStage(global::Unity.Netcode.NetworkUpdateStage.PostScriptLateUpdate);
					}
				};
			}
		}

		[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Size = 1)]
		internal struct NetworkPostLateUpdate
		{
			public static global::UnityEngine.LowLevel.PlayerLoopSystem CreateLoopSystem()
			{
				return new global::UnityEngine.LowLevel.PlayerLoopSystem
				{
					type = typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkPostLateUpdate),
					updateDelegate = delegate
					{
						RunNetworkUpdateStage(global::Unity.Netcode.NetworkUpdateStage.PostLateUpdate);
					}
				};
			}
		}

		private enum LoopSystemPosition
		{
			After = 0,
			Before = 1
		}

		private static global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkUpdateStage, global::System.Collections.Generic.HashSet<global::Unity.Netcode.INetworkUpdateSystem>> s_UpdateSystem_Sets;

		private static global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkUpdateStage, global::Unity.Netcode.INetworkUpdateSystem[]> s_UpdateSystem_Arrays;

		private const int k_UpdateSystem_InitialArrayCapacity = 1024;

		public static global::Unity.Netcode.NetworkUpdateStage UpdateStage;

		static NetworkUpdateLoop()
		{
			s_UpdateSystem_Sets = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkUpdateStage, global::System.Collections.Generic.HashSet<global::Unity.Netcode.INetworkUpdateSystem>>();
			s_UpdateSystem_Arrays = new global::System.Collections.Generic.Dictionary<global::Unity.Netcode.NetworkUpdateStage, global::Unity.Netcode.INetworkUpdateSystem[]>();
			foreach (global::Unity.Netcode.NetworkUpdateStage value in global::System.Enum.GetValues(typeof(global::Unity.Netcode.NetworkUpdateStage)))
			{
				s_UpdateSystem_Sets.Add(value, new global::System.Collections.Generic.HashSet<global::Unity.Netcode.INetworkUpdateSystem>());
				s_UpdateSystem_Arrays.Add(value, new global::Unity.Netcode.INetworkUpdateSystem[1024]);
			}
		}

		public static void RegisterAllNetworkUpdates(this global::Unity.Netcode.INetworkUpdateSystem updateSystem)
		{
			foreach (global::Unity.Netcode.NetworkUpdateStage value in global::System.Enum.GetValues(typeof(global::Unity.Netcode.NetworkUpdateStage)))
			{
				updateSystem.RegisterNetworkUpdate(value);
			}
		}

		public static void RegisterNetworkUpdate(this global::Unity.Netcode.INetworkUpdateSystem updateSystem, global::Unity.Netcode.NetworkUpdateStage updateStage = global::Unity.Netcode.NetworkUpdateStage.Update)
		{
			global::System.Collections.Generic.HashSet<global::Unity.Netcode.INetworkUpdateSystem> hashSet = s_UpdateSystem_Sets[updateStage];
			if (!hashSet.Contains(updateSystem))
			{
				hashSet.Add(updateSystem);
				int count = hashSet.Count;
				global::Unity.Netcode.INetworkUpdateSystem[] array = s_UpdateSystem_Arrays[updateStage];
				int num = array.Length;
				if (count > num)
				{
					global::Unity.Netcode.INetworkUpdateSystem[] array2 = (s_UpdateSystem_Arrays[updateStage] = new global::Unity.Netcode.INetworkUpdateSystem[num *= 2]);
					array = array2;
				}
				hashSet.CopyTo(array);
				if (count < num)
				{
					array[count] = null;
				}
			}
		}

		public static void UnregisterAllNetworkUpdates(this global::Unity.Netcode.INetworkUpdateSystem updateSystem)
		{
			foreach (global::Unity.Netcode.NetworkUpdateStage value in global::System.Enum.GetValues(typeof(global::Unity.Netcode.NetworkUpdateStage)))
			{
				updateSystem.UnregisterNetworkUpdate(value);
			}
		}

		public static void UnregisterNetworkUpdate(this global::Unity.Netcode.INetworkUpdateSystem updateSystem, global::Unity.Netcode.NetworkUpdateStage updateStage = global::Unity.Netcode.NetworkUpdateStage.Update)
		{
			global::System.Collections.Generic.HashSet<global::Unity.Netcode.INetworkUpdateSystem> hashSet = s_UpdateSystem_Sets[updateStage];
			if (hashSet.Contains(updateSystem))
			{
				hashSet.Remove(updateSystem);
				int count = hashSet.Count;
				global::Unity.Netcode.INetworkUpdateSystem[] array = s_UpdateSystem_Arrays[updateStage];
				int num = array.Length;
				hashSet.CopyTo(array);
				if (count < num)
				{
					array[count] = null;
				}
			}
		}

		internal static void RunNetworkUpdateStage(global::Unity.Netcode.NetworkUpdateStage updateStage)
		{
			UpdateStage = updateStage;
			global::Unity.Netcode.INetworkUpdateSystem[] array = s_UpdateSystem_Arrays[updateStage];
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				global::Unity.Netcode.INetworkUpdateSystem networkUpdateSystem = array[i];
				if (networkUpdateSystem != null)
				{
					networkUpdateSystem.NetworkUpdate(updateStage);
					continue;
				}
				break;
			}
		}

		[global::UnityEngine.RuntimeInitializeOnLoadMethod(global::UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Initialize()
		{
			UnregisterLoopSystems();
			RegisterLoopSystems();
		}

		private static bool TryAddLoopSystem(ref global::UnityEngine.LowLevel.PlayerLoopSystem parentLoopSystem, global::UnityEngine.LowLevel.PlayerLoopSystem childLoopSystem, global::System.Type anchorSystemType, global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition loopSystemPosition)
		{
			int num = -1;
			if (anchorSystemType != null)
			{
				for (int i = 0; i < parentLoopSystem.subSystemList.Length; i++)
				{
					if (parentLoopSystem.subSystemList[i].type == anchorSystemType)
					{
						num = ((loopSystemPosition == global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.After) ? (i + 1) : i);
						break;
					}
				}
			}
			else
			{
				num = ((loopSystemPosition == global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.After) ? parentLoopSystem.subSystemList.Length : 0);
			}
			if (num == -1)
			{
				return false;
			}
			global::UnityEngine.LowLevel.PlayerLoopSystem[] array = new global::UnityEngine.LowLevel.PlayerLoopSystem[parentLoopSystem.subSystemList.Length + 1];
			if (num > 0)
			{
				global::System.Array.Copy(parentLoopSystem.subSystemList, array, num);
			}
			array[num] = childLoopSystem;
			if (num < parentLoopSystem.subSystemList.Length)
			{
				global::System.Array.Copy(parentLoopSystem.subSystemList, num, array, num + 1, parentLoopSystem.subSystemList.Length - num);
			}
			parentLoopSystem.subSystemList = array;
			return true;
		}

		private static bool TryRemoveLoopSystem(ref global::UnityEngine.LowLevel.PlayerLoopSystem parentLoopSystem, global::System.Type childSystemType)
		{
			int num = -1;
			for (int i = 0; i < parentLoopSystem.subSystemList.Length; i++)
			{
				if (parentLoopSystem.subSystemList[i].type == childSystemType)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return false;
			}
			global::UnityEngine.LowLevel.PlayerLoopSystem[] array = new global::UnityEngine.LowLevel.PlayerLoopSystem[parentLoopSystem.subSystemList.Length - 1];
			if (num > 0)
			{
				global::System.Array.Copy(parentLoopSystem.subSystemList, array, num);
			}
			if (num < parentLoopSystem.subSystemList.Length - 1)
			{
				global::System.Array.Copy(parentLoopSystem.subSystemList, num + 1, array, num, parentLoopSystem.subSystemList.Length - num - 1);
			}
			parentLoopSystem.subSystemList = array;
			return true;
		}

		internal static void RegisterLoopSystems()
		{
			global::UnityEngine.LowLevel.PlayerLoopSystem currentPlayerLoop = global::UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();
			for (int i = 0; i < currentPlayerLoop.subSystemList.Length; i++)
			{
				ref global::UnityEngine.LowLevel.PlayerLoopSystem reference = ref currentPlayerLoop.subSystemList[i];
				if (reference.type == typeof(global::UnityEngine.PlayerLoop.Initialization))
				{
					TryAddLoopSystem(ref reference, global::Unity.Netcode.NetworkUpdateLoop.NetworkInitialization.CreateLoopSystem(), null, global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.After);
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.EarlyUpdate))
				{
					TryAddLoopSystem(ref reference, global::Unity.Netcode.NetworkUpdateLoop.NetworkEarlyUpdate.CreateLoopSystem(), typeof(global::UnityEngine.PlayerLoop.EarlyUpdate.ScriptRunDelayedStartupFrame), global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.Before);
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.FixedUpdate))
				{
					TryAddLoopSystem(ref reference, global::Unity.Netcode.NetworkUpdateLoop.NetworkFixedUpdate.CreateLoopSystem(), typeof(global::UnityEngine.PlayerLoop.FixedUpdate.ScriptRunBehaviourFixedUpdate), global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.Before);
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.PreUpdate))
				{
					TryAddLoopSystem(ref reference, global::Unity.Netcode.NetworkUpdateLoop.NetworkPreUpdate.CreateLoopSystem(), typeof(global::UnityEngine.PlayerLoop.PreUpdate.PhysicsUpdate), global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.Before);
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.Update))
				{
					TryAddLoopSystem(ref reference, global::Unity.Netcode.NetworkUpdateLoop.NetworkUpdate.CreateLoopSystem(), typeof(global::UnityEngine.PlayerLoop.Update.ScriptRunBehaviourUpdate), global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.Before);
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.PreLateUpdate))
				{
					TryAddLoopSystem(ref reference, global::Unity.Netcode.NetworkUpdateLoop.NetworkPreLateUpdate.CreateLoopSystem(), typeof(global::UnityEngine.PlayerLoop.PreLateUpdate.ScriptRunBehaviourLateUpdate), global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.Before);
					TryAddLoopSystem(ref reference, global::Unity.Netcode.NetworkUpdateLoop.NetworkPostScriptLateUpdate.CreateLoopSystem(), typeof(global::UnityEngine.PlayerLoop.PreLateUpdate.ScriptRunBehaviourLateUpdate), global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.After);
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.PostLateUpdate))
				{
					TryAddLoopSystem(ref reference, global::Unity.Netcode.NetworkUpdateLoop.NetworkPostLateUpdate.CreateLoopSystem(), typeof(global::UnityEngine.PlayerLoop.PostLateUpdate.PlayerSendFrameComplete), global::Unity.Netcode.NetworkUpdateLoop.LoopSystemPosition.After);
				}
			}
			global::UnityEngine.LowLevel.PlayerLoop.SetPlayerLoop(currentPlayerLoop);
		}

		internal static void UnregisterLoopSystems()
		{
			global::UnityEngine.LowLevel.PlayerLoopSystem currentPlayerLoop = global::UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();
			for (int i = 0; i < currentPlayerLoop.subSystemList.Length; i++)
			{
				ref global::UnityEngine.LowLevel.PlayerLoopSystem reference = ref currentPlayerLoop.subSystemList[i];
				if (reference.type == typeof(global::UnityEngine.PlayerLoop.Initialization))
				{
					TryRemoveLoopSystem(ref reference, typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkInitialization));
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.EarlyUpdate))
				{
					TryRemoveLoopSystem(ref reference, typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkEarlyUpdate));
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.FixedUpdate))
				{
					TryRemoveLoopSystem(ref reference, typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkFixedUpdate));
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.PreUpdate))
				{
					TryRemoveLoopSystem(ref reference, typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkPreUpdate));
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.Update))
				{
					TryRemoveLoopSystem(ref reference, typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkUpdate));
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.PreLateUpdate))
				{
					TryRemoveLoopSystem(ref reference, typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkPreLateUpdate));
					TryRemoveLoopSystem(ref reference, typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkPostScriptLateUpdate));
				}
				else if (reference.type == typeof(global::UnityEngine.PlayerLoop.PostLateUpdate))
				{
					TryRemoveLoopSystem(ref reference, typeof(global::Unity.Netcode.NetworkUpdateLoop.NetworkPostLateUpdate));
				}
			}
			global::UnityEngine.LowLevel.PlayerLoop.SetPlayerLoop(currentPlayerLoop);
		}
	}
}
