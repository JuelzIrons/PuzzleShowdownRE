namespace Unity.Multiplayer.Tools.Common
{
	[global::System.Serializable]
	internal class SerializedDictionary<K, V> : global::System.Collections.Generic.SortedDictionary<K, V>, global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<K> m_Keys = new global::System.Collections.Generic.List<K>();

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<V> m_Values = new global::System.Collections.Generic.List<V>();

		public SerializedDictionary()
		{
		}

		public SerializedDictionary(global::System.Collections.Generic.Comparer<K> comparer)
			: base((global::System.Collections.Generic.IComparer<K>)comparer)
		{
		}

		public void OnBeforeSerialize()
		{
			m_Keys.Clear();
			m_Values.Clear();
			using global::System.Collections.Generic.SortedDictionary<K, V>.Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				global::System.Collections.Generic.KeyValuePair<K, V> current = enumerator.Current;
				m_Keys.Add(current.Key);
				m_Values.Add(current.Value);
			}
		}

		public void OnAfterDeserialize()
		{
			Clear();
			for (int i = 0; i < m_Keys.Count; i++)
			{
				base[m_Keys[i]] = m_Values[i];
			}
			m_Keys.Clear();
			m_Values.Clear();
		}
	}
}
