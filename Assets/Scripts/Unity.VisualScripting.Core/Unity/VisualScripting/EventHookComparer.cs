namespace Unity.VisualScripting
{
	public class EventHookComparer : global::System.Collections.Generic.IEqualityComparer<global::Unity.VisualScripting.EventHook>
	{
		public bool Equals(global::Unity.VisualScripting.EventHook x, global::Unity.VisualScripting.EventHook y)
		{
			return x.Equals(y);
		}

		public int GetHashCode(global::Unity.VisualScripting.EventHook obj)
		{
			return obj.GetHashCode();
		}
	}
}
