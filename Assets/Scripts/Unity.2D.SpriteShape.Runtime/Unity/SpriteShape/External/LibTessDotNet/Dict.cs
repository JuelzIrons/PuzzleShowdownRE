namespace Unity.SpriteShape.External.LibTessDotNet
{
	internal class Dict<TValue> where TValue : class
	{
		public class Node
		{
			internal TValue _key;

			internal global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node _prev;

			internal global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node _next;

			public TValue Key => _key;

			public global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node Prev => _prev;

			public global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node Next => _next;
		}

		public delegate bool LessOrEqual(TValue lhs, TValue rhs);

		private global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.LessOrEqual _leq;

		private global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node _head;

		public Dict(global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.LessOrEqual leq)
		{
			_leq = leq;
			_head = new global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node
			{
				_key = null
			};
			_head._prev = _head;
			_head._next = _head;
		}

		public global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node Insert(TValue key)
		{
			return InsertBefore(_head, key);
		}

		public global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node InsertBefore(global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node node, TValue key)
		{
			do
			{
				node = node._prev;
			}
			while (node._key != null && !_leq(node._key, key));
			global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node node2 = new global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node
			{
				_key = key
			};
			node2._next = node._next;
			node._next._prev = node2;
			node2._prev = node;
			node._next = node2;
			return node2;
		}

		public global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node Find(TValue key)
		{
			global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node node = _head;
			do
			{
				node = node._next;
			}
			while (node._key != null && !_leq(key, node._key));
			return node;
		}

		public global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node Min()
		{
			return _head._next;
		}

		public void Remove(global::Unity.SpriteShape.External.LibTessDotNet.Dict<TValue>.Node node)
		{
			node._next._prev = node._prev;
			node._prev._next = node._next;
		}
	}
}
