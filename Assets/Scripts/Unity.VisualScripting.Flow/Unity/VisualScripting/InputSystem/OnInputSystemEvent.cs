namespace Unity.VisualScripting.InputSystem
{
	[global::Unity.VisualScripting.UnitCategory("Events/Input")]
	public abstract class OnInputSystemEvent : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		private new class Data : global::Unity.VisualScripting.EventUnit<global::Unity.VisualScripting.EmptyEventArgs>.Data
		{
			internal global::UnityEngine.InputSystem.InputAction Action;
		}

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		public global::Unity.VisualScripting.InputSystem.InputActionChangeOption InputActionChangeType;

		private global::UnityEngine.Vector2 m_Value;

		protected override string hookName
		{
			get
			{
				if (global::UnityEngine.InputSystem.InputSystem.settings.updateMode != global::UnityEngine.InputSystem.InputSettings.UpdateMode.ProcessEventsInDynamicUpdate)
				{
					return "FixedUpdate";
				}
				return "Update";
			}
		}

		protected abstract global::Unity.VisualScripting.InputSystem.OutputType OutputType { get; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput InputAction { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput Target { get; private set; }

		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput FloatValue { get; private set; }

		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput Vector2Value { get; private set; }

		public override global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.InputSystem.OnInputSystemEvent.Data();
		}

		protected override void Definition()
		{
			base.Definition();
			Target = ValueInput(typeof(global::UnityEngine.InputSystem.PlayerInput), "Target");
			Target.SetDefaultValue(null);
			Target.NullMeansSelf();
			InputAction = ValueInput(typeof(global::UnityEngine.InputSystem.InputAction), "InputAction");
			InputAction.SetDefaultValue(null);
			switch (OutputType)
			{
			case global::Unity.VisualScripting.InputSystem.OutputType.Float:
				FloatValue = ValueOutput("FloatValue", (global::Unity.VisualScripting.Flow _) => m_Value.x);
				break;
			case global::Unity.VisualScripting.InputSystem.OutputType.Vector2:
				Vector2Value = ValueOutput("Vector2Value", (global::Unity.VisualScripting.Flow _) => m_Value);
				break;
			default:
				throw new global::System.ArgumentOutOfRangeException();
			case global::Unity.VisualScripting.InputSystem.OutputType.Button:
				break;
			}
		}

		public override void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			base.StartListening(stack);
			global::Unity.VisualScripting.GraphReference reference = stack.ToReference();
			global::UnityEngine.InputSystem.PlayerInput playerInput = global::Unity.VisualScripting.Flow.FetchValue<global::UnityEngine.InputSystem.PlayerInput>(Target, reference);
			global::UnityEngine.InputSystem.InputAction inputAction = global::Unity.VisualScripting.Flow.FetchValue<global::UnityEngine.InputSystem.InputAction>(InputAction, reference);
			if (inputAction != null)
			{
				stack.GetElementData<global::Unity.VisualScripting.InputSystem.OnInputSystemEvent.Data>(this).Action = (playerInput ? playerInput.actions.FindAction(inputAction.id) : ((inputAction.actionMap != null) ? inputAction : null));
			}
		}

		public override void StopListening(global::Unity.VisualScripting.GraphStack stack)
		{
			base.StopListening(stack);
			stack.GetElementData<global::Unity.VisualScripting.InputSystem.OnInputSystemEvent.Data>(this).Action = null;
		}

		protected override bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.EmptyEventArgs args)
		{
			global::Unity.VisualScripting.InputSystem.OnInputSystemEvent.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.InputSystem.OnInputSystemEvent.Data>(this);
			if (elementData.Action == null)
			{
				return false;
			}
			bool result = InputActionChangeType switch
			{
				global::Unity.VisualScripting.InputSystem.InputActionChangeOption.OnPressed => elementData.Action.WasPressedThisFrame(), 
				global::Unity.VisualScripting.InputSystem.InputActionChangeOption.OnHold => (OutputType == global::Unity.VisualScripting.InputSystem.OutputType.Vector2) ? elementData.Action.IsInProgress() : elementData.Action.IsPressed(), 
				global::Unity.VisualScripting.InputSystem.InputActionChangeOption.OnReleased => elementData.Action.WasReleasedThisFrame(), 
				_ => throw new global::System.ArgumentOutOfRangeException(), 
			};
			DoAssignArguments(flow, elementData);
			return result;
		}

		private void DoAssignArguments(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.InputSystem.OnInputSystemEvent.Data data)
		{
			switch (OutputType)
			{
			case global::Unity.VisualScripting.InputSystem.OutputType.Float:
			{
				float num = data.Action.ReadValue<float>();
				m_Value.Set(num, 0f);
				flow.SetValue(FloatValue, num);
				break;
			}
			case global::Unity.VisualScripting.InputSystem.OutputType.Vector2:
				flow.SetValue(value: m_Value = data.Action.ReadValue<global::UnityEngine.Vector2>(), port: Vector2Value);
				break;
			default:
				throw new global::System.ArgumentOutOfRangeException();
			case global::Unity.VisualScripting.InputSystem.OutputType.Button:
				break;
			}
		}
	}
}
