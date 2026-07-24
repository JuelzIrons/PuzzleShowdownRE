namespace UnityEngine.InputSystem
{
	[global::System.Serializable]
	public struct InputActionProperty : global::System.IEquatable<global::UnityEngine.InputSystem.InputActionProperty>, global::System.IEquatable<global::UnityEngine.InputSystem.InputAction>, global::System.IEquatable<global::UnityEngine.InputSystem.InputActionReference>
	{
		[global::UnityEngine.SerializeField]
		private bool m_UseReference;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.InputAction m_Action;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.InputSystem.InputActionReference m_Reference;

		public global::UnityEngine.InputSystem.InputAction action
		{
			get
			{
				if (!m_UseReference)
				{
					return m_Action;
				}
				if (!(m_Reference != null))
				{
					return null;
				}
				return m_Reference.action;
			}
		}

		public global::UnityEngine.InputSystem.InputActionReference reference
		{
			get
			{
				if (!m_UseReference)
				{
					return null;
				}
				return m_Reference;
			}
		}

		internal global::UnityEngine.InputSystem.InputAction serializedAction => m_Action;

		internal global::UnityEngine.InputSystem.InputActionReference serializedReference => m_Reference;

		public InputActionProperty(global::UnityEngine.InputSystem.InputAction action)
		{
			m_UseReference = false;
			m_Action = action;
			m_Reference = null;
		}

		public InputActionProperty(global::UnityEngine.InputSystem.InputActionReference reference)
		{
			m_UseReference = true;
			m_Action = null;
			m_Reference = reference;
		}

		public bool Equals(global::UnityEngine.InputSystem.InputActionProperty other)
		{
			if (m_Reference == other.m_Reference && m_UseReference == other.m_UseReference)
			{
				return m_Action == other.m_Action;
			}
			return false;
		}

		public bool Equals(global::UnityEngine.InputSystem.InputAction other)
		{
			return action == other;
		}

		public bool Equals(global::UnityEngine.InputSystem.InputActionReference other)
		{
			return m_Reference == other;
		}

		public override bool Equals(object obj)
		{
			if (m_UseReference)
			{
				return Equals(obj as global::UnityEngine.InputSystem.InputActionReference);
			}
			return Equals(obj as global::UnityEngine.InputSystem.InputAction);
		}

		public override int GetHashCode()
		{
			if (m_UseReference)
			{
				if (!(m_Reference != null))
				{
					return 0;
				}
				return m_Reference.GetHashCode();
			}
			if (m_Action == null)
			{
				return 0;
			}
			return m_Action.GetHashCode();
		}

		public static bool operator ==(global::UnityEngine.InputSystem.InputActionProperty left, global::UnityEngine.InputSystem.InputActionProperty right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.InputSystem.InputActionProperty left, global::UnityEngine.InputSystem.InputActionProperty right)
		{
			return !left.Equals(right);
		}
	}
}
