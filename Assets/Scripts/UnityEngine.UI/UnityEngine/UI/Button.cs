namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Button", 30)]
	public class Button : global::UnityEngine.UI.Selectable, global::UnityEngine.EventSystems.IPointerClickHandler, global::UnityEngine.EventSystems.IEventSystemHandler, global::UnityEngine.EventSystems.ISubmitHandler
	{
		[global::System.Serializable]
		public class ButtonClickedEvent : global::UnityEngine.Events.UnityEvent
		{
		}

		[global::UnityEngine.Serialization.FormerlySerializedAs("onClick")]
		[global::UnityEngine.SerializeField]
		private global::UnityEngine.UI.Button.ButtonClickedEvent m_OnClick = new global::UnityEngine.UI.Button.ButtonClickedEvent();

		public global::UnityEngine.UI.Button.ButtonClickedEvent onClick
		{
			get
			{
				return m_OnClick;
			}
			set
			{
				m_OnClick = value;
			}
		}

		protected Button()
		{
		}

		private void Press()
		{
			if (IsActive() && IsInteractable())
			{
				global::UnityEngine.UISystemProfilerApi.AddMarker("Button.onClick", this);
				m_OnClick.Invoke();
			}
		}

		public virtual void OnPointerClick(global::UnityEngine.EventSystems.PointerEventData eventData)
		{
			if (eventData.button == global::UnityEngine.EventSystems.PointerEventData.InputButton.Left)
			{
				Press();
			}
		}

		public virtual void OnSubmit(global::UnityEngine.EventSystems.BaseEventData eventData)
		{
			Press();
			if (IsActive() && IsInteractable())
			{
				DoStateTransition(global::UnityEngine.UI.Selectable.SelectionState.Pressed, instant: false);
				StartCoroutine(OnFinishSubmit());
			}
		}

		private global::System.Collections.IEnumerator OnFinishSubmit()
		{
			float fadeTime = base.colors.fadeDuration;
			float elapsedTime = 0f;
			while (elapsedTime < fadeTime)
			{
				elapsedTime += global::UnityEngine.Time.unscaledDeltaTime;
				yield return null;
			}
			DoStateTransition(base.currentSelectionState, instant: false);
		}
	}
}
