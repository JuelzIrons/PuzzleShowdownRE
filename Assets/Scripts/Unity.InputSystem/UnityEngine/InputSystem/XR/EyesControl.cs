namespace UnityEngine.InputSystem.XR
{
	public class EyesControl : global::UnityEngine.InputSystem.InputControl<global::UnityEngine.InputSystem.XR.Eyes>
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 0u, displayName = "LeftEyePosition")]
		public global::UnityEngine.InputSystem.Controls.Vector3Control leftEyePosition { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 12u, displayName = "LeftEyeRotation")]
		public global::UnityEngine.InputSystem.Controls.QuaternionControl leftEyeRotation { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 28u, displayName = "RightEyePosition")]
		public global::UnityEngine.InputSystem.Controls.Vector3Control rightEyePosition { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 40u, displayName = "RightEyeRotation")]
		public global::UnityEngine.InputSystem.Controls.QuaternionControl rightEyeRotation { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 56u, displayName = "FixationPoint")]
		public global::UnityEngine.InputSystem.Controls.Vector3Control fixationPoint { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 68u, displayName = "LeftEyeOpenAmount")]
		public global::UnityEngine.InputSystem.Controls.AxisControl leftEyeOpenAmount { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 72u, displayName = "RightEyeOpenAmount")]
		public global::UnityEngine.InputSystem.Controls.AxisControl rightEyeOpenAmount { get; set; }

		protected override void FinishSetup()
		{
			leftEyePosition = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("leftEyePosition");
			leftEyeRotation = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("leftEyeRotation");
			rightEyePosition = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("rightEyePosition");
			rightEyeRotation = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("rightEyeRotation");
			fixationPoint = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("fixationPoint");
			leftEyeOpenAmount = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("leftEyeOpenAmount");
			rightEyeOpenAmount = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("rightEyeOpenAmount");
			base.FinishSetup();
		}

		public unsafe override global::UnityEngine.InputSystem.XR.Eyes ReadUnprocessedValueFromState(void* statePtr)
		{
			return new global::UnityEngine.InputSystem.XR.Eyes
			{
				leftEyePosition = leftEyePosition.ReadUnprocessedValueFromStateWithCaching(statePtr),
				leftEyeRotation = leftEyeRotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyePosition = rightEyePosition.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyeRotation = rightEyeRotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				fixationPoint = fixationPoint.ReadUnprocessedValueFromStateWithCaching(statePtr),
				leftEyeOpenAmount = leftEyeOpenAmount.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyeOpenAmount = rightEyeOpenAmount.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		public unsafe override void WriteValueIntoState(global::UnityEngine.InputSystem.XR.Eyes value, void* statePtr)
		{
			leftEyePosition.WriteValueIntoState(value.leftEyePosition, statePtr);
			leftEyeRotation.WriteValueIntoState(value.leftEyeRotation, statePtr);
			rightEyePosition.WriteValueIntoState(value.rightEyePosition, statePtr);
			rightEyeRotation.WriteValueIntoState(value.rightEyeRotation, statePtr);
			fixationPoint.WriteValueIntoState(value.fixationPoint, statePtr);
			leftEyeOpenAmount.WriteValueIntoState(value.leftEyeOpenAmount, statePtr);
			rightEyeOpenAmount.WriteValueIntoState(value.rightEyeOpenAmount, statePtr);
		}
	}
}
