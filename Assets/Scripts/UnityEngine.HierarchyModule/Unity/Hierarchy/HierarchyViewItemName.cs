namespace Unity.Hierarchy
{
	[global::UnityEngine.UIElements.UxmlElement]
	[global::UnityEngine.Bindings.VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
	internal class HierarchyViewItemName : global::UnityEngine.UIElements.VisualElement
	{
		internal const string k_StyleName = "hierarchy-item__name";

		private bool m_PrewarmControl;

		public string Text
		{
			get
			{
				return Label.text;
			}
			set
			{
				Label.text = value;
			}
		}

		internal bool IsRenaming { get; set; }

		public global::UnityEngine.UIElements.Label Label { get; } = new global::UnityEngine.UIElements.Label();

		private global::UnityEngine.UIElements.TextField TextField { get; } = new global::UnityEngine.UIElements.TextField();

		public event global::System.Action OnBeginRename;

		public event global::System.Action<string, bool> OnEndRename;

		public HierarchyViewItemName()
		{
			AddToClassList("hierarchy-item__name");
			focusable = true;
			base.delegatesFocus = false;
			m_PrewarmControl = false;
			Add(Label);
			Add(TextField);
			TextField.selectAllOnFocus = true;
			TextField.selectAllOnMouseUp = false;
			TextField.style.display = global::UnityEngine.UIElements.DisplayStyle.None;
			TextField.RegisterCallback<global::UnityEngine.UIElements.MouseUpEvent>(OnMouseUpEvent);
			TextField.RegisterCallback<global::UnityEngine.UIElements.KeyDownEvent>(OnInterceptKeyDownEvent, global::UnityEngine.UIElements.TrickleDown.TrickleDown);
			TextField.RegisterCallback<global::UnityEngine.UIElements.KeyDownEvent>(OnKeyDownEvent);
			TextField.RegisterCallback<global::UnityEngine.UIElements.BlurEvent>(OnBlurEvent);
		}

		public void BeginRename()
		{
			if (!IsRenaming)
			{
				IsRenaming = true;
				base.delegatesFocus = true;
				m_PrewarmControl = true;
				Label.style.display = global::UnityEngine.UIElements.DisplayStyle.None;
				TextField.style.display = global::UnityEngine.UIElements.DisplayStyle.Flex;
				TextField.value = Text;
				global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.TextElement>(TextField).Focus();
				this.OnBeginRename?.Invoke();
			}
		}

		public void CancelRename()
		{
			if (IsRenaming)
			{
				EndRename(canceled: true);
			}
		}

		private void EndRename(bool canceled = false)
		{
			IsRenaming = false;
			base.delegatesFocus = false;
			m_PrewarmControl = false;
			TextField.style.display = global::UnityEngine.UIElements.DisplayStyle.None;
			Label.style.display = global::UnityEngine.UIElements.DisplayStyle.Flex;
			if (!canceled && !string.IsNullOrEmpty(TextField.value))
			{
				Label.text = TextField.value;
			}
			this.OnEndRename?.Invoke(Text, canceled);
		}

		private void OnMouseUpEvent(global::UnityEngine.UIElements.MouseUpEvent evt)
		{
			if (IsRenaming)
			{
				global::UnityEngine.UIElements.UQueryExtensions.Q<global::UnityEngine.UIElements.TextElement>(TextField).Focus();
				evt.StopPropagation();
			}
		}

		private void OnInterceptKeyDownEvent(global::UnityEngine.UIElements.KeyDownEvent evt)
		{
			if (m_PrewarmControl)
			{
				if (evt.keyCode == global::UnityEngine.KeyCode.None)
				{
					evt.StopPropagation();
				}
				else
				{
					m_PrewarmControl = false;
				}
			}
		}

		private void OnKeyDownEvent(global::UnityEngine.UIElements.KeyDownEvent evt)
		{
			if (IsRenaming && evt.keyCode == global::UnityEngine.KeyCode.Escape)
			{
				EndRename(canceled: true);
			}
			evt.StopPropagation();
		}

		private void OnBlurEvent(global::UnityEngine.UIElements.BlurEvent evt)
		{
			if (IsRenaming)
			{
				EndRename();
			}
		}
	}
}
