namespace UnityEngine.InputSystem.Composites
{
	[global::UnityEngine.InputSystem.Utilities.DisplayStringFormat("{modifier}+{binding}")]
	[global::System.ComponentModel.DisplayName("Binding With One Modifier")]
	public class OneModifierComposite : global::UnityEngine.InputSystem.InputBindingComposite
	{
		public enum ModifiersOrder
		{
			Default = 0,
			Ordered = 1,
			Unordered = 2
		}

		[global::UnityEngine.InputSystem.Layouts.InputControl(layout = "Button")]
		public int modifier;

		[global::UnityEngine.InputSystem.Layouts.InputControl]
		public int binding;

		[global::UnityEngine.Tooltip("Obsolete please use modifiers Order. If enabled, this will override the Input Consumption setting, allowing the modifier keys to be pressed after the button and the composite will still trigger.")]
		[global::System.Obsolete("Use ModifiersOrder.Unordered with 'modifiersOrder' instead")]
		public bool overrideModifiersNeedToBePressedFirst;

		[global::UnityEngine.Tooltip("By default it follows the Input Consumption setting to determine if the modifers keys need to be pressed first.")]
		public global::UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrder modifiersOrder;

		private int m_ValueSizeInBytes;

		private global::System.Type m_ValueType;

		private bool m_BindingIsButton;

		public override global::System.Type valueType => m_ValueType;

		public override int valueSizeInBytes => m_ValueSizeInBytes;

		public override float EvaluateMagnitude(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			if (ModifierIsPressed(ref context))
			{
				return context.EvaluateMagnitude(binding);
			}
			return 0f;
		}

		public unsafe override void ReadValue(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (ModifierIsPressed(ref context))
			{
				context.ReadValue(binding, buffer, bufferSize);
			}
			else
			{
				global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemClear(buffer, m_ValueSizeInBytes);
			}
		}

		private bool ModifierIsPressed(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(modifier);
			if (flag && m_BindingIsButton && modifiersOrder == global::UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrder.Ordered)
			{
				double pressTime = context.GetPressTime(binding);
				return context.GetPressTime(modifier) <= pressTime;
			}
			return flag;
		}

		protected override void FinishSetup(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			DetermineValueTypeAndSize(ref context, binding, out m_ValueType, out m_ValueSizeInBytes, out m_BindingIsButton);
			if (modifiersOrder == global::UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrder.Default)
			{
				if (overrideModifiersNeedToBePressedFirst)
				{
					modifiersOrder = global::UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrder.Unordered;
				}
				else
				{
					modifiersOrder = (global::UnityEngine.InputSystem.InputSystem.settings.shortcutKeysConsumeInput ? global::UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrder.Ordered : global::UnityEngine.InputSystem.Composites.OneModifierComposite.ModifiersOrder.Unordered);
				}
			}
		}

		public override object ReadValueAsObject(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(modifier))
			{
				return context.ReadValueAsObject(binding);
			}
			return null;
		}

		internal static void DetermineValueTypeAndSize(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context, int part, out global::System.Type valueType, out int valueSizeInBytes, out bool isButton)
		{
			valueSizeInBytes = 0;
			isButton = true;
			global::System.Type type = null;
			foreach (global::UnityEngine.InputSystem.InputBindingCompositeContext.PartBinding control in context.controls)
			{
				if (control.part == part)
				{
					global::System.Type type2 = control.control.valueType;
					if (type == null || type2.IsAssignableFrom(type))
					{
						type = type2;
					}
					else if (!type.IsAssignableFrom(type2))
					{
						type = typeof(global::UnityEngine.Object);
					}
					valueSizeInBytes = global::System.Math.Max(control.control.valueSizeInBytes, valueSizeInBytes);
					isButton &= control.control.isButton;
				}
			}
			valueType = type;
		}
	}
}
