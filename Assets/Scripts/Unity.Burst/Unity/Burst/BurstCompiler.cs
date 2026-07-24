namespace Unity.Burst
{
	public static class BurstCompiler
	{
		private class CommandBuilder
		{
			private global::System.Text.StringBuilder _builder;

			private bool _hasArgs;

			public CommandBuilder()
			{
				_builder = new global::System.Text.StringBuilder();
				_hasArgs = false;
			}

			public global::Unity.Burst.BurstCompiler.CommandBuilder Begin(string cmd)
			{
				_builder.Clear();
				_hasArgs = false;
				_builder.Append(cmd);
				return this;
			}

			public global::Unity.Burst.BurstCompiler.CommandBuilder With(string arg)
			{
				if (!_hasArgs)
				{
					_builder.Append(' ');
				}
				_hasArgs = true;
				_builder.Append(arg);
				return this;
			}

			public global::Unity.Burst.BurstCompiler.CommandBuilder With(global::System.IntPtr arg)
			{
				if (!_hasArgs)
				{
					_builder.Append(' ');
				}
				_hasArgs = true;
				_builder.AppendFormat("0x{0:X16}", arg.ToInt64());
				return this;
			}

			public global::Unity.Burst.BurstCompiler.CommandBuilder And(char sep = '|')
			{
				_builder.Append(sep);
				return this;
			}

			public string SendToCompiler()
			{
				return SendRawCommandToCompiler(_builder.ToString());
			}
		}

		[global::System.AttributeUsage(global::System.AttributeTargets.Assembly, AllowMultiple = true)]
		internal class StaticTypeReinitAttribute : global::System.Attribute
		{
			public readonly global::System.Type reinitType;

			public StaticTypeReinitAttribute(global::System.Type toReinit)
			{
				reinitType = toReinit;
			}
		}

		[global::Unity.Burst.BurstCompile]
		internal static class BurstCompilerHelper
		{
			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
			private delegate bool IsBurstEnabledDelegate();

			[global::System.Runtime.InteropServices.UnmanagedFunctionPointer(global::System.Runtime.InteropServices.CallingConvention.Cdecl)]
			internal delegate bool IsBurstEnabled_00000145_0024PostfixBurstDelegate();

			internal static class IsBurstEnabled_00000145_0024BurstDirectCall
			{
				private static global::System.IntPtr Pointer;

				[global::Unity.Burst.BurstDiscard]
				private static void GetFunctionPointerDiscard(ref global::System.IntPtr P_0)
				{
					if (Pointer == (global::System.IntPtr)0)
					{
						Pointer = CompileFunctionPointer<global::Unity.Burst.BurstCompiler.BurstCompilerHelper.IsBurstEnabled_00000145_0024PostfixBurstDelegate>(IsBurstEnabled).Value;
					}
					P_0 = Pointer;
				}

				private static global::System.IntPtr GetFunctionPointer()
				{
					nint result = 0;
					GetFunctionPointerDiscard(ref result);
					return result;
				}

				public unsafe static bool Invoke()
				{
					if (IsEnabled)
					{
						global::System.IntPtr functionPointer = GetFunctionPointer();
						if (functionPointer != (global::System.IntPtr)0)
						{
							return ((delegate* unmanaged[Cdecl]<bool>)functionPointer)();
						}
					}
					return IsBurstEnabled_0024BurstManaged();
				}
			}

			private static readonly global::Unity.Burst.BurstCompiler.BurstCompilerHelper.IsBurstEnabledDelegate IsBurstEnabledImpl = IsBurstEnabled;

			public static readonly bool IsBurstGenerated = IsCompiledByBurst(IsBurstEnabledImpl);

			[global::Unity.Burst.BurstCompile]
			[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Burst.BurstCompiler.BurstCompilerHelper.IsBurstEnabledDelegate))]
			private static bool IsBurstEnabled()
			{
				return global::Unity.Burst.BurstCompiler.BurstCompilerHelper.IsBurstEnabled_00000145_0024BurstDirectCall.Invoke();
			}

			[global::Unity.Burst.BurstDiscard]
			private static void DiscardedMethod(ref bool value)
			{
				value = false;
			}

			private unsafe static bool IsCompiledByBurst(global::System.Delegate del)
			{
				return global::Unity.Burst.LowLevel.BurstCompilerService.GetAsyncCompiledAsyncDelegateMethod(global::Unity.Burst.LowLevel.BurstCompilerService.CompileAsyncDelegateMethod(del, string.Empty)) != null;
			}

			[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
			[global::Unity.Burst.BurstCompile]
			[global::AOT.MonoPInvokeCallback(typeof(global::Unity.Burst.BurstCompiler.BurstCompilerHelper.IsBurstEnabledDelegate))]
			internal static bool IsBurstEnabled_0024BurstManaged()
			{
				bool value = true;
				DiscardedMethod(ref value);
				return value;
			}
		}

		private class FakeDelegate
		{
			[global::UnityEngine.Scripting.Preserve]
			public global::System.Reflection.MethodInfo Method { get; }

			public FakeDelegate(global::System.Reflection.MethodInfo method)
			{
				Method = method;
			}
		}

		[global::System.ThreadStatic]
		private static global::Unity.Burst.BurstCompiler.CommandBuilder _cmdBuilder;

		internal static bool _IsEnabled;

		public static readonly global::Unity.Burst.BurstCompilerOptions Options = new global::Unity.Burst.BurstCompilerOptions(isGlobal: true);

		internal static global::System.Action OnCompileILPPMethod2;

		private static readonly global::System.Reflection.MethodInfo DummyMethodInfo = typeof(global::Unity.Burst.BurstCompiler).GetMethod("DummyMethod", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.NonPublic);

		public static bool IsEnabled
		{
			get
			{
				if (_IsEnabled)
				{
					return global::Unity.Burst.BurstCompiler.BurstCompilerHelper.IsBurstGenerated;
				}
				return false;
			}
		}

		public static bool IsLoadAdditionalLibrarySupported()
		{
			return IsApiAvailable("LoadBurstLibrary");
		}

		private static global::Unity.Burst.BurstCompiler.CommandBuilder BeginCompilerCommand(string cmd)
		{
			if (_cmdBuilder == null)
			{
				_cmdBuilder = new global::Unity.Burst.BurstCompiler.CommandBuilder();
			}
			return _cmdBuilder.Begin(cmd);
		}

		public static void SetExecutionMode(global::Unity.Burst.BurstExecutionEnvironment mode)
		{
			global::Unity.Burst.LowLevel.BurstCompilerService.SetCurrentExecutionMode((uint)mode);
		}

		public static global::Unity.Burst.BurstExecutionEnvironment GetExecutionMode()
		{
			return (global::Unity.Burst.BurstExecutionEnvironment)global::Unity.Burst.LowLevel.BurstCompilerService.GetCurrentExecutionMode();
		}

		internal unsafe static T CompileDelegate<T>(T delegateMethod, bool deterministicCompilation = false) where T : class
		{
			return (T)(object)global::System.Runtime.InteropServices.Marshal.GetDelegateForFunctionPointer((global::System.IntPtr)Compile(delegateMethod, isFunctionPointer: false, deterministicCompilation), delegateMethod.GetType());
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void VerifyDelegateIsNotMulticast<T>(T delegateMethod) where T : class
		{
			if ((delegateMethod as global::System.Delegate).GetInvocationList().Length > 1)
			{
				throw new global::System.InvalidOperationException($"Burst does not support multicast delegates, please use a regular delegate for `{delegateMethod}'");
			}
		}

		[global::System.Diagnostics.Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void VerifyDelegateHasCorrectUnmanagedFunctionPointerAttribute<T>(T delegateMethod) where T : class
		{
			global::System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute customAttribute = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute>(delegateMethod.GetType());
			if (customAttribute == null || customAttribute.CallingConvention != global::System.Runtime.InteropServices.CallingConvention.Cdecl)
			{
				global::UnityEngine.Debug.LogWarning("The delegate type " + delegateMethod.GetType().FullName + " should be decorated with [UnmanagedFunctionPointer(CallingConvention.Cdecl)] to ensure runtime interoperabilty between managed code and Burst-compiled code.");
			}
		}

		[global::System.Obsolete("This method will be removed in a future version of Burst")]
		public static global::System.IntPtr CompileILPPMethod(global::System.RuntimeMethodHandle burstMethodHandle, global::System.RuntimeMethodHandle managedMethodHandle, global::System.RuntimeTypeHandle delegateTypeHandle)
		{
			throw new global::System.NotImplementedException();
		}

		public unsafe static global::System.IntPtr CompileILPPMethod2(global::System.RuntimeMethodHandle burstMethodHandle)
		{
			if (burstMethodHandle.Value == global::System.IntPtr.Zero)
			{
				throw new global::System.ArgumentNullException("burstMethodHandle");
			}
			OnCompileILPPMethod2?.Invoke();
			global::System.Reflection.MethodInfo methodInfo = (global::System.Reflection.MethodInfo)global::System.Reflection.MethodBase.GetMethodFromHandle(burstMethodHandle);
			return (global::System.IntPtr)Compile(new global::Unity.Burst.BurstCompiler.FakeDelegate(methodInfo), methodInfo, isFunctionPointer: true, isILPostProcessing: true);
		}

		[global::System.Obsolete("This method will be removed in a future version of Burst")]
		public unsafe static void* GetILPPMethodFunctionPointer(global::System.IntPtr ilppMethod)
		{
			throw new global::System.NotImplementedException();
		}

		public unsafe static void* GetILPPMethodFunctionPointer2(global::System.IntPtr ilppMethod, global::System.RuntimeMethodHandle managedMethodHandle, global::System.RuntimeTypeHandle delegateTypeHandle)
		{
			if (managedMethodHandle.Value == global::System.IntPtr.Zero)
			{
				throw new global::System.ArgumentNullException("managedMethodHandle");
			}
			if (delegateTypeHandle.Value == global::System.IntPtr.Zero)
			{
				throw new global::System.ArgumentNullException("delegateTypeHandle");
			}
			if (ilppMethod == global::System.IntPtr.Zero)
			{
				GetManagedFallbackDelegate(out var managedFallbackDelegate, out var _);
				return (void*)global::System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(managedFallbackDelegate);
			}
			return ilppMethod.ToPointer();
			void GetManagedFallbackDelegate(out global::System.Delegate reference, out global::System.Runtime.InteropServices.GCHandle reference2)
			{
				global::System.Reflection.MethodInfo method = (global::System.Reflection.MethodInfo)global::System.Reflection.MethodBase.GetMethodFromHandle(managedMethodHandle);
				global::System.Type typeFromHandle = global::System.Type.GetTypeFromHandle(delegateTypeHandle);
				reference = global::System.Delegate.CreateDelegate(typeFromHandle, method);
				reference2 = global::System.Runtime.InteropServices.GCHandle.Alloc(reference);
			}
		}

		[global::System.Obsolete("This method will be removed in a future version of Burst")]
		public unsafe static void* CompileUnsafeStaticMethod(global::System.RuntimeMethodHandle handle)
		{
			throw new global::System.NotImplementedException();
		}

		public unsafe static global::Unity.Burst.FunctionPointer<T> CompileFunctionPointer<T>(T delegateMethod) where T : class
		{
			return new global::Unity.Burst.FunctionPointer<T>(new global::System.IntPtr(Compile(delegateMethod, isFunctionPointer: true)));
		}

		private unsafe static void* Compile(object delegateObj, bool isFunctionPointer, bool deterministicCompilation = false)
		{
			if (!(delegateObj is global::System.Delegate))
			{
				throw new global::System.ArgumentException("object instance must be a System.Delegate", "delegateObj");
			}
			global::System.Delegate obj = (global::System.Delegate)delegateObj;
			return Compile(obj, obj.Method, isFunctionPointer, isILPostProcessing: false, deterministicCompilation);
		}

		private unsafe static void* Compile(object delegateObj, global::System.Reflection.MethodInfo methodInfo, bool isFunctionPointer, bool isILPostProcessing, bool deterministicCompilation = false)
		{
			if (delegateObj == null)
			{
				throw new global::System.ArgumentNullException("delegateObj");
			}
			if (delegateObj.GetType().IsGenericType)
			{
				throw new global::System.InvalidOperationException($"The delegate type `{delegateObj.GetType()}` must be a non-generic type");
			}
			if (!methodInfo.IsStatic)
			{
				throw new global::System.InvalidOperationException($"The method `{methodInfo}` must be static. Instance methods are not supported");
			}
			if (methodInfo.IsGenericMethod)
			{
				throw new global::System.InvalidOperationException($"The method `{methodInfo}` must be a non-generic method");
			}
			global::System.Delegate obj = null;
			if (!isILPostProcessing)
			{
				obj = delegateObj as global::System.Delegate;
			}
			global::System.Delegate obj2 = delegateObj as global::System.Delegate;
			if (global::Unity.Burst.BurstCompilerOptions.HasBurstCompileAttribute(methodInfo))
			{
				void* ptr = null;
				if (Options.EnableBurstCompilation && global::Unity.Burst.BurstCompiler.BurstCompilerHelper.IsBurstGenerated)
				{
					if (isFunctionPointer && methodInfo.Name.EndsWith("$BurstManaged"))
					{
						delegateObj = methodInfo.DeclaringType.GetMethod(methodInfo.Name.Replace("$BurstManaged", ""), global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic).CreateDelegate(obj2.GetType());
					}
					ptr = global::Unity.Burst.LowLevel.BurstCompilerService.GetAsyncCompiledAsyncDelegateMethod(global::Unity.Burst.LowLevel.BurstCompilerService.CompileAsyncDelegateMethod(delegateObj, string.Empty));
				}
				if (ptr == null)
				{
					if (isILPostProcessing)
					{
						return null;
					}
					global::System.Runtime.InteropServices.GCHandle.Alloc(obj);
					ptr = (void*)global::System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(obj);
				}
				if (ptr == null)
				{
					throw new global::System.InvalidOperationException($"Burst failed to compile the function pointer `{methodInfo}`");
				}
				return ptr;
			}
			throw new global::System.InvalidOperationException($"Burst cannot compile the function pointer `{methodInfo}` because the `[BurstCompile]` attribute is missing");
		}

		internal static void Shutdown()
		{
		}

		internal static void Cancel()
		{
		}

		internal static bool IsCurrentCompilationDone()
		{
			return true;
		}

		internal static void Enable()
		{
		}

		internal static void Disable()
		{
		}

		internal static bool IsHostEditorArm()
		{
			return false;
		}

		internal static void TriggerUnsafeStaticMethodRecompilation()
		{
			global::System.Reflection.Assembly[] assemblies = global::System.AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				foreach (global::System.Attribute item in global::System.Linq.Enumerable.Where(global::System.Reflection.CustomAttributeExtensions.GetCustomAttributes(assemblies[i]), (global::System.Attribute x) => x.GetType().FullName == "Unity.Burst.BurstCompiler+StaticTypeReinitAttribute"))
				{
					(item as global::Unity.Burst.BurstCompiler.StaticTypeReinitAttribute).reinitType.GetMethod("Constructor", global::System.Reflection.BindingFlags.Static | global::System.Reflection.BindingFlags.Public).Invoke(null, new object[0]);
				}
			}
		}

		internal static void TriggerRecompilation()
		{
		}

		internal static void UnloadAdditionalLibraries()
		{
			SendCommandToCompiler("$unload_burst_natives");
		}

		internal static bool IsApiAvailable(string apiName)
		{
			return SendCommandToCompiler("$is_native_api_available", apiName) == "True";
		}

		internal static int RequestSetProtocolVersion(int version)
		{
			string text = SendCommandToCompiler("$request_set_protocol_version_editor", $"{version}");
			if (string.IsNullOrEmpty(text) || !int.TryParse(text, out var result))
			{
				result = 0;
			}
			SendCommandToCompiler("$set_protocol_version_burst", $"{result}");
			return result;
		}

		internal static void Initialize(string[] assemblyFolders, string[] ignoreAssemblies)
		{
		}

		internal static void NotifyCompilationStarted(string[] assemblyFolders, string[] ignoreAssemblies)
		{
		}

		internal static void NotifyAssemblyCompilationNotRequired(string assemblyName)
		{
		}

		internal static void NotifyAssemblyCompilationFinished(string assemblyName, string[] defines)
		{
		}

		internal static void NotifyCompilationFinished()
		{
		}

		internal static string AotCompilation(string[] assemblyFolders, string[] assemblyRoots, string options)
		{
			return "failed";
		}

		internal static void SetProfilerCallbacks()
		{
		}

		private static string SendRawCommandToCompiler(string command)
		{
			string disassembly = global::Unity.Burst.LowLevel.BurstCompilerService.GetDisassembly(DummyMethodInfo, command);
			if (!string.IsNullOrEmpty(disassembly))
			{
				return disassembly.TrimStart('\n');
			}
			return "";
		}

		private static string SendCommandToCompiler(string commandName, string commandArgs = null)
		{
			if (commandName == null)
			{
				throw new global::System.ArgumentNullException("commandName");
			}
			if (commandArgs == null)
			{
				return SendRawCommandToCompiler(commandName);
			}
			return BeginCompilerCommand(commandName).With(commandArgs).SendToCompiler();
		}

		private static void DummyMethod()
		{
		}
	}
}
