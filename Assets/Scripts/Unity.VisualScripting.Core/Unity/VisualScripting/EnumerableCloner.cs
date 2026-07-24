namespace Unity.VisualScripting
{
	public sealed class EnumerableCloner : global::Unity.VisualScripting.Cloner<global::System.Collections.IEnumerable>
	{
		private readonly global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IOptimizedInvoker> addMethods = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Unity.VisualScripting.IOptimizedInvoker>();

		public override bool Handles(global::System.Type type)
		{
			if (typeof(global::System.Collections.IEnumerable).IsAssignableFrom(type) && !typeof(global::System.Collections.IList).IsAssignableFrom(type))
			{
				return GetAddMethod(type) != null;
			}
			return false;
		}

		public override void FillClone(global::System.Type type, ref global::System.Collections.IEnumerable clone, global::System.Collections.IEnumerable original, global::Unity.VisualScripting.CloningContext context)
		{
			global::Unity.VisualScripting.IOptimizedInvoker addMethod = GetAddMethod(type);
			if (addMethod == null)
			{
				throw new global::System.InvalidOperationException($"Cannot instantiate enumerable type '{type}' because it does not provide an add method.");
			}
			foreach (object item in original)
			{
				addMethod.Invoke(item, global::Unity.VisualScripting.Cloning.Clone(context, item));
			}
		}

		private global::Unity.VisualScripting.IOptimizedInvoker GetAddMethod(global::System.Type type)
		{
			if (!addMethods.ContainsKey(type))
			{
				global::System.Reflection.MethodInfo obj = global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetDeclaredMethod(global::Unity.VisualScripting.FullSerializer.fsReflectionUtility.GetInterface(type, typeof(global::System.Collections.Generic.ICollection<>))?, "Add") ?? global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetFlattenedMethod(type, "Add") ?? global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetFlattenedMethod(type, "Push") ?? global::Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.GetFlattenedMethod(type, "Enqueue");
				addMethods.Add(type, obj?.Prewarm());
			}
			return addMethods[type];
		}
	}
}
