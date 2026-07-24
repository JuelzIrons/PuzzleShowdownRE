namespace Unity.VisualScripting
{
	public class EnsureThat
	{
		internal string paramName;

		public void IsTrue(bool value)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Booleans_IsTrueFailed, paramName);
		}

		public void IsFalse(bool value)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !value)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Booleans_IsFalseFailed, paramName);
		}

		public void HasItems<T>(T value) where T : class, global::System.Collections.ICollection
		{
			if (global::Unity.VisualScripting.Ensure.IsActive)
			{
				IsNotNull(value);
				if (value.Count < 1)
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_HasItemsFailed, paramName);
				}
			}
		}

		public void HasItems<T>(global::System.Collections.Generic.ICollection<T> value)
		{
			if (global::Unity.VisualScripting.Ensure.IsActive)
			{
				IsNotNull(value);
				if (value.Count < 1)
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_HasItemsFailed, paramName);
				}
			}
		}

		public void HasItems<T>(T[] value)
		{
			if (global::Unity.VisualScripting.Ensure.IsActive)
			{
				IsNotNull(value);
				if (value.Length < 1)
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_HasItemsFailed, paramName);
				}
			}
		}

		public void HasNoNullItem<T>(T value) where T : class, global::System.Collections.IEnumerable
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive)
			{
				return;
			}
			IsNotNull(value);
			foreach (object item in value)
			{
				if (item == null)
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_HasNoNullItemFailed, paramName);
				}
			}
		}

		public void HasItems<T>(global::System.Collections.Generic.IList<T> value)
		{
			HasItems((global::System.Collections.Generic.ICollection<T>)value);
		}

		public void HasItems<TKey, TValue>(global::System.Collections.Generic.IDictionary<TKey, TValue> value)
		{
			if (global::Unity.VisualScripting.Ensure.IsActive)
			{
				IsNotNull(value);
				if (value.Count < 1)
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_HasItemsFailed, paramName);
				}
			}
		}

		public void SizeIs<T>(T[] value, int expected)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.Length == expected)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Length), paramName);
		}

		public void SizeIs<T>(T[] value, long expected)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.Length == expected)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Length), paramName);
		}

		public void SizeIs<T>(T value, int expected) where T : global::System.Collections.ICollection
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.Count == expected)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
		}

		public void SizeIs<T>(T value, long expected) where T : global::System.Collections.ICollection
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.Count == expected)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
		}

		public void SizeIs<T>(global::System.Collections.Generic.ICollection<T> value, int expected)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.Count == expected)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
		}

		public void SizeIs<T>(global::System.Collections.Generic.ICollection<T> value, long expected)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.Count == expected)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
		}

		public void SizeIs<T>(global::System.Collections.Generic.IList<T> value, int expected)
		{
			SizeIs((global::System.Collections.Generic.ICollection<T>)value, expected);
		}

		public void SizeIs<T>(global::System.Collections.Generic.IList<T> value, long expected)
		{
			SizeIs((global::System.Collections.Generic.ICollection<T>)value, expected);
		}

		public void SizeIs<TKey, TValue>(global::System.Collections.Generic.IDictionary<TKey, TValue> value, int expected)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.Count == expected)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
		}

		public void SizeIs<TKey, TValue>(global::System.Collections.Generic.IDictionary<TKey, TValue> value, long expected)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.Count == expected)
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_SizeIs_Failed.Inject(expected, value.Count), paramName);
		}

		public void IsKeyOf<TKey, TValue>(global::System.Collections.Generic.IDictionary<TKey, TValue> value, TKey expectedKey, string keyLabel = null)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.ContainsKey(expectedKey))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_ContainsKey_Failed.Inject(expectedKey, keyLabel ?? paramName.Prettify()), paramName);
		}

		public void Any<T>(global::System.Collections.Generic.IList<T> value, global::System.Func<T, bool> predicate)
		{
			Any((global::System.Collections.Generic.ICollection<T>)value, predicate);
		}

		public void Any<T>(global::System.Collections.Generic.ICollection<T> value, global::System.Func<T, bool> predicate)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || global::System.Linq.Enumerable.Any(value, predicate))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_Any_Failed, paramName);
		}

		public void Any<T>(T[] value, global::System.Func<T, bool> predicate)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || global::System.Linq.Enumerable.Any(value, predicate))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Collections_Any_Failed, paramName);
		}

		public void Is<T>(T param, T expected) where T : struct, global::System.IComparable<T>
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || param.IsEq(expected))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Comp_Is_Failed.Inject(param, expected), paramName);
		}

		public void IsNot<T>(T param, T expected) where T : struct, global::System.IComparable<T>
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !param.IsEq(expected))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Comp_IsNot_Failed.Inject(param, expected), paramName);
		}

		public void IsLt<T>(T param, T limit) where T : struct, global::System.IComparable<T>
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || param.IsLt(limit))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Comp_IsNotLt.Inject(param, limit), paramName);
		}

		public void IsLte<T>(T param, T limit) where T : struct, global::System.IComparable<T>
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !param.IsGt(limit))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Comp_IsNotLte.Inject(param, limit), paramName);
		}

		public void IsGt<T>(T param, T limit) where T : struct, global::System.IComparable<T>
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || param.IsGt(limit))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Comp_IsNotGt.Inject(param, limit), paramName);
		}

		public void IsGte<T>(T param, T limit) where T : struct, global::System.IComparable<T>
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !param.IsLt(limit))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Comp_IsNotGte.Inject(param, limit), paramName);
		}

		public void IsInRange<T>(T param, T min, T max) where T : struct, global::System.IComparable<T>
		{
			if (global::Unity.VisualScripting.Ensure.IsActive)
			{
				if (param.IsLt(min))
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Comp_IsNotInRange_ToLow.Inject(param, min), paramName);
				}
				if (param.IsGt(max))
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Comp_IsNotInRange_ToHigh.Inject(param, max), paramName);
				}
			}
		}

		public void IsNotEmpty(global::System.Guid value)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !value.Equals(global::System.Guid.Empty))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Guids_IsNotEmpty_Failed, paramName);
		}

		public void IsNotNull<T>(T? value) where T : struct
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value.HasValue)
			{
				return;
			}
			throw new global::System.ArgumentNullException(paramName, global::Unity.VisualScripting.ExceptionMessages.Common_IsNotNull_Failed);
		}

		public void IsNull<T>([global::JetBrains.Annotations.NoEnumeration] T value)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value == null)
			{
				return;
			}
			throw new global::System.ArgumentNullException(paramName, global::Unity.VisualScripting.ExceptionMessages.Common_IsNull_Failed);
		}

		public void IsNotNull<T>([global::JetBrains.Annotations.NoEnumeration] T value)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value != null)
			{
				return;
			}
			throw new global::System.ArgumentNullException(paramName, global::Unity.VisualScripting.ExceptionMessages.Common_IsNotNull_Failed);
		}

		public void HasAttribute(global::System.Type param, global::System.Type attributeType)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || param.HasAttribute(attributeType))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Reflection_HasAttribute_Failed.Inject(param.ToString(), attributeType.ToString()), paramName);
		}

		public void HasAttribute<TAttribute>(global::System.Type param) where TAttribute : global::System.Attribute
		{
			HasAttribute(param, typeof(TAttribute));
		}

		private void HasConstructorAccepting(global::System.Type param, global::System.Type[] parameterTypes, bool nonPublic)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !(param.GetConstructorAccepting(parameterTypes, nonPublic) == null))
			{
				return;
			}
			throw new global::System.ArgumentException((nonPublic ? global::Unity.VisualScripting.ExceptionMessages.Reflection_HasConstructor_Failed : global::Unity.VisualScripting.ExceptionMessages.Reflection_HasPublicConstructor_Failed).Inject(param.ToString(), parameterTypes.ToCommaSeparatedString()), paramName);
		}

		public void HasConstructorAccepting(global::System.Type param, params global::System.Type[] parameterTypes)
		{
			HasConstructorAccepting(param, parameterTypes, nonPublic: true);
		}

		public void HasPublicConstructorAccepting(global::System.Type param, params global::System.Type[] parameterTypes)
		{
			HasConstructorAccepting(param, parameterTypes, nonPublic: false);
		}

		public void IsNotNullOrWhiteSpace(string value)
		{
			if (global::Unity.VisualScripting.Ensure.IsActive)
			{
				IsNotNull(value);
				if (global::Unity.VisualScripting.StringUtility.IsNullOrWhiteSpace(value))
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_IsNotNullOrWhiteSpace_Failed, paramName);
				}
			}
		}

		public void IsNotNullOrEmpty(string value)
		{
			if (global::Unity.VisualScripting.Ensure.IsActive)
			{
				IsNotNull(value);
				if (string.IsNullOrEmpty(value))
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_IsNotNullOrEmpty_Failed, paramName);
				}
			}
		}

		public void IsNotNull(string value)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || value != null)
			{
				return;
			}
			throw new global::System.ArgumentNullException(paramName, global::Unity.VisualScripting.ExceptionMessages.Common_IsNotNull_Failed);
		}

		public void IsNotEmpty(string value)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !string.Empty.Equals(value))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_IsNotEmpty_Failed, paramName);
		}

		public void HasLengthBetween(string value, int minLength, int maxLength)
		{
			if (global::Unity.VisualScripting.Ensure.IsActive)
			{
				IsNotNull(value);
				int length = value.Length;
				if (length < minLength)
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_HasLengthBetween_Failed_ToShort.Inject(minLength, maxLength, length), paramName);
				}
				if (length > maxLength)
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_HasLengthBetween_Failed_ToLong.Inject(minLength, maxLength, length), paramName);
				}
			}
		}

		public void Matches(string value, string match)
		{
			Matches(value, new global::System.Text.RegularExpressions.Regex(match));
		}

		public void Matches(string value, global::System.Text.RegularExpressions.Regex match)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || match.IsMatch(value))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_Matches_Failed.Inject(value, match), paramName);
		}

		public void SizeIs(string value, int expected)
		{
			if (global::Unity.VisualScripting.Ensure.IsActive)
			{
				IsNotNull(value);
				if (value.Length != expected)
				{
					throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_SizeIs_Failed.Inject(expected, value.Length), paramName);
				}
			}
		}

		public void IsEqualTo(string value, string expected)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || StringEquals(value, expected))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_IsEqualTo_Failed.Inject(value, expected), paramName);
		}

		public void IsEqualTo(string value, string expected, global::System.StringComparison comparison)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || StringEquals(value, expected, comparison))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_IsEqualTo_Failed.Inject(value, expected), paramName);
		}

		public void IsNotEqualTo(string value, string expected)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !StringEquals(value, expected))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_IsNotEqualTo_Failed.Inject(value, expected), paramName);
		}

		public void IsNotEqualTo(string value, string expected, global::System.StringComparison comparison)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !StringEquals(value, expected, comparison))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_IsNotEqualTo_Failed.Inject(value, expected), paramName);
		}

		public void IsGuid(string value)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || global::Unity.VisualScripting.StringUtility.IsGuid(value))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Strings_IsGuid_Failed.Inject(value), paramName);
		}

		private bool StringEquals(string x, string y, global::System.StringComparison? comparison = null)
		{
			if (!comparison.HasValue)
			{
				return string.Equals(x, y);
			}
			return string.Equals(x, y, comparison.Value);
		}

		public void IsOfType<T>(T param, global::System.Type expectedType)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || expectedType.IsAssignableFrom(param))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Types_IsOfType_Failed.Inject(expectedType.ToString(), param?.GetType().ToString() ?? "null"), paramName);
		}

		public void IsOfType(global::System.Type param, global::System.Type expectedType)
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || expectedType.IsAssignableFrom(param))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.Types_IsOfType_Failed.Inject(expectedType.ToString(), param.ToString()), paramName);
		}

		public void IsOfType<T>(object param)
		{
			IsOfType(param, typeof(T));
		}

		public void IsOfType<T>(global::System.Type param)
		{
			IsOfType(param, typeof(T));
		}

		public void IsNotDefault<T>(T param) where T : struct
		{
			if (!global::Unity.VisualScripting.Ensure.IsActive || !default(T).Equals(param))
			{
				return;
			}
			throw new global::System.ArgumentException(global::Unity.VisualScripting.ExceptionMessages.ValueTypes_IsNotDefault_Failed, paramName);
		}
	}
}
