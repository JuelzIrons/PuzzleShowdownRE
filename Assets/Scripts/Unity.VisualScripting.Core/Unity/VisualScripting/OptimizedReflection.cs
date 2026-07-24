namespace Unity.VisualScripting
{
	public static class OptimizedReflection
	{
		private static readonly global::System.Collections.Generic.Dictionary<global::System.Reflection.FieldInfo, global::Unity.VisualScripting.IOptimizedAccessor> fieldAccessors;

		private static readonly global::System.Collections.Generic.Dictionary<global::System.Reflection.PropertyInfo, global::Unity.VisualScripting.IOptimizedAccessor> propertyAccessors;

		private static readonly global::System.Collections.Generic.Dictionary<global::System.Reflection.MethodInfo, global::Unity.VisualScripting.IOptimizedInvoker> methodInvokers;

		public static readonly bool jitAvailable;

		private static bool _useJitIfAvailable;

		internal static bool useJit
		{
			get
			{
				if (useJitIfAvailable)
				{
					return jitAvailable;
				}
				return false;
			}
		}

		public static bool useJitIfAvailable
		{
			get
			{
				return _useJitIfAvailable;
			}
			set
			{
				_useJitIfAvailable = value;
				ClearCache();
			}
		}

		public static bool safeMode { get; set; }

		static OptimizedReflection()
		{
			_useJitIfAvailable = true;
			fieldAccessors = new global::System.Collections.Generic.Dictionary<global::System.Reflection.FieldInfo, global::Unity.VisualScripting.IOptimizedAccessor>();
			propertyAccessors = new global::System.Collections.Generic.Dictionary<global::System.Reflection.PropertyInfo, global::Unity.VisualScripting.IOptimizedAccessor>();
			methodInvokers = new global::System.Collections.Generic.Dictionary<global::System.Reflection.MethodInfo, global::Unity.VisualScripting.IOptimizedInvoker>();
			jitAvailable = global::Unity.VisualScripting.PlatformUtility.supportsJit;
		}

		internal static void OnRuntimeMethodLoad()
		{
			safeMode = global::UnityEngine.Application.isEditor || global::UnityEngine.Debug.isDebugBuild;
		}

		public static void ClearCache()
		{
			fieldAccessors.Clear();
			propertyAccessors.Clear();
			methodInvokers.Clear();
		}

		internal static void VerifyStaticTarget(global::System.Type targetType, object target)
		{
			VerifyTarget(targetType, target, @static: true);
		}

		internal static void VerifyInstanceTarget<TTArget>(object target)
		{
			VerifyTarget(typeof(TTArget), target, @static: false);
		}

		private static void VerifyTarget(global::System.Type targetType, object target, bool @static)
		{
			global::Unity.VisualScripting.Ensure.That("targetType").IsNotNull(targetType);
			if (@static)
			{
				if (target != null)
				{
					throw new global::System.Reflection.TargetException($"Superfluous target object for '{targetType}'.");
				}
				return;
			}
			if (target == null)
			{
				throw new global::System.Reflection.TargetException($"Missing target object for '{targetType}'.");
			}
			if (!targetType.IsAssignableFrom(targetType))
			{
				throw new global::System.Reflection.TargetException($"The target object does not match the target type.\nProvided: {target.GetType()}\nExpected: {targetType}");
			}
		}

		private static bool SupportsOptimization(global::System.Reflection.MemberInfo memberInfo)
		{
			if (memberInfo.DeclaringType.IsValueType && !memberInfo.IsStatic())
			{
				return false;
			}
			return true;
		}

		public static global::Unity.VisualScripting.IOptimizedAccessor Prewarm(this global::System.Reflection.FieldInfo fieldInfo)
		{
			return GetFieldAccessor(fieldInfo);
		}

		public static object GetValueOptimized(this global::System.Reflection.FieldInfo fieldInfo, object target)
		{
			return GetFieldAccessor(fieldInfo).GetValue(target);
		}

		public static void SetValueOptimized(this global::System.Reflection.FieldInfo fieldInfo, object target, object value)
		{
			GetFieldAccessor(fieldInfo).SetValue(target, value);
		}

		public static bool SupportsOptimization(this global::System.Reflection.FieldInfo fieldInfo)
		{
			if (!SupportsOptimization((global::System.Reflection.MemberInfo)fieldInfo))
			{
				return false;
			}
			return true;
		}

		private static global::Unity.VisualScripting.IOptimizedAccessor GetFieldAccessor(global::System.Reflection.FieldInfo fieldInfo)
		{
			global::Unity.VisualScripting.Ensure.That("fieldInfo").IsNotNull(fieldInfo);
			lock (fieldAccessors)
			{
				if (!fieldAccessors.TryGetValue(fieldInfo, out var value))
				{
					if (fieldInfo.SupportsOptimization())
					{
						global::System.Type type = ((!fieldInfo.IsStatic) ? typeof(global::Unity.VisualScripting.InstanceFieldAccessor<, >).MakeGenericType(fieldInfo.DeclaringType, fieldInfo.FieldType) : typeof(global::Unity.VisualScripting.StaticFieldAccessor<>).MakeGenericType(fieldInfo.FieldType));
						value = (global::Unity.VisualScripting.IOptimizedAccessor)global::System.Activator.CreateInstance(type, fieldInfo);
					}
					else
					{
						value = new global::Unity.VisualScripting.ReflectionFieldAccessor(fieldInfo);
					}
					value.Compile();
					fieldAccessors.Add(fieldInfo, value);
				}
				return value;
			}
		}

		public static global::Unity.VisualScripting.IOptimizedAccessor Prewarm(this global::System.Reflection.PropertyInfo propertyInfo)
		{
			return GetPropertyAccessor(propertyInfo);
		}

		public static object GetValueOptimized(this global::System.Reflection.PropertyInfo propertyInfo, object target)
		{
			return GetPropertyAccessor(propertyInfo).GetValue(target);
		}

		public static void SetValueOptimized(this global::System.Reflection.PropertyInfo propertyInfo, object target, object value)
		{
			GetPropertyAccessor(propertyInfo).SetValue(target, value);
		}

		public static bool SupportsOptimization(this global::System.Reflection.PropertyInfo propertyInfo)
		{
			if (!SupportsOptimization((global::System.Reflection.MemberInfo)propertyInfo))
			{
				return false;
			}
			return true;
		}

		private static global::Unity.VisualScripting.IOptimizedAccessor GetPropertyAccessor(global::System.Reflection.PropertyInfo propertyInfo)
		{
			global::Unity.VisualScripting.Ensure.That("propertyInfo").IsNotNull(propertyInfo);
			lock (propertyAccessors)
			{
				if (!propertyAccessors.TryGetValue(propertyInfo, out var value))
				{
					if (propertyInfo.SupportsOptimization())
					{
						global::System.Type type = ((!propertyInfo.IsStatic()) ? typeof(global::Unity.VisualScripting.InstancePropertyAccessor<, >).MakeGenericType(propertyInfo.DeclaringType, propertyInfo.PropertyType) : typeof(global::Unity.VisualScripting.StaticPropertyAccessor<>).MakeGenericType(propertyInfo.PropertyType));
						value = (global::Unity.VisualScripting.IOptimizedAccessor)global::System.Activator.CreateInstance(type, propertyInfo);
					}
					else
					{
						value = new global::Unity.VisualScripting.ReflectionPropertyAccessor(propertyInfo);
					}
					value.Compile();
					propertyAccessors.Add(propertyInfo, value);
				}
				return value;
			}
		}

		public static global::Unity.VisualScripting.IOptimizedInvoker Prewarm(this global::System.Reflection.MethodInfo methodInfo)
		{
			return GetMethodInvoker(methodInfo);
		}

		public static object InvokeOptimized(this global::System.Reflection.MethodInfo methodInfo, object target, params object[] args)
		{
			return GetMethodInvoker(methodInfo).Invoke(target, args);
		}

		public static object InvokeOptimized(this global::System.Reflection.MethodInfo methodInfo, object target)
		{
			return GetMethodInvoker(methodInfo).Invoke(target);
		}

		public static object InvokeOptimized(this global::System.Reflection.MethodInfo methodInfo, object target, object arg0)
		{
			return GetMethodInvoker(methodInfo).Invoke(target, arg0);
		}

		public static object InvokeOptimized(this global::System.Reflection.MethodInfo methodInfo, object target, object arg0, object arg1)
		{
			return GetMethodInvoker(methodInfo).Invoke(target, arg0, arg1);
		}

		public static object InvokeOptimized(this global::System.Reflection.MethodInfo methodInfo, object target, object arg0, object arg1, object arg2)
		{
			return GetMethodInvoker(methodInfo).Invoke(target, arg0, arg1, arg2);
		}

		public static object InvokeOptimized(this global::System.Reflection.MethodInfo methodInfo, object target, object arg0, object arg1, object arg2, object arg3)
		{
			return GetMethodInvoker(methodInfo).Invoke(target, arg0, arg1, arg2, arg3);
		}

		public static object InvokeOptimized(this global::System.Reflection.MethodInfo methodInfo, object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			return GetMethodInvoker(methodInfo).Invoke(target, arg0, arg1, arg2, arg3, arg4);
		}

		public static bool SupportsOptimization(this global::System.Reflection.MethodInfo methodInfo)
		{
			if (!SupportsOptimization((global::System.Reflection.MemberInfo)methodInfo))
			{
				return false;
			}
			global::System.Reflection.ParameterInfo[] parameters = methodInfo.GetParameters();
			if (parameters.Length > 5)
			{
				return false;
			}
			if (global::System.Linq.Enumerable.Any(parameters, (global::System.Reflection.ParameterInfo parameter) => parameter.ParameterType.IsByRef))
			{
				return false;
			}
			if (!jitAvailable && methodInfo.IsVirtual && !methodInfo.IsFinal)
			{
				return false;
			}
			if (methodInfo.CallingConvention == global::System.Reflection.CallingConventions.VarArgs)
			{
				return false;
			}
			return true;
		}

		private static global::Unity.VisualScripting.IOptimizedInvoker GetMethodInvoker(global::System.Reflection.MethodInfo methodInfo)
		{
			global::Unity.VisualScripting.Ensure.That("methodInfo").IsNotNull(methodInfo);
			lock (methodInvokers)
			{
				if (!methodInvokers.TryGetValue(methodInfo, out var value))
				{
					if (methodInfo.SupportsOptimization())
					{
						global::System.Reflection.ParameterInfo[] parameters = methodInfo.GetParameters();
						global::System.Type type;
						if (methodInfo.ReturnType == typeof(void))
						{
							if (methodInfo.IsStatic)
							{
								if (parameters.Length == 0)
								{
									type = typeof(global::Unity.VisualScripting.StaticActionInvoker);
								}
								else if (parameters.Length == 1)
								{
									type = typeof(global::Unity.VisualScripting.StaticActionInvoker<>).MakeGenericType(parameters[0].ParameterType);
								}
								else if (parameters.Length == 2)
								{
									type = typeof(global::Unity.VisualScripting.StaticActionInvoker<, >).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType);
								}
								else if (parameters.Length == 3)
								{
									type = typeof(global::Unity.VisualScripting.StaticActionInvoker<, , >).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType);
								}
								else if (parameters.Length == 4)
								{
									type = typeof(global::Unity.VisualScripting.StaticActionInvoker<, , , >).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType);
								}
								else
								{
									if (parameters.Length != 5)
									{
										throw new global::System.NotSupportedException();
									}
									type = typeof(global::Unity.VisualScripting.StaticActionInvoker<, , , , >).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType);
								}
							}
							else if (parameters.Length == 0)
							{
								type = typeof(global::Unity.VisualScripting.InstanceActionInvoker<>).MakeGenericType(methodInfo.DeclaringType);
							}
							else if (parameters.Length == 1)
							{
								type = typeof(global::Unity.VisualScripting.InstanceActionInvoker<, >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType);
							}
							else if (parameters.Length == 2)
							{
								type = typeof(global::Unity.VisualScripting.InstanceActionInvoker<, , >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType);
							}
							else if (parameters.Length == 3)
							{
								type = typeof(global::Unity.VisualScripting.InstanceActionInvoker<, , , >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType);
							}
							else if (parameters.Length == 4)
							{
								type = typeof(global::Unity.VisualScripting.InstanceActionInvoker<, , , , >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType);
							}
							else
							{
								if (parameters.Length != 5)
								{
									throw new global::System.NotSupportedException();
								}
								type = typeof(global::Unity.VisualScripting.InstanceActionInvoker<, , , , , >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType);
							}
						}
						else if (methodInfo.IsStatic)
						{
							if (parameters.Length == 0)
							{
								type = typeof(global::Unity.VisualScripting.StaticFunctionInvoker<>).MakeGenericType(methodInfo.ReturnType);
							}
							else if (parameters.Length == 1)
							{
								type = typeof(global::Unity.VisualScripting.StaticFunctionInvoker<, >).MakeGenericType(parameters[0].ParameterType, methodInfo.ReturnType);
							}
							else if (parameters.Length == 2)
							{
								type = typeof(global::Unity.VisualScripting.StaticFunctionInvoker<, , >).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, methodInfo.ReturnType);
							}
							else if (parameters.Length == 3)
							{
								type = typeof(global::Unity.VisualScripting.StaticFunctionInvoker<, , , >).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, methodInfo.ReturnType);
							}
							else if (parameters.Length == 4)
							{
								type = typeof(global::Unity.VisualScripting.StaticFunctionInvoker<, , , , >).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, methodInfo.ReturnType);
							}
							else
							{
								if (parameters.Length != 5)
								{
									throw new global::System.NotSupportedException();
								}
								type = typeof(global::Unity.VisualScripting.StaticFunctionInvoker<, , , , , >).MakeGenericType(parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType, methodInfo.ReturnType);
							}
						}
						else if (parameters.Length == 0)
						{
							type = typeof(global::Unity.VisualScripting.InstanceFunctionInvoker<, >).MakeGenericType(methodInfo.DeclaringType, methodInfo.ReturnType);
						}
						else if (parameters.Length == 1)
						{
							type = typeof(global::Unity.VisualScripting.InstanceFunctionInvoker<, , >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType, methodInfo.ReturnType);
						}
						else if (parameters.Length == 2)
						{
							type = typeof(global::Unity.VisualScripting.InstanceFunctionInvoker<, , , >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType, methodInfo.ReturnType);
						}
						else if (parameters.Length == 3)
						{
							type = typeof(global::Unity.VisualScripting.InstanceFunctionInvoker<, , , , >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, methodInfo.ReturnType);
						}
						else if (parameters.Length == 4)
						{
							type = typeof(global::Unity.VisualScripting.InstanceFunctionInvoker<, , , , , >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, methodInfo.ReturnType);
						}
						else
						{
							if (parameters.Length != 5)
							{
								throw new global::System.NotSupportedException();
							}
							type = typeof(global::Unity.VisualScripting.InstanceFunctionInvoker<, , , , , , >).MakeGenericType(methodInfo.DeclaringType, parameters[0].ParameterType, parameters[1].ParameterType, parameters[2].ParameterType, parameters[3].ParameterType, parameters[4].ParameterType, methodInfo.ReturnType);
						}
						value = (global::Unity.VisualScripting.IOptimizedInvoker)global::System.Activator.CreateInstance(type, methodInfo);
					}
					else
					{
						value = new global::Unity.VisualScripting.ReflectionInvoker(methodInfo);
					}
					value.Compile();
					methodInvokers.Add(methodInfo, value);
				}
				return value;
			}
		}
	}
}
