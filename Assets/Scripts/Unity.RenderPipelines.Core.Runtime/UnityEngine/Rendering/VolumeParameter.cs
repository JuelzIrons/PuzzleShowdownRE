namespace UnityEngine.Rendering
{
	public abstract class VolumeParameter : global::System.ICloneable
	{
		public const string k_DebuggerDisplay = "{m_Value} ({m_OverrideState})";

		[global::UnityEngine.SerializeField]
		protected bool m_OverrideState;

		public virtual bool overrideState
		{
			get
			{
				return m_OverrideState;
			}
			set
			{
				m_OverrideState = value;
			}
		}

		internal abstract void Interp(global::UnityEngine.Rendering.VolumeParameter from, global::UnityEngine.Rendering.VolumeParameter to, float t);

		public T GetValue<T>()
		{
			return ((global::UnityEngine.Rendering.VolumeParameter<T>)this).value;
		}

		public abstract void SetValue(global::UnityEngine.Rendering.VolumeParameter parameter);

		protected internal virtual void OnEnable()
		{
		}

		protected internal virtual void OnDisable()
		{
		}

		public static bool IsObjectParameter(global::System.Type type)
		{
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(global::UnityEngine.Rendering.ObjectParameter<>))
			{
				return true;
			}
			if (type.BaseType != null)
			{
				return IsObjectParameter(type.BaseType);
			}
			return false;
		}

		public virtual void Release()
		{
		}

		public abstract object Clone();
	}
	[global::System.Serializable]
	[global::System.Diagnostics.DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	public class VolumeParameter<T> : global::UnityEngine.Rendering.VolumeParameter, global::System.IEquatable<global::UnityEngine.Rendering.VolumeParameter<T>>
	{
		[global::UnityEngine.SerializeField]
		protected T m_Value;

		public virtual T value
		{
			get
			{
				return m_Value;
			}
			set
			{
				m_Value = value;
			}
		}

		public VolumeParameter()
			: this(default(T), false)
		{
		}

		protected VolumeParameter(T value, bool overrideState = false)
		{
			m_Value = value;
			this.overrideState = overrideState;
		}

		internal override void Interp(global::UnityEngine.Rendering.VolumeParameter from, global::UnityEngine.Rendering.VolumeParameter to, float t)
		{
			Interp((from as global::UnityEngine.Rendering.VolumeParameter<T>).value, (to as global::UnityEngine.Rendering.VolumeParameter<T>).value, t);
		}

		public virtual void Interp(T from, T to, float t)
		{
			m_Value = ((t > 0f) ? to : from);
		}

		public void Override(T x)
		{
			overrideState = true;
			m_Value = x;
		}

		[global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public override void SetValue(global::UnityEngine.Rendering.VolumeParameter parameter)
		{
			m_Value = ((global::UnityEngine.Rendering.VolumeParameter<T>)parameter).m_Value;
		}

		public override int GetHashCode()
		{
			int num = 17;
			num = num * 23 + overrideState.GetHashCode();
			if (!global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(value, default(T)))
			{
				num = num * 23 + value.GetHashCode();
			}
			return num;
		}

		public override string ToString()
		{
			return $"{value} ({overrideState})";
		}

		public static bool operator ==(global::UnityEngine.Rendering.VolumeParameter<T> lhs, T rhs)
		{
			if (lhs != null && lhs.value != null)
			{
				return lhs.value.Equals(rhs);
			}
			return false;
		}

		public static bool operator !=(global::UnityEngine.Rendering.VolumeParameter<T> lhs, T rhs)
		{
			return !(lhs == rhs);
		}

		public bool Equals(global::UnityEngine.Rendering.VolumeParameter<T> other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			return global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(m_Value, other.m_Value);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as global::UnityEngine.Rendering.VolumeParameter<T>);
		}

		public override object Clone()
		{
			return new global::UnityEngine.Rendering.VolumeParameter<T>(GetValue<T>(), overrideState);
		}

		public static explicit operator T(global::UnityEngine.Rendering.VolumeParameter<T> prop)
		{
			return prop.m_Value;
		}
	}
}
