namespace UnityEngine.InputSystem.LowLevel
{
	internal static class InputUpdate
	{
		[global::System.Serializable]
		public struct UpdateStepCount
		{
			private bool m_WasUpdated;

			public uint value { get; private set; }

			public void OnBeforeUpdate()
			{
				m_WasUpdated = true;
				value++;
			}

			public void OnUpdate()
			{
				if (!m_WasUpdated)
				{
					value++;
				}
				m_WasUpdated = false;
			}
		}

		[global::System.Serializable]
		public struct SerializedState
		{
			public global::UnityEngine.InputSystem.LowLevel.InputUpdateType lastUpdateType;

			public global::UnityEngine.InputSystem.LowLevel.InputUpdate.UpdateStepCount playerUpdateStepCount;
		}

		public static uint s_UpdateStepCount;

		public static global::UnityEngine.InputSystem.LowLevel.InputUpdateType s_LatestUpdateType;

		public static global::UnityEngine.InputSystem.LowLevel.InputUpdate.UpdateStepCount s_PlayerUpdateStepCount;

		internal static void OnBeforeUpdate(global::UnityEngine.InputSystem.LowLevel.InputUpdateType type)
		{
			s_LatestUpdateType = type;
			if ((uint)(type - 1) <= 1u || type == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Manual)
			{
				s_PlayerUpdateStepCount.OnBeforeUpdate();
				s_UpdateStepCount = s_PlayerUpdateStepCount.value;
			}
		}

		internal static void OnUpdate(global::UnityEngine.InputSystem.LowLevel.InputUpdateType type)
		{
			s_LatestUpdateType = type;
			if ((uint)(type - 1) <= 1u || type == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Manual)
			{
				s_PlayerUpdateStepCount.OnUpdate();
				s_UpdateStepCount = s_PlayerUpdateStepCount.value;
			}
		}

		public static global::UnityEngine.InputSystem.LowLevel.InputUpdate.SerializedState Save()
		{
			return new global::UnityEngine.InputSystem.LowLevel.InputUpdate.SerializedState
			{
				lastUpdateType = s_LatestUpdateType,
				playerUpdateStepCount = s_PlayerUpdateStepCount
			};
		}

		public static void Restore(global::UnityEngine.InputSystem.LowLevel.InputUpdate.SerializedState state)
		{
			s_LatestUpdateType = state.lastUpdateType;
			s_PlayerUpdateStepCount = state.playerUpdateStepCount;
			global::UnityEngine.InputSystem.LowLevel.InputUpdateType inputUpdateType = s_LatestUpdateType;
			if ((uint)(inputUpdateType - 1) <= 1u || inputUpdateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Manual)
			{
				s_UpdateStepCount = s_PlayerUpdateStepCount.value;
			}
			else
			{
				s_UpdateStepCount = 0u;
			}
		}

		public static global::UnityEngine.InputSystem.LowLevel.InputUpdateType GetUpdateTypeForPlayer(this global::UnityEngine.InputSystem.LowLevel.InputUpdateType mask)
		{
			if ((mask & global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Manual) != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None)
			{
				return global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Manual;
			}
			if ((mask & global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Dynamic) != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None)
			{
				return global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Dynamic;
			}
			if ((mask & global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Fixed) != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None)
			{
				return global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Fixed;
			}
			return global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None;
		}

		public static bool IsPlayerUpdate(this global::UnityEngine.InputSystem.LowLevel.InputUpdateType updateType)
		{
			if (updateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.Editor)
			{
				return false;
			}
			return updateType != global::UnityEngine.InputSystem.LowLevel.InputUpdateType.None;
		}
	}
}
