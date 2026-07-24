namespace UnityEngine.InputSystem.Composites
{
	[global::UnityEngine.InputSystem.Utilities.DisplayStringFormat("{up}/{left}/{down}/{right}")]
	[global::System.ComponentModel.DisplayName("Up/Down/Left/Right Composite")]
	public class Vector2Composite : global::UnityEngine.InputSystem.InputBindingComposite<global::UnityEngine.Vector2>
	{
		public enum Mode
		{
			Analog = 2,
			DigitalNormalized = 0,
			Digital = 1
		}

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int up;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int down;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int left;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int right;

		[global::System.Obsolete("Use Mode.DigitalNormalized with 'mode' instead")]
		public bool normalize = true;

		public global::UnityEngine.InputSystem.Composites.Vector2Composite.Mode mode;

		public override global::UnityEngine.Vector2 ReadValue(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			global::UnityEngine.InputSystem.Composites.Vector2Composite.Mode mode = this.mode;
			if (mode == global::UnityEngine.InputSystem.Composites.Vector2Composite.Mode.Analog)
			{
				float num = context.ReadValue<float>(up);
				float num2 = context.ReadValue<float>(down);
				float num3 = context.ReadValue<float>(left);
				float num4 = context.ReadValue<float>(right);
				return global::UnityEngine.InputSystem.Controls.DpadControl.MakeDpadVector(num, num2, num3, num4);
			}
			bool num5 = context.ReadValueAsButton(up);
			bool flag = context.ReadValueAsButton(down);
			bool flag2 = context.ReadValueAsButton(left);
			bool flag3 = context.ReadValueAsButton(right);
			if (!normalize)
			{
				mode = global::UnityEngine.InputSystem.Composites.Vector2Composite.Mode.Digital;
			}
			return global::UnityEngine.InputSystem.Controls.DpadControl.MakeDpadVector(num5, flag, flag2, flag3, mode == global::UnityEngine.InputSystem.Composites.Vector2Composite.Mode.DigitalNormalized);
		}

		public override float EvaluateMagnitude(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			return ReadValue(ref context).magnitude;
		}
	}
}
