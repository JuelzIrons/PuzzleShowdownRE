public class LocalMpCtrlManager : global::UnityEngine.MonoBehaviour
{
	public static LocalMpCtrlManager Instance;

	public bool P1Occupied;

	public bool P2Occupied;

	public global::UnityEngine.Transform LeftPos;

	public global::UnityEngine.Transform RightPos;

	public LocalInputController m_inputP1;

	public LocalInputController m_inputP2;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_p1ReadyObj;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_p2ReadyObj;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_charSelectScreen;

	[global::UnityEngine.SerializeField]
	private CharacterSelectGrid m_selectGrid;

	public bool BlockInputSelection;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<LocalInputController> m_inputCtrls = new global::System.Collections.Generic.List<LocalInputController>();

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_anyKeySelect;

	public void DevicesChanged()
	{
		foreach (LocalInputController inputCtrl in m_inputCtrls)
		{
			inputCtrl.TryResubscribe();
		}
	}

	private void Update()
	{
		m_anyKeySelect.SetActive(P1Occupied && P2Occupied);
		if (global::UnityEngine.Input.anyKeyDown && P1Occupied && P2Occupied && !BlockInputSelection)
		{
			CheckForBothLockedIn();
		}
	}

	public void CheckForBothLockedIn()
	{
		PersistentInputReader.Instance.AllowPausing = false;
		if (P1Occupied && P2Occupied && !BlockInputSelection)
		{
			m_charSelectScreen.SetActive(value: true);
			if (m_inputP1.CtrlStyle == ControllerStyle.Keyboard)
			{
				PersistentInputReader instance = PersistentInputReader.Instance;
				instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
				PersistentInputReader instance2 = PersistentInputReader.Instance;
				instance2.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance2.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
				PersistentInputReader instance3 = PersistentInputReader.Instance;
				instance3.KeyboardAltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance3.KeyboardAltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
				PersistentInputReader instance4 = PersistentInputReader.Instance;
				instance4.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance4.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
				PersistentInputReader instance5 = PersistentInputReader.Instance;
				instance5.KeyboardSpecialEnterAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance5.KeyboardSpecialEnterAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
				GameManager.Instance.P1InputScheme = 0;
			}
			else if (m_inputP1.CtrlStyle == ControllerStyle.Controller1)
			{
				PersistentInputReader instance6 = PersistentInputReader.Instance;
				instance6.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance6.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
				PersistentInputReader instance7 = PersistentInputReader.Instance;
				instance7.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance7.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
				PersistentInputReader instance8 = PersistentInputReader.Instance;
				instance8.Controller1AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance8.Controller1AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
				PersistentInputReader instance9 = PersistentInputReader.Instance;
				instance9.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance9.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
				GameManager.Instance.P1InputScheme = 1;
			}
			else if (m_inputP1.CtrlStyle == ControllerStyle.Controller2)
			{
				PersistentInputReader instance10 = PersistentInputReader.Instance;
				instance10.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance10.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
				PersistentInputReader instance11 = PersistentInputReader.Instance;
				instance11.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance11.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
				PersistentInputReader instance12 = PersistentInputReader.Instance;
				instance12.Controller2AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance12.Controller2AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
				PersistentInputReader instance13 = PersistentInputReader.Instance;
				instance13.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance13.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
				GameManager.Instance.P1InputScheme = 2;
			}
			if (m_inputP2.CtrlStyle == ControllerStyle.Keyboard)
			{
				PersistentInputReader instance14 = PersistentInputReader.Instance;
				instance14.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance14.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP2));
				PersistentInputReader instance15 = PersistentInputReader.Instance;
				instance15.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance15.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP2));
				PersistentInputReader instance16 = PersistentInputReader.Instance;
				instance16.KeyboardAltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance16.KeyboardAltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
				PersistentInputReader instance17 = PersistentInputReader.Instance;
				instance17.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance17.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
				PersistentInputReader instance18 = PersistentInputReader.Instance;
				instance18.KeyboardSpecialEnterAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance18.KeyboardSpecialEnterAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP2));
				GameManager.Instance.P2InputScheme = 0;
			}
			else if (m_inputP2.CtrlStyle == ControllerStyle.Controller1)
			{
				PersistentInputReader instance19 = PersistentInputReader.Instance;
				instance19.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance19.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP2));
				PersistentInputReader instance20 = PersistentInputReader.Instance;
				instance20.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance20.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP2));
				PersistentInputReader instance21 = PersistentInputReader.Instance;
				instance21.Controller1AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance21.Controller1AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
				PersistentInputReader instance22 = PersistentInputReader.Instance;
				instance22.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance22.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
				GameManager.Instance.P2InputScheme = 1;
			}
			else if (m_inputP2.CtrlStyle == ControllerStyle.Controller2)
			{
				PersistentInputReader instance23 = PersistentInputReader.Instance;
				instance23.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance23.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP2));
				PersistentInputReader instance24 = PersistentInputReader.Instance;
				instance24.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance24.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP2));
				PersistentInputReader instance25 = PersistentInputReader.Instance;
				instance25.Controller2AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance25.Controller2AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
				PersistentInputReader instance26 = PersistentInputReader.Instance;
				instance26.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Combine(instance26.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
				GameManager.Instance.P2InputScheme = 2;
			}
			CharacterSelectGrid selectGrid = m_selectGrid;
			selectGrid.ALLREADYACTION = (global::System.Action)global::System.Delegate.Combine(selectGrid.ALLREADYACTION, new global::System.Action(AllReady));
			BlockInputSelection = true;
		}
	}

	public void GoBack()
	{
		GameManager.Instance.OutOfGamemodeDestroy();
		PersistentInputReader.Instance.ClearAllSubscriptions();
		if (TallyManager.Instance != null)
		{
			global::UnityEngine.Object.Destroy(TallyManager.Instance.gameObject);
		}
		SceneLoader.Instance.LoadSceneByEnumRegularFade(AllGameScenes.MainMenu);
	}

	public void UnsubscribeToAllInputs()
	{
		if (m_inputP1.CtrlStyle == ControllerStyle.Keyboard)
		{
			PersistentInputReader instance = PersistentInputReader.Instance;
			instance.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
			PersistentInputReader instance2 = PersistentInputReader.Instance;
			instance2.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance2.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
			PersistentInputReader instance3 = PersistentInputReader.Instance;
			instance3.KeyboardAltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance3.KeyboardAltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader instance4 = PersistentInputReader.Instance;
			instance4.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance4.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader instance5 = PersistentInputReader.Instance;
			instance5.KeyboardSpecialEnterAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance5.KeyboardSpecialEnterAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
		}
		else if (m_inputP1.CtrlStyle == ControllerStyle.Controller1)
		{
			PersistentInputReader instance6 = PersistentInputReader.Instance;
			instance6.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance6.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
			PersistentInputReader instance7 = PersistentInputReader.Instance;
			instance7.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance7.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
			PersistentInputReader instance8 = PersistentInputReader.Instance;
			instance8.Controller1AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance8.Controller1AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader instance9 = PersistentInputReader.Instance;
			instance9.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance9.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
		}
		else if (m_inputP1.CtrlStyle == ControllerStyle.Controller2)
		{
			PersistentInputReader instance10 = PersistentInputReader.Instance;
			instance10.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance10.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP1));
			PersistentInputReader instance11 = PersistentInputReader.Instance;
			instance11.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance11.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP1));
			PersistentInputReader instance12 = PersistentInputReader.Instance;
			instance12.Controller2AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance12.Controller2AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
			PersistentInputReader instance13 = PersistentInputReader.Instance;
			instance13.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance13.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP1));
		}
		if (m_inputP2.CtrlStyle == ControllerStyle.Keyboard)
		{
			PersistentInputReader instance14 = PersistentInputReader.Instance;
			instance14.KeyboardMoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance14.KeyboardMoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP2));
			PersistentInputReader instance15 = PersistentInputReader.Instance;
			instance15.KeyboardSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance15.KeyboardSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP2));
			PersistentInputReader instance16 = PersistentInputReader.Instance;
			instance16.KeyboardAltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance16.KeyboardAltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
			PersistentInputReader instance17 = PersistentInputReader.Instance;
			instance17.KeyboardStartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance17.KeyboardStartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
			PersistentInputReader instance18 = PersistentInputReader.Instance;
			instance18.KeyboardSpecialEnterAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance18.KeyboardSpecialEnterAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP2));
		}
		else if (m_inputP2.CtrlStyle == ControllerStyle.Controller1)
		{
			PersistentInputReader instance19 = PersistentInputReader.Instance;
			instance19.Controller1MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance19.Controller1MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP2));
			PersistentInputReader instance20 = PersistentInputReader.Instance;
			instance20.Controller1SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance20.Controller1SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP2));
			PersistentInputReader instance21 = PersistentInputReader.Instance;
			instance21.Controller1AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance21.Controller1AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
			PersistentInputReader instance22 = PersistentInputReader.Instance;
			instance22.Controller1StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance22.Controller1StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
		}
		else if (m_inputP2.CtrlStyle == ControllerStyle.Controller2)
		{
			PersistentInputReader instance23 = PersistentInputReader.Instance;
			instance23.Controller2MoveAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance23.Controller2MoveAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.MoveCursorP2));
			PersistentInputReader instance24 = PersistentInputReader.Instance;
			instance24.Controller2SwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance24.Controller2SwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.SelectP2));
			PersistentInputReader instance25 = PersistentInputReader.Instance;
			instance25.Controller2AltSwapAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance25.Controller2AltSwapAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
			PersistentInputReader instance26 = PersistentInputReader.Instance;
			instance26.Controller2StartAction = (global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>)global::System.Delegate.Remove(instance26.Controller2StartAction, new global::System.Action<global::UnityEngine.InputSystem.InputAction.CallbackContext>(m_selectGrid.DeselectP2));
		}
		foreach (LocalInputController inputCtrl in m_inputCtrls)
		{
			inputCtrl.ForceUnsub();
		}
	}

	private void AllReady()
	{
		UnsubscribeToAllInputs();
		CharacterSelectGrid selectGrid = m_selectGrid;
		selectGrid.ALLREADYACTION = (global::System.Action)global::System.Delegate.Remove(selectGrid.ALLREADYACTION, new global::System.Action(AllReady));
		
		{
			GameManager.Instance.PlayLocalMp();
		}, null, 1f), delegate
		{
		});
	}

	private void Start()
	{
		BlockInputSelection = false;
		Instance = this;
	}
}
