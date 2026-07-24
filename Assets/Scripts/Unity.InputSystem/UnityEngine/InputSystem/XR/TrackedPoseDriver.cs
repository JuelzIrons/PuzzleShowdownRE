namespace UnityEngine.InputSystem.XR
{
	[global::System.Serializable]
	[global::UnityEngine.AddComponentMenu("XR/Tracked Pose Driver (Input System)")]
	[global::UnityEngine.HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.18/manual/TrackedInputDevices.html#tracked-pose-driver")]
	public class TrackedPoseDriver : global::UnityEngine.MonoBehaviour, global::UnityEngine.ISerializationCallbackReceiver
	{
		public enum TrackingType
		{
			RotationAndPosition = 0,
			RotationOnly = 1,
			PositionOnly = 2
		}

		[global::System.Flags]
		private enum TrackingStates
		{
			None = 0,
			Position = 1,
			Rotation = 2
		}

		public enum UpdateType
		{
			UpdateAndBeforeRender = 0,
			Update = 1,
			BeforeRender = 2
		}

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Which Transform properties to update.")]
		private global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingType m_TrackingType;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Updates the Transform properties after these phases of Input System event processing.")]
		private global::UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateType m_UpdateType;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("Ignore Tracking State and always treat the input pose as valid.")]
		private bool m_IgnoreTrackingState;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("The input action to read the position value of a tracked device. Must be a Vector 3 control type.")]
		private global::UnityEngine.InputSystem.InputActionProperty m_PositionInput;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("The input action to read the rotation value of a tracked device. Must be a Quaternion control type.")]
		private global::UnityEngine.InputSystem.InputActionProperty m_RotationInput;

		[global::UnityEngine.SerializeField]
		[global::UnityEngine.Tooltip("The input action to read the tracking state value of a tracked device. Identifies if position and rotation have valid data. Must be an Integer control type.")]
		private global::UnityEngine.InputSystem.InputActionProperty m_TrackingStateInput;

		private global::UnityEngine.Vector3 m_CurrentPosition = global::UnityEngine.Vector3.zero;

		private global::UnityEngine.Quaternion m_CurrentRotation = global::UnityEngine.Quaternion.identity;

		private global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates m_CurrentTrackingState = global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.Position | global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.Rotation;

		private bool m_RotationBound;

		private bool m_PositionBound;

		private bool m_TrackingStateBound;

		private bool m_IsFirstUpdate = true;

		[global::System.Obsolete]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputAction m_PositionAction;

		[global::System.Obsolete]
		[global::UnityEngine.SerializeField]
		[global::UnityEngine.HideInInspector]
		private global::UnityEngine.InputSystem.InputAction m_RotationAction;

		public global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingType trackingType
		{
			get
			{
				return m_TrackingType;
			}
			set
			{
				m_TrackingType = value;
			}
		}

		public global::UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateType updateType
		{
			get
			{
				return m_UpdateType;
			}
			set
			{
				m_UpdateType = value;
			}
		}

		public bool ignoreTrackingState
		{
			get
			{
				return m_IgnoreTrackingState;
			}
			set
			{
				m_IgnoreTrackingState = value;
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty positionInput
		{
			get
			{
				return m_PositionInput;
			}
			set
			{
				if (global::UnityEngine.Application.isPlaying)
				{
					UnbindPosition();
				}
				m_PositionInput = value;
				if (global::UnityEngine.Application.isPlaying && base.isActiveAndEnabled)
				{
					BindPosition();
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty rotationInput
		{
			get
			{
				return m_RotationInput;
			}
			set
			{
				if (global::UnityEngine.Application.isPlaying)
				{
					UnbindRotation();
				}
				m_RotationInput = value;
				if (global::UnityEngine.Application.isPlaying && base.isActiveAndEnabled)
				{
					BindRotation();
				}
			}
		}

		public global::UnityEngine.InputSystem.InputActionProperty trackingStateInput
		{
			get
			{
				return m_TrackingStateInput;
			}
			set
			{
				if (global::UnityEngine.Application.isPlaying)
				{
					UnbindTrackingState();
				}
				m_TrackingStateInput = value;
				if (global::UnityEngine.Application.isPlaying && base.isActiveAndEnabled)
				{
					BindTrackingState();
				}
			}
		}

		public global::UnityEngine.InputSystem.InputAction positionAction
		{
			get
			{
				return m_PositionInput.action;
			}
			set
			{
				positionInput = new global::UnityEngine.InputSystem.InputActionProperty(value);
			}
		}

		public global::UnityEngine.InputSystem.InputAction rotationAction
		{
			get
			{
				return m_RotationInput.action;
			}
			set
			{
				rotationInput = new global::UnityEngine.InputSystem.InputActionProperty(value);
			}
		}

		private void BindActions()
		{
			BindPosition();
			BindRotation();
			BindTrackingState();
		}

		private void UnbindActions()
		{
			UnbindPosition();
			UnbindRotation();
			UnbindTrackingState();
		}

		private void BindPosition()
		{
			if (m_PositionBound)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction action = m_PositionInput.action;
			if (action != null)
			{
				action.performed += OnPositionPerformed;
				action.canceled += OnPositionCanceled;
				m_PositionBound = true;
				if (m_PositionInput.reference == null)
				{
					RenameAndEnable(action, base.gameObject.name + " - TPD - Position");
				}
			}
		}

		private void BindRotation()
		{
			if (m_RotationBound)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction action = m_RotationInput.action;
			if (action != null)
			{
				action.performed += OnRotationPerformed;
				action.canceled += OnRotationCanceled;
				m_RotationBound = true;
				if (m_RotationInput.reference == null)
				{
					RenameAndEnable(action, base.gameObject.name + " - TPD - Rotation");
				}
			}
		}

		private void BindTrackingState()
		{
			if (m_TrackingStateBound)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction action = m_TrackingStateInput.action;
			if (action != null)
			{
				action.performed += OnTrackingStatePerformed;
				action.canceled += OnTrackingStateCanceled;
				m_TrackingStateBound = true;
				if (m_TrackingStateInput.reference == null)
				{
					RenameAndEnable(action, base.gameObject.name + " - TPD - Tracking State");
				}
			}
		}

		private static void RenameAndEnable(global::UnityEngine.InputSystem.InputAction action, string name)
		{
			action.Rename(name);
			action.Enable();
		}

		private void UnbindPosition()
		{
			if (!m_PositionBound)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction action = m_PositionInput.action;
			if (action != null)
			{
				if (m_PositionInput.reference == null)
				{
					action.Disable();
				}
				action.performed -= OnPositionPerformed;
				action.canceled -= OnPositionCanceled;
				m_PositionBound = false;
			}
		}

		private void UnbindRotation()
		{
			if (!m_RotationBound)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction action = m_RotationInput.action;
			if (action != null)
			{
				if (m_RotationInput.reference == null)
				{
					action.Disable();
				}
				action.performed -= OnRotationPerformed;
				action.canceled -= OnRotationCanceled;
				m_RotationBound = false;
			}
		}

		private void UnbindTrackingState()
		{
			if (!m_TrackingStateBound)
			{
				return;
			}
			global::UnityEngine.InputSystem.InputAction action = m_TrackingStateInput.action;
			if (action != null)
			{
				if (m_TrackingStateInput.reference == null)
				{
					action.Disable();
				}
				action.performed -= OnTrackingStatePerformed;
				action.canceled -= OnTrackingStateCanceled;
				m_TrackingStateBound = false;
			}
		}

		private void OnPositionPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			m_CurrentPosition = context.ReadValue<global::UnityEngine.Vector3>();
		}

		private void OnPositionCanceled(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			m_CurrentPosition = global::UnityEngine.Vector3.zero;
		}

		private void OnRotationPerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			m_CurrentRotation = context.ReadValue<global::UnityEngine.Quaternion>();
		}

		private void OnRotationCanceled(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			m_CurrentRotation = global::UnityEngine.Quaternion.identity;
		}

		private void OnTrackingStatePerformed(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			m_CurrentTrackingState = (global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates)context.ReadValue<int>();
		}

		private void OnTrackingStateCanceled(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
		{
			m_CurrentTrackingState = global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.None;
		}

		protected void Reset()
		{
			m_PositionInput = new global::UnityEngine.InputSystem.InputActionProperty(new global::UnityEngine.InputSystem.InputAction("Position", global::UnityEngine.InputSystem.InputActionType.Value, null, null, null, "Vector3"));
			m_RotationInput = new global::UnityEngine.InputSystem.InputActionProperty(new global::UnityEngine.InputSystem.InputAction("Rotation", global::UnityEngine.InputSystem.InputActionType.Value, null, null, null, "Quaternion"));
			m_TrackingStateInput = new global::UnityEngine.InputSystem.InputActionProperty(new global::UnityEngine.InputSystem.InputAction("Tracking State", global::UnityEngine.InputSystem.InputActionType.Value, null, null, null, "Integer"));
		}

		protected virtual void Awake()
		{
		}

		protected void OnEnable()
		{
			global::UnityEngine.InputSystem.InputSystem.onAfterUpdate += UpdateCallback;
			global::UnityEngine.InputSystem.InputSystem.onDeviceChange += OnDeviceChanged;
			BindActions();
			m_IsFirstUpdate = true;
		}

		protected void OnDisable()
		{
			UnbindActions();
			global::UnityEngine.InputSystem.InputSystem.onAfterUpdate -= UpdateCallback;
			global::UnityEngine.InputSystem.InputSystem.onDeviceChange -= OnDeviceChanged;
		}

		protected virtual void OnDestroy()
		{
		}

		protected void UpdateCallback()
		{
			if (m_IsFirstUpdate)
			{
				if (HasResolvedControl(m_PositionInput.action))
				{
					m_CurrentPosition = m_PositionInput.action.ReadValue<global::UnityEngine.Vector3>();
				}
				else
				{
					m_CurrentPosition = base.transform.localPosition;
				}
				if (HasResolvedControl(m_RotationInput.action))
				{
					m_CurrentRotation = m_RotationInput.action.ReadValue<global::UnityEngine.Quaternion>();
				}
				else
				{
					m_CurrentRotation = base.transform.localRotation;
				}
				ReadTrackingState();
				m_IsFirstUpdate = false;
			}
			if (global::UnityEngine.InputSystem.LowLevel.InputState.currentUpdateType == global::UnityEngine.InputSystem.LowLevel.InputUpdateType.BeforeRender)
			{
				OnBeforeRender();
			}
			else
			{
				OnUpdate();
			}
		}

		private void OnDeviceChanged(global::UnityEngine.InputSystem.InputDevice inputDevice, global::UnityEngine.InputSystem.InputDeviceChange inputDeviceChange)
		{
			if (!m_IsFirstUpdate)
			{
				ReadTrackingStateWithoutTrackingAction();
			}
		}

		private void ReadTrackingStateWithoutTrackingAction()
		{
			global::UnityEngine.InputSystem.InputAction action = m_TrackingStateInput.action;
			if (action == null || action.m_BindingsCount == 0)
			{
				bool flag = HasResolvedControl(m_PositionInput.action);
				bool flag2 = HasResolvedControl(m_RotationInput.action);
				if (flag && flag2)
				{
					m_CurrentTrackingState = global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.Position | global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.Rotation;
				}
				else if (flag)
				{
					m_CurrentTrackingState = global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.Position;
				}
				else if (flag2)
				{
					m_CurrentTrackingState = global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.Rotation;
				}
				else
				{
					m_CurrentTrackingState = global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.None;
				}
			}
		}

		private void ReadTrackingState()
		{
			global::UnityEngine.InputSystem.InputAction action = m_TrackingStateInput.action;
			if (action != null && !action.enabled)
			{
				m_CurrentTrackingState = global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.None;
			}
			else if (HasResolvedControl(action))
			{
				m_CurrentTrackingState = (global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates)action.ReadValue<int>();
			}
			else
			{
				ReadTrackingStateWithoutTrackingAction();
			}
		}

		protected virtual void OnUpdate()
		{
			if (m_UpdateType == global::UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateType.Update || m_UpdateType == global::UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				PerformUpdate();
			}
		}

		protected virtual void OnBeforeRender()
		{
			if (m_UpdateType == global::UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateType.BeforeRender || m_UpdateType == global::UnityEngine.InputSystem.XR.TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				PerformUpdate();
			}
		}

		protected virtual void PerformUpdate()
		{
			SetLocalTransform(m_CurrentPosition, m_CurrentRotation);
		}

		protected virtual void SetLocalTransform(global::UnityEngine.Vector3 newPosition, global::UnityEngine.Quaternion newRotation)
		{
			bool flag = m_IgnoreTrackingState || (m_CurrentTrackingState & global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.Position) != 0;
			bool flag2 = m_IgnoreTrackingState || (m_CurrentTrackingState & global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingStates.Rotation) != 0;
			switch (m_TrackingType)
			{
			case global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingType.RotationAndPosition:
				if (flag2 && flag)
				{
					base.transform.SetLocalPositionAndRotation(newPosition, newRotation);
				}
				else if (flag2)
				{
					base.transform.localRotation = newRotation;
				}
				else if (flag)
				{
					base.transform.localPosition = newPosition;
				}
				break;
			case global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingType.PositionOnly:
				if (flag)
				{
					base.transform.localPosition = newPosition;
				}
				break;
			case global::UnityEngine.InputSystem.XR.TrackedPoseDriver.TrackingType.RotationOnly:
				if (flag2)
				{
					base.transform.localRotation = newRotation;
				}
				break;
			}
		}

		private unsafe static bool HasResolvedControl(global::UnityEngine.InputSystem.InputAction action)
		{
			if (action == null)
			{
				return false;
			}
			global::UnityEngine.InputSystem.InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			global::UnityEngine.InputSystem.InputActionState state = orCreateActionMap.m_State;
			if (state == null)
			{
				return false;
			}
			int actionIndexInState = action.m_ActionIndexInState;
			int totalBindingCount = state.totalBindingCount;
			for (int i = 0; i < totalBindingCount; i++)
			{
				ref global::UnityEngine.InputSystem.InputActionState.BindingState reference = ref state.bindingStates[i];
				if (reference.actionIndex == actionIndexInState && !reference.isComposite && reference.controlCount > 0)
				{
					return true;
				}
			}
			return false;
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		void global::UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if ((object)m_PositionInput.serializedReference == null && m_PositionInput.serializedAction == null && m_PositionAction != null)
			{
				m_PositionInput = new global::UnityEngine.InputSystem.InputActionProperty(m_PositionAction);
			}
			if ((object)m_RotationInput.serializedReference == null && m_RotationInput.serializedAction == null && m_RotationAction != null)
			{
				m_RotationInput = new global::UnityEngine.InputSystem.InputActionProperty(m_RotationAction);
			}
		}
	}
}
