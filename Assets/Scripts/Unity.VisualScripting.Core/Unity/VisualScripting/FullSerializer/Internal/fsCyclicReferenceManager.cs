namespace Unity.VisualScripting.FullSerializer.Internal
{
	public class fsCyclicReferenceManager
	{
		private class ObjectReferenceEqualityComparator : global::System.Collections.Generic.IEqualityComparer<object>
		{
			public static readonly global::System.Collections.Generic.IEqualityComparer<object> Instance = new global::Unity.VisualScripting.FullSerializer.Internal.fsCyclicReferenceManager.ObjectReferenceEqualityComparator();

			bool global::System.Collections.Generic.IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			int global::System.Collections.Generic.IEqualityComparer<object>.GetHashCode(object obj)
			{
				return global::System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
			}
		}

		private global::System.Collections.Generic.Dictionary<object, int> _objectIds = new global::System.Collections.Generic.Dictionary<object, int>(global::Unity.VisualScripting.FullSerializer.Internal.fsCyclicReferenceManager.ObjectReferenceEqualityComparator.Instance);

		private int _nextId;

		private global::System.Collections.Generic.Dictionary<int, object> _marked = new global::System.Collections.Generic.Dictionary<int, object>();

		private int _depth;

		public void Enter()
		{
			_depth++;
		}

		public bool Exit()
		{
			_depth--;
			if (_depth == 0)
			{
				_objectIds = new global::System.Collections.Generic.Dictionary<object, int>(global::Unity.VisualScripting.FullSerializer.Internal.fsCyclicReferenceManager.ObjectReferenceEqualityComparator.Instance);
				_nextId = 0;
				_marked = new global::System.Collections.Generic.Dictionary<int, object>();
			}
			if (_depth < 0)
			{
				_depth = 0;
				throw new global::System.InvalidOperationException("Internal Error - Mismatched Enter/Exit. Please report a bug at https://github.com/jacobdufault/fullserializer/issues with the serialization data.");
			}
			return _depth == 0;
		}

		public object GetReferenceObject(int id)
		{
			if (!_marked.ContainsKey(id))
			{
				throw new global::System.InvalidOperationException("Internal Deserialization Error - Object definition has not been encountered for object with id=" + id + "; have you reordered or modified the serialized data? If this is an issue with an unmodified Full Serializer implementation and unmodified serialization data, please report an issue with an included test case.");
			}
			return _marked[id];
		}

		public void AddReferenceWithId(int id, object reference)
		{
			_marked[id] = reference;
		}

		public int GetReferenceId(object item)
		{
			if (!_objectIds.TryGetValue(item, out var value))
			{
				value = _nextId++;
				_objectIds[item] = value;
			}
			return value;
		}

		public bool IsReference(object item)
		{
			return _marked.ContainsKey(GetReferenceId(item));
		}

		public void MarkSerialized(object item)
		{
			int referenceId = GetReferenceId(item);
			if (_marked.ContainsKey(referenceId))
			{
				throw new global::System.InvalidOperationException("Internal Error - " + item?.ToString() + " has already been marked as serialized");
			}
			_marked[referenceId] = item;
		}
	}
}
