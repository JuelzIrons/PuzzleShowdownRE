namespace UnityEngine.InputSystem
{
	public class InputActionReference : global::UnityEngine.ScriptableObject
	{
		[global::UnityEngine.SerializeField]
		internal global::UnityEngine.InputSystem.InputActionAsset m_Asset;

		[global::UnityEngine.SerializeField]
		internal string m_ActionId;

		[global::System.NonSerialized]
		private global::UnityEngine.InputSystem.InputAction m_Action;

		public global::UnityEngine.InputSystem.InputActionAsset asset
		{
			get
			{
				if (m_Action?.m_ActionMap == null)
				{
					return m_Asset;
				}
				return m_Action.m_ActionMap.asset;
			}
		}

		public global::UnityEngine.InputSystem.InputAction action
		{
			get
			{
				if (m_Action != null && m_Action.actionMap != null && m_Action.actionMap.asset == m_Asset && (bool)m_Asset)
				{
					return m_Action;
				}
				return m_Action = (m_Asset ? m_Asset.FindAction(new global::System.Guid(m_ActionId)) : null);
			}
		}

		public void Set(global::UnityEngine.InputSystem.InputAction action)
		{
			if (action == null)
			{
				m_Asset = null;
				m_ActionId = null;
				m_Action = null;
				base.name = string.Empty;
				return;
			}
			global::UnityEngine.InputSystem.InputActionMap actionMap = action.actionMap;
			if (actionMap == null || actionMap.asset == null)
			{
				throw new global::System.InvalidOperationException($"Action '{action}' must be part of an InputActionAsset in order to be able to create an InputActionReference for it");
			}
			SetInternal(actionMap.asset, action);
		}

		public void Set(global::UnityEngine.InputSystem.InputActionAsset asset, string mapName, string actionName)
		{
			if (asset == null)
			{
				throw new global::System.ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(mapName))
			{
				throw new global::System.ArgumentNullException("mapName");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw new global::System.ArgumentNullException("actionName");
			}
			global::UnityEngine.InputSystem.InputAction inputAction = (asset.FindActionMap(mapName) ?? throw new global::System.ArgumentException($"No action map '{mapName}' in '{asset}'", "mapName")).FindAction(actionName);
			if (inputAction == null)
			{
				throw new global::System.ArgumentException($"No action '{actionName}' in map '{mapName}' of asset '{asset}'", "actionName");
			}
			SetInternal(asset, inputAction);
		}

		private void SetInternal(global::UnityEngine.InputSystem.InputActionAsset assetArg, global::UnityEngine.InputSystem.InputAction actionArg)
		{
			CheckImmutableReference();
			m_Asset = assetArg;
			m_ActionId = actionArg.id.ToString();
			m_Action = actionArg;
			base.name = GetDisplayName(actionArg);
		}

		public override string ToString()
		{
			global::UnityEngine.InputSystem.InputAction inputAction = action;
			if (inputAction == null)
			{
				return base.ToString();
			}
			if (inputAction.actionMap != null)
			{
				if (!(m_Asset != null))
				{
					return inputAction.actionMap.name + "/" + inputAction.name;
				}
				return m_Asset.name + ":" + inputAction.actionMap.name + "/" + inputAction.name;
			}
			if (!(m_Asset != null))
			{
				return m_ActionId;
			}
			return m_Asset.name + ":" + m_ActionId;
		}

		private static string GetDisplayName(global::UnityEngine.InputSystem.InputAction action)
		{
			if (string.IsNullOrEmpty(action?.actionMap?.name))
			{
				return action?.name;
			}
			return action.actionMap?.name + "/" + action.name;
		}

		internal string ToDisplayName()
		{
			if (!string.IsNullOrEmpty(base.name))
			{
				return base.name;
			}
			return GetDisplayName(action);
		}

		public static implicit operator global::UnityEngine.InputSystem.InputAction(global::UnityEngine.InputSystem.InputActionReference reference)
		{
			return reference?.action;
		}

		public static global::UnityEngine.InputSystem.InputActionReference Create(global::UnityEngine.InputSystem.InputAction action)
		{
			global::UnityEngine.InputSystem.InputActionReference inputActionReference = global::UnityEngine.ScriptableObject.CreateInstance<global::UnityEngine.InputSystem.InputActionReference>();
			inputActionReference.Set(action);
			return inputActionReference;
		}

		internal static void InvalidateAll()
		{
			global::UnityEngine.Object[] array = global::UnityEngine.Resources.FindObjectsOfTypeAll(typeof(global::UnityEngine.InputSystem.InputActionReference));
			for (int i = 0; i < array.Length; i++)
			{
				((global::UnityEngine.InputSystem.InputActionReference)array[i]).Invalidate();
			}
		}

		internal void Invalidate()
		{
			m_Action = null;
		}

		public global::UnityEngine.InputSystem.InputAction ToInputAction()
		{
			return action;
		}

		private void CheckImmutableReference()
		{
		}
	}
}
