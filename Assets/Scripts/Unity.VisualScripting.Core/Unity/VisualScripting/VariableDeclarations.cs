namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	public sealed class VariableDeclarations : global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.VariableDeclaration>, global::System.Collections.IEnumerable, global::Unity.VisualScripting.ISpecifiesCloner
	{
		public global::Unity.VisualScripting.VariableKind Kind;

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectorWide(true)]
		private global::Unity.VisualScripting.VariableDeclarationCollection collection;

		internal global::System.Action OnVariableChanged;

		public object this[[global::Unity.VisualScripting.InspectorVariableName(global::Unity.VisualScripting.ActionDirection.Any)] string variable]
		{
			get
			{
				return Get(variable);
			}
			set
			{
				Set(variable, value);
			}
		}

		global::Unity.VisualScripting.ICloner global::Unity.VisualScripting.ISpecifiesCloner.cloner => global::Unity.VisualScripting.VariableDeclarationsCloner.instance;

		public VariableDeclarations()
		{
			collection = new global::Unity.VisualScripting.VariableDeclarationCollection();
		}

		public void Set([global::Unity.VisualScripting.InspectorVariableName(global::Unity.VisualScripting.ActionDirection.Set)] string variable, object value)
		{
			if (string.IsNullOrEmpty(variable))
			{
				return;
			}
			if (collection.TryGetValue(variable, out var value2))
			{
				if (value2.value != value)
				{
					value2.value = value;
					OnVariableChanged?.Invoke();
				}
			}
			else
			{
				collection.Add(new global::Unity.VisualScripting.VariableDeclaration(variable, value));
				OnVariableChanged?.Invoke();
			}
		}

		public object Get([global::Unity.VisualScripting.InspectorVariableName(global::Unity.VisualScripting.ActionDirection.Get)] string variable)
		{
			if (string.IsNullOrEmpty(variable))
			{
				throw new global::System.ArgumentException("No variable name specified.", "variable");
			}
			if (collection.TryGetValue(variable, out var value))
			{
				return value.value;
			}
			throw new global::System.InvalidOperationException("Variable not found: '" + variable + "'.");
		}

		public T Get<T>([global::Unity.VisualScripting.InspectorVariableName(global::Unity.VisualScripting.ActionDirection.Get)] string variable)
		{
			return (T)Get(variable, typeof(T));
		}

		public object Get([global::Unity.VisualScripting.InspectorVariableName(global::Unity.VisualScripting.ActionDirection.Get)] string variable, global::System.Type expectedType)
		{
			return global::Unity.VisualScripting.ConversionUtility.Convert(Get(variable), expectedType);
		}

		public void Clear()
		{
			collection.Clear();
		}

		public bool IsDefined([global::Unity.VisualScripting.InspectorVariableName(global::Unity.VisualScripting.ActionDirection.Any)] string variable)
		{
			if (string.IsNullOrEmpty(variable))
			{
				throw new global::System.ArgumentException("No variable name specified.", "variable");
			}
			return collection.Contains(variable);
		}

		public global::Unity.VisualScripting.VariableDeclaration GetDeclaration(string variable)
		{
			if (collection.TryGetValue(variable, out var value))
			{
				return value;
			}
			throw new global::System.InvalidOperationException("Variable not found: '" + variable + "'.");
		}

		public global::System.Collections.Generic.IEnumerator<global::Unity.VisualScripting.VariableDeclaration> GetEnumerator()
		{
			return collection.GetEnumerator();
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return ((global::System.Collections.IEnumerable)collection).GetEnumerator();
		}
	}
}
