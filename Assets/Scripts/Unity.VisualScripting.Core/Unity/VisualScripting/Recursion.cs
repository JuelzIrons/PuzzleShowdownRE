namespace Unity.VisualScripting
{
	public class Recursion<T> : global::Unity.VisualScripting.IPoolable, global::System.IDisposable
	{
		private readonly global::System.Collections.Generic.Stack<T> traversedOrder;

		private readonly global::System.Collections.Generic.Dictionary<T, int> traversedCount;

		private bool disposed;

		protected int maxDepth;

		protected Recursion()
		{
			traversedOrder = new global::System.Collections.Generic.Stack<T>();
			traversedCount = new global::System.Collections.Generic.Dictionary<T, int>();
		}

		public void Enter(T o)
		{
			if (!TryEnter(o))
			{
				throw new global::System.StackOverflowException(string.Format("Max recursion depth of {0} has been exceeded. Consider increasing '{1}.{2}'.", maxDepth, "Recursion", "defaultMaxDepth"));
			}
		}

		public bool TryEnter(T o)
		{
			if (disposed)
			{
				throw new global::System.ObjectDisposedException(ToString());
			}
			if (traversedCount.TryGetValue(o, out var value))
			{
				if (value < maxDepth)
				{
					traversedOrder.Push(o);
					traversedCount[o]++;
					return true;
				}
				return false;
			}
			traversedOrder.Push(o);
			traversedCount.Add(o, 1);
			return true;
		}

		public void Exit(T o)
		{
			if (traversedOrder.Count == 0)
			{
				throw new global::System.InvalidOperationException("Trying to exit an empty recursion stack.");
			}
			T val = traversedOrder.Peek();
			if (!global::System.Collections.Generic.EqualityComparer<T>.Default.Equals(o, val))
			{
				throw new global::System.InvalidOperationException($"Exiting recursion stack in a non-consecutive order:\nProvided: {o} / Expected: {val}");
			}
			traversedOrder.Pop();
			if (traversedCount[val]-- == 0)
			{
				traversedCount.Remove(val);
			}
		}

		public void Dispose()
		{
			if (disposed)
			{
				throw new global::System.ObjectDisposedException(ToString());
			}
			Free();
		}

		protected virtual void Free()
		{
			global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.Recursion<T>>.Free(this);
		}

		void global::Unity.VisualScripting.IPoolable.New()
		{
			disposed = false;
		}

		void global::Unity.VisualScripting.IPoolable.Free()
		{
			disposed = true;
			traversedCount.Clear();
			traversedOrder.Clear();
		}

		public static global::Unity.VisualScripting.Recursion<T> New()
		{
			return New(global::Unity.VisualScripting.Recursion.defaultMaxDepth);
		}

		public static global::Unity.VisualScripting.Recursion<T> New(int maxDepth)
		{
			if (!global::Unity.VisualScripting.Recursion.safeMode)
			{
				return null;
			}
			if (maxDepth < 1)
			{
				throw new global::System.ArgumentException("Max recursion depth must be at least one.", "maxDepth");
			}
			global::Unity.VisualScripting.Recursion<T> recursion = global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.Recursion<T>>.New(() => new global::Unity.VisualScripting.Recursion<T>());
			recursion.maxDepth = maxDepth;
			return recursion;
		}
	}
	public sealed class Recursion : global::Unity.VisualScripting.Recursion<object>
	{
		public static int defaultMaxDepth { get; set; } = 100;

		public static bool safeMode { get; set; }

		private Recursion()
		{
		}

		internal static void OnRuntimeMethodLoad()
		{
			safeMode = global::UnityEngine.Application.isEditor || global::UnityEngine.Debug.isDebugBuild;
		}

		protected override void Free()
		{
			global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.Recursion>.Free(this);
		}

		public new static global::Unity.VisualScripting.Recursion New()
		{
			return New(defaultMaxDepth);
		}

		public new static global::Unity.VisualScripting.Recursion New(int maxDepth)
		{
			if (!safeMode)
			{
				return null;
			}
			if (maxDepth < 1)
			{
				throw new global::System.ArgumentException("Max recursion depth must be at least one.", "maxDepth");
			}
			global::Unity.VisualScripting.Recursion recursion = global::Unity.VisualScripting.GenericPool<global::Unity.VisualScripting.Recursion>.New(() => new global::Unity.VisualScripting.Recursion());
			recursion.maxDepth = maxDepth;
			return recursion;
		}
	}
}
