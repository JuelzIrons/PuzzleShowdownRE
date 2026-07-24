namespace Newtonsoft.Json.Utilities
{
	internal static class DynamicUtils
	{
		internal static class BinderWrapper
		{
			public const string CSharpAssemblyName = "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private const string BinderTypeName = "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private const string CSharpArgumentInfoTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private const string CSharpArgumentInfoFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private const string CSharpBinderFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			private static object? _getCSharpArgumentInfoArray;

			private static object? _setCSharpArgumentInfoArray;

			private static global::Newtonsoft.Json.Utilities.MethodCall<object?, object?>? _getMemberCall;

			private static global::Newtonsoft.Json.Utilities.MethodCall<object?, object?>? _setMemberCall;

			private static bool _init;

			private static void Init()
			{
				if (!_init)
				{
					if (global::System.Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", throwOnError: false) == null)
					{
						throw new global::System.InvalidOperationException("Could not resolve type '{0}'. You may need to add a reference to Microsoft.CSharp.dll to work with dynamic types.".FormatWith(global::System.Globalization.CultureInfo.InvariantCulture, "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
					}
					_getCSharpArgumentInfoArray = CreateSharpArgumentInfoArray(default(int));
					_setCSharpArgumentInfoArray = CreateSharpArgumentInfoArray(0, 3);
					CreateMemberCalls();
					_init = true;
				}
			}

			private static object CreateSharpArgumentInfoArray(params int[] values)
			{
				global::System.Type type = global::System.Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", throwOnError: true);
				global::System.Type type2 = global::System.Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", throwOnError: true);
				global::System.Array array = global::System.Array.CreateInstance(type, values.Length);
				for (int i = 0; i < values.Length; i++)
				{
					object value = type.GetMethod("Create", new global::System.Type[2]
					{
						type2,
						typeof(string)
					}).Invoke(null, new object[2] { 0, null });
					array.SetValue(value, i);
				}
				return array;
			}

			private static void CreateMemberCalls()
			{
				global::System.Type type = global::System.Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", throwOnError: true);
				global::System.Type type2 = global::System.Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", throwOnError: true);
				global::System.Type type3 = global::System.Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", throwOnError: true);
				global::System.Type type4 = typeof(global::System.Collections.Generic.IEnumerable<>).MakeGenericType(type);
				global::System.Reflection.MethodInfo method = type3.GetMethod("GetMember", new global::System.Type[4]
				{
					type2,
					typeof(string),
					typeof(global::System.Type),
					type4
				});
				_getMemberCall = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
				global::System.Reflection.MethodInfo method2 = type3.GetMethod("SetMember", new global::System.Type[4]
				{
					type2,
					typeof(string),
					typeof(global::System.Type),
					type4
				});
				_setMemberCall = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method2);
			}

			public static global::System.Runtime.CompilerServices.CallSiteBinder GetMember(string name, global::System.Type context)
			{
				Init();
				return (global::System.Runtime.CompilerServices.CallSiteBinder)_getMemberCall(null, 0, name, context, _getCSharpArgumentInfoArray);
			}

			public static global::System.Runtime.CompilerServices.CallSiteBinder SetMember(string name, global::System.Type context)
			{
				Init();
				return (global::System.Runtime.CompilerServices.CallSiteBinder)_setMemberCall(null, 0, name, context, _setCSharpArgumentInfoArray);
			}
		}

		public static global::System.Collections.Generic.IEnumerable<string> GetDynamicMemberNames(this global::System.Dynamic.IDynamicMetaObjectProvider dynamicProvider)
		{
			return dynamicProvider.GetMetaObject(global::System.Linq.Expressions.Expression.Constant(dynamicProvider)).GetDynamicMemberNames();
		}
	}
}
