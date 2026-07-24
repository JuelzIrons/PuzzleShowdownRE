namespace Unity.VisualScripting
{
	public struct EventHook
	{
		public readonly string name;

		public readonly object target;

		public readonly object tag;

		public EventHook(string name, object target = null, object tag = null)
		{
			global::Unity.VisualScripting.Ensure.That("name").IsNotNull(name);
			this.name = name;
			this.target = target;
			this.tag = tag;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is global::Unity.VisualScripting.EventHook other))
			{
				return false;
			}
			return Equals(other);
		}

		public bool Equals(global::Unity.VisualScripting.EventHook other)
		{
			if (name == other.name && object.Equals(target, other.target))
			{
				return object.Equals(tag, other.tag);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return global::Unity.VisualScripting.HashUtility.GetHashCode(name, target, tag);
		}

		public static bool operator ==(global::Unity.VisualScripting.EventHook a, global::Unity.VisualScripting.EventHook b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(global::Unity.VisualScripting.EventHook a, global::Unity.VisualScripting.EventHook b)
		{
			return !(a == b);
		}

		public static implicit operator global::Unity.VisualScripting.EventHook(string name)
		{
			return new global::Unity.VisualScripting.EventHook(name);
		}
	}
}
