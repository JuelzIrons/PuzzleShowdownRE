namespace UnityEngine.InputSystem.Composites
{
	[global::System.ComponentModel.DesignTimeVisible(false)]
	[global::UnityEngine.InputSystem.Utilities.DisplayStringFormat("{modifier}+{button}")]
	public class ButtonWithOneModifier : global::UnityEngine.InputSystem.InputBindingComposite<float>
	{
		public enum ModifiersOrder
		{
			Default = 0,
			Ordered = 1,
			Unordered = 2
		}

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Button")]
		public int modifier;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Button")]
		public int button;

		[global::UnityEngine.Tooltip("Obsolete please use modifiers Order. If enabled, this will override the Input Consumption setting, allowing the modifier keys to be pressed after the button and the composite will still trigger.")]
		[global::System.Obsolete("Use ModifiersOrder.Unordered with 'modifiersOrder' instead")]
		public bool overrideModifiersNeedToBePressedFirst;

		[global::UnityEngine.Tooltip("By default it follows the Input Consumption setting to determine if the modifers keys need to be pressed first.")]
		public global::UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrder modifiersOrder;

		public override float ReadValue(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			if (ModifierIsPressed(ref context))
			{
				return context.ReadValue<float>(button);
			}
			return 0f;
		}

		private bool ModifierIsPressed(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(modifier);
			if (flag && modifiersOrder == global::UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrder.Ordered)
			{
				double pressTime = context.GetPressTime(button);
				return context.GetPressTime(modifier) <= pressTime;
			}
			return flag;
		}

		public override float EvaluateMagnitude(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			return ReadValue(ref context);
		}

		protected override void FinishSetup(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			if (modifiersOrder == global::UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrder.Default)
			{
				if (overrideModifiersNeedToBePressedFirst)
				{
					modifiersOrder = global::UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrder.Unordered;
				}
				else
				{
					modifiersOrder = (global::UnityEngine.InputSystem.InputSystem.settings.shortcutKeysConsumeInput ? global::UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrder.Ordered : global::UnityEngine.InputSystem.Composites.ButtonWithOneModifier.ModifiersOrder.Unordered);
				}
			}
		}
	}
}
