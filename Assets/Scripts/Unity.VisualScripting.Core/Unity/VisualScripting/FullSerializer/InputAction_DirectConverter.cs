namespace Unity.VisualScripting.FullSerializer
{
	[global::JetBrains.Annotations.UsedImplicitly]
	public class InputAction_DirectConverter : global::Unity.VisualScripting.FullSerializer.fsDirectConverter<global::UnityEngine.InputSystem.InputAction>
	{
		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoSerialize(global::UnityEngine.InputSystem.InputAction model, global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> serialized)
		{
			return global::Unity.VisualScripting.FullSerializer.fsResult.Success + SerializeMember(serialized, null, "id", model.id.ToString()) + SerializeMember(serialized, null, "name", model.name.ToString()) + SerializeMember(serialized, null, "expectedControlType", model.expectedControlType) + SerializeMember(serialized, null, "type", model.type);
		}

		protected override global::Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(global::System.Collections.Generic.Dictionary<string, global::Unity.VisualScripting.FullSerializer.fsData> data, ref global::UnityEngine.InputSystem.InputAction model)
		{
			string value;
			string value2;
			string value3;
			global::UnityEngine.InputSystem.InputActionType value4;
			global::Unity.VisualScripting.FullSerializer.fsResult result = global::Unity.VisualScripting.FullSerializer.fsResult.Success + DeserializeMember<string>(data, null, "id", out value) + DeserializeMember<string>(data, null, "name", out value2) + DeserializeMember<string>(data, null, "expectedControlType", out value3) + DeserializeMember<global::UnityEngine.InputSystem.InputActionType>(data, null, "type", out value4);
			model = MakeInputActionWithId(value, value2, value3, value4);
			return result;
		}

		public static global::UnityEngine.InputSystem.InputAction MakeInputActionWithId(string actionId, string actionName, string expectedControlType, global::UnityEngine.InputSystem.InputActionType type)
		{
			global::UnityEngine.InputSystem.InputAction inputAction = new global::UnityEngine.InputSystem.InputAction();
			typeof(global::UnityEngine.InputSystem.InputAction).GetField("m_Id", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic).SetValue(inputAction, actionId);
			typeof(global::UnityEngine.InputSystem.InputAction).GetField("m_Name", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic).SetValue(inputAction, actionName);
			typeof(global::UnityEngine.InputSystem.InputAction).GetField("m_Type", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.NonPublic).SetValue(inputAction, type);
			inputAction.expectedControlType = expectedControlType;
			return inputAction;
		}

		public override object CreateInstance(global::Unity.VisualScripting.FullSerializer.fsData data, global::System.Type storageType)
		{
			return new global::UnityEngine.InputSystem.InputAction();
		}
	}
}
