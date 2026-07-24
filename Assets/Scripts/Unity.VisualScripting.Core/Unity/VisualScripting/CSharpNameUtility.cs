namespace Unity.VisualScripting
{
	public static class CSharpNameUtility
	{
		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, string> primitives = new global::System.Collections.Generic.Dictionary<global::System.Type, string>
		{
			{
				typeof(byte),
				"byte"
			},
			{
				typeof(sbyte),
				"sbyte"
			},
			{
				typeof(short),
				"short"
			},
			{
				typeof(ushort),
				"ushort"
			},
			{
				typeof(int),
				"int"
			},
			{
				typeof(uint),
				"uint"
			},
			{
				typeof(long),
				"long"
			},
			{
				typeof(ulong),
				"ulong"
			},
			{
				typeof(float),
				"float"
			},
			{
				typeof(double),
				"double"
			},
			{
				typeof(decimal),
				"decimal"
			},
			{
				typeof(string),
				"string"
			},
			{
				typeof(char),
				"char"
			},
			{
				typeof(bool),
				"bool"
			},
			{
				typeof(void),
				"void"
			},
			{
				typeof(object),
				"object"
			}
		};

		public static readonly global::System.Collections.Generic.Dictionary<string, string> operators = new global::System.Collections.Generic.Dictionary<string, string>
		{
			{ "op_Addition", "+" },
			{ "op_Subtraction", "-" },
			{ "op_Multiply", "*" },
			{ "op_Division", "/" },
			{ "op_Modulus", "%" },
			{ "op_ExclusiveOr", "^" },
			{ "op_BitwiseAnd", "&" },
			{ "op_BitwiseOr", "|" },
			{ "op_LogicalAnd", "&&" },
			{ "op_LogicalOr", "||" },
			{ "op_Assign", "=" },
			{ "op_LeftShift", "<<" },
			{ "op_RightShift", ">>" },
			{ "op_Equality", "==" },
			{ "op_GreaterThan", ">" },
			{ "op_LessThan", "<" },
			{ "op_Inequality", "!=" },
			{ "op_GreaterThanOrEqual", ">=" },
			{ "op_LessThanOrEqual", "<=" },
			{ "op_MultiplicationAssignment", "*=" },
			{ "op_SubtractionAssignment", "-=" },
			{ "op_ExclusiveOrAssignment", "^=" },
			{ "op_LeftShiftAssignment", "<<=" },
			{ "op_ModulusAssignment", "%=" },
			{ "op_AdditionAssignment", "+=" },
			{ "op_BitwiseAndAssignment", "&=" },
			{ "op_BitwiseOrAssignment", "|=" },
			{ "op_Comma", "," },
			{ "op_DivisionAssignment", "/=" },
			{ "op_Decrement", "--" },
			{ "op_Increment", "++" },
			{ "op_UnaryNegation", "-" },
			{ "op_UnaryPlus", "+" },
			{ "op_OnesComplement", "~" }
		};

		private static readonly global::System.Collections.Generic.HashSet<char> illegalTypeFileNameCharacters = new global::System.Collections.Generic.HashSet<char> { '<', '>', '?', ' ', ',', ':' };

		public static string CSharpName(this global::System.Reflection.MemberInfo member, global::Unity.VisualScripting.ActionDirection direction)
		{
			if (member is global::System.Reflection.MethodInfo && ((global::System.Reflection.MethodInfo)member).IsOperator())
			{
				return operators[member.Name] + " operator";
			}
			if (member is global::System.Reflection.ConstructorInfo)
			{
				return "new " + member.DeclaringType.CSharpName();
			}
			if ((member is global::System.Reflection.FieldInfo || member is global::System.Reflection.PropertyInfo) && direction != global::Unity.VisualScripting.ActionDirection.Any)
			{
				return member.Name + " (" + direction.ToString().ToLower() + ")";
			}
			return member.Name;
		}

		public static string CSharpName(this global::System.Type type, bool includeGenericParameters = true)
		{
			return type.CSharpName(global::Unity.VisualScripting.TypeQualifier.Name, includeGenericParameters);
		}

		public static string CSharpFullName(this global::System.Type type, bool includeGenericParameters = true)
		{
			return type.CSharpName(global::Unity.VisualScripting.TypeQualifier.Namespace, includeGenericParameters);
		}

		public static string CSharpUniqueName(this global::System.Type type, bool includeGenericParameters = true)
		{
			return type.CSharpName(global::Unity.VisualScripting.TypeQualifier.GlobalNamespace, includeGenericParameters);
		}

		public static string CSharpFileName(this global::System.Type type, bool includeNamespace, bool includeGenericParameters = false)
		{
			string text = type.CSharpName(includeNamespace ? global::Unity.VisualScripting.TypeQualifier.Namespace : global::Unity.VisualScripting.TypeQualifier.Name, includeGenericParameters);
			if (!includeGenericParameters && type.IsGenericType && text.Contains('<'))
			{
				text = text.Substring(0, text.IndexOf('<'));
			}
			return text.ReplaceMultiple(illegalTypeFileNameCharacters, '_').Trim('_').RemoveConsecutiveCharacters('_');
		}

		private static string CSharpName(this global::System.Type type, global::Unity.VisualScripting.TypeQualifier qualifier, bool includeGenericParameters = true)
		{
			if (primitives.ContainsKey(type))
			{
				return primitives[type];
			}
			if (type.IsGenericParameter)
			{
				if (!includeGenericParameters)
				{
					return "";
				}
				return type.Name;
			}
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(global::System.Nullable<>))
			{
				return global::System.Nullable.GetUnderlyingType(type).CSharpName(qualifier, includeGenericParameters) + "?";
			}
			string text = type.Name;
			if (type.IsGenericType && text.Contains('`'))
			{
				text = text.Substring(0, text.IndexOf('`'));
			}
			global::System.Collections.Generic.IEnumerable<global::System.Type> genericArguments = type.GetGenericArguments();
			if (type.IsNested)
			{
				text = type.DeclaringType.CSharpName(qualifier, includeGenericParameters) + "." + text;
				if (type.DeclaringType.IsGenericType)
				{
					global::System.Linq.Enumerable.Skip(genericArguments, type.DeclaringType.GetGenericArguments().Length);
				}
			}
			if (!type.IsNested)
			{
				if ((qualifier == global::Unity.VisualScripting.TypeQualifier.Namespace || qualifier == global::Unity.VisualScripting.TypeQualifier.GlobalNamespace) && type.Namespace != null)
				{
					text = type.Namespace + "." + text;
				}
				if (qualifier == global::Unity.VisualScripting.TypeQualifier.GlobalNamespace)
				{
					text = "global::" + text;
				}
			}
			if (global::System.Linq.Enumerable.Any(genericArguments))
			{
				text += "<";
				text += string.Join(includeGenericParameters ? ", " : ",", global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(genericArguments, (global::System.Type t) => t.CSharpName(qualifier, includeGenericParameters))));
				text += ">";
			}
			return text;
		}
	}
}
