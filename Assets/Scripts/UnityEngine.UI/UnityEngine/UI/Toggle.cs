namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Toggle", 30)]
	[global::UnityEngine.RequireComponent(typeof(global::UnityEngine.RectTransform))]
	public class Toggle : global::UnityEngine.UI.Selectable, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.ISubmitHandler, global::UnityEngine.UI.ICanvasElement
	{
		public enum ToggleTransition
		{
			None = 0,
			Fade = 1
		}

		[global::System.Serializable]
		public class ToggleEvent : global::UnityEngine.Events.UnityEvent<bool>
		{
		}

		public global::UnityEngine.UI.Toggle.ToggleTransition toggleTransition = global::UnityEngine.UI.Toggle.ToggleTransition.Fade;

		public global::UnityEngine.UI.Graphic graphic;

		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.ToggleGroup m_Group;

		public global::UnityEngine.UI.Toggle.ToggleEvent onValueChanged = new global::UnityEngine.UI.Toggle.ToggleEvent();

		[global::UnityEngine.Tooltip("Is the toggle currently on or off?")]
		[global::UnityEngine.SerializeField]
		private bool m_IsOn;

		public global::UnityEngine.UI.ToggleGroup group
		{
			get
			{
				return m_Group;
			}
			set
			{
				SetToggleGroup(value, setMemberValue: true);
				PlayEffect(instant: true);
			}
		}

		public bool isOn
		{
			get
			{
				return m_IsOn;
			}
			set
			{
				Set(value);
			}
		}

		global::UnityEngine.Transform global::UnityEngine.UI.ICanvasElement.transform => base.transform;

		protected Toggle()
		{
		}

		public virtual void Rebuild(global::UnityEngine.UI.CanvasUpdate executing)
		{
		}

		public virtual void LayoutComplete()
		{
		}

		public virtual void GraphicUpdateComplete()
		{
		}

		protected override void OnDestroy()
		{
			if (m_Group != null)
			{
				m_Group.EnsureValidState();
			}
			base.OnDestroy();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			SetToggleGroup(m_Group, setMemberValue: false);
			PlayEffect(instant: true);
		}

		protected override void OnDisable()
		{
			SetToggleGroup(null, setMemberValue: false);
			base.OnDisable();
		}

		protected override void OnDidApplyAnimationProperties()
		{
			if (graphic != null)
			{
				bool flag = !global::UnityEngine.Mathf.Approximately(graphic.canvasRenderer.GetColor().a, 0f);
				if (m_IsOn != flag)
				{
					m_IsOn = flag;
					Set(!flag);
				}
			}
			base.OnDidApplyAnimationProperties();
		}

		private void SetToggleGroup(global::UnityEngine.UI.ToggleGroup newGroup, bool setMemberValue)
		{
			if (m_Group != null)
			{
				m_Group.UnregisterToggle(this);
			}
			if (setMemberValue)
			{
				m_Group = newGroup;
			}
			if (newGroup != null && IsActive())
			{
				newGroup.RegisterToggle(this);
			}
			if (newGroup != null && isOn && IsActive())
			{
				newGroup.NotifyToggleOn(this);
			}
		}

		public void SetIsOnWithoutNotify(bool value)
		{
			Set(value, sendCallback: false);
		}

		private void Set(bool value, bool sendCallback = true)
		{
			if (m_IsOn != value)
			{
				m_IsOn = value;
				if (m_Group != null && m_Group.isActiveAndEnabled && IsActive() && (m_IsOn || (!m_Group.AnyTogglesOn() && !m_Group.allowSwitchOff)))
				{
					m_IsOn = true;
					m_Group.NotifyToggleOn(this, sendCallback);
				}
				PlayEffect(toggleTransition == global::UnityEngine.UI.Toggle.ToggleTransition.None);
				if (sendCallback)
				{
					global::UnityEngine.UISystemProfilerApi.AddMarker("Toggle.value", this);
					onValueChanged.Invoke(m_IsOn);
				}
			}
		}

		private void PlayEffect(bool instant)
		{
			if (!(graphic == null))
			{
				graphic.CrossFadeAlpha(m_IsOn ? 1f : 0f, instant ? 0f : 0.1f, ignoreTimeScale: true);
			}
		}

		protected override void Start()
		{
			PlayEffect(instant: true);
		}

		private void InternalToggle()
		{
			if (IsActive() && IsInteractable())
			{
				isOn = !isOn;
			}
		}

		public virtual void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left)
			{
				InternalToggle();
			}
		}

		public virtual void OnSubmit(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			InternalToggle();
		}
	}
}
