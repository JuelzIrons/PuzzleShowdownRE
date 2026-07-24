namespace UnityEngine.InputSystem.Utilities
{
	internal sealed class SavedStructState<T> : global::UnityEngine.InputSystem.Utilities.ISavedState where T : struct
	{
		public delegate void TypedRestore(ref T state);

		private T m_State;

		private global::UnityEngine.InputSystem.Utilities.SavedStructState<T>.TypedRestore m_RestoreAction;

		private global::System.Action m_StaticDisposeCurrentState;

		internal SavedStructState(ref T state, global::UnityEngine.InputSystem.Utilities.SavedStructState<T>.TypedRestore restoreAction, global::System.Action staticDisposeCurrentState = null)
		{
			m_State = state;
			m_RestoreAction = restoreAction;
			m_StaticDisposeCurrentState = staticDisposeCurrentState;
		}

		public void StaticDisposeCurrentState()
		{
			if (m_StaticDisposeCurrentState != null)
			{
				m_StaticDisposeCurrentState();
				m_StaticDisposeCurrentState = null;
			}
		}

		public void RestoreSavedState()
		{
			m_RestoreAction(ref m_State);
			m_RestoreAction = null;
		}
	}
}
