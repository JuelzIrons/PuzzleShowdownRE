namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerIndirectToggle : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.Toggle valueToggle;

		public global::UnityEngine.UI.Image checkmarkImage;

		public global::System.Func<int, bool> getter;

		public global::System.Action<int, bool> setter;

		internal int index;

		public void Init()
		{
			UpdateValueLabel();
			valueToggle.onValueChanged.AddListener(OnToggleValueChanged);
		}

		private void OnToggleValueChanged(bool value)
		{
			setter(index, value);
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			nameLabel.color = colorSelected;
			checkmarkImage.color = colorSelected;
			return true;
		}

		public override void OnDeselection()
		{
			nameLabel.color = colorDefault;
			checkmarkImage.color = colorDefault;
		}

		public override void OnAction()
		{
			bool arg = !getter(index);
			setter(index, arg);
			UpdateValueLabel();
		}

		internal void UpdateValueLabel()
		{
			if (valueToggle != null)
			{
				valueToggle.isOn = getter(index);
			}
		}
	}
}
