namespace Newtonsoft.Json.Utilities
{
	internal class FSharpUtils
	{
		private static readonly object Lock = new object();

		private static global::Newtonsoft.Json.Utilities.FSharpUtils? _instance;

		private global::System.Reflection.MethodInfo _ofSeq;

		private global::System.Type _mapType;

		public const string FSharpSetTypeName = "FSharpSet`1";

		public const string FSharpListTypeName = "FSharpList`1";

		public const string FSharpMapTypeName = "FSharpMap`2";

		public static global::Newtonsoft.Json.Utilities.FSharpUtils Instance => _instance;

		public global::System.Reflection.Assembly FSharpCoreAssembly { get; private set; }

		public global::Newtonsoft.Json.Utilities.MethodCall<object?, object> IsUnion { get; private set; }

		public global::Newtonsoft.Json.Utilities.MethodCall<object?, object> GetUnionCases { get; private set; }

		public global::Newtonsoft.Json.Utilities.MethodCall<object?, object> PreComputeUnionTagReader { get; private set; }

		public global::Newtonsoft.Json.Utilities.MethodCall<object?, object> PreComputeUnionReader { get; private set; }

		public global::Newtonsoft.Json.Utilities.MethodCall<object?, object> PreComputeUnionConstructor { get; private set; }

		public global::System.Func<object, object> GetUnionCaseInfoDeclaringType { get; private set; }

		public global::System.Func<object, object> GetUnionCaseInfoName { get; private set; }

		public global::System.Func<object, object> GetUnionCaseInfoTag { get; private set; }

		public global::Newtonsoft.Json.Utilities.MethodCall<object, object?> GetUnionCaseInfoFields { get; private set; }

		private FSharpUtils(global::System.Reflection.Assembly fsharpCoreAssembly)
		{
			FSharpCoreAssembly = fsharpCoreAssembly;
			global::System.Type type = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpType");
			global::System.Reflection.MethodInfo methodWithNonPublicFallback = GetMethodWithNonPublicFallback(type, "IsUnion", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public);
			IsUnion = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			global::System.Reflection.MethodInfo methodWithNonPublicFallback2 = GetMethodWithNonPublicFallback(type, "GetUnionCases", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public);
			GetUnionCases = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback2);
			global::System.Type type2 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpValue");
			PreComputeUnionTagReader = CreateFSharpFuncCall(type2, "PreComputeUnionTagReader");
			PreComputeUnionReader = CreateFSharpFuncCall(type2, "PreComputeUnionReader");
			PreComputeUnionConstructor = CreateFSharpFuncCall(type2, "PreComputeUnionConstructor");
			global::System.Type type3 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.UnionCaseInfo");
			GetUnionCaseInfoName = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Name"));
			GetUnionCaseInfoTag = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Tag"));
			GetUnionCaseInfoDeclaringType = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("DeclaringType"));
			GetUnionCaseInfoFields = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(type3.GetMethod("GetFields"));
			global::System.Type type4 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.ListModule");
			_ofSeq = type4.GetMethod("OfSeq");
			_mapType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpMap`2");
		}

		public static void EnsureInitialized(global::System.Reflection.Assembly fsharpCoreAssembly)
		{
			if (_instance != null)
			{
				return;
			}
			lock (Lock)
			{
				if (_instance == null)
				{
					_instance = new global::Newtonsoft.Json.Utilities.FSharpUtils(fsharpCoreAssembly);
				}
			}
		}

		private static global::System.Reflection.MethodInfo GetMethodWithNonPublicFallback(global::System.Type type, string methodName, global::System.Reflection.BindingFlags bindingFlags)
		{
			global::System.Reflection.MethodInfo method = type.GetMethod(methodName, bindingFlags);
			if (method == null && (bindingFlags & global::System.Reflection.BindingFlags.NonPublic) != global::System.Reflection.BindingFlags.NonPublic)
			{
				method = type.GetMethod(methodName, bindingFlags | global::System.Reflection.BindingFlags.NonPublic);
			}
			return method;
		}

		private static global::Newtonsoft.Json.Utilities.MethodCall<object?, object> CreateFSharpFuncCall(global::System.Type type, string methodName)
		{
			global::System.Reflection.MethodInfo methodWithNonPublicFallback = GetMethodWithNonPublicFallback(type, methodName, global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public);
			global::System.Reflection.MethodInfo method = methodWithNonPublicFallback.ReturnType.GetMethod("Invoke", global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public);
			global::Newtonsoft.Json.Utilities.MethodCall<object?, object?> call = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			global::Newtonsoft.Json.Utilities.MethodCall<object?, object> invoke = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object? target, object?[] args) => new global::Newtonsoft.Json.Utilities.FSharpFunction(call(target, args), invoke);
		}

		public global::Newtonsoft.Json.Serialization.ObjectConstructor<object> CreateSeq(global::System.Type t)
		{
			global::System.Reflection.MethodInfo method = _ofSeq.MakeGenericMethod(t);
			return global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
		}

		public global::Newtonsoft.Json.Serialization.ObjectConstructor<object> CreateMap(global::System.Type keyType, global::System.Type valueType)
		{
			return (global::Newtonsoft.Json.Serialization.ObjectConstructor<object>)typeof(global::Newtonsoft.Json.Utilities.FSharpUtils).GetMethod("BuildMapCreator").MakeGenericMethod(keyType, valueType).Invoke(this, null);
		}

		public global::Newtonsoft.Json.Serialization.ObjectConstructor<object> BuildMapCreator<TKey, TValue>()
		{
			global::System.Reflection.ConstructorInfo constructor = _mapType.MakeGenericType(typeof(TKey), typeof(TValue)).GetConstructor(new global::System.Type[1] { typeof(global::System.Collections.Generic.IEnumerable<global::System.Tuple<TKey, TValue>>) });
			global::Newtonsoft.Json.Serialization.ObjectConstructor<object> ctorDelegate = global::Newtonsoft.Json.Serialization.JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			return delegate(object?[] args)
			{
				global::System.Collections.Generic.IEnumerable<global::System.Tuple<TKey, TValue>> enumerable = global::System.Linq.Enumerable.Select((global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<TKey, TValue>>)args[0], (global::System.Collections.Generic.KeyValuePair<TKey, TValue> kv) => new global::System.Tuple<TKey, TValue>(kv.Key, kv.Value));
				return ctorDelegate(enumerable);
			};
		}
	}
}
