namespace UnityEngine.InputSystem.Controls
{
	public class Vector3Control : global::UnityEngine.InputSystem.InputControl<global::UnityEngine.Vector3>
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 0u, displayName = "X")]
		public global::UnityEngine.InputSystem.Controls.AxisControl x { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 4u, displayName = "Y")]
		public global::UnityEngine.InputSystem.Controls.AxisControl y { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 8u, displayName = "Z")]
		public global::UnityEngine.InputSystem.Controls.AxisControl z { get; set; }

		public Vector3Control()
		{
			m_StateBlock.format = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatVector3;
		}

		protected override void FinishSetup()
		{
			x = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("x");
			y = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("y");
			z = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("z");
			base.FinishSetup();
		}

		public unsafe override global::UnityEngine.Vector3 ReadUnprocessedValueFromState(void* statePtr)
		{
			if ((int)m_OptimizedControlDataType == 1447379763)
			{
				return *(global::UnityEngine.Vector3*)((byte*)statePtr + (int)m_StateBlock.byteOffset);
			}
			return new global::UnityEngine.Vector3(x.ReadUnprocessedValueFromStateWithCaching(statePtr), y.ReadUnprocessedValueFromStateWithCaching(statePtr), z.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		public unsafe override void WriteValueIntoState(global::UnityEngine.Vector3 value, void* statePtr)
		{
			if ((int)m_OptimizedControlDataType == 1447379763)
			{
				*(global::UnityEngine.Vector3*)((byte*)statePtr + (int)m_StateBlock.byteOffset) = value;
				return;
			}
			x.WriteValueIntoState(value.x, statePtr);
			y.WriteValueIntoState(value.y, statePtr);
			z.WriteValueIntoState(value.z, statePtr);
		}

		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		protected override global::UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
		{
			if (m_StateBlock.sizeInBits == 96 && m_StateBlock.bitOffset == 0 && x.optimizedControlDataType == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && y.optimizedControlDataType == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && z.optimizedControlDataType == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && y.m_StateBlock.byteOffset == x.m_StateBlock.byteOffset + 4 && z.m_StateBlock.byteOffset == x.m_StateBlock.byteOffset + 8)
			{
				return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatVector3;
			}
			return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatInvalid;
		}
	}
}
