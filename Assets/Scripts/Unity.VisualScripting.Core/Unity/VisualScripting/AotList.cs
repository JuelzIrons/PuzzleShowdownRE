namespace Unity.VisualScripting
{
	public sealed class AotList : global::System.Collections.ArrayList
	{
		public AotList()
		{
		}

		public AotList(int capacity)
			: base(capacity)
		{
		}

		public AotList(global::System.Collections.ICollection c)
			: base(c)
		{
		}

		[global::UnityEngine.Scripting.Preserve]
		public static void AotStubs()
		{
			global::Unity.VisualScripting.AotList aotList = new global::Unity.VisualScripting.AotList();
			aotList.Add(null);
			aotList.Remove(null);
			_ = aotList[0];
			aotList[0] = null;
			aotList.Contains(null);
			aotList.Clear();
			_ = aotList.Count;
		}
	}
}
