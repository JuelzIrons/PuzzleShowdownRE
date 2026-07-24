namespace Unity.VisualScripting
{
	public class OverrideStack<T>
	{
		private readonly global::System.Func<T> getValue;

		private readonly global::System.Action<T> setValue;

		private readonly global::System.Action clearValue;

		private T _value;

		private readonly global::System.Collections.Generic.Stack<T> previous = new global::System.Collections.Generic.Stack<T>();

		public T value
		{
			get
			{
				return getValue();
			}
			internal set
			{
				setValue(value);
			}
		}

		public OverrideStack(T defaultValue)
		{
			_value = defaultValue;
			getValue = () => _value;
			setValue = delegate(T value)
			{
				_value = value;
			};
		}

		public OverrideStack(global::System.Func<T> getValue, global::System.Action<T> setValue)
		{
			global::Unity.VisualScripting.Ensure.That("getValue").IsNotNull(getValue);
			global::Unity.VisualScripting.Ensure.That("setValue").IsNotNull(setValue);
			this.getValue = getValue;
			this.setValue = setValue;
		}

		public OverrideStack(global::System.Func<T> getValue, global::System.Action<T> setValue, global::System.Action clearValue)
			: this(getValue, setValue)
		{
			global::Unity.VisualScripting.Ensure.That("clearValue").IsNotNull(clearValue);
			this.clearValue = clearValue;
		}

		public global::Unity.VisualScripting.OverrideLayer<T> Override(T item)
		{
			return new global::Unity.VisualScripting.OverrideLayer<T>(this, item);
		}

		public void BeginOverride(T item)
		{
			previous.Push(value);
			value = item;
		}

		public void EndOverride()
		{
			if (previous.Count == 0)
			{
				throw new global::System.InvalidOperationException();
			}
			value = previous.Pop();
			if (previous.Count == 0)
			{
				clearValue?.Invoke();
			}
		}

		public static implicit operator T(global::Unity.VisualScripting.OverrideStack<T> stack)
		{
			global::Unity.VisualScripting.Ensure.That("stack").IsNotNull(stack);
			return stack.value;
		}
	}
}
