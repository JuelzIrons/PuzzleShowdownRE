public class LocalInputController : global::UnityEngine.MonoBehaviour
{
	public ControllerStyle CtrlStyle;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform m_middlePos;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_backbtn;

	public bool IsOccP1;

	public bool IsOccP2;

	private bool HasSubscribed;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.UI.Image m_vis;

	public bool IsOccupying
	{
		get
		{
			if (!IsOccP1)
			{
				return IsOccP2;
			}
			return true;
		}
	}

	private void Start()
	{
		SubscribeToEvents();
		base.transform.position = m_middlePos.position;
	}

	public void TryResubscribe()
	{
		SubscribeToEvents();
		BackOut(default(global::UnityEngine.InputSystem.InputAction.CallbackContext));
		base.transform.position = m_middlePos.position;
	}

	public void ForceUnsub()
	{
		if (HasSubscribed)
		{
			switch (CtrlStyle)
			{
			case ControllerStyle.Keyboard:
			{
				PersistentInputReader instance3 = PersistentInputReader.Instance;
				instance3.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Move));
				HasSubscribed = false;
				break;
			}
			case ControllerStyle.Controller1:
			{
				PersistentInputReader instance2 = PersistentInputReader.Instance;
				instance2.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Move));
				HasSubscribed = false;
				break;
			}
			case ControllerStyle.Controller2:
			{
				PersistentInputReader instance = PersistentInputReader.Instance;
				instance.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Move));
				HasSubscribed = false;
				break;
			}
			}
		}
	}

	private void SubscribeToEvents()
	{
		global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Gamepad> all = global::UnityEngine.InputSystem.Gamepad.all;
		if (HasSubscribed)
		{
			switch (CtrlStyle)
			{
			case ControllerStyle.Keyboard:
				if (global::UnityEngine.InputSystem.Keyboard.current == null)
				{
					m_vis.enabled = false;
					PersistentInputReader instance2 = PersistentInputReader.Instance;
					instance2.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Move));
					base.transform.position = m_middlePos.position;
					BackOut(default(global::UnityEngine.InputSystem.InputAction.CallbackContext));
					HasSubscribed = false;
				}
				break;
			case ControllerStyle.Controller1:
				if (all.Count < 1)
				{
					m_vis.enabled = false;
					PersistentInputReader instance3 = PersistentInputReader.Instance;
					instance3.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Move));
					base.transform.position = m_middlePos.position;
					BackOut(default(global::UnityEngine.InputSystem.InputAction.CallbackContext));
					HasSubscribed = false;
				}
				break;
			case ControllerStyle.Controller2:
				if (all.Count <= 1)
				{
					m_vis.enabled = false;
					PersistentInputReader instance = PersistentInputReader.Instance;
					instance.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Move));
					BackOut(default(global::UnityEngine.InputSystem.InputAction.CallbackContext));
					HasSubscribed = false;
				}
				break;
			}
		}
		else
		{
			if (HasSubscribed)
			{
				return;
			}
			switch (CtrlStyle)
			{
			case ControllerStyle.Keyboard:
				if (global::UnityEngine.InputSystem.Keyboard.current != null)
				{
					m_vis.enabled = true;
					PersistentInputReader instance5 = PersistentInputReader.Instance;
					instance5.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance5.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Move));
					HasSubscribed = true;
				}
				break;
			case ControllerStyle.Controller1:
				if (all.Count != 0)
				{
					m_vis.enabled = true;
					PersistentInputReader instance6 = PersistentInputReader.Instance;
					instance6.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance6.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Move));
					HasSubscribed = true;
				}
				break;
			case ControllerStyle.Controller2:
				if (all.Count > 1)
				{
					m_vis.enabled = true;
					PersistentInputReader instance4 = PersistentInputReader.Instance;
					instance4.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance4.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Move));
					HasSubscribed = true;
				}
				break;
			}
		}
	}

	public void Move(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.ReadValue<global::UnityEngine.Vector2>() == global::UnityEngine.Vector2.up)
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
		}
		else if (context.ReadValue<global::UnityEngine.Vector2>() == global::UnityEngine.Vector2.down)
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_backbtn);
		}
		global::UnityEngine.Debug.Log("Tryna move!");
		if (!context.performed || LocalMpCtrlManager.Instance.BlockInputSelection)
		{
			return;
		}
		if (!IsOccupying)
		{
			if (context.ReadValue<global::UnityEngine.Vector2>() == global::UnityEngine.Vector2.left && !LocalMpCtrlManager.Instance.P1Occupied)
			{
				LocalMpCtrlManager.Instance.P1Occupied = true;
				IsOccP1 = true;
				LocalMpCtrlManager.Instance.m_inputP1 = this;
				base.transform.position = new global::UnityEngine.Vector3(LocalMpCtrlManager.Instance.LeftPos.position.x, base.transform.position.y, base.transform.position.z);
				LocalMpCtrlManager.Instance.BlockInputSelection = true;
				StartCoroutine(ReenableInputDetectionAfterOneFrame());
			}
			else if (context.ReadValue<global::UnityEngine.Vector2>() == global::UnityEngine.Vector2.right && !LocalMpCtrlManager.Instance.P2Occupied)
			{
				LocalMpCtrlManager.Instance.P2Occupied = true;
				IsOccP2 = true;
				LocalMpCtrlManager.Instance.m_inputP2 = this;
				base.transform.position = new global::UnityEngine.Vector3(LocalMpCtrlManager.Instance.RightPos.position.x, base.transform.position.y, base.transform.position.z);
				LocalMpCtrlManager.Instance.BlockInputSelection = true;
				StartCoroutine(ReenableInputDetectionAfterOneFrame());
			}
		}
		else if (context.ReadValue<global::UnityEngine.Vector2>() == global::UnityEngine.Vector2.left && IsOccP2)
		{
			LocalMpCtrlManager.Instance.P2Occupied = false;
			IsOccP2 = false;
			LocalMpCtrlManager.Instance.m_inputP2 = null;
			base.transform.position = m_middlePos.position;
		}
		else if (context.ReadValue<global::UnityEngine.Vector2>() == global::UnityEngine.Vector2.right && IsOccP1)
		{
			LocalMpCtrlManager.Instance.P1Occupied = false;
			LocalMpCtrlManager.Instance.m_inputP1 = null;
			IsOccP1 = false;
			base.transform.position = m_middlePos.position;
		}
	}

	private global::System.Collections.IEnumerator ReenableInputDetectionAfterOneFrame()
	{
		yield return new global::UnityEngine.WaitForSeconds(0.2f);
		LocalMpCtrlManager.Instance.BlockInputSelection = false;
	}

	public void BackOut(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (IsOccupying)
		{
			if (IsOccP1)
			{
				LocalMpCtrlManager.Instance.P1Occupied = false;
				base.transform.position = m_middlePos.position;
				IsOccP1 = false;
			}
			else if (IsOccP2)
			{
				LocalMpCtrlManager.Instance.P2Occupied = false;
				base.transform.position = m_middlePos.position;
				IsOccP2 = false;
			}
		}
	}
}
