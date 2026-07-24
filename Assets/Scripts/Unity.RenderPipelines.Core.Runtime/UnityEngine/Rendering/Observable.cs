namespace UnityEngine.Rendering
{
	public struct Observable<T>
	{
		private T m_Value;

		public T value
		{
			get
			{
				return m_Value;
			}
			set
			{
				if (!global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(value, m_Value))
				{
					m_Value = value;
					this.onValueChanged?.Invoke(value);
				}
			}
		}

		public event global::System.Action<T> onValueChanged;

		public Observable(T newValue)
		{
			m_Value = newValue;
			this.onValueChanged = null;
		}
	}
}
