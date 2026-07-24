namespace UnityEngine.InputSystem.Utilities
{
	public struct NamedValue : global::System.IEquatable<global::UnityEngine.InputSystem.Utilities.NamedValue>
	{
		public const string Separator = ",";

		public string name { get; set; }

		public global::UnityEngine.InputSystem.Utilities.PrimitiveValue value { get; set; }

		public global::System.TypeCode type => value.type;

		public global::UnityEngine.InputSystem.Utilities.NamedValue ConvertTo(global::System.TypeCode type)
		{
			return new global::UnityEngine.InputSystem.Utilities.NamedValue
			{
				name = name,
				value = value.ConvertTo(type)
			};
		}

		public static global::UnityEngine.InputSystem.Utilities.NamedValue From<TValue>(string name, TValue value) where TValue : struct
		{
			return new global::UnityEngine.InputSystem.Utilities.NamedValue
			{
				name = name,
				value = global::UnityEngine.InputSystem.Utilities.PrimitiveValue.From(value)
			};
		}

		public override string ToString()
		{
			return $"{name}={value}";
		}

		public bool Equals(global::UnityEngine.InputSystem.Utilities.NamedValue other)
		{
			if (string.Equals(name, other.name, global::System.StringComparison.InvariantCultureIgnoreCase))
			{
				return value == other.value;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is global::UnityEngine.InputSystem.Utilities.NamedValue other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (((name != null) ? name.GetHashCode() : 0) * 397) ^ value.GetHashCode();
		}

		public static bool operator ==(global::UnityEngine.InputSystem.Utilities.NamedValue left, global::UnityEngine.InputSystem.Utilities.NamedValue right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.InputSystem.Utilities.NamedValue left, global::UnityEngine.InputSystem.Utilities.NamedValue right)
		{
			return !left.Equals(right);
		}

		public static global::UnityEngine.InputSystem.Utilities.NamedValue[] ParseMultiple(string parameterString)
		{
			if (parameterString == null)
			{
				throw new global::System.ArgumentNullException("parameterString");
			}
			parameterString = parameterString.Trim();
			if (string.IsNullOrEmpty(parameterString))
			{
				return null;
			}
			int num = parameterString.CountOccurrences(","[0]) + 1;
			global::UnityEngine.InputSystem.Utilities.NamedValue[] array = new global::UnityEngine.InputSystem.Utilities.NamedValue[num];
			int index = 0;
			for (int i = 0; i < num; i++)
			{
				global::UnityEngine.InputSystem.Utilities.NamedValue namedValue = ParseParameter(parameterString, ref index);
				array[i] = namedValue;
			}
			return array;
		}

		public static global::UnityEngine.InputSystem.Utilities.NamedValue Parse(string str)
		{
			int index = 0;
			return ParseParameter(str, ref index);
		}

		private static global::UnityEngine.InputSystem.Utilities.NamedValue ParseParameter(string parameterString, ref int index)
		{
			global::UnityEngine.InputSystem.Utilities.NamedValue result = default(global::UnityEngine.InputSystem.Utilities.NamedValue);
			int length = parameterString.Length;
			while (index < length && char.IsWhiteSpace(parameterString[index]))
			{
				index++;
			}
			int num = index;
			while (index < length)
			{
				char c = parameterString[index];
				if (c == '=' || c == ","[0] || char.IsWhiteSpace(c))
				{
					break;
				}
				index++;
			}
			result.name = parameterString.Substring(num, index - num);
			while (index < length && char.IsWhiteSpace(parameterString[index]))
			{
				index++;
			}
			if (index == length || parameterString[index] != '=')
			{
				result.value = true;
			}
			else
			{
				index++;
				while (index < length && char.IsWhiteSpace(parameterString[index]))
				{
					index++;
				}
				int num2 = index;
				while (index < length && parameterString[index] != ","[0] && !char.IsWhiteSpace(parameterString[index]))
				{
					index++;
				}
				string text = parameterString.Substring(num2, index - num2);
				result.value = global::UnityEngine.InputSystem.Utilities.PrimitiveValue.FromString(text);
			}
			if (index < length && parameterString[index] == ","[0])
			{
				index++;
			}
			return result;
		}

		public void ApplyToObject(object instance)
		{
			if (instance == null)
			{
				throw new global::System.ArgumentNullException("instance");
			}
			global::System.Type type = instance.GetType();
			global::System.Reflection.FieldInfo field = type.GetField(name, global::System.Reflection.BindingFlags.IgnoreCase | global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic);
			if (field == null)
			{
				throw new global::System.ArgumentException("Cannot find public field '" + name + "' in '" + type.Name + "' (while trying to apply parameter)", "instance");
			}
			global::System.TypeCode typeCode = global::System.Type.GetTypeCode(field.FieldType);
			field.SetValue(instance, value.ConvertTo(typeCode).ToObject());
		}

		public static void ApplyAllToObject<TParameterList>(object instance, TParameterList parameters) where TParameterList : global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.Utilities.NamedValue>
		{
			foreach (global::UnityEngine.InputSystem.Utilities.NamedValue item in parameters)
			{
				item.ApplyToObject(instance);
			}
		}
	}
}
