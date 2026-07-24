namespace UnityEngine.Rendering
{
	public static class DelegateUtility
	{
		public static global::System.Delegate Cast(global::System.Delegate source, global::System.Type type)
		{
			if ((object)source == null)
			{
				return null;
			}
			global::System.Delegate[] invocationList = source.GetInvocationList();
			if (invocationList.Length == 1)
			{
				return global::System.Delegate.CreateDelegate(type, invocationList[0].Target, invocationList[0].Method);
			}
			global::System.Delegate[] array = new global::System.Delegate[invocationList.Length];
			for (int i = 0; i < invocationList.Length; i++)
			{
				array[i] = global::System.Delegate.CreateDelegate(type, invocationList[i].Target, invocationList[i].Method);
			}
			return global::System.Delegate.Combine(array);
		}
	}
}
