namespace UnityEngine.Timeline
{
	[global::System.Serializable]
	internal struct ObjectId : global::System.IEquatable<global::UnityEngine.Timeline.ObjectId>, global::System.IComparable<global::UnityEngine.Timeline.ObjectId>
	{
		public static readonly global::UnityEngine.Timeline.ObjectId InvalidId = new global::UnityEngine.Timeline.ObjectId(-1);

		public static readonly global::UnityEngine.Timeline.ObjectId DefaultId = new global::UnityEngine.Timeline.ObjectId(0);

		[global::UnityEngine.SerializeField]
		private int m_Data;

		internal ObjectId(int data)
		{
			m_Data = data;
		}

		public static implicit operator global::UnityEngine.Timeline.ObjectId(global::UnityEngine.EntityId entityId)
		{
			return new global::UnityEngine.Timeline.ObjectId
			{
				m_Data = entityId
			};
		}

		public static implicit operator global::UnityEngine.EntityId(global::UnityEngine.Timeline.ObjectId objectId)
		{
			return objectId.m_Data;
		}

		public override bool Equals(object obj)
		{
			if (obj is global::UnityEngine.Timeline.ObjectId other)
			{
				return Equals(other);
			}
			return false;
		}

		public bool Equals(global::UnityEngine.Timeline.ObjectId other)
		{
			return m_Data == other.m_Data;
		}

		public int CompareTo(global::UnityEngine.Timeline.ObjectId other)
		{
			return m_Data.CompareTo(other.m_Data);
		}

		public static bool operator ==(global::UnityEngine.Timeline.ObjectId left, global::UnityEngine.Timeline.ObjectId right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(global::UnityEngine.Timeline.ObjectId left, global::UnityEngine.Timeline.ObjectId right)
		{
			return !left.Equals(right);
		}

		public static bool operator <(global::UnityEngine.Timeline.ObjectId left, global::UnityEngine.Timeline.ObjectId right)
		{
			return left.m_Data < right.m_Data;
		}

		public static bool operator >(global::UnityEngine.Timeline.ObjectId left, global::UnityEngine.Timeline.ObjectId right)
		{
			return left.m_Data > right.m_Data;
		}

		public static bool operator <=(global::UnityEngine.Timeline.ObjectId left, global::UnityEngine.Timeline.ObjectId right)
		{
			return left.m_Data <= right.m_Data;
		}

		public static bool operator >=(global::UnityEngine.Timeline.ObjectId left, global::UnityEngine.Timeline.ObjectId right)
		{
			return left.m_Data >= right.m_Data;
		}

		public override int GetHashCode()
		{
			return m_Data;
		}
	}
}
