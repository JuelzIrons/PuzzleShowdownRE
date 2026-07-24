namespace UnityEngine.InputSystem
{
	public abstract class InputBindingComposite
	{
		internal static global::UnityEngine.InputSystem.Utilities.TypeTable s_Composites;

		public abstract global::System.Type valueType { get; }

		public abstract int valueSizeInBytes { get; }

		public unsafe abstract void ReadValue(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context, void* buffer, int bufferSize);

		public abstract object ReadValueAsObject(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context);

		public virtual float EvaluateMagnitude(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			return -1f;
		}

		protected virtual void FinishSetup(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
		}

		internal void CallFinishSetup(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			FinishSetup(ref context);
		}

		internal static global::System.Type GetValueType(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new global::System.ArgumentNullException("composite");
			}
			global::System.Type type = s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				return null;
			}
			return global::UnityEngine.InputSystem.Utilities.TypeHelpers.GetGenericTypeArgumentFromHierarchy(type, typeof(global::UnityEngine.InputSystem.InputBindingComposite<>), 0);
		}

		public static string GetExpectedControlLayoutName(string composite, string part)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new global::System.ArgumentNullException("composite");
			}
			if (string.IsNullOrEmpty(part))
			{
				throw new global::System.ArgumentNullException("part");
			}
			global::System.Type type = s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				return null;
			}
			global::System.Reflection.FieldInfo field = type.GetField(part, global::System.Reflection.BindingFlags.IgnoreCase | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
			if (field == null)
			{
				return null;
			}
			return global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.InputSystem.Layouts.InputControlAttribute>(field, inherit: false)?.layout;
		}

		internal static global::System.Collections.Generic.IEnumerable<string> GetPartNames(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new global::System.ArgumentNullException("composite");
			}
			global::System.Type type = s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				yield break;
			}
			global::System.Reflection.FieldInfo[] fields = type.GetFields(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
			foreach (global::System.Reflection.FieldInfo fieldInfo in fields)
			{
				if (global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.InputSystem.Layouts.InputControlAttribute>(fieldInfo) != null)
				{
					yield return fieldInfo.Name;
				}
			}
		}

		internal static string GetDisplayFormatString(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new global::System.ArgumentNullException("composite");
			}
			global::System.Type type = s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				return null;
			}
			return global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.InputSystem.Utilities.DisplayStringFormatAttribute>(type)?.formatString;
		}
	}
	public abstract class InputBindingComposite<TValue> : global::UnityEngine.InputSystem.InputBindingComposite where TValue : struct
	{
		public override global::System.Type valueType => typeof(TValue);

		public override int valueSizeInBytes => global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>();

		public abstract TValue ReadValue(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context);

		public unsafe override void ReadValue(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (buffer == null)
			{
				throw new global::System.ArgumentNullException("buffer");
			}
			int num = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>();
			if (bufferSize < num)
			{
				throw new global::System.ArgumentException($"Expected buffer of at least {(global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>())} bytes but got buffer of only {bufferSize} bytes instead", "bufferSize");
			}
			TValue output = ReadValue(ref context);
			void* source = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output);
			global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.MemCpy(buffer, source, num);
		}

		public unsafe override object ReadValueAsObject(ref global::UnityEngine.InputSystem.InputBindingCompositeContext context)
		{
			TValue output = default(TValue);
			void* buffer = global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf(ref output);
			ReadValue(ref context, buffer, global::Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<TValue>());
			return output;
		}
	}
}
