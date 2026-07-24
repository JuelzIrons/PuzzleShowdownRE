namespace UnityEngine.InputSystem.XR
{
	[global::UnityEngine.Scripting.Preserve]
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.XR.PoseState))]
	public class PoseControl : global::UnityEngine.InputSystem.InputControl<global::UnityEngine.InputSystem.XR.PoseState>
	{
		public global::UnityEngine.InputSystem.Controls.ButtonControl isTracked { get; set; }

		public global::UnityEngine.InputSystem.Controls.IntegerControl trackingState { get; set; }

		public global::UnityEngine.InputSystem.Controls.Vector3Control position { get; set; }

		public global::UnityEngine.InputSystem.Controls.QuaternionControl rotation { get; set; }

		public global::UnityEngine.InputSystem.Controls.Vector3Control velocity { get; set; }

		public global::UnityEngine.InputSystem.Controls.Vector3Control angularVelocity { get; set; }

		public PoseControl()
		{
			m_StateBlock.format = global::UnityEngine.InputSystem.XR.PoseState.s_Format;
		}

		protected override void FinishSetup()
		{
			isTracked = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("isTracked");
			trackingState = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("trackingState");
			position = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("position");
			rotation = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("rotation");
			velocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("velocity");
			angularVelocity = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("angularVelocity");
			base.FinishSetup();
		}

		public unsafe override global::UnityEngine.InputSystem.XR.PoseState ReadUnprocessedValueFromState(void* statePtr)
		{
			if ((int)m_OptimizedControlDataType == 1349481317)
			{
				return *(global::UnityEngine.InputSystem.XR.PoseState*)((byte*)statePtr + (int)m_StateBlock.byteOffset);
			}
			return new global::UnityEngine.InputSystem.XR.PoseState
			{
				isTracked = (isTracked.ReadUnprocessedValueFromStateWithCaching(statePtr) > 0.5f),
				trackingState = (global::UnityEngine.XR.InputTrackingState)trackingState.ReadUnprocessedValueFromStateWithCaching(statePtr),
				position = position.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rotation = rotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				velocity = velocity.ReadUnprocessedValueFromStateWithCaching(statePtr),
				angularVelocity = angularVelocity.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		public unsafe override void WriteValueIntoState(global::UnityEngine.InputSystem.XR.PoseState value, void* statePtr)
		{
			if ((int)m_OptimizedControlDataType == 1349481317)
			{
				*(global::UnityEngine.InputSystem.XR.PoseState*)((byte*)statePtr + (int)m_StateBlock.byteOffset) = value;
				return;
			}
			isTracked.WriteValueIntoState(value.isTracked, statePtr);
			trackingState.WriteValueIntoState((uint)value.trackingState, statePtr);
			position.WriteValueIntoState(value.position, statePtr);
			rotation.WriteValueIntoState(value.rotation, statePtr);
			velocity.WriteValueIntoState(value.velocity, statePtr);
			angularVelocity.WriteValueIntoState(value.angularVelocity, statePtr);
		}

		protected override global::UnityEngine.InputSystem.Utilities.FourCC CalculateOptimizedControlDataType()
		{
			if (m_StateBlock.sizeInBits == 480 && m_StateBlock.bitOffset == 0 && isTracked.optimizedControlDataType == 1113150533 && trackingState.optimizedControlDataType == 1229870112 && position.optimizedControlDataType == 1447379763 && rotation.optimizedControlDataType == 1364541780 && velocity.optimizedControlDataType == 1447379763 && angularVelocity.optimizedControlDataType == 1447379763 && trackingState.m_StateBlock.byteOffset == isTracked.m_StateBlock.byteOffset + 4 && position.m_StateBlock.byteOffset == isTracked.m_StateBlock.byteOffset + 8 && rotation.m_StateBlock.byteOffset == isTracked.m_StateBlock.byteOffset + 20 && velocity.m_StateBlock.byteOffset == isTracked.m_StateBlock.byteOffset + 36 && angularVelocity.m_StateBlock.byteOffset == isTracked.m_StateBlock.byteOffset + 48)
			{
				return 1349481317;
			}
			return 0;
		}
	}
}
