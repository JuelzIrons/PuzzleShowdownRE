namespace Unity.VisualScripting
{
	public static class Cloning
	{
		private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, bool> skippable;

		public static global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.ICloner> cloners { get; }

		public static global::Unity.VisualScripting.ArrayCloner arrayCloner { get; }

		public static global::Unity.VisualScripting.DictionaryCloner dictionaryCloner { get; }

		public static global::Unity.VisualScripting.EnumerableCloner enumerableCloner { get; }

		public static global::Unity.VisualScripting.ListCloner listCloner { get; }

		public static global::Unity.VisualScripting.AnimationCurveCloner animationCurveCloner { get; }

		internal static global::Unity.VisualScripting.GradientCloner gradientCloner { get; }

		public static global::Unity.VisualScripting.FieldsCloner fieldsCloner { get; }

		public static global::Unity.VisualScripting.FakeSerializationCloner fakeSerializationCloner { get; }

		static Cloning()
		{
			skippable = new global::System.Collections.Generic.Dictionary<global::System.Type, bool>();
			cloners = new global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.ICloner>();
			arrayCloner = new global::Unity.VisualScripting.ArrayCloner();
			dictionaryCloner = new global::Unity.VisualScripting.DictionaryCloner();
			enumerableCloner = new global::Unity.VisualScripting.EnumerableCloner();
			listCloner = new global::Unity.VisualScripting.ListCloner();
			animationCurveCloner = new global::Unity.VisualScripting.AnimationCurveCloner();
			gradientCloner = new global::Unity.VisualScripting.GradientCloner();
			fieldsCloner = new global::Unity.VisualScripting.FieldsCloner();
			fakeSerializationCloner = new global::Unity.VisualScripting.FakeSerializationCloner();
			cloners.Add(arrayCloner);
			cloners.Add(dictionaryCloner);
			cloners.Add(enumerableCloner);
			cloners.Add(listCloner);
			cloners.Add(animationCurveCloner);
			cloners.Add(gradientCloner);
		}

		public static object Clone(this object original, global::Unity.VisualScripting.ICloner fallbackCloner, bool tryPreserveInstances)
		{
			using global::Unity.VisualScripting.CloningContext context = global::Unity.VisualScripting.CloningContext.New(fallbackCloner, tryPreserveInstances);
			return Clone(context, original);
		}

		public static T Clone<T>(this T original, global::Unity.VisualScripting.ICloner fallbackCloner, bool tryPreserveInstances)
		{
			return (T)((object)original).Clone(fallbackCloner, tryPreserveInstances);
		}

		public static object CloneViaFakeSerialization(this object original)
		{
			return original.Clone(fakeSerializationCloner, tryPreserveInstances: true);
		}

		public static T CloneViaFakeSerialization<T>(this T original)
		{
			return (T)((object)original).CloneViaFakeSerialization();
		}

		internal static object Clone(global::Unity.VisualScripting.CloningContext context, object original)
		{
			object clone = null;
			CloneInto(context, ref clone, original);
			return clone;
		}

		internal static void CloneInto(global::Unity.VisualScripting.CloningContext context, ref object clone, object original)
		{
			if (original == null)
			{
				clone = null;
				return;
			}
			global::System.Type type = original.GetType();
			if (Skippable(type))
			{
				clone = original;
				return;
			}
			if (context.clonings.ContainsKey(original))
			{
				clone = context.clonings[original];
				return;
			}
			global::Unity.VisualScripting.ICloner cloner = GetCloner(original, type, context.fallbackCloner);
			if (clone == null)
			{
				clone = cloner.ConstructClone(type, original);
			}
			context.clonings.Add(original, clone);
			cloner.BeforeClone(type, original);
			cloner.FillClone(type, ref clone, original, context);
			cloner.AfterClone(type, clone);
			context.clonings[original] = clone;
		}

		[global::JetBrains.Annotations.CanBeNull]
		public static global::Unity.VisualScripting.ICloner GetCloner(object original, global::System.Type type)
		{
			if (original is global::Unity.VisualScripting.ISpecifiesCloner specifiesCloner)
			{
				return specifiesCloner.cloner;
			}
			return global::System.Linq.Enumerable.FirstOrDefault(cloners, (global::Unity.VisualScripting.ICloner cloner) => cloner.Handles(type));
		}

		private static global::Unity.VisualScripting.ICloner GetCloner(object original, global::System.Type type, global::Unity.VisualScripting.ICloner fallbackCloner)
		{
			global::Unity.VisualScripting.ICloner cloner = GetCloner(original, type);
			if (cloner != null)
			{
				return cloner;
			}
			global::Unity.VisualScripting.Ensure.That("fallbackCloner").IsNotNull(fallbackCloner);
			return fallbackCloner;
		}

		private static bool Skippable(global::System.Type type)
		{
			if (!skippable.TryGetValue(type, out var value))
			{
				value = type.IsValueType || type == typeof(string) || typeof(global::System.Type).IsAssignableFrom(type) || typeof(global::UnityEngine.Object).IsAssignableFrom(type);
				skippable.Add(type, value);
			}
			return value;
		}
	}
}
