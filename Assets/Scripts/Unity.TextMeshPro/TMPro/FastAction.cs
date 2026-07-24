namespace TMPro
{
	public class FastAction
	{
		private global::System.Collections.Generic.LinkedList<global::System.Action> delegates = new global::System.Collections.Generic.LinkedList<global::System.Action>();

		private global::System.Collections.Generic.Dictionary<global::System.Action, global::System.Collections.Generic.LinkedListNode<global::System.Action>> lookup = new global::System.Collections.Generic.Dictionary<global::System.Action, global::System.Collections.Generic.LinkedListNode<global::System.Action>>();

		public void Add(global::System.Action rhs)
		{
			if (!lookup.ContainsKey(rhs))
			{
				lookup[rhs] = delegates.AddLast(rhs);
			}
		}

		public void Remove(global::System.Action rhs)
		{
			if (lookup.TryGetValue(rhs, out var value))
			{
				lookup.Remove(rhs);
				delegates.Remove(value);
			}
		}

		public void Call()
		{
			for (global::System.Collections.Generic.LinkedListNode<global::System.Action> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value();
			}
		}
	}
	public class FastAction<A>
	{
		private global::System.Collections.Generic.LinkedList<global::System.Action<A>> delegates = new global::System.Collections.Generic.LinkedList<global::System.Action<A>>();

		private global::System.Collections.Generic.Dictionary<global::System.Action<A>, global::System.Collections.Generic.LinkedListNode<global::System.Action<A>>> lookup = new global::System.Collections.Generic.Dictionary<global::System.Action<A>, global::System.Collections.Generic.LinkedListNode<global::System.Action<A>>>();

		public void Add(global::System.Action<A> rhs)
		{
			if (!lookup.ContainsKey(rhs))
			{
				lookup[rhs] = delegates.AddLast(rhs);
			}
		}

		public void Remove(global::System.Action<A> rhs)
		{
			if (lookup.TryGetValue(rhs, out var value))
			{
				lookup.Remove(rhs);
				delegates.Remove(value);
			}
		}

		public void Call(A a)
		{
			for (global::System.Collections.Generic.LinkedListNode<global::System.Action<A>> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a);
			}
		}
	}
	public class FastAction<A, B>
	{
		private global::System.Collections.Generic.LinkedList<global::System.Action<A, B>> delegates = new global::System.Collections.Generic.LinkedList<global::System.Action<A, B>>();

		private global::System.Collections.Generic.Dictionary<global::System.Action<A, B>, global::System.Collections.Generic.LinkedListNode<global::System.Action<A, B>>> lookup = new global::System.Collections.Generic.Dictionary<global::System.Action<A, B>, global::System.Collections.Generic.LinkedListNode<global::System.Action<A, B>>>();

		public void Add(global::System.Action<A, B> rhs)
		{
			if (!lookup.ContainsKey(rhs))
			{
				lookup[rhs] = delegates.AddLast(rhs);
			}
		}

		public void Remove(global::System.Action<A, B> rhs)
		{
			if (lookup.TryGetValue(rhs, out var value))
			{
				lookup.Remove(rhs);
				delegates.Remove(value);
			}
		}

		public void Call(A a, B b)
		{
			for (global::System.Collections.Generic.LinkedListNode<global::System.Action<A, B>> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a, b);
			}
		}
	}
	public class FastAction<A, B, C>
	{
		private global::System.Collections.Generic.LinkedList<global::System.Action<A, B, C>> delegates = new global::System.Collections.Generic.LinkedList<global::System.Action<A, B, C>>();

		private global::System.Collections.Generic.Dictionary<global::System.Action<A, B, C>, global::System.Collections.Generic.LinkedListNode<global::System.Action<A, B, C>>> lookup = new global::System.Collections.Generic.Dictionary<global::System.Action<A, B, C>, global::System.Collections.Generic.LinkedListNode<global::System.Action<A, B, C>>>();

		public void Add(global::System.Action<A, B, C> rhs)
		{
			if (!lookup.ContainsKey(rhs))
			{
				lookup[rhs] = delegates.AddLast(rhs);
			}
		}

		public void Remove(global::System.Action<A, B, C> rhs)
		{
			if (lookup.TryGetValue(rhs, out var value))
			{
				lookup.Remove(rhs);
				delegates.Remove(value);
			}
		}

		public void Call(A a, B b, C c)
		{
			for (global::System.Collections.Generic.LinkedListNode<global::System.Action<A, B, C>> linkedListNode = delegates.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				linkedListNode.Value(a, b, c);
			}
		}
	}
}
