namespace UnityEngine.Rendering.UI
{
	public class DebugUIHandlerIndirectFloatField : global::UnityEngine.Rendering.UI.DebugUIHandlerWidget
	{
		public global::UnityEngine.UI.Text nameLabel;

		public global::UnityEngine.UI.Text valueLabel;

		public global::System.Func<float> getter;

		public global::System.Action<float> setter;

		public global::System.Func<float> incStepGetter;

		public global::System.Func<float> incStepMultGetter;

		public global::System.Func<float> decimalsGetter;

		public void Init()
		{
			UpdateValueLabel();
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

		public override void OnIncrement(bool fast)
		{
			ChangeValue(fast, 1f);
		}

		public override void OnDecrement(bool fast)
		{
			ChangeValue(fast, -1f);
		}

		private void ChangeValue(bool fast, float multiplier)
		{
			float num = getter();
			num += incStepGetter() * (fast ? incStepMultGetter() : 1f) * multiplier;
			setter(num);
			UpdateValueLabel();
		}

		private void UpdateValueLabel()
		{
			if (valueLabel != null)
			{
				valueLabel.text = getter().ToString("N" + decimalsGetter());
			}
		}
	}
}
