namespace UnityEngine.Experimental.Rendering
{
	internal class XRLayoutStack : global::System.IDisposable
	{
		private readonly global::System.Collections.Generic.Stack<global::UnityEngine.Experimental.Rendering.XRLayout> m_Stack = new global::System.Collections.Generic.Stack<global::UnityEngine.Experimental.Rendering.XRLayout>();

		public global::UnityEngine.Experimental.Rendering.XRLayout top => m_Stack.Peek();

		public global::UnityEngine.Experimental.Rendering.XRLayout New()
		{
			global::UnityEngine.Pool.GenericPool<global::UnityEngine.Experimental.Rendering.XRLayout>.Get(out var value);
			m_Stack.Push(value);
			return value;
		}

		public void Release()
		{
			if (!m_Stack.TryPop(out var result))
			{
				throw new global::System.InvalidOperationException("Calling Release without calling New first.");
			}
			result.Clear();
			global::UnityEngine.Pool.GenericPool<global::UnityEngine.Experimental.Rendering.XRLayout>.Release(result);
		}

		public void Dispose()
		{
			if (m_Stack.Count != 0)
			{
				throw new global::System.Exception("Stack is not empty. Did you skip a call to Release?");
			}
		}
	}
}
