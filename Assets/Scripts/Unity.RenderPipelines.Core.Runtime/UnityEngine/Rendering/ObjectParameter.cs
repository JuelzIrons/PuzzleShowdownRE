namespace UnityEngine.Rendering
{
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class ObjectParameter<T> : global::UnityEngine.Rendering.VolumeParameter<T>
	{
		internal global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeParameter> parameters { get; private set; }

		public sealed override bool overrideState
		{
			get
			{
				return true;
			}
			set
			{
				m_OverrideState = true;
			}
		}

		public sealed override T value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = value;
				if (m_Value == null)
				{
					parameters = null;
					return;
				}
				parameters = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.Where(m_Value.GetType().GetFields(global::System.Reflection.BindingFlags.Instance | global::System.Reflection.BindingFlags.Public), (global::System.Reflection.FieldInfo t) => t.FieldType.IsSubclassOf(typeof(global::UnityEngine.Rendering.VolumeParameter))), (global::System.Reflection.FieldInfo t) => t.MetadataToken), (global::System.Reflection.FieldInfo t) => (global::UnityEngine.Rendering.VolumeParameter)t.GetValue(m_Value))).AsReadOnly();
			}
		}

		public ObjectParameter(T value)
		{
			m_OverrideState = true;
			this.value = value;
		}

		internal override void Interp(global::UnityEngine.Rendering.VolumeParameter from, global::UnityEngine.Rendering.VolumeParameter to, float t)
		{
			if (m_Value == null)
			{
				return;
			}
			global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeParameter> readOnlyCollection = parameters;
			global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeParameter> readOnlyCollection2 = ((global::UnityEngine.Rendering.ObjectParameter<T>)from).parameters;
			global::System.Collections.ObjectModel.ReadOnlyCollection<global::UnityEngine.Rendering.VolumeParameter> readOnlyCollection3 = ((global::UnityEngine.Rendering.ObjectParameter<T>)to).parameters;
			for (int i = 0; i < readOnlyCollection2.Count; i++)
			{
				readOnlyCollection[i].overrideState = readOnlyCollection3[i].overrideState;
				if (readOnlyCollection3[i].overrideState)
				{
					readOnlyCollection[i].Interp(readOnlyCollection2[i], readOnlyCollection3[i], t);
				}
			}
		}
	}
}
