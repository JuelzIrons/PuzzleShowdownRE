namespace Newtonsoft.Json.Utilities
{
	internal class ReflectionObject
	{
		public global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? Creator { get; }

		public global::System.Collections.Generic.IDictionary<string, global::Newtonsoft.Json.Utilities.ReflectionMember> Members { get; }

		private ReflectionObject(global::Newtonsoft.Json.Serialization.ObjectConstructor<object>? creator)
		{
			Members = new global::System.Collections.Generic.Dictionary<string, global::Newtonsoft.Json.Utilities.ReflectionMember>();
			Creator = creator;
		}

		public object? GetValue(object target, string member)
		{
			return Members[member].Getter(target);
		}

		public void SetValue(object target, string member, object? value)
		{
			Members[member].Setter(target, value);
		}

		public global::System.Type GetType(string member)
		{
			return Members[member].MemberType;
		}

		public static global::Newtonsoft.Json.Utilities.ReflectionObject Create(global::System.Type t, params string[] memberNames)
		{
			return Create(t, null, memberNames);
		}

		public static global::Newtonsoft.Json.Utilities.ReflectionObject Create(global::System.Type t, global::System.Reflection.MethodBase? creator, params string[] memberNames)
		{
			global::Newtonsoft.Json.Utilities.ReflectionDelegateFactory reflectionDelegateFactory = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory;
			global::Newtonsoft.Json.Serialization.ObjectConstructor<object> creator2 = null;
			if (creator != null)
			{
				creator2 = reflectionDelegateFactory.CreateParameterizedConstructor(creator);
			}
			else if (global::Newtonsoft.Json.Utilities.ReflectionUtils.HasDefaultConstructor(t, nonPublic: false))
			{
				global::System.Func<object> ctor = reflectionDelegateFactory.CreateDefaultConstructor<object>(t);
				creator2 = (object?[] args) => ctor();
			}
			global::Newtonsoft.Json.Utilities.ReflectionObject reflectionObject = new global::Newtonsoft.Json.Utilities.ReflectionObject(creator2);
			foreach (string text in memberNames)
			{
				global::System.Reflection.MemberInfo[] member = t.GetMember(text, global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
				if (member.Length != 1)
				{
					throw new global::System.ArgumentException("Expected a single member with the name '{0}'.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, text));
				}
				global::System.Reflection.MemberInfo memberInfo = global::System.Linq.Enumerable.Single(member);
				global::Newtonsoft.Json.Utilities.ReflectionMember reflectionMember = new global::Newtonsoft.Json.Utilities.ReflectionMember();
				switch (memberInfo.MemberType())
				{
				case global::System.Reflection.MemberTypes.Field:
				case global::System.Reflection.MemberTypes.Property:
					if (global::Newtonsoft.Json.Utilities.ReflectionUtils.CanReadMemberValue(memberInfo, nonPublic: false))
					{
						reflectionMember.Getter = reflectionDelegateFactory.CreateGet<object>(memberInfo);
					}
					if (global::Newtonsoft.Json.Utilities.ReflectionUtils.CanSetMemberValue(memberInfo, nonPublic: false, canSetReadOnly: false))
					{
						reflectionMember.Setter = reflectionDelegateFactory.CreateSet<object>(memberInfo);
					}
					break;
				case global::System.Reflection.MemberTypes.Method:
				{
					global::System.Reflection.MethodInfo methodInfo = (global::System.Reflection.MethodInfo)memberInfo;
					if (!methodInfo.IsPublic)
					{
						break;
					}
					global::System.Reflection.ParameterInfo[] parameters = methodInfo.GetParameters();
					if (parameters.Length == 0 && methodInfo.ReturnType != typeof(void))
					{
						global::Newtonsoft.Json.Utilities.MethodCall<object, object?> call = reflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
						reflectionMember.Getter = (object target) => call(target);
					}
					else if (parameters.Length == 1 && methodInfo.ReturnType == typeof(void))
					{
						global::Newtonsoft.Json.Utilities.MethodCall<object, object?> call2 = reflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
						reflectionMember.Setter = delegate(object target, object? arg)
						{
							call2(target, arg);
						};
					}
					break;
				}
				default:
					throw new global::System.ArgumentException("Unexpected member type '{0}' for member '{1}'.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, memberInfo.MemberType(), memberInfo.Name));
				}
				reflectionMember.MemberType = global::Newtonsoft.Json.Utilities.ReflectionUtils.GetMemberUnderlyingType(memberInfo);
				reflectionObject.Members[text] = reflectionMember;
			}
			return reflectionObject;
		}
	}
}
