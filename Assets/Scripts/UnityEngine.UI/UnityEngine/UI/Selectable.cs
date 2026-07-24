namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Selectable", 35)]
	[global::UnityEngine.ExecuteAlways]
	[global::UnityEngine.SelectionBase]
	[global::UnityEngine.DisallowMultipleComponent]
	public class Selectable : global::UnityEngine.EventSystems.UIBehaviour, global::UnityEngine.EventSystems.IMoveHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.IPointerDownHandler, global::UnityEngine.EventSystems.IPointerUpHandler, global::UnityEngine.EventSystems.IPointerEnterHandler, global::UnityEngine.EventSystems.IPointerExitHandler, global::UnityEngine.EventSystems.ISelectHandler, global::UnityEngine.EventSystems.IDeselectHandler
	{
		public enum Transition
		{
			None = 0,
			ColorTint = 1,
			SpriteSwap = 2,
			Animation = 3
		}

		protected enum SelectionState
		{
			Normal = 0,
			Highlighted = 1,
			Pressed = 2,
			Selected = 3,
			Disabled = 4
		}

		protected static global::UnityEngine.UI.Selectable[] s_Selectables = new global::UnityEngine.UI.Selectable[10];

		protected static int s_SelectableCount = 0;

		private bool m_EnableCalled;

		[global::UnityEngine.Serialization.FormerlySerializedAs("navigation")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Navigation m_Navigation = global::UnityEngine.UI.Navigation.defaultNavigation;

		[global::UnityEngine.Serialization.FormerlySerializedAs("transition")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Selectable.Transition m_Transition = global::UnityEngine.UI.Selectable.Transition.ColorTint;

		[global::UnityEngine.Serialization.FormerlySerializedAs("colors")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.ColorBlock m_Colors = global::UnityEngine.UI.ColorBlock.defaultColorBlock;

		[global::UnityEngine.Serialization.FormerlySerializedAs("spriteState")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.SpriteState m_SpriteState;

		[global::UnityEngine.Serialization.FormerlySerializedAs("animationTriggers")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.AnimationTriggers m_AnimationTriggers = new global::UnityEngine.UI.AnimationTriggers();

		[global::UnityEngine.Tooltip("Can the Selectable be interacted with?")]
		[global::UnityEngine.SerializeField]
		private bool m_Interactable = true;

		[global::UnityEngine.Serialization.FormerlySerializedAs("highlightGraphic")]
		[global::UnityEngine.Serialization.FormerlySerializedAs("m_HighlightGraphic")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Graphic m_TargetGraphic;

		private bool m_GroupsAllowInteraction = true;

		protected int m_CurrentIndex = -1;

		private readonly global::System.Collections.Generic.List<global::UnityEngine.CanvasGroup> m_CanvasGroupCache = new global::System.Collections.Generic.List<global::UnityEngine.CanvasGroup>();

		public static global::UnityEngine.UI.Selectable[] allSelectablesArray
		{
			get
			{
				global::UnityEngine.UI.Selectable[] array = new global::UnityEngine.UI.Selectable[s_SelectableCount];
				global::System.Array.Copy(s_Selectables, array, s_SelectableCount);
				return array;
			}
		}

		public static int allSelectableCount => s_SelectableCount;

		[global::System.Obsolete("Replaced with allSelectablesArray to have better performance when disabling a element", false)]
		public static global::System.Collections.Generic.List<global::UnityEngine.UI.Selectable> allSelectables => new global::System.Collections.Generic.List<global::UnityEngine.UI.Selectable>(allSelectablesArray);

		public global::UnityEngine.UI.Navigation navigation
		{
			get
			{
				return m_Navigation;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_Navigation, value))
				{
					OnSetProperty();
				}
			}
		}

		public global::UnityEngine.UI.Selectable.Transition transition
		{
			get
			{
				return m_Transition;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_Transition, value))
				{
					OnSetProperty();
				}
			}
		}

		public global::UnityEngine.UI.ColorBlock colors
		{
			get
			{
				return m_Colors;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_Colors, value))
				{
					OnSetProperty();
				}
			}
		}

		public global::UnityEngine.UI.SpriteState spriteState
		{
			get
			{
				return m_SpriteState;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_SpriteState, value))
				{
					OnSetProperty();
				}
			}
		}

		public global::UnityEngine.UI.AnimationTriggers animationTriggers
		{
			get
			{
				return m_AnimationTriggers;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_AnimationTriggers, value))
				{
					OnSetProperty();
				}
			}
		}

		public global::UnityEngine.UI.Graphic targetGraphic
		{
			get
			{
				return m_TargetGraphic;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetClass(ref m_TargetGraphic, value))
				{
					OnSetProperty();
				}
			}
		}

		public bool interactable
		{
			get
			{
				return m_Interactable;
			}
			set
			{
				if (global::UnityEngine.UI.SetPropertyUtility.SetStruct(ref m_Interactable, value))
				{
					if (!m_Interactable && global::UnityEngine.EventSystems.EventSystem.current != null && global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == base.gameObject)
					{
						global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
					}
					OnSetProperty();
				}
			}
		}

		private bool isPointerInside { get; set; }

		private bool isPointerDown { get; set; }

		private bool hasSelection { get; set; }

		public global::UnityEngine.UI.Image image
		{
			get
			{
				return m_TargetGraphic as global::UnityEngine.UI.Image;
			}
			set
			{
				m_TargetGraphic = value;
			}
		}

		public global::UnityEngine.Animator animator => GetComponent<global::UnityEngine.Animator>();

		protected global::UnityEngine.UI.Selectable.SelectionState currentSelectionState
		{
			get
			{
				if (!IsInteractable())
				{
					return global::UnityEngine.UI.Selectable.SelectionState.Disabled;
				}
				if (isPointerDown)
				{
					return global::UnityEngine.UI.Selectable.SelectionState.Pressed;
				}
				if (hasSelection)
				{
					return global::UnityEngine.UI.Selectable.SelectionState.Selected;
				}
				if (isPointerInside)
				{
					return global::UnityEngine.UI.Selectable.SelectionState.Highlighted;
				}
				return global::UnityEngine.UI.Selectable.SelectionState.Normal;
			}
		}

		public static int AllSelectablesNoAlloc(global::UnityEngine.UI.Selectable[] selectables)
		{
			int num = ((selectables.Length < s_SelectableCount) ? selectables.Length : s_SelectableCount);
			global::System.Array.Copy(s_Selectables, selectables, num);
			return num;
		}

		protected Selectable()
		{
		}

		protected override void Awake()
		{
			if (m_TargetGraphic == null)
			{
				m_TargetGraphic = GetComponent<global::UnityEngine.UI.Graphic>();
			}
		}

		protected override void OnCanvasGroupChanged()
		{
			bool flag = ParentGroupAllowsInteraction();
			if (flag != m_GroupsAllowInteraction)
			{
				m_GroupsAllowInteraction = flag;
				OnSetProperty();
			}
		}

		private bool ParentGroupAllowsInteraction()
		{
			global::UnityEngine.Transform parent = base.transform;
			while (parent != null)
			{
				parent.GetComponents(m_CanvasGroupCache);
				for (int i = 0; i < m_CanvasGroupCache.Count; i++)
				{
					if (m_CanvasGroupCache[i].enabled && !m_CanvasGroupCache[i].interactable)
					{
						return false;
					}
					if (m_CanvasGroupCache[i].ignoreParentGroups)
					{
						return true;
					}
				}
				parent = parent.parent;
			}
			return true;
		}

		public virtual bool IsInteractable()
		{
			if (m_GroupsAllowInteraction)
			{
				return m_Interactable;
			}
			return false;
		}

		protected override void OnDidApplyAnimationProperties()
		{
			OnSetProperty();
		}

		protected override void OnEnable()
		{
			if (!m_EnableCalled)
			{
				base.OnEnable();
				if (s_SelectableCount == s_Selectables.Length)
				{
					global::UnityEngine.UI.Selectable[] destinationArray = new global::UnityEngine.UI.Selectable[s_Selectables.Length * 2];
					global::System.Array.Copy(s_Selectables, destinationArray, s_Selectables.Length);
					s_Selectables = destinationArray;
				}
				if ((bool)global::UnityEngine.EventSystems.EventSystem.current && global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == base.gameObject)
				{
					hasSelection = true;
				}
				m_CurrentIndex = s_SelectableCount;
				s_Selectables[m_CurrentIndex] = this;
				s_SelectableCount++;
				isPointerDown = false;
				m_GroupsAllowInteraction = ParentGroupAllowsInteraction();
				DoStateTransition(currentSelectionState, instant: true);
				m_EnableCalled = true;
			}
		}

		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			OnCanvasGroupChanged();
		}

		private void OnSetProperty()
		{
			DoStateTransition(currentSelectionState, instant: false);
		}

		protected override void OnDisable()
		{
			if (m_EnableCalled)
			{
				s_SelectableCount--;
				s_Selectables[s_SelectableCount].m_CurrentIndex = m_CurrentIndex;
				s_Selectables[m_CurrentIndex] = s_Selectables[s_SelectableCount];
				s_Selectables[s_SelectableCount] = null;
				InstantClearState();
				base.OnDisable();
				m_EnableCalled = false;
			}
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			if (!hasFocus && IsPressed())
			{
				InstantClearState();
			}
		}

		protected virtual void InstantClearState()
		{
			string normalTrigger = m_AnimationTriggers.normalTrigger;
			isPointerInside = false;
			isPointerDown = false;
			hasSelection = false;
			switch (m_Transition)
			{
			case global::UnityEngine.UI.Selectable.Transition.ColorTint:
				StartColorTween(global::UnityEngine.Color.white, instant: true);
				break;
			case global::UnityEngine.UI.Selectable.Transition.SpriteSwap:
				DoSpriteSwap(null);
				break;
			case global::UnityEngine.UI.Selectable.Transition.Animation:
				TriggerAnimation(normalTrigger);
				break;
			}
		}

		protected virtual void DoStateTransition(global::UnityEngine.UI.Selectable.SelectionState state, bool instant)
		{
			if (base.gameObject.activeInHierarchy)
			{
				global::UnityEngine.Color color;
				global::UnityEngine.Sprite newSprite;
				string triggername;
				switch (state)
				{
				case global::UnityEngine.UI.Selectable.SelectionState.Normal:
					color = m_Colors.normalColor;
					newSprite = null;
					triggername = m_AnimationTriggers.normalTrigger;
					break;
				case global::UnityEngine.UI.Selectable.SelectionState.Highlighted:
					color = m_Colors.highlightedColor;
					newSprite = m_SpriteState.highlightedSprite;
					triggername = m_AnimationTriggers.highlightedTrigger;
					break;
				case global::UnityEngine.UI.Selectable.SelectionState.Pressed:
					color = m_Colors.pressedColor;
					newSprite = m_SpriteState.pressedSprite;
					triggername = m_AnimationTriggers.pressedTrigger;
					break;
				case global::UnityEngine.UI.Selectable.SelectionState.Selected:
					color = m_Colors.selectedColor;
					newSprite = m_SpriteState.selectedSprite;
					triggername = m_AnimationTriggers.selectedTrigger;
					break;
				case global::UnityEngine.UI.Selectable.SelectionState.Disabled:
					color = m_Colors.disabledColor;
					newSprite = m_SpriteState.disabledSprite;
					triggername = m_AnimationTriggers.disabledTrigger;
					break;
				default:
					color = global::UnityEngine.Color.black;
					newSprite = null;
					triggername = string.Empty;
					break;
				}
				switch (m_Transition)
				{
				case global::UnityEngine.UI.Selectable.Transition.ColorTint:
					StartColorTween(color * m_Colors.colorMultiplier, instant);
					break;
				case global::UnityEngine.UI.Selectable.Transition.SpriteSwap:
					DoSpriteSwap(newSprite);
					break;
				case global::UnityEngine.UI.Selectable.Transition.Animation:
					TriggerAnimation(triggername);
					break;
				}
			}
		}

		public global::UnityEngine.UI.Selectable FindSelectable(global::UnityEngine.Vector3 dir)
		{
			dir = dir.normalized;
			global::UnityEngine.Vector3 vector = global::UnityEngine.Quaternion.Inverse(base.transform.rotation) * dir;
			global::UnityEngine.Vector3 vector2 = base.transform.TransformPoint(GetPointOnRectEdge(base.transform as global::UnityEngine.RectTransform, vector));
			float num = float.NegativeInfinity;
			float num2 = float.NegativeInfinity;
			float num3 = 0f;
			bool flag = navigation.wrapAround && (m_Navigation.mode == global::UnityEngine.UI.Navigation.Mode.Vertical || m_Navigation.mode == global::UnityEngine.UI.Navigation.Mode.Horizontal);
			global::UnityEngine.UI.Selectable selectable = null;
			global::UnityEngine.UI.Selectable result = null;
			for (int i = 0; i < s_SelectableCount; i++)
			{
				global::UnityEngine.UI.Selectable selectable2 = s_Selectables[i];
				if (selectable2 == this || !selectable2.IsInteractable() || selectable2.navigation.mode == global::UnityEngine.UI.Navigation.Mode.None)
				{
					continue;
				}
				global::UnityEngine.RectTransform rectTransform = selectable2.transform as global::UnityEngine.RectTransform;
				global::UnityEngine.Vector3 position = ((rectTransform != null) ? ((global::UnityEngine.Vector3)rectTransform.rect.center) : global::UnityEngine.Vector3.zero);
				global::UnityEngine.Vector3 rhs = selectable2.transform.TransformPoint(position) - vector2;
				float num4 = global::UnityEngine.Vector3.Dot(dir, rhs);
				if (flag && num4 < 0f)
				{
					num3 = (0f - num4) * rhs.sqrMagnitude;
					if (num3 > num2)
					{
						num2 = num3;
						result = selectable2;
					}
				}
				else if (!(num4 <= 0f))
				{
					num3 = num4 / rhs.sqrMagnitude;
					if (num3 > num)
					{
						num = num3;
						selectable = selectable2;
					}
				}
			}
			if (flag && null == selectable)
			{
				return result;
			}
			return selectable;
		}

		private static global::UnityEngine.Vector3 GetPointOnRectEdge(global::UnityEngine.RectTransform rect, global::UnityEngine.Vector2 dir)
		{
			if (rect == null)
			{
				return global::UnityEngine.Vector3.zero;
			}
			if (dir != global::UnityEngine.Vector2.zero)
			{
				dir /= global::UnityEngine.Mathf.Max(global::UnityEngine.Mathf.Abs(dir.x), global::UnityEngine.Mathf.Abs(dir.y));
			}
			dir = rect.rect.center + global::UnityEngine.Vector2.Scale(rect.rect.size, dir * 0.5f);
			return dir;
		}

		private void Navigate(global::UnityEngine.EventSystems.AxisEventData eventData, global::UnityEngine.UI.Selectable sel)
		{
			if (sel != null && sel.IsActive())
			{
				eventData.selectedObject = sel.gameObject;
			}
		}

		public virtual global::UnityEngine.UI.Selectable FindSelectableOnLeft()
		{
			if (m_Navigation.mode == global::UnityEngine.UI.Navigation.Mode.Explicit)
			{
				return m_Navigation.selectOnLeft;
			}
			if ((m_Navigation.mode & global::UnityEngine.UI.Navigation.Mode.Horizontal) != global::UnityEngine.UI.Navigation.Mode.None)
			{
				return FindSelectable(base.transform.rotation * global::UnityEngine.Vector3.left);
			}
			return null;
		}

		public virtual global::UnityEngine.UI.Selectable FindSelectableOnRight()
		{
			if (m_Navigation.mode == global::UnityEngine.UI.Navigation.Mode.Explicit)
			{
				return m_Navigation.selectOnRight;
			}
			if ((m_Navigation.mode & global::UnityEngine.UI.Navigation.Mode.Horizontal) != global::UnityEngine.UI.Navigation.Mode.None)
			{
				return FindSelectable(base.transform.rotation * global::UnityEngine.Vector3.right);
			}
			return null;
		}

		public virtual global::UnityEngine.UI.Selectable FindSelectableOnUp()
		{
			if (m_Navigation.mode == global::UnityEngine.UI.Navigation.Mode.Explicit)
			{
				return m_Navigation.selectOnUp;
			}
			if ((m_Navigation.mode & global::UnityEngine.UI.Navigation.Mode.Vertical) != global::UnityEngine.UI.Navigation.Mode.None)
			{
				return FindSelectable(base.transform.rotation * global::UnityEngine.Vector3.up);
			}
			return null;
		}

		public virtual global::UnityEngine.UI.Selectable FindSelectableOnDown()
		{
			if (m_Navigation.mode == global::UnityEngine.UI.Navigation.Mode.Explicit)
			{
				return m_Navigation.selectOnDown;
			}
			if ((m_Navigation.mode & global::UnityEngine.UI.Navigation.Mode.Vertical) != global::UnityEngine.UI.Navigation.Mode.None)
			{
				return FindSelectable(base.transform.rotation * global::UnityEngine.Vector3.down);
			}
			return null;
		}

		public virtual void OnMove(global::UnityEngine.EventSystems.AxisEventData eventData)
		{
			switch (eventData.moveDir)
			{
			case global::UnityEngine.EventSystems.MoveDirection.Right:
				Navigate(eventData, FindSelectableOnRight());
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Up:
				Navigate(eventData, FindSelectableOnUp());
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Left:
				Navigate(eventData, FindSelectableOnLeft());
				break;
			case global::UnityEngine.EventSystems.MoveDirection.Down:
				Navigate(eventData, FindSelectableOnDown());
				break;
			}
		}

		private void StartColorTween(global::UnityEngine.Color targetColor, bool instant)
		{
			if (!(m_TargetGraphic == null))
			{
				m_TargetGraphic.CrossFadeColor(targetColor, instant ? 0f : m_Colors.fadeDuration, ignoreTimeScale: true, useAlpha: true);
			}
		}

		private void DoSpriteSwap(global::UnityEngine.Sprite newSprite)
		{
			if (!(image == null))
			{
				image.overrideSprite = newSprite;
			}
		}

		private void TriggerAnimation(string triggername)
		{
			if (transition == global::UnityEngine.UI.Selectable.Transition.Animation && !(animator == null) && animator.isActiveAndEnabled && animator.hasBoundPlayables && !string.IsNullOrEmpty(triggername))
			{
				animator.ResetTrigger(m_AnimationTriggers.normalTrigger);
				animator.ResetTrigger(m_AnimationTriggers.highlightedTrigger);
				animator.ResetTrigger(m_AnimationTriggers.pressedTrigger);
				animator.ResetTrigger(m_AnimationTriggers.selectedTrigger);
				animator.ResetTrigger(m_AnimationTriggers.disabledTrigger);
				animator.SetTrigger(triggername);
			}
		}

		protected bool IsHighlighted()
		{
			if (!IsActive() || !IsInteractable())
			{
				return false;
			}
			if (isPointerInside && !isPointerDown)
			{
				return !hasSelection;
			}
			return false;
		}

		protected bool IsPressed()
		{
			if (!IsActive() || !IsInteractable())
			{
				return false;
			}
			return isPointerDown;
		}

		private void EvaluateAndTransitionToSelectionState()
		{
			if (IsActive() && IsInteractable())
			{
				DoStateTransition(currentSelectionState, instant: false);
			}
		}

		public virtual void OnPointerDown(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left)
			{
				if (IsInteractable() && navigation.mode != global::UnityEngine.UI.Navigation.Mode.None && global::UnityEngine.EventSystems.EventSystem.current != null)
				{
					global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
				}
				isPointerDown = true;
				EvaluateAndTransitionToSelectionState();
			}
		}

		public virtual void OnPointerUp(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left)
			{
				isPointerDown = false;
				EvaluateAndTransitionToSelectionState();
			}
		}

		public virtual void OnPointerEnter(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			isPointerInside = true;
			EvaluateAndTransitionToSelectionState();
		}

		public virtual void OnPointerExit(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			isPointerInside = false;
			EvaluateAndTransitionToSelectionState();
		}

		public virtual void OnSelect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			hasSelection = true;
			EvaluateAndTransitionToSelectionState();
		}

		public virtual void OnDeselect(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			hasSelection = false;
			EvaluateAndTransitionToSelectionState();
		}

		public virtual void Select()
		{
			if (!(global::UnityEngine.EventSystems.EventSystem.current == null) && !global::UnityEngine.EventSystems.EventSystem.current.alreadySelecting)
			{
				global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(base.gameObject);
			}
		}
	}
}
