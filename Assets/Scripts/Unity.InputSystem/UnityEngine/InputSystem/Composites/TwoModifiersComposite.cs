namespace UnityEngine.InputSystem.Composites
{
	[global::UnityEngine.InputSystem.Utilities.DisplayStringFormat("{modifier1}+{modifier2}+{binding}")]
	[global::System.ComponentModel.DisplayName("Binding With Two Modifiers")]
	public class TwoModifiersComposite : global::UnityEngine.InputSystem.InputBindingComposite
	{
		public enum ModifiersOrder
		{
			Default = 0,
			Ordered = 1,
			Unordered = 2
		}

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Button")]
		public int modifier1;

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Button")]
		public int modifier2;

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public int binding;

		[global::UnityEngine.Tooltip("Obsolete please use modifiers Order. If enabled, this will override the Input Consumption setting, allowing the modifier keys to be pressed after the button and the composite will still trigger.")]
		[global::System.Obsolete("Use ModifiersOrder.Unordered with 'modifiersOrder' instead")]
		public bool overrideModifiersNeedToBePressedFirst;

		[global::UnityEngine.Tooltip("By default it follows the Input Consumption setting to determine if the modifers keys need to be pressed first.")]
		public global::UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrder modifiersOrder;

		private int m_ValueSizeInBytes;

		private global::System.Type m_ValueType;

		private bool m_BindingIsButton;

		public override global::System.Type valueType => m_ValueType;

		public override int valueSizeInBytes => m_ValueSizeInBytes;

		public override float EvaluateMagnitude(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			if (ModifiersArePressed(ref context))
			{
				return context.EvaluateMagnitude(binding);
			}
			return 0f;
		}

		public unsafe override void ReadValue(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (ModifiersArePressed(ref context))
			{
				context.ReadValue(binding, buffer, bufferSize);
			}
			else
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(buffer, m_ValueSizeInBytes);
			}
		}

		private bool ModifiersArePressed(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(modifier1) && context.ReadValueAsButton(modifier2);
			if (flag && m_BindingIsButton && modifiersOrder == global::UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrder.Ordered)
			{
				double pressTime = context.GetPressTime(binding);
				double pressTime2 = context.GetPressTime(modifier1);
				double pressTime3 = context.GetPressTime(modifier2);
				if (pressTime2 <= pressTime)
				{
					return pressTime3 <= pressTime;
				}
				return false;
			}
			return flag;
		}

		protected override void FinishSetup(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			global::UnityEngine.InputSystem.Composites.OneModifierComposite.DetermineValueTypeAndSize(ref context, binding, out m_ValueType, out m_ValueSizeInBytes, out m_BindingIsButton);
			if (modifiersOrder == global::UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrder.Default)
			{
				if (overrideModifiersNeedToBePressedFirst)
				{
					modifiersOrder = global::UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrder.Unordered;
				}
				else
				{
					modifiersOrder = (global::UnityEngine.InputSystem.InputSystem.settings.shortcutKeysConsumeInput ? global::UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrder.Ordered : global::UnityEngine.InputSystem.Composites.TwoModifiersComposite.ModifiersOrder.Unordered);
				}
			}
		}

		public override object ReadValueAsObject(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(modifier1) && context.ReadValueAsButton(modifier2))
			{
				return context.ReadValueAsObject(binding);
			}
			return null;
		}
	}
}
