namespace Unity.VisualScripting
{
	public abstract class ReflectedCloner : global::Unity.VisualScripting.Cloner<object>
	{
		private readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Reflection.MemberInfo[]> accessors = new global::System.Collections.Generic.Dictionary<global::System.Type, global::System.Reflection.MemberInfo[]>();

		private readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IOptimizedAccessor[]> optimizedAccessors = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IOptimizedAccessor[]>();

		public override bool Handles(global::System.Type type)
		{
			return false;
		}

		public override void FillClone(global::System.Type type, ref object clone, object original, global::Unity.VisualScripting.CloningContext context)
		{
			if (global::Unity.VisualScripting.PlatformUtility.supportsJit)
			{
				global::Unity.VisualScripting.IOptimizedAccessor[] array = GetOptimizedAccessors(type);
				foreach (global::Unity.VisualScripting.IOptimizedAccessor optimizedAccessor in array)
				{
					if (context.tryPreserveInstances)
					{
						object clone2 = optimizedAccessor.GetValue(clone);
						global::Unity.VisualScripting.Cloning.CloneInto(context, ref clone2, optimizedAccessor.GetValue(original));
						optimizedAccessor.SetValue(clone, clone2);
					}
					else
					{
						optimizedAccessor.SetValue(clone, global::Unity.VisualScripting.Cloning.Clone(context, optimizedAccessor.GetValue(original)));
					}
				}
				return;
			}
			global::System.Reflection.MemberInfo[] array2 = GetAccessors(type);
			foreach (global::System.Reflection.MemberInfo memberInfo in array2)
			{
				if (memberInfo is global::System.Reflection.FieldInfo)
				{
					global::System.Reflection.FieldInfo fieldInfo = (global::System.Reflection.FieldInfo)memberInfo;
					if (context.tryPreserveInstances)
					{
						object clone3 = fieldInfo.GetValue(clone);
						global::Unity.VisualScripting.Cloning.CloneInto(context, ref clone3, fieldInfo.GetValue(original));
						fieldInfo.SetValue(clone, clone3);
					}
					else
					{
						fieldInfo.SetValue(clone, global::Unity.VisualScripting.Cloning.Clone(context, fieldInfo.GetValue(original)));
					}
				}
				else if (memberInfo is global::System.Reflection.PropertyInfo)
				{
					global::System.Reflection.PropertyInfo propertyInfo = (global::System.Reflection.PropertyInfo)memberInfo;
					if (context.tryPreserveInstances)
					{
						object clone4 = propertyInfo.GetValue(clone, null);
						global::Unity.VisualScripting.Cloning.CloneInto(context, ref clone4, propertyInfo.GetValue(original, null));
						propertyInfo.SetValue(clone, clone4, null);
					}
					else
					{
						propertyInfo.SetValue(clone, global::Unity.VisualScripting.Cloning.Clone(context, propertyInfo.GetValue(original, null)), null);
					}
				}
			}
		}

		private global::System.Reflection.MemberInfo[] GetAccessors(global::System.Type type)
		{
			if (!accessors.ContainsKey(type))
			{
				accessors.Add(type, global::System.Linq.Enumerable.ToArray(GetMembers(type)));
			}
			return accessors[type];
		}

		private global::Unity.VisualScripting.IOptimizedAccessor[] GetOptimizedAccessors(global::System.Type type)
		{
			if (!optimizedAccessors.ContainsKey(type))
			{
				global::System.Collections.Generic.List<global::Unity.VisualScripting.IOptimizedAccessor> list = new global::System.Collections.Generic.List<global::Unity.VisualScripting.IOptimizedAccessor>();
				foreach (global::System.Reflection.MemberInfo member in GetMembers(type))
				{
					if (member is global::System.Reflection.FieldInfo)
					{
						list.Add(((global::System.Reflection.FieldInfo)member).Prewarm());
					}
					else if (member is global::System.Reflection.PropertyInfo)
					{
						list.Add(((global::System.Reflection.PropertyInfo)member).Prewarm());
					}
				}
				optimizedAccessors.Add(type, list.ToArray());
			}
			return optimizedAccessors[type];
		}

		protected virtual global::System.Collections.Generic.IEnumerable<global::System.Reflection.MemberInfo> GetMembers(global::System.Type type)
		{
			global::System.Reflection.BindingFlags bindingAttr = global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public | global::System.Reflection.BindingFlags.NonPublic;
			return global::Unity.VisualScripting.LinqUtility.Concat<global::System.Reflection.MemberInfo>(new global::System.Collections.IEnumerable[2]
			{
				global::System.Linq.Enumerable.Where(type.GetFields(bindingAttr), IncludeField),
				global::System.Linq.Enumerable.Where(type.GetProperties(bindingAttr), IncludeProperty)
			});
		}

		protected virtual bool IncludeField(global::System.Reflection.FieldInfo field)
		{
			return false;
		}

		protected virtual bool IncludeProperty(global::System.Reflection.PropertyInfo property)
		{
			return false;
		}
	}
}
