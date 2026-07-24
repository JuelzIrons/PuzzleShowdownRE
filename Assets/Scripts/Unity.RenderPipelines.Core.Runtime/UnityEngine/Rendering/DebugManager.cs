namespace UnityEngine.Rendering
{
	public sealed class DebugManager
	{
		public enum UIMode
		{
			EditorMode = 0,
			RuntimeMode = 1
		}

		private class UIState
		{
			public global::UnityEngine.Rendering.DebugManager.UIMode mode;

			[global::UnityEngine.SerializeField]
			private bool m_Open;

			public bool open
			{
				get
				{
					return m_Open;
				}
				set
				{
					if (m_Open != value)
					{
						m_Open = value;
						global::UnityEngine.Rendering.DebugManager.windowStateChanged?.Invoke(mode, m_Open);
					}
				}
			}
		}

		private const string kEnableDebugBtn1 = "Enable Debug Button 1";

		private const string kEnableDebugBtn2 = "Enable Debug Button 2";

		private const string kDebugPreviousBtn = "Debug Previous";

		private const string kDebugNextBtn = "Debug Next";

		private const string kValidateBtn = "Debug Validate";

		private const string kPersistentBtn = "Debug Persistent";

		private const string kDPadVertical = "Debug Vertical";

		private const string kDPadHorizontal = "Debug Horizontal";

		private const string kMultiplierBtn = "Debug Multiplier";

		private const string kResetBtn = "Debug Reset";

		private const string kEnableDebug = "Enable Debug";

		private global::UnityEngine.Rendering.DebugActionDesc[] m_DebugActions;

		private global::UnityEngine.Rendering.DebugActionState[] m_DebugActionStates;

		private global::UnityEngine.InputSystem.InputActionMap debugActionMap = new global::UnityEngine.InputSystem.InputActionMap("Debug Menu");

		private static readonly global::System.Lazy<global::UnityEngine.Rendering.DebugManager> s_Instance = new global::System.Lazy<global::UnityEngine.Rendering.DebugManager>(() => new global::UnityEngine.Rendering.DebugManager());

		private global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.DebugUI.Panel> m_ReadOnlyPanels;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Panel> m_Panels = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Panel>();

		public bool refreshEditorRequested;

		private int? m_RequestedPanelIndex;

		private global::UnityEngine.GameObject m_Root;

		private global::UnityEngine.Rendering.UI.DebugUIHandlerCanvas m_RootUICanvas;

		private global::UnityEngine.GameObject m_PersistentRoot;

		private global::UnityEngine.Rendering.UI.DebugUIHandlerPersistentCanvas m_RootUIPersistentCanvas;

		private global::UnityEngine.Rendering.DebugManager.UIState editorUIState = new global::UnityEngine.Rendering.DebugManager.UIState
		{
			mode = global::UnityEngine.Rendering.DebugManager.UIMode.EditorMode
		};

		private bool m_EnableRuntimeUI = true;

		private global::UnityEngine.Rendering.DebugManager.UIState runtimeUIState = new global::UnityEngine.Rendering.DebugManager.UIState
		{
			mode = global::UnityEngine.Rendering.DebugManager.UIMode.RuntimeMode
		};

		public static global::UnityEngine.Rendering.DebugManager instance => s_Instance.Value;

		public global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.DebugUI.Panel> panels
		{
			get
			{
				if (m_ReadOnlyPanels == null)
				{
					UpdateReadOnlyCollection();
				}
				return m_ReadOnlyPanels;
			}
		}

		public bool isAnyDebugUIActive
		{
			get
			{
				if (!displayRuntimeUI)
				{
					return displayPersistentRuntimeUI;
				}
				return true;
			}
		}

		public bool displayEditorUI
		{
			get
			{
				return editorUIState.open;
			}
			set
			{
				editorUIState.open = value;
			}
		}

		public bool enableRuntimeUI
		{
			get
			{
				return m_EnableRuntimeUI;
			}
			set
			{
				if (value != m_EnableRuntimeUI)
				{
					m_EnableRuntimeUI = value;
					global::UnityEngine.Rendering.DebugUpdater.SetEnabled(value);
				}
			}
		}

		public bool displayRuntimeUI
		{
			get
			{
				if (m_Root != null)
				{
					return m_Root.activeInHierarchy;
				}
				return false;
			}
			set
			{
				if (value)
				{
					m_Root = global::UnityEngine.Object.Instantiate(global::UnityEngine.Resources.Load<global::UnityEngine.Transform>("DebugUICanvas")).gameObject;
					m_Root.name = "[Debug Canvas]";
					m_Root.transform.localPosition = global::UnityEngine.Vector3.zero;
					m_RootUICanvas = m_Root.GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerCanvas>();
					m_Root.SetActive(value: true);
				}
				else
				{
					global::UnityEngine.Rendering.CoreUtils.Destroy(m_Root);
					m_Root = null;
					m_RootUICanvas = null;
				}
				this.onDisplayRuntimeUIChanged(value);
				global::UnityEngine.Rendering.DebugUpdater.HandleInternalEventSystemComponents(value);
				runtimeUIState.open = m_Root != null && m_Root.activeInHierarchy;
			}
		}

		public bool displayPersistentRuntimeUI
		{
			get
			{
				if (m_RootUIPersistentCanvas != null)
				{
					return m_PersistentRoot.activeInHierarchy;
				}
				return false;
			}
			set
			{
				if (value)
				{
					EnsurePersistentCanvas();
					return;
				}
				global::UnityEngine.Rendering.CoreUtils.Destroy(m_PersistentRoot);
				m_PersistentRoot = null;
				m_RootUIPersistentCanvas = null;
			}
		}

		public event global::System.Action<bool> onDisplayRuntimeUIChanged = delegate
		{
		};

		public event global::System.Action onSetDirty = delegate
		{
		};

		private event global::System.Action resetData;

		public static event global::System.Action<global::UnityEngine.Rendering.DebugManager.UIMode, bool> windowStateChanged;

		private void RegisterActions()
		{
			m_DebugActions = new global::UnityEngine.Rendering.DebugActionDesc[9];
			m_DebugActionStates = new global::UnityEngine.Rendering.DebugActionState[9];
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc = new global::UnityEngine.Rendering.DebugActionDesc();
			debugActionDesc.buttonAction = debugActionMap.FindAction("Enable Debug");
			debugActionDesc.repeatMode = global::UnityEngine.Rendering.DebugActionRepeatMode.Never;
			AddAction(global::UnityEngine.Rendering.DebugAction.EnableDebugMenu, debugActionDesc);
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc2 = new global::UnityEngine.Rendering.DebugActionDesc();
			debugActionDesc2.buttonAction = debugActionMap.FindAction("Debug Reset");
			debugActionDesc2.repeatMode = global::UnityEngine.Rendering.DebugActionRepeatMode.Never;
			AddAction(global::UnityEngine.Rendering.DebugAction.ResetAll, debugActionDesc2);
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc3 = new global::UnityEngine.Rendering.DebugActionDesc();
			debugActionDesc3.buttonAction = debugActionMap.FindAction("Debug Next");
			debugActionDesc3.repeatMode = global::UnityEngine.Rendering.DebugActionRepeatMode.Never;
			AddAction(global::UnityEngine.Rendering.DebugAction.NextDebugPanel, debugActionDesc3);
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc4 = new global::UnityEngine.Rendering.DebugActionDesc();
			debugActionDesc4.buttonAction = debugActionMap.FindAction("Debug Previous");
			debugActionDesc4.repeatMode = global::UnityEngine.Rendering.DebugActionRepeatMode.Never;
			AddAction(global::UnityEngine.Rendering.DebugAction.PreviousDebugPanel, debugActionDesc4);
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc5 = new global::UnityEngine.Rendering.DebugActionDesc();
			debugActionDesc5.buttonAction = debugActionMap.FindAction("Debug Validate");
			debugActionDesc5.repeatMode = global::UnityEngine.Rendering.DebugActionRepeatMode.Never;
			AddAction(global::UnityEngine.Rendering.DebugAction.Action, debugActionDesc5);
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc6 = new global::UnityEngine.Rendering.DebugActionDesc();
			debugActionDesc6.buttonAction = debugActionMap.FindAction("Debug Persistent");
			debugActionDesc6.repeatMode = global::UnityEngine.Rendering.DebugActionRepeatMode.Never;
			AddAction(global::UnityEngine.Rendering.DebugAction.MakePersistent, debugActionDesc6);
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc7 = new global::UnityEngine.Rendering.DebugActionDesc();
			debugActionDesc7.buttonAction = debugActionMap.FindAction("Debug Multiplier");
			debugActionDesc7.repeatMode = global::UnityEngine.Rendering.DebugActionRepeatMode.Delay;
			debugActionDesc5.repeatDelay = 0f;
			AddAction(global::UnityEngine.Rendering.DebugAction.Multiplier, debugActionDesc7);
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc8 = new global::UnityEngine.Rendering.DebugActionDesc();
			debugActionDesc8.buttonAction = debugActionMap.FindAction("Debug Vertical");
			debugActionDesc8.repeatMode = global::UnityEngine.Rendering.DebugActionRepeatMode.Delay;
			debugActionDesc8.repeatDelay = 0.16f;
			AddAction(global::UnityEngine.Rendering.DebugAction.MoveVertical, debugActionDesc8);
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc9 = new global::UnityEngine.Rendering.DebugActionDesc();
			debugActionDesc9.buttonAction = debugActionMap.FindAction("Debug Horizontal");
			debugActionDesc9.repeatMode = global::UnityEngine.Rendering.DebugActionRepeatMode.Delay;
			debugActionDesc9.repeatDelay = 0.16f;
			AddAction(global::UnityEngine.Rendering.DebugAction.MoveHorizontal, debugActionDesc9);
		}

		internal void EnableInputActions()
		{
			foreach (global::UnityEngine.InputSystem.InputAction item in debugActionMap)
			{
				item.Enable();
			}
		}

		private void AddAction(global::UnityEngine.Rendering.DebugAction action, global::UnityEngine.Rendering.DebugActionDesc desc)
		{
			m_DebugActions[(int)action] = desc;
			m_DebugActionStates[(int)action] = new global::UnityEngine.Rendering.DebugActionState();
		}

		private void SampleAction(int actionIndex)
		{
			global::UnityEngine.Rendering.DebugActionDesc debugActionDesc = m_DebugActions[actionIndex];
			global::UnityEngine.Rendering.DebugActionState debugActionState = m_DebugActionStates[actionIndex];
			if (!debugActionState.runningAction && debugActionDesc.buttonAction != null)
			{
				float num = debugActionDesc.buttonAction.ReadValue<float>();
				if (!global::UnityEngine.Mathf.Approximately(num, 0f))
				{
					debugActionState.TriggerWithButton(debugActionDesc.buttonAction, num);
				}
			}
		}

		private void UpdateAction(int actionIndex)
		{
			global::UnityEngine.Rendering.DebugActionDesc desc = m_DebugActions[actionIndex];
			global::UnityEngine.Rendering.DebugActionState debugActionState = m_DebugActionStates[actionIndex];
			if (debugActionState.runningAction)
			{
				debugActionState.Update(desc);
			}
		}

		internal void UpdateActions()
		{
			for (int i = 0; i < m_DebugActions.Length; i++)
			{
				UpdateAction(i);
				SampleAction(i);
			}
		}

		internal float GetAction(global::UnityEngine.Rendering.DebugAction action)
		{
			return m_DebugActionStates[(int)action].actionState;
		}

		internal bool GetActionToggleDebugMenuWithTouch()
		{
			if (!global::UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.enabled)
			{
				return false;
			}
			global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.EnhancedTouch.Touch> activeTouches = global::UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
			int count = activeTouches.Count;
			global::UnityEngine.InputSystem.TouchPhase? touchPhase = null;
			if (count == 3)
			{
				foreach (global::UnityEngine.InputSystem.EnhancedTouch.Touch item in activeTouches)
				{
					if ((!touchPhase.HasValue || item.phase == touchPhase.Value) && item.tapCount == 2)
					{
						return true;
					}
				}
			}
			return false;
		}

		internal bool GetActionReleaseScrollTarget()
		{
			bool num = global::UnityEngine.InputSystem.Mouse.current != null && global::UnityEngine.InputSystem.Mouse.current.scroll.ReadValue() != global::UnityEngine.Vector2.zero;
			bool flag = global::UnityEngine.InputSystem.Touchscreen.current != null;
			return num || flag;
		}

		private void RegisterInputs()
		{
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddCompositeBinding(global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(debugActionMap, "Enable Debug", global::UnityEngine.InputSystem.InputActionType.Button), "ButtonWithOneModifier").With("Modifier", "<Gamepad>/rightStickPress").With("Button", "<Gamepad>/leftStickPress")
				.With("Modifier", "<Keyboard>/leftCtrl")
				.With("Button", "<Keyboard>/backspace");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddCompositeBinding(global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(debugActionMap, "Debug Reset", global::UnityEngine.InputSystem.InputActionType.Button), "ButtonWithOneModifier").With("Modifier", "<Gamepad>/rightStickPress").With("Button", "<Gamepad>/b")
				.With("Modifier", "<Keyboard>/leftAlt")
				.With("Button", "<Keyboard>/backspace");
			global::UnityEngine.InputSystem.InputAction action = global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(debugActionMap, "Debug Next", global::UnityEngine.InputSystem.InputActionType.Button);
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action, "<Keyboard>/pageDown");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action, "<Gamepad>/rightShoulder");
			global::UnityEngine.InputSystem.InputAction action2 = global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(debugActionMap, "Debug Previous", global::UnityEngine.InputSystem.InputActionType.Button);
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action2, "<Keyboard>/pageUp");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action2, "<Gamepad>/leftShoulder");
			global::UnityEngine.InputSystem.InputAction action3 = global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(debugActionMap, "Debug Validate", global::UnityEngine.InputSystem.InputActionType.Button);
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action3, "<Keyboard>/enter");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action3, "<Gamepad>/a");
			global::UnityEngine.InputSystem.InputAction action4 = global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(debugActionMap, "Debug Persistent", global::UnityEngine.InputSystem.InputActionType.Button);
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action4, "<Keyboard>/rightShift");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action4, "<Gamepad>/x");
			global::UnityEngine.InputSystem.InputAction action5 = global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(debugActionMap, "Debug Multiplier");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action5, "<Keyboard>/leftShift");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddBinding(action5, "<Gamepad>/y");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddCompositeBinding(global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(debugActionMap, "Debug Vertical"), "1DAxis").With("Positive", "<Gamepad>/dpad/up").With("Negative", "<Gamepad>/dpad/down")
				.With("Positive", "<Keyboard>/upArrow")
				.With("Negative", "<Keyboard>/downArrow");
			global::UnityEngine.InputSystem.InputActionSetupExtensions.AddCompositeBinding(global::UnityEngine.InputSystem.InputActionSetupExtensions.AddAction(debugActionMap, "Debug Horizontal"), "1DAxis").With("Positive", "<Gamepad>/dpad/right").With("Negative", "<Gamepad>/dpad/left")
				.With("Positive", "<Keyboard>/rightArrow")
				.With("Negative", "<Keyboard>/leftArrow");
		}

		private void UpdateReadOnlyCollection()
		{
			m_Panels.Sort();
			m_ReadOnlyPanels = m_Panels.AsReadOnly();
		}

		private DebugManager()
		{
		}

		public void RefreshEditor()
		{
			refreshEditorRequested = true;
		}

		public void Reset()
		{
			this.resetData?.Invoke();
			ReDrawOnScreenDebug();
		}

		public void ReDrawOnScreenDebug()
		{
			if (displayRuntimeUI)
			{
				m_RootUICanvas?.RequestHierarchyReset();
			}
		}

		public void RegisterData(global::UnityEngine.Rendering.IDebugData data)
		{
			resetData += data.GetReset();
		}

		public void UnregisterData(global::UnityEngine.Rendering.IDebugData data)
		{
			resetData -= data.GetReset();
		}

		public int GetState()
		{
			int num = 17;
			foreach (global::UnityEngine.Rendering.DebugUI.Panel panel in m_Panels)
			{
				num = num * 23 + panel.GetHashCode();
			}
			return num;
		}

		internal void RegisterRootCanvas(global::UnityEngine.Rendering.UI.DebugUIHandlerCanvas root)
		{
			m_Root = root.gameObject;
			m_RootUICanvas = root;
		}

		internal void ChangeSelection(global::UnityEngine.Rendering.UI.DebugUIHandlerWidget widget, bool fromNext)
		{
			m_RootUICanvas.ChangeSelection(widget, fromNext);
		}

		internal void SetScrollTarget(global::UnityEngine.Rendering.UI.DebugUIHandlerWidget widget)
		{
			if (m_RootUICanvas != null)
			{
				m_RootUICanvas.SetScrollTarget(widget);
			}
		}

		private void EnsurePersistentCanvas()
		{
			if (m_RootUIPersistentCanvas == null)
			{
				global::UnityEngine.Rendering.UI.DebugUIHandlerPersistentCanvas debugUIHandlerPersistentCanvas = global::UnityEngine.Object.FindFirstObjectByType<global::UnityEngine.Rendering.UI.DebugUIHandlerPersistentCanvas>();
				if (debugUIHandlerPersistentCanvas == null)
				{
					m_PersistentRoot = global::UnityEngine.Object.Instantiate(global::UnityEngine.Resources.Load<global::UnityEngine.Transform>("DebugUIPersistentCanvas")).gameObject;
					m_PersistentRoot.name = "[Debug Canvas - Persistent]";
					m_PersistentRoot.transform.localPosition = global::UnityEngine.Vector3.zero;
				}
				else
				{
					m_PersistentRoot = debugUIHandlerPersistentCanvas.gameObject;
				}
				m_RootUIPersistentCanvas = m_PersistentRoot.GetComponent<global::UnityEngine.Rendering.UI.DebugUIHandlerPersistentCanvas>();
			}
		}

		internal void TogglePersistent(global::UnityEngine.Rendering.DebugUI.Widget widget, int? forceTupleIndex = null)
		{
			if (widget == null)
			{
				return;
			}
			EnsurePersistentCanvas();
			if (!(widget is global::UnityEngine.Rendering.DebugUI.Value widget2))
			{
				if (!(widget is global::UnityEngine.Rendering.DebugUI.ValueTuple widget3))
				{
					if (widget is global::UnityEngine.Rendering.DebugUI.Container container)
					{
						int value = global::System.Linq.Enumerable.Max(container.children, (global::UnityEngine.Rendering.DebugUI.Widget w) => (w as global::UnityEngine.Rendering.DebugUI.ValueTuple)?.pinnedElementIndex ?? (-1));
						{
							foreach (global::UnityEngine.Rendering.DebugUI.Widget child in container.children)
							{
								if (child is global::UnityEngine.Rendering.DebugUI.Value || child is global::UnityEngine.Rendering.DebugUI.ValueTuple)
								{
									TogglePersistent(child, value);
								}
							}
							return;
						}
					}
					global::UnityEngine.Debug.Log("Only readonly items can be made persistent.");
				}
				else
				{
					m_RootUIPersistentCanvas.Toggle(widget3, forceTupleIndex);
				}
			}
			else
			{
				m_RootUIPersistentCanvas.Toggle(widget2);
			}
		}

		private void OnPanelDirty(global::UnityEngine.Rendering.DebugUI.Panel panel)
		{
			this.onSetDirty();
		}

		public int PanelIndex([global::System.Diagnostics.CodeAnalysis.DisallowNull] string displayName)
		{
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			for (int i = 0; i < m_Panels.Count; i++)
			{
				if (displayName.Equals(m_Panels[i].displayName, global::System.StringComparison.InvariantCultureIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		[global::System.Obsolete("Method is obsolete. Use PanelDisplayName instead. #from(6000.4) (UnityUpgradable) -> PanelDisplayName", true)]
		public string PanelDiplayName(int panelIndex)
		{
			return PanelDisplayName(panelIndex);
		}

		public string PanelDisplayName(int panelIndex)
		{
			if (panelIndex < 0 || panelIndex > m_Panels.Count - 1)
			{
				return string.Empty;
			}
			return m_Panels[panelIndex].displayName;
		}

		public void RequestEditorWindowPanelIndex(int index)
		{
			m_RequestedPanelIndex = index;
		}

		internal int? GetRequestedEditorWindowPanelIndex()
		{
			int? requestedPanelIndex = m_RequestedPanelIndex;
			m_RequestedPanelIndex = null;
			return requestedPanelIndex;
		}

		public global::UnityEngine.Rendering.DebugUI.Panel GetPanel(string displayName, bool createIfNull = false, int groupIndex = 0, bool overrideIfExist = false)
		{
			int num = PanelIndex(displayName);
			global::UnityEngine.Rendering.DebugUI.Panel panel = ((num >= 0) ? m_Panels[num] : null);
			if (panel != null)
			{
				if (!overrideIfExist)
				{
					return panel;
				}
				panel.onSetDirty -= OnPanelDirty;
				RemovePanel(panel);
				panel = null;
			}
			if (createIfNull)
			{
				panel = new global::UnityEngine.Rendering.DebugUI.Panel
				{
					displayName = displayName,
					groupIndex = groupIndex
				};
				panel.onSetDirty += OnPanelDirty;
				m_Panels.Add(panel);
				UpdateReadOnlyCollection();
			}
			return panel;
		}

		public int FindPanelIndex(string displayName)
		{
			return m_Panels.FindIndex((global::UnityEngine.Rendering.DebugUI.Panel p) => p.displayName == displayName);
		}

		public void RemovePanel(string displayName)
		{
			global::UnityEngine.Rendering.DebugUI.Panel panel = null;
			foreach (global::UnityEngine.Rendering.DebugUI.Panel panel2 in m_Panels)
			{
				if (panel2.displayName == displayName)
				{
					panel2.onSetDirty -= OnPanelDirty;
					panel = panel2;
					break;
				}
			}
			RemovePanel(panel);
		}

		public void RemovePanel(global::UnityEngine.Rendering.DebugUI.Panel panel)
		{
			if (panel != null)
			{
				m_Panels.Remove(panel);
				UpdateReadOnlyCollection();
			}
		}

		public global::UnityEngine.Rendering.DebugUI.Widget[] GetItems(global::UnityEngine.Rendering.DebugUI.Flags flags)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> value;
			using (global::UnityEngine.Rendering.ListPool<global::UnityEngine.Rendering.DebugUI.Widget>.Get(out value))
			{
				foreach (global::UnityEngine.Rendering.DebugUI.Panel panel in m_Panels)
				{
					global::UnityEngine.Rendering.DebugUI.Widget[] itemsFromContainer = GetItemsFromContainer(flags, panel);
					value.AddRange(itemsFromContainer);
				}
				return value.ToArray();
			}
		}

		internal global::UnityEngine.Rendering.DebugUI.Widget[] GetItemsFromContainer(global::UnityEngine.Rendering.DebugUI.Flags flags, global::UnityEngine.Rendering.DebugUI.IContainer container)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> value;
			using (global::UnityEngine.Rendering.ListPool<global::UnityEngine.Rendering.DebugUI.Widget>.Get(out value))
			{
				foreach (global::UnityEngine.Rendering.DebugUI.Widget child in container.children)
				{
					if (child.flags.HasFlag(flags))
					{
						value.Add(child);
					}
					else if (child is global::UnityEngine.Rendering.DebugUI.IContainer container2)
					{
						value.AddRange(GetItemsFromContainer(flags, container2));
					}
				}
				return value.ToArray();
			}
		}

		public global::UnityEngine.Rendering.DebugUI.Widget GetItem(string queryPath)
		{
			foreach (global::UnityEngine.Rendering.DebugUI.Panel panel in m_Panels)
			{
				global::UnityEngine.Rendering.DebugUI.Widget item = GetItem(queryPath, panel);
				if (item != null)
				{
					return item;
				}
			}
			return null;
		}

		private global::UnityEngine.Rendering.DebugUI.Widget GetItem(string queryPath, global::UnityEngine.Rendering.DebugUI.IContainer container)
		{
			foreach (global::UnityEngine.Rendering.DebugUI.Widget child in container.children)
			{
				if (child.queryPath == queryPath)
				{
					return child;
				}
				if (child is global::UnityEngine.Rendering.DebugUI.IContainer container2)
				{
					global::UnityEngine.Rendering.DebugUI.Widget item = GetItem(queryPath, container2);
					if (item != null)
					{
						return item;
					}
				}
			}
			return null;
		}

		[global::System.Obsolete("Use DebugManager.instance.displayEditorUI property instead. #from(2023.1)")]
		public void ToggleEditorUI(bool open)
		{
			editorUIState.open = open;
		}
	}
}
