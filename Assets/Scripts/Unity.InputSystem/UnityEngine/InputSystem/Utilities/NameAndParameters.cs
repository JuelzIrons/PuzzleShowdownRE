namespace UnityEngine.InputSystem.Utilities
{
	public struct NameAndParameters
	{
		public string name { get; set; }

		public global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NamedValue> parameters { get; set; }

		public override string ToString()
		{
			if (parameters.Count == 0)
			{
				return name;
			}
			string text = string.Join(",", global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(parameters, (global::UnityEngine.InputSystem.Utilities.NamedValue x) => x.ToString())));
			return name + "(" + text + ")";
		}

		internal static string ToSerializableString(global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.Utilities.NameAndParameters> list)
		{
			if (list == null)
			{
				return string.Empty;
			}
			return string.Join(",", global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(list, (global::UnityEngine.InputSystem.Utilities.NameAndParameters x) => x.ToString())));
		}

		internal static global::UnityEngine.InputSystem.Utilities.NameAndParameters Create(string name, global::System.Collections.Generic.IList<global::UnityEngine.InputSystem.Utilities.NamedValue> parameters)
		{
			return new global::UnityEngine.InputSystem.Utilities.NameAndParameters
			{
				name = name,
				parameters = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NamedValue>(global::System.Linq.Enumerable.ToArray(parameters))
			};
		}

		public static global::System.Collections.Generic.IEnumerable<global::UnityEngine.InputSystem.Utilities.NameAndParameters> ParseMultiple(string text)
		{
			global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.NameAndParameters> list = null;
			if (!ParseMultiple(text, ref list))
			{
				return global::System.Linq.Enumerable.Empty<global::UnityEngine.InputSystem.Utilities.NameAndParameters>();
			}
			return list;
		}

		internal static bool ParseMultiple(string text, ref global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.NameAndParameters> list)
		{
			text = text.Trim();
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			if (list == null)
			{
				list = new global::System.Collections.Generic.List<global::UnityEngine.InputSystem.Utilities.NameAndParameters>();
			}
			else
			{
				list.Clear();
			}
			int index = 0;
			int length = text.Length;
			while (index < length)
			{
				list.Add(ParseNameAndParameters(text, ref index));
			}
			return true;
		}

		internal static string ParseName(string text)
		{
			if (text == null)
			{
				throw new global::System.ArgumentNullException("text");
			}
			int index = 0;
			return ParseNameAndParameters(text, ref index, nameOnly: true).name;
		}

		public static global::UnityEngine.InputSystem.Utilities.NameAndParameters Parse(string text)
		{
			if (text == null)
			{
				throw new global::System.ArgumentNullException("text");
			}
			int index = 0;
			return ParseNameAndParameters(text, ref index);
		}

		private static global::UnityEngine.InputSystem.Utilities.NameAndParameters ParseNameAndParameters(string text, ref int index, bool nameOnly = false)
		{
			int length = text.Length;
			while (index < length && char.IsWhiteSpace(text[index]))
			{
				index++;
			}
			int num = index;
			while (index < length)
			{
				char c = text[index];
				if (c == '(' || c == ","[0] || char.IsWhiteSpace(c))
				{
					break;
				}
				index++;
			}
			if (index - num == 0)
			{
				throw new global::System.ArgumentException($"Expecting name at position {num} in '{text}'", "text");
			}
			string text2 = text.Substring(num, index - num);
			if (nameOnly)
			{
				return new global::UnityEngine.InputSystem.Utilities.NameAndParameters
				{
					name = text2
				};
			}
			while (index < length && char.IsWhiteSpace(text[index]))
			{
				index++;
			}
			global::UnityEngine.InputSystem.Utilities.NamedValue[] array = null;
			if (index < length && text[index] == '(')
			{
				index++;
				int num2 = text.IndexOf(')', index);
				if (num2 == -1)
				{
					throw new global::System.ArgumentException($"Expecting ')' after '(' at position {index} in '{text}'", "text");
				}
				array = global::UnityEngine.InputSystem.Utilities.NamedValue.ParseMultiple(text.Substring(index, num2 - index));
				index = num2 + 1;
			}
			if (index < length && (text[index] == ',' || text[index] == ';'))
			{
				index++;
			}
			return new global::UnityEngine.InputSystem.Utilities.NameAndParameters
			{
				name = text2,
				parameters = new global::UnityEngine.InputSystem.Utilities.ReadOnlyArray<global::UnityEngine.InputSystem.Utilities.NamedValue>(array)
			};
		}
	}
}
