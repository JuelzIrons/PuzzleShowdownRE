namespace UnityEngine.InputSystem.XR
{
	public class BoneControl : global::UnityEngine.InputSystem.InputControl<global::UnityEngine.InputSystem.XR.Bone>
	{
		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 0u, displayName = "parentBoneIndex")]
		public global::UnityEngine.InputSystem.Controls.IntegerControl parentBoneIndex { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 4u, displayName = "Position")]
		public global::UnityEngine.InputSystem.Controls.Vector3Control position { get; set; }

		[global::UnityEngine.InputSystem.Layouts.InputControl(offset = 16u, displayName = "Rotation")]
		public global::UnityEngine.InputSystem.Controls.QuaternionControl rotation { get; set; }

		protected override void FinishSetup()
		{
			parentBoneIndex = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("parentBoneIndex");
			position = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector3Control>("position");
			rotation = GetChildControl<global::UnityEngine.InputSystem.Controls.QuaternionControl>("rotation");
			base.FinishSetup();
		}

		public unsafe override global::UnityEngine.InputSystem.XR.Bone ReadUnprocessedValueFromState(void* statePtr)
		{
			return new global::UnityEngine.InputSystem.XR.Bone
			{
				parentBoneIndex = (uint)parentBoneIndex.ReadUnprocessedValueFromStateWithCaching(statePtr),
				position = position.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rotation = rotation.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		public unsafe override void WriteValueIntoState(global::UnityEngine.InputSystem.XR.Bone value, void* statePtr)
		{
			parentBoneIndex.WriteValueIntoState((int)value.parentBoneIndex, statePtr);
			position.WriteValueIntoState(value.position, statePtr);
			rotation.WriteValueIntoState(value.rotation, statePtr);
		}
	}
}
