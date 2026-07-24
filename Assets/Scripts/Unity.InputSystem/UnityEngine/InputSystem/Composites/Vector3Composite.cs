namespace UnityEngine.InputSystem.Composites
{
	[global::UnityEngine.InputSystem.Utilities.DisplayStringFormat("{up}+{down}/{left}+{right}/{forward}+{backward}")]
	[global::System.ComponentModel.DisplayName("Up/Down/Left/Right/Forward/Backward Composite")]
	public class Vector3Composite : global::UnityEngine.InputSystem.InputBindingComposite<global::UnityEngine.Vector3>
	{
		public enum Mode
		{
			Analog = 0,
			DigitalNormalized = 1,
			Digital = 2
		}

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int up;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int down;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int left;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int right;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int forward;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Axis")]
		public int backward;

		public global::UnityEngine.InputSystem.Composites.Vector3Composite.Mode mode;

		public override global::UnityEngine.Vector3 ReadValue(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			if (mode == global::UnityEngine.InputSystem.Composites.Vector3Composite.Mode.Analog)
			{
				float num = context.ReadValue<float>(up);
				float num2 = context.ReadValue<float>(down);
				float num3 = context.ReadValue<float>(left);
				float num4 = context.ReadValue<float>(right);
				float num5 = context.ReadValue<float>(forward);
				float num6 = context.ReadValue<float>(backward);
				return new global::UnityEngine.Vector3(num4 - num3, num - num2, num5 - num6);
			}
			float num7 = (context.ReadValueAsButton(up) ? 1f : 0f);
			float num8 = (context.ReadValueAsButton(down) ? (-1f) : 0f);
			float num9 = (context.ReadValueAsButton(left) ? (-1f) : 0f);
			float num10 = (context.ReadValueAsButton(right) ? 1f : 0f);
			float num11 = (context.ReadValueAsButton(forward) ? 1f : 0f);
			float num12 = (context.ReadValueAsButton(backward) ? (-1f) : 0f);
			global::UnityEngine.Vector3 result = new global::UnityEngine.Vector3(num9 + num10, num7 + num8, num11 + num12);
			if (mode == global::UnityEngine.InputSystem.Composites.Vector3Composite.Mode.DigitalNormalized)
			{
				return result.normalized;
			}
			return result;
		}

		public override float EvaluateMagnitude(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			return ReadValue(ref context).magnitude;
		}
	}
}
