namespace UnityEngine.InputSystem
{
	[global::UnityEngine.InputSystem.Layouts.InputControlLayout(stateType = typeof(global::UnityEngine.InputSystem.LowLevel.PenState), isGenericTypeOfDevice = true)]
	public class Pen : global::UnityEngine.InputSystem.Pointer
	{
		public global::UnityEngine.InputSystem.Controls.ButtonControl tip { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl eraser { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl firstBarrelButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl secondBarrelButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl thirdBarrelButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl fourthBarrelButton { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl inRange { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.Vector2Control tilt { get; protected set; }

		public global::UnityEngine.InputSystem.Controls.AxisControl twist { get; protected set; }

		public new static global::UnityEngine.InputSystem.Pen current { get; internal set; }

		public global::UnityEngine.InputSystem.Controls.ButtonControl this[global::UnityEngine.InputSystem.PenButton button] => button switch
		{
			global::UnityEngine.InputSystem.PenButton.Tip => tip, 
			global::UnityEngine.InputSystem.PenButton.Eraser => eraser, 
			global::UnityEngine.InputSystem.PenButton.BarrelFirst => firstBarrelButton, 
			global::UnityEngine.InputSystem.PenButton.BarrelSecond => secondBarrelButton, 
			global::UnityEngine.InputSystem.PenButton.BarrelThird => thirdBarrelButton, 
			global::UnityEngine.InputSystem.PenButton.BarrelFourth => fourthBarrelButton, 
			global::UnityEngine.InputSystem.PenButton.InRange => inRange, 
			_ => throw new global::System.ComponentModel.InvalidEnumArgumentException("button", (int)button, typeof(global::UnityEngine.InputSystem.PenButton)), 
		};

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
			tip = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("tip");
			eraser = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("eraser");
			firstBarrelButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("barrel1");
			secondBarrelButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("barrel2");
			thirdBarrelButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("barrel3");
			fourthBarrelButton = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("barrel4");
			inRange = GetChildControl<global::UnityEngine.InputSystem.Controls.ButtonControl>("inRange");
			tilt = GetChildControl<global::UnityEngine.InputSystem.Controls.Vector2Control>("tilt");
			twist = GetChildControl<global::UnityEngine.InputSystem.Controls.AxisControl>("twist");
			base.displayIndex = GetChildControl<global::UnityEngine.InputSystem.Controls.IntegerControl>("displayIndex");
			base.FinishSetup();
		}
	}
}
