public class SpeedSelector : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Canvas m_canvas;

	[global::UnityEngine.SerializeField]
	private int m_cap;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_valueText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.RectTransform m_cursorTransfrom;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.RectTransform[] m_bounds;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animator m_animBlink;

	public int Value;

	private global::UnityEngine.Vector2 m_dir;

	private float m_timer;

	private float m_cdTimer;

	[global::UnityEngine.SerializeField]
	private AutoSelect SelectableToHover;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_nextButton;

	private bool m_isSubbed;

	private void Start()
	{
		Value = global::UnityEngine.PlayerPrefs.GetInt("SELECTED_SPEED_LVL", 1);
		UpdateCursorVisuals();
		_ = SelectableToHover == null;
	}

	private void OnDisable()
	{
		_ = SelectableToHover == null;
	}

	private void UnSub()
	{
		m_isSubbed = false;
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryMove));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryMove));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryMove));
		PersistentInputReader instance4 = PersistentInputReader.Instance;
		instance4.KeyboardSpecialEnterAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance4.KeyboardSpecialEnterAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
		PersistentInputReader instance5 = PersistentInputReader.Instance;
		instance5.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance5.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
		PersistentInputReader instance6 = PersistentInputReader.Instance;
		instance6.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance6.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
		PersistentInputReader instance7 = PersistentInputReader.Instance;
		instance7.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance7.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
		PersistentInputReader instance8 = PersistentInputReader.Instance;
		instance8.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance8.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
	}

	private void Sub()
	{
		m_isSubbed = true;
		PersistentInputReader instance = PersistentInputReader.Instance;
		instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryMove));
		PersistentInputReader instance2 = PersistentInputReader.Instance;
		instance2.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance2.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryMove));
		PersistentInputReader instance3 = PersistentInputReader.Instance;
		instance3.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance3.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(TryMove));
		PersistentInputReader instance4 = PersistentInputReader.Instance;
		instance4.KeyboardSpecialEnterAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance4.KeyboardSpecialEnterAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
		PersistentInputReader instance5 = PersistentInputReader.Instance;
		instance5.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance5.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
		PersistentInputReader instance6 = PersistentInputReader.Instance;
		instance6.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance6.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
		PersistentInputReader instance7 = PersistentInputReader.Instance;
		instance7.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance7.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
		PersistentInputReader instance8 = PersistentInputReader.Instance;
		instance8.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance8.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(Confirm));
	}

	public void Confirm(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(m_nextButton);
	}

	public void TryMove(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (m_canvas.gameObject.activeSelf && !(global::UnityEngine.EventSystems.EventSystem.current == null) && !(SelectableToHover.gameObject != global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject))
		{
			if (context.performed)
			{
				m_dir = context.ReadValue<global::UnityEngine.Vector2>();
				MoveCursor(global::UnityEngine.Mathf.CeilToInt(context.ReadValue<global::UnityEngine.Vector2>().x));
				m_timer = 0f;
				m_cdTimer = 0f;
			}
			if (context.canceled)
			{
				m_timer = 0f;
				m_cdTimer = 0f;
				m_dir = global::UnityEngine.Vector2.zero;
			}
		}
	}

	private void Update()
	{
		if (SelectableToHover != null && SelectableToHover.m_isSelected && !m_isSubbed)
		{
			if (m_animBlink != null)
			{
				m_animBlink.SetBool("isBlinking", value: true);
			}
			Sub();
		}
		if (SelectableToHover != null && !SelectableToHover.m_isSelected && m_isSubbed)
		{
			if (m_animBlink != null)
			{
				m_animBlink.SetBool("isBlinking", value: false);
			}
			UnSub();
		}
		if (SelectableToHover != null && SelectableToHover.m_isSelected)
		{
			m_cursorTransfrom.GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
		}
		else if (SelectableToHover != null)
		{
			m_cursorTransfrom.GetComponent<global::UnityEngine.UI.Image>().color = new global::UnityEngine.Color(1f, 1f, 1f, 0.5f);
		}
		if (!(m_dir == global::UnityEngine.Vector2.zero))
		{
			m_timer += global::UnityEngine.Time.deltaTime;
			m_cdTimer += global::UnityEngine.Time.deltaTime;
			if (m_timer > 0.05f && m_cdTimer > 0.4f)
			{
				MoveCursor(global::UnityEngine.Mathf.CeilToInt(m_dir.x));
				m_timer = 0f;
			}
		}
	}

	private void MoveCursor(int x)
	{
		Value = global::UnityEngine.Mathf.Clamp(Value + x, 1, m_cap);
		UpdateCursorVisuals();
	}

	private void UpdateCursorVisuals()
	{
		m_valueText.text = Value.ToString("00");
		float t = (float)Value / (float)m_cap;
		global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.Lerp(m_bounds[0].position, m_bounds[1].position, t);
		m_cursorTransfrom.transform.position = new global::UnityEngine.Vector3(vector.x, m_cursorTransfrom.transform.position.y, 0f);
	}

	public void SaveSettings()
	{
		global::UnityEngine.PlayerPrefs.SetInt("SELECTED_SPEED_LVL", Value);
		GameManager.Instance.SelectedSpeedLevel = Value;
	}
}
