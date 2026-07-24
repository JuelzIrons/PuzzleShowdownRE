namespace Unity.Multiplayer.Tools.Common
{
	internal class EnumMap<TEnum, TValue> : global::System.Collections.Generic.IEnumerable<global::System.Collections.Generic.KeyValuePair<TEnum, TValue>>, global::System.Collections.IEnumerable where TEnum : unmanaged, global::System.Enum
	{
		private static readonly int s_Count;

		private readonly TValue[] m_Values;

		public TValue this[TEnum key]
		{
			get
			{
				return m_Values[key.UnsafeCastToInt()];
			}
			set
			{
				m_Values[key.UnsafeCastToInt()] = value;
			}
		}

		public int Count => s_Count;

		public TValue[] Values => m_Values;

		static EnumMap()
		{
			s_Count = global::Unity.Multiplayer.Tools.Common.EnumContinuity.ValidateEnumForEnumMap<TEnum, TValue>();
		}

		public EnumMap()
		{
			m_Values = new TValue[s_Count];
		}

		public EnumMap(TValue value)
			: this()
		{
			global::System.Array.Fill(m_Values, value);
		}

		public EnumMap(TValue[] values)
		{
			m_Values = values;
		}

		public void Add(TEnum key, TValue value)
		{
			m_Values[key.UnsafeCastToInt()] = value;
		}

		public global::System.Collections.Generic.IEnumerator<global::System.Collections.Generic.KeyValuePair<TEnum, TValue>> GetEnumerator()
		{
			int i = 0;
			while (i < s_Count)
			{
				yield return new global::System.Collections.Generic.KeyValuePair<TEnum, TValue>(i.UnsafeCastToEnum<TEnum>(), m_Values[i]);
				int num = i + 1;
				i = num;
			}
		}

		global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
