namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.MouseState), isGenericTypeOfDevice = true)]
	public class Mouse : global::UnityEngine.InputSystem.Pointer, global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver
	{
		internal static global::UnityEngine.InputSystem.Mouse s_PlatformMouseDevice;

		public global::UnityEngine.InputSystem.Controls.DeltaControl scroll { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl leftButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl middleButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl rightButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl backButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl forwardButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.IntegerControl clickCount { get; protected set; }

		public new static global::UnityEngine.InputSystem.Mouse current { get; private set; }

		public override void MakeCurrent()
		{
			base.MakeCurrent();
			current = this;
		}

		protected override void OnAdded()
		{
			base.OnAdded();
			if (base.native && s_PlatformMouseDevice == null)
			{
				s_PlatformMouseDevice = this;
			}
		}

		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (current == this)
			{
				current = null;
			}
		}

		public void WarpCursorPosition(global::UnityEngine.Vector2 position)
		{
			global::UnityEngine.InputSystem.LowLevel.WarpMousePositionCommand command = global::UnityEngine.InputSystem.LowLevel.WarpMousePositionCommand.Create(position);
			ExecuteCommand(ref command);
		}

		protected override void FinishSetup()
		{
			scroll = GetChildControl<global::UnityEngine.InputSystem.Controls.DeltaControl>("scroll");
			leftButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("leftButton");
			middleButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("middleButton");
			rightButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("rightButton");
			forwardButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("forwardButton");
			backButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("backButton");
			base.displayIndex = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("displayIndex");
			clickCount = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("clickCount");
			base.FinishSetup();
		}

		protected new void OnNextUpdate()
		{
			base.OnNextUpdate();
			global::UnityEngine.InputSystem.LowLevel.InputState.Change(scroll, global::UnityEngine.Vector2.zero);
		}

		protected new unsafe void OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			scroll.AccumulateValueInEvent(base.currentStatePtr, eventPtr);
			base.OnStateEvent(eventPtr);
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnNextUpdate()
		{
			OnNextUpdate();
		}

		void global::UnityEngine.InputSystem.LowLevel.IInputStateCallbackReceiver.OnStateEvent(global::UnityEngine.InputSystem.LowLevel.InputEventPtr eventPtr)
		{
			OnStateEvent(eventPtr);
		}
	}
}
