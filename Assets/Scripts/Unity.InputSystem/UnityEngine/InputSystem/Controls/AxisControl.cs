namespace UnityEngine.InputSystem.Controls
{
	public class AxisControl : global::UnityEngine.InputSystem.InputControl<float>
	{
		public enum Clamp
		{
			None = 0,
			BeforeNormalize = 1,
			AfterNormalize = 2,
			ToConstantBeforeNormalize = 3
		}

		public global::UnityEngine.InputSystem.Controls.AxisControl.Clamp clamp;

		public float clampMin;

		public float clampMax;

		public float clampConstant;

		public bool invert;

		public bool normalize;

		public float normalizeMin;

		public float normalizeMax;

		public float normalizeZero;

		public bool scale;

		public float scaleFactor;

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		protected float Preprocess(float value)
		{
			if (scale)
			{
				value *= scaleFactor;
			}
			if (clamp == global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.ToConstantBeforeNormalize)
			{
				if (value < clampMin || value > clampMax)
				{
					value = clampConstant;
				}
			}
			else if (clamp == global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.BeforeNormalize)
			{
				value = global::UnityEngine.Mathf.Clamp(value, clampMin, clampMax);
			}
			if (normalize)
			{
				value = global::UnityEngine.InputSystem.Processors.NormalizeProcessor.Normalize(value, normalizeMin, normalizeMax, normalizeZero);
			}
			if (clamp == global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.AfterNormalize)
			{
				value = global::UnityEngine.Mathf.Clamp(value, clampMin, clampMax);
			}
			if (invert)
			{
				value *= -1f;
			}
			return value;
		}

		private float Unpreprocess(float value)
		{
			if (invert)
			{
				value *= -1f;
			}
			if (normalize)
			{
				value = global::UnityEngine.InputSystem.Processors.NormalizeProcessor.Denormalize(value, normalizeMin, normalizeMax, normalizeZero);
			}
			if (scale)
			{
				value /= scaleFactor;
			}
			return value;
		}

		public AxisControl()
		{
			m_StateBlock.format = global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat;
		}

		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.hasDefaultState && normalize && global::UnityEngine.Mathf.Abs(normalizeZero) > global::UnityEngine.Mathf.Epsilon)
			{
				m_DefaultState = base.stateBlock.FloatToPrimitiveValue(normalizeZero);
			}
		}

		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			switch (m_OptimizedControlDataType)
			{
			case 1179407392:
				return *(float*)((byte*)statePtr + m_StateBlock.m_ByteOffset);
			case 1113150533:
				if (((byte*)statePtr)[m_StateBlock.m_ByteOffset] == 0)
				{
					return 0f;
				}
				return 1f;
			default:
			{
				float num = base.stateBlock.ReadFloat(statePtr);
				return Preprocess(num);
			}
			}
		}

		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			switch (m_OptimizedControlDataType)
			{
			case 1179407392:
				*(float*)((byte*)statePtr + m_StateBlock.m_ByteOffset) = value;
				break;
			case 1113150533:
				((sbyte*)statePtr)[m_StateBlock.m_ByteOffset] = ((value >= 0.5f) ? ((sbyte)1) : ((sbyte)0));
				break;
			default:
				value = Unpreprocess(value);
				base.stateBlock.WriteFloat(statePtr, value);
				break;
			}
		}

		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			float a = ReadValueFromState(firstStatePtr);
			float b = ReadValueFromState(secondStatePtr);
			return !global::UnityEngine.Mathf.Approximately(a, b);
		}

		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return EvaluateMagnitude(ReadValueFromStateWithCaching(statePtr));
		}

		private float EvaluateMagnitude(float value)
		{
			if (m_MinValue.isEmpty || m_MaxValue.isEmpty)
			{
				return global::UnityEngine.Mathf.Abs(value);
			}
			float num = m_MinValue.ToSingle();
			float max = m_MaxValue.ToSingle();
			float num2 = global::UnityEngine.Mathf.Clamp(value, num, max);
			if (num < 0f)
			{
				if (num2 < 0f)
				{
					return global::UnityEngine.InputSystem.Processors.NormalizeProcessor.Normalize(global::UnityEngine.Mathf.Abs(num2), 0f, global::UnityEngine.Mathf.Abs(num), 0f);
				}
				return global::UnityEngine.InputSystem.Processors.NormalizeProcessor.Normalize(num2, 0f, max, 0f);
			}
			return global::UnityEngine.InputSystem.Processors.NormalizeProcessor.Normalize(num2, num, max, 0f);
		}

		protected override global::UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
		{
			bool flag = clamp == global::UnityEngine.InputSystem.Controls.AxisControl.Clamp.None && !invert && !normalize && !scale;
			if (flag && m_StateBlock.format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat && m_StateBlock.sizeInBits == 32 && m_StateBlock.bitOffset == 0)
			{
				return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatFloat;
			}
			if (flag && m_StateBlock.format == global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatBit && m_StateBlock.sizeInBits == 8 && m_StateBlock.bitOffset == 0)
			{
				return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatByte;
			}
			return global::UnityEngine.InputSystem.LowLevel.InputStateBlock.FormatInvalid;
		}
	}
}
