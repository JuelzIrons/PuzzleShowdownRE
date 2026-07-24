namespace UnityEngine.InputSystem.Controls
{
	public class DpadControl : global::UnityEngine.InputSystem.Controls.Vector2Control
	{
		[global::UnityEngine.InputSystem.Layouts.InputControlLayout(hideInUI = true)]
		public class DpadAxisControl : global::UnityEngine.InputSystem.Controls.AxisControl
		{
			public int component { get; set; }

			protected override void FinishSetup()
			{
				base.FinishSetup();
				component = ((!(base.name == "x")) ? 1 : 0);
				m_StateBlock = m_Parent.m_StateBlock;
			}

			public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
			{
				return ((global::UnityEngine.InputSystem.Controls.DpadControl)m_Parent).ReadUnprocessedValueFromState(statePtr)[component];
			}
		}

		internal enum ButtonBits
		{
			Up = 0,
			Down = 1,
			Left = 2,
			Right = 3
		}

		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "x", layout = "DpadAxis", useStateFrom = "right", synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(name = "y", layout = "DpadAxis", useStateFrom = "up", synthetic = true)]
		[global::UnityEngine.InputSystem.Layouts.InputControl(bit = 0u, displayName = "Up")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl up { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(bit = 1u, displayName = "Down")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl down { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(bit = 2u, displayName = "Left")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl left { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(bit = 3u, displayName = "Right")]
		public global::UnityEngine.InputSystem.Controls.ButtonControl right { get; set; }

		public DpadControl()
		{
			m_StateBlock.sizeInBits = 4u;
			m_StateBlock.format = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit;
		}

		protected override void FinishSetup()
		{
			up = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("up");
			down = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("down");
			left = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("left");
			right = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("right");
			base.FinishSetup();
		}

		public unsafe override global::UnityEngine.Vector2 ReadUnprocessedValueFromState(void* statePtr)
		{
			bool num = up.ReadValueFromStateWithCaching(statePtr) >= up.pressPointOrDefault;
			bool flag = down.ReadValueFromStateWithCaching(statePtr) >= down.pressPointOrDefault;
			bool flag2 = left.ReadValueFromStateWithCaching(statePtr) >= left.pressPointOrDefault;
			bool flag3 = right.ReadValueFromStateWithCaching(statePtr) >= right.pressPointOrDefault;
			return MakeDpadVector(num, flag, flag2, flag3);
		}

		public unsafe override void WriteValueIntoState(global::UnityEngine.Vector2 value, void* statePtr)
		{
			bool flag = up.IsValueConsideredPressed(value.y);
			bool flag2 = down.IsValueConsideredPressed(value.y * -1f);
			bool flag3 = left.IsValueConsideredPressed(value.x * -1f);
			bool flag4 = right.IsValueConsideredPressed(value.x);
			up.WriteValueIntoState((flag && !flag2) ? value.y : 0f, statePtr);
			down.WriteValueIntoState((flag2 && !flag) ? (value.y * -1f) : 0f, statePtr);
			left.WriteValueIntoState((flag3 && !flag4) ? (value.x * -1f) : 0f, statePtr);
			right.WriteValueIntoState((flag4 && !flag3) ? value.x : 0f, statePtr);
		}

		public static global::UnityEngine.Vector2 MakeDpadVector(bool up, bool down, bool left, bool right, bool normalize = true)
		{
			float num = (up ? 1f : 0f);
			float num2 = (down ? (-1f) : 0f);
			float num3 = (left ? (-1f) : 0f);
			float num4 = (right ? 1f : 0f);
			global::UnityEngine.Vector2 result = new global::UnityEngine.Vector2(num3 + num4, num + num2);
			if (normalize && result.x != 0f && result.y != 0f)
			{
				result = new global::UnityEngine.Vector2(result.x * 0.707107f, result.y * 0.707107f);
			}
			return result;
		}

		public static global::UnityEngine.Vector2 MakeDpadVector(float up, float down, float left, float right)
		{
			return new global::UnityEngine.Vector2(0f - left + right, up - down);
		}
	}
}
