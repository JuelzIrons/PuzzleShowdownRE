namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("Count = {Count}")]
	[global::System.Diagnostics.DebuggerTypeProxy(typeof(global::UnityEngine.Rendering.SerializedDictionaryDebugView<, >))]
	public class SerializedDictionary<K, V> : global::UnityEngine.Rendering.SerializedDictionary<K, V, K, V>
	{
		public override K SerializeKey(K key)
		{
			return key;
		}

		public override V SerializeValue(V val)
		{
			return val;
		}

		public override K DeserializeKey(K key)
		{
			return key;
		}

		public override V DeserializeValue(V val)
		{
			return val;
		}
	}
	[global::System.Serializable]
	public abstract class SerializedDictionary<K, V, SK, SV> : global::System.Collections.Generic.Dictionary<K, V>, global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<SK> m_Keys = new global::System.Collections.Generic.List<SK>();

		[global::UnityEngine.SerializeField]
		private global::System.Collections.Generic.List<SV> m_Values = new global::System.Collections.Generic.List<SV>();

		public abstract SK SerializeKey(K key);

		public abstract SV SerializeValue(V value);

		public abstract K DeserializeKey(SK serializedKey);

		public abstract V DeserializeValue(SV serializedValue);

		public void OnBeforeSerialize()
		{
			m_Keys.Clear();
			m_Values.Clear();
			using global::System.Collections.Generic.Dictionary<K, V>.Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				global::System.Collections.Generic.KeyValuePair<K, V> current = enumerator.Current;
				m_Keys.Add(SerializeKey(current.Key));
				m_Values.Add(SerializeValue(current.Value));
			}
		}

		public void OnAfterDeserialize()
		{
			Clear();
			for (int i = 0; i < m_Keys.Count; i++)
			{
				Add(DeserializeKey(m_Keys[i]), DeserializeValue(m_Values[i]));
			}
		}
	}
}
