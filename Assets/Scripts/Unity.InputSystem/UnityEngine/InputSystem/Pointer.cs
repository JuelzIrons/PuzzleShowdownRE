namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.PointerState), isGenericTypeOfDevice = true)]
	public class Pointer : global::UnityEngine.InputSystem.InputDevice, global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver
	{
		public global::UnityEngine.InputSystem.Controls.Vector2Control position { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.DeltaControl delta { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.Vector2Control radius { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.AxisControl pressure { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl press { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.IntegerControl displayIndex { get; protected set; }

		public static global::UnityEngine.InputSystem.Pointer current { get; internal set; }

		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (current == this)
			{
				current = null;
			}
		}

		protected override void FinishSetup()
		{
			position = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("position");
			delta = GetChildControl<global::UnityEngine.InputSystem.Controls.DeltaControl>("delta");
			radius = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("radius");
			pressure = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("pressure");
			press = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("press");
			displayIndex = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("displayIndex");
			base.FinishSetup();
		}

		protected void OnNextUpdate()
		{
			global::UnityEngine.InputSystem.LowLevel.InputState.Change(delta, global::UnityEngine.Vector2.zero);
		}

		protected unsafe void OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			delta.AccumulateValueInEvent(base.currentStatePtr, eventPtr);
			global::UnityEngine.InputSystem.LowLevel.InputState.Change(this, eventPtr);
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnNextUpdate()
		{
			OnNextUpdate();
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			OnStateEvent(eventPtr);
		}

		bool global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.GetStateOffsetForEvent(global::UnityEngine.InputSystem.InputControl control, global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr, ref uint offset)
		{
			return false;
		}
	}
}
