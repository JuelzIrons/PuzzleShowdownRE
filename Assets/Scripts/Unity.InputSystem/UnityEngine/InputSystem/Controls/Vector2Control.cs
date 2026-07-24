namespace UnityEngine.InputSystem.Controls
{
	public class Vector2Control : global::UnityEngine.InputSystem.InputControl<global::UnityEngine.Vector2>
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 0u, displayName = "X")]
		public global::UnityEngine.InputSystem.Controls.AxisControl x { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 4u, displayName = "Y")]
		public global::UnityEngine.InputSystem.Controls.AxisControl y { get; set; }

		public Vector2Control()
		{
			m_StateBlock.format = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatVector2;
		}

		protected override void FinishSetup()
		{
			x = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("x");
			y = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("y");
			base.FinishSetup();
		}

		public unsafe override global::UnityEngine.Vector2 ReadUnprocessedValueFromState(void* statePtr)
		{
			if ((int)m_OptimizedControlDataType == 1447379762)
			{
				return *(global::UnityEngine.Vector2*)((byte*)statePtr + (int)m_StateBlock.byteOffset);
			}
			return new global::UnityEngine.Vector2(x.ReadUnprocessedValueFromStateWithCaching(statePtr), y.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		public unsafe override void WriteValueIntoState(global::UnityEngine.Vector2 value, void* statePtr)
		{
			if ((int)m_OptimizedControlDataType == 1447379762)
			{
				*(global::UnityEngine.Vector2*)((byte*)statePtr + (int)m_StateBlock.byteOffset) = value;
				return;
			}
			x.WriteValueIntoState(value.x, statePtr);
			y.WriteValueIntoState(value.y, statePtr);
		}

		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		protected override global::UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
		{
			if (m_StateBlock.sizeInBits == 64 && m_StateBlock.bitOffset == 0 && x.optimizedControlDataType == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && y.optimizedControlDataType == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && y.m_StateBlock.byteOffset == x.m_StateBlock.byteOffset + 4)
			{
				return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatVector2;
			}
			return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatInvalid;
		}
	}
}
