namespace Unity.VisualScripting
{
	public static class AttributeUtility
	{
		private class AttributeCache
		{
			public global::System.Collections.Generic.List<global::System.Attribute> inheritedAttributes { get; } = new global::System.Collections.Generic.List<global::System.Attribute>();

			public global::System.Collections.Generic.List<global::System.Attribute> definedAttributes { get; } = new global::System.Collections.Generic.List<global::System.Attribute>();

			public AttributeCache(global::System.Reflection.MemberInfo element)
			{
				global::Unity.VisualScripting.Ensure.That("element").IsNotNull(element);
				try
				{
					try
					{
						Cache(global::System.Attribute.GetCustomAttributes(element, inherit: true), inheritedAttributes);
					}
					catch (global::System.InvalidCastException arg)
					{
						Cache(global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Cast<global::System.Attribute>(element.GetCustomAttributes(inherit: true))), inheritedAttributes);
						global::UnityEngine.Debug.LogWarning($"Failed to fetch inherited attributes on {element}.\n{arg}");
					}
				}
				catch (global::System.Exception arg2)
				{
					global::UnityEngine.Debug.LogWarning($"Failed to fetch inherited attributes on {element}.\n{arg2}");
				}
				try
				{
					try
					{
						Cache(global::System.Attribute.GetCustomAttributes(element, inherit: false), definedAttributes);
					}
					catch (global::System.InvalidCastException)
					{
						Cache(global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Cast<global::System.Attribute>(element.GetCustomAttributes(inherit: false))), definedAttributes);
					}
				}
				catch (global::System.Exception arg3)
				{
					global::UnityEngine.Debug.LogWarning($"Failed to fetch defined attributes on {element}.\n{arg3}");
				}
			}

