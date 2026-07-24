namespace Unity.VisualScripting
{
	public sealed class AotDictionary : global::System.Collections.Specialized.OrderedDictionary
	{
		public AotDictionary()
		{
		}

		public AotDictionary(global::System.Collections.IEqualityComparer comparer)
			: base(comparer)
		{
		}

		public AotDictionary(int capacity)
			: base(capacity)
		{
		}

		public AotDictionary(int capacity, global::System.Collections.IEqualityComparer comparer)
			: base(capacity, comparer)
		{
		}

		[global::UnityEngine.Scripting.Preserve]
		public static void AotStubs()
		{
			global::Unity.VisualScripting.AotDictionary aotDictionary = new global::Unity.VisualScripting.AotDictionary();
			aotDictionary.Add(null, null);
			aotDictionary.Remove(null);
			_ = aotDictionary[null];
			aotDictionary[null] = null;
			aotDictionary.Contains(null);
			aotDictionary.Clear();
			_ = aotDictionary.Count;
		}
	}
}
