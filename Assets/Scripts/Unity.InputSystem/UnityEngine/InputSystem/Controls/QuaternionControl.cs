namespace UnityEngine.InputSystem.Controls
{
	public class QuaternionControl : global::UnityEngine.InputSystem.InputControl<global::UnityEngine.Quaternion>
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "X")]
		public global::UnityEngine.InputSystem.Controls.AxisControl x { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Y")]
		public global::UnityEngine.InputSystem.Controls.AxisControl y { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "Z")]
		public global::UnityEngine.InputSystem.Controls.AxisControl z { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(displayName = "W")]
		public global::UnityEngine.InputSystem.Controls.AxisControl w { get; set; }

		public QuaternionControl()
		{
			m_StateBlock.sizeInBits = 128u;
			m_StateBlock.format = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatQuaternion;
		}

		protected override void FinishSetup()
		{
			x = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("x");
			y = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("y");
			z = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("z");
			w = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("w");
			base.FinishSetup();
		}

		public unsafe override global::UnityEngine.Quaternion ReadUnprocessedValueFromState(void* statePtr)
		{
			if ((int)m_OptimizedControlDataType == 1364541780)
			{
				return *(global::UnityEngine.Quaternion*)((byte*)statePtr + (int)m_StateBlock.byteOffset);
			}
			return new global::UnityEngine.Quaternion(x.ReadValueFromStateWithCaching(statePtr), y.ReadValueFromStateWithCaching(statePtr), z.ReadValueFromStateWithCaching(statePtr), w.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		public unsafe override void WriteValueIntoState(global::UnityEngine.Quaternion value, void* statePtr)
		{
			if ((int)m_OptimizedControlDataType == 1364541780)
			{
				*(global::UnityEngine.Quaternion*)((byte*)statePtr + (int)m_StateBlock.byteOffset) = value;
				return;
			}
			x.WriteValueIntoState(value.x, statePtr);
			y.WriteValueIntoState(value.y, statePtr);
			z.WriteValueIntoState(value.z, statePtr);
			w.WriteValueIntoState(value.w, statePtr);
		}

		protected override global::UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
		{
			if (m_StateBlock.sizeInBits == 128 && m_StateBlock.bitOffset == 0 && x.optimizedControlDataType == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && y.optimizedControlDataType == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && z.optimizedControlDataType == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && w.optimizedControlDataType == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && y.m_StateBlock.byteOffset == x.m_StateBlock.byteOffset + 4 && z.m_StateBlock.byteOffset == x.m_StateBlock.byteOffset + 8 && w.m_StateBlock.byteOffset == x.m_StateBlock.byteOffset + 12 && x.m_ProcessorStack.length == 0 && y.m_ProcessorStack.length == 0 && z.m_ProcessorStack.length == 0)
			{
				return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatQuaternion;
			}
			return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatInvalid;
		}
	}
}
