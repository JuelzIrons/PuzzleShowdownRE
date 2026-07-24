namespace TMPro
{
	public static class ObjectUtilsBridge
	{
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		public static void MarkDirty(this global::UnityEngine.Object obj)
		{
			obj.MarkDirty();
		}
	}
}
