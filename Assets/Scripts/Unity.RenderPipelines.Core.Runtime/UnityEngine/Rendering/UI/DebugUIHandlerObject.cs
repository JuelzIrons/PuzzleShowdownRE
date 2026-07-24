namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerObject : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.Text valueLabel;

		internal override void SetWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			global::UnityEngine.Rendering.DebugUI.ObjectField objectField = CastWidget<global::UnityEngine.Rendering.DebugUI.ObjectField>();
			nameLabel.text = objectField.displayName;
			global::UnityEngine.Object value = objectField.GetValue();
			valueLabel.text = ((value != null) ? value.name : "None");
		}

		public override bool OnSelection(bool fromNext, global::UnityEngine.Rendering.UI.DebugUIHandlerWidget previous)
		{
			nameLabel.color = colorSelected;
			valueLabel.color = colorSelected;
			return true;
		}

		public override void OnDeselection()
		{
			nameLabel.color = colorDefault;
			valueLabel.color = colorDefault;
		}
	}
}