			public AttributeCache(global::System.Reflection.ParameterInfo element)
			{
				global::Unity.VisualScripting.Ensure.That("element").IsNotNull(element);
				try
				{
					try
					{
						Cache(global::System.Attribute.GetCustomAttributes(element, inherit: true), inheritedAttributes);
					}
					catch (global::System.InvalidCastException arg)
					{
						Cache(global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Cast<global::System.Attribute>(element.GetCustomAttributes(inherit: true))), inheritedAttributes);
						global::UnityEngine.Debug.LogWarning($"Failed to fetch inherited attributes on {element}.\n{arg}");
					}
				}
				catch (global::System.Exception arg2)
				{
					global::UnityEngine.Debug.LogWarning($"Failed to fetch inherited attributes on {element}.\n{arg2}");
				}
				try
				{
					try
					{
						Cache(global::System.Attribute.GetCustomAttributes(element, inherit: false), definedAttributes);
					}
					catch (global::System.InvalidCastException)
					{
						Cache(global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Cast<global::System.Attribute>(element.GetCustomAttributes(inherit: false))), definedAttributes);
					}
				}
				catch (global::System.Exception arg3)
				{
					global::UnityEngine.Debug.LogWarning($"Failed to fetch defined attributes on {element}.\n{arg3}");
				}
			}

			public AttributeCache(global::Unity.VisualScripting.IAttributeProvider element)
			{
				global::Unity.VisualScripting.Ensure.That("element").IsNotNull(element);
				try
				{
					Cache(element.GetCustomAttributes(inherit: true), inheritedAttributes);
				}
				catch (global::System.Exception arg)
				{
					global::UnityEngine.Debug.LogWarning($"Failed to fetch inherited attributes on {element}.\n{arg}");
				}
				try
				{
					Cache(element.GetCustomAttributes(inherit: false), definedAttributes);
				}
				catch (global::System.Exception arg2)
				{
					global::UnityEngine.Debug.LogWarning($"Failed to fetch defined attributes on {element}.\n{arg2}");
				}
			}

			private void Cache(global::System.Attribute[] attributeObjects, global::System.Collections.Generic.List<global::System.Attribute> cache)
			{
				foreach (global::System.Attribute item in attributeObjects)
				{
					cache.Add(item);
				}
			}

			private bool HasAttribute(global::System.Type attributeType, global::System.Collections.Generic.List<global::System.Attribute> cache)
			{
				for (int i = 0; i < cache.Count; i++)
				{
					global::System.Attribute o = cache[i];
					if (attributeType.IsInstanceOfType(o))
					{
						return true;
					}
				}
				return false;
			}

			private global::System.Attribute GetAttribute(global::System.Type attributeType, global::System.Collections.Generic.List<global::System.Attribute> cache)
			{
				for (int i = 0; i < cache.Count; i++)
				{
					global::System.Attribute attribute = cache[i];
					if (attributeType.IsInstanceOfType(attribute))
					{
						return attribute;
					}
				}
				return null;
			}

			private global::System.Collections.Generic.IEnumerable<global::System.Attribute> GetAttributes(global::System.Type attributeType, global::System.Collections.Generic.List<global::System.Attribute> cache)
			{
				for (int i = 0; i < cache.Count; i++)
				{
					global::System.Attribute attribute = cache[i];
					if (attributeType.IsInstanceOfType(attribute))
					{
						yield return attribute;
					}
				}
			}

			public bool HasAttribute(global::System.Type attributeType, bool inherit = true)
			{
				if (inherit)
				{
					return HasAttribute(attributeType, inheritedAttributes);
				}
				return HasAttribute(attributeType, definedAttributes);
			}

			public global::System.Attribute GetAttribute(global::System.Type attributeType, bool inherit = true)
			{
				if (inherit)
				{
					return GetAttribute(attributeType, inheritedAttributes);
				}
				return GetAttribute(attributeType, definedAttributes);
			}

			public global::System.Collections.Generic.IEnumerable<global::System.Attribute> GetAttributes(global::System.Type attributeType, bool inherit = true)
			{
				if (inherit)
				{
					return GetAttributes(attributeType, inheritedAttributes);
				}
				return GetAttributes(attributeType, definedAttributes);
			}

			public bool HasAttribute<TAttribute>(bool inherit = true) where TAttribute : global::System.Attribute
			{
				return HasAttribute(typeof(TAttribute), inherit);
			}

			public TAttribute GetAttribute<TAttribute>(bool inherit = true) where TAttribute : global::System.Attribute
			{
				return (TAttribute)GetAttribute(typeof(TAttribute), inherit);
			}

			public global::System.Collections.Generic.IEnumerable<TAttribute> GetAttributes<TAttribute>(bool inherit = true) where TAttribute : global::System.Attribute
			{
				return global::System.Linq.Enumerable.Cast<TAttribute>(GetAttributes(typeof(TAttribute), inherit));
			}
		}

		private static readonly global::System.Collections.Generic.Dictionary<object, global::Unity.VisualScripting.AttributeUtility.AttributeCache> optimizedCaches = new global::System.Collections.Generic.Dictionary<object, global::Unity.VisualScripting.AttributeUtility.AttributeCache>();

		private static global::Unity.VisualScripting.AttributeUtility.AttributeCache GetAttributeCache(global::System.Reflection.MemberInfo element)
		{
			global::Unity.VisualScripting.Ensure.That("element").IsNotNull(element);
			lock (optimizedCaches)
			{
				if (!optimizedCaches.TryGetValue(element, out var value))
				{
					value = new global::Unity.VisualScripting.AttributeUtility.AttributeCache(element);
					optimizedCaches.Add(element, value);
				}
				return value;
			}
		}

		private static global::Unity.VisualScripting.AttributeUtility.AttributeCache GetAttributeCache(global::System.Reflection.ParameterInfo element)
		{
			global::Unity.VisualScripting.Ensure.That("element").IsNotNull(element);
			lock (optimizedCaches)
			{
				if (!optimizedCaches.TryGetValue(element, out var value))
				{
					value = new global::Unity.VisualScripting.AttributeUtility.AttributeCache(element);
					optimizedCaches.Add(element, value);
				}
				return value;
			}
		}

		private static global::Unity.VisualScripting.AttributeUtility.AttributeCache GetAttributeCache(global::Unity.VisualScripting.IAttributeProvider element)
		{
			global::Unity.VisualScripting.Ensure.That("element").IsNotNull(element);
			lock (optimizedCaches)
			{
				if (!optimizedCaches.TryGetValue(element, out var value))
				{
					value = new global::Unity.VisualScripting.AttributeUtility.AttributeCache(element);
					optimizedCaches.Add(element, value);
				}
				return value;
			}
		}

		public static void CacheAttributes(global::System.Reflection.MemberInfo element)
		{
			GetAttributeCache(element);
		}

		internal static global::System.Collections.Generic.IEnumerable<T> GetAttributeOfEnumMember<T>(this global::System.Enum enumVal) where T : global::System.Attribute
		{
			return global::System.Linq.Enumerable.Cast<T>(enumVal.GetType().GetMember(enumVal.ToString())[0].GetCustomAttributes(typeof(T), inherit: false));
		}

		public static bool HasAttribute(this global::System.Reflection.MemberInfo element, global::System.Type attributeType, bool inherit = true)
		{
			return GetAttributeCache(element).HasAttribute(attributeType, inherit);
		}

		public static global::System.Attribute GetAttribute(this global::System.Reflection.MemberInfo element, global::System.Type attributeType, bool inherit = true)
		{
			return GetAttributeCache(element).GetAttribute(attributeType, inherit);
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Attribute> GetAttributes(this global::System.Reflection.MemberInfo element, global::System.Type attributeType, bool inherit = true)
		{
			return GetAttributeCache(element).GetAttributes(attributeType, inherit);
		}

		public static bool HasAttribute<TAttribute>(this global::System.Reflection.MemberInfo element, bool inherit = true) where TAttribute : global::System.Attribute
		{
			return GetAttributeCache(element).HasAttribute<TAttribute>(inherit);
		}

		public static TAttribute GetAttribute<TAttribute>(this global::System.Reflection.MemberInfo element, bool inherit = true) where TAttribute : global::System.Attribute
		{
			return GetAttributeCache(element).GetAttribute<TAttribute>(inherit);
		}

		public static global::System.Collections.Generic.IEnumerable<TAttribute> GetAttributes<TAttribute>(this global::System.Reflection.MemberInfo element, bool inherit = true) where TAttribute : global::System.Attribute
		{
			return GetAttributeCache(element).GetAttributes<TAttribute>(inherit);
		}

		public static void CacheAttributes(global::System.Reflection.ParameterInfo element)
		{
			GetAttributeCache(element);
		}

		public static bool HasAttribute(this global::System.Reflection.ParameterInfo element, global::System.Type attributeType, bool inherit = true)
		{
			return GetAttributeCache(element).HasAttribute(attributeType, inherit);
		}

		public static global::System.Attribute GetAttribute(this global::System.Reflection.ParameterInfo element, global::System.Type attributeType, bool inherit = true)
		{
			return GetAttributeCache(element).GetAttribute(attributeType, inherit);
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Attribute> GetAttributes(this global::System.Reflection.ParameterInfo element, global::System.Type attributeType, bool inherit = true)
		{
			return GetAttributeCache(element).GetAttributes(attributeType, inherit);
		}

		public static bool HasAttribute<TAttribute>(this global::System.Reflection.ParameterInfo element, bool inherit = true) where TAttribute : global::System.Attribute
		{
			return GetAttributeCache(element).HasAttribute<TAttribute>(inherit);
		}

		public static TAttribute GetAttribute<TAttribute>(this global::System.Reflection.ParameterInfo element, bool inherit = true) where TAttribute : global::System.Attribute
		{
			return GetAttributeCache(element).GetAttribute<TAttribute>(inherit);
		}

		public static global::System.Collections.Generic.IEnumerable<TAttribute> GetAttributes<TAttribute>(this global::System.Reflection.ParameterInfo element, bool inherit = true) where TAttribute : global::System.Attribute
		{
			return GetAttributeCache(element).GetAttributes<TAttribute>(inherit);
		}

		public static void CacheAttributes(global::Unity.VisualScripting.IAttributeProvider element)
		{
			GetAttributeCache(element);
		}

		public static bool HasAttribute(this global::Unity.VisualScripting.IAttributeProvider element, global::System.Type attributeType, bool inherit = true)
		{
			return GetAttributeCache(element).HasAttribute(attributeType, inherit);
		}

		public static global::System.Attribute GetAttribute(this global::Unity.VisualScripting.IAttributeProvider element, global::System.Type attributeType, bool inherit = true)
		{
			return GetAttributeCache(element).GetAttribute(attributeType, inherit);
		}

		public static global::System.Collections.Generic.IEnumerable<global::System.Attribute> GetAttributes(this global::Unity.VisualScripting.IAttributeProvider element, global::System.Type attributeType, bool inherit = true)
		{
			return GetAttributeCache(element).GetAttributes(attributeType, inherit);
		}

		public static bool HasAttribute<TAttribute>(this global::Unity.VisualScripting.IAttributeProvider element, bool inherit = true) where TAttribute : global::System.Attribute
		{
			return GetAttributeCache(element).HasAttribute<TAttribute>(inherit);
		}

		public static TAttribute GetAttribute<TAttribute>(this global::Unity.VisualScripting.IAttributeProvider element, bool inherit = true) where TAttribute : global::System.Attribute
		{
			return GetAttributeCache(element).GetAttribute<TAttribute>(inherit);
		}

		public static global::System.Collections.Generic.IEnumerable<TAttribute> GetAttributes<TAttribute>(this global::Unity.VisualScripting.IAttributeProvider element, bool inherit = true) where TAttribute : global::System.Attribute
		{
			return GetAttributeCache(element).GetAttributes<TAttribute>(inherit);
		}

		public static bool CheckCondition(global::System.Type type, object target, string conditionMemberName, bool fallback)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			try
			{
				if (target != null && !type.IsInstanceOfType(target))
				{
					throw new global::System.ArgumentException("Target is not an instance of type.", "target");
				}
				if (conditionMemberName == null)
				{
					return fallback;
				}
				global::Unity.VisualScripting.Member obj = global::System.Linq.Enumerable.FirstOrDefault(type.GetMember(conditionMemberName, global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic))?.ToManipulator();
				if (obj == null)
				{
					throw new global::System.MissingMemberException(type.ToString(), conditionMemberName);
				}
				return obj.Get<bool>(target);
			}
			catch (global::System.Exception ex)
			{
				global::UnityEngine.Debug.LogWarning("Failed to check attribute condition: \n" + ex);
				return fallback;
			}
		}

		public static bool CheckCondition<T>(T target, string conditionMemberName, bool fallback)
		{
			return CheckCondition(target?.GetType() ?? typeof(T), target, conditionMemberName, fallback);
		}
	}
}
