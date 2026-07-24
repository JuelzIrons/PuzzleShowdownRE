namespace Unity.VisualScripting
{
	public sealed class GraphReference : global::Unity.VisualScripting.GraphPointer
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		private int hashCode;

		private static readonly global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Unity.VisualScripting.GraphReference>> internPool;

		static GraphReference()
		{
			internPool = new global::System.Collections.Generic.Dictionary<int, global::System.Collections.Generic.List<global::Unity.VisualScripting.GraphReference>>();
			global::Unity.VisualScripting.ReferenceCollector.onSceneUnloaded += FreeInvalidInterns;
		}

		private GraphReference()
		{
		}

		public static global::Unity.VisualScripting.GraphReference New(global::Unity.VisualScripting.IGraphRoot root, bool ensureValid)
		{
			if (!ensureValid && !global::Unity.VisualScripting.GraphPointer.IsValidRoot(root))
			{
				return null;
			}
			global::Unity.VisualScripting.GraphReference graphReference = new global::Unity.VisualScripting.GraphReference();
			graphReference.Initialize(root);
			graphReference.Hash();
			return graphReference;
		}

		public static global::Unity.VisualScripting.GraphReference New(global::Unity.VisualScripting.IGraphRoot root, global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IGraphParentElement> parentElements, bool ensureValid)
		{
			if (!ensureValid && !global::Unity.VisualScripting.GraphPointer.IsValidRoot(root))
			{
				return null;
			}
			global::Unity.VisualScripting.GraphReference graphReference = new global::Unity.VisualScripting.GraphReference();
			graphReference.Initialize(root, parentElements, ensureValid);
			graphReference.Hash();
			return graphReference;
		}

		public static global::Unity.VisualScripting.GraphReference New(global::UnityEngine.Object rootObject, global::System.Collections.Generic.IEnumerable<global::System.Guid> parentElementGuids, bool ensureValid)
		{
			if (!ensureValid && !global::Unity.VisualScripting.GraphPointer.IsValidRoot(rootObject))
			{
				return null;
			}
			global::Unity.VisualScripting.GraphReference graphReference = new global::Unity.VisualScripting.GraphReference();
			graphReference.Initialize(rootObject, parentElementGuids, ensureValid);
			graphReference.Hash();
			return graphReference;
		}

		private static global::Unity.VisualScripting.GraphReference New(global::Unity.VisualScripting.GraphPointer model)
		{
			global::Unity.VisualScripting.GraphReference graphReference = new global::Unity.VisualScripting.GraphReference();
			graphReference.CopyFrom(model);
			return graphReference;
		}

		public override void CopyFrom(global::Unity.VisualScripting.GraphPointer other)
		{
			base.CopyFrom(other);
			if (other is global::Unity.VisualScripting.GraphReference graphReference)
			{
				hashCode = graphReference.hashCode;
			}
			else
			{
				Hash();
			}
		}

		public global::Unity.VisualScripting.GraphReference Clone()
		{
			return New(this);
		}

		public override global::Unity.VisualScripting.GraphReference AsReference()
		{
			return this;
		}

		public global::Unity.VisualScripting.GraphStack ToStackPooled()
		{
			return global::Unity.VisualScripting.GraphStack.New(this);
		}

		internal void Release()
		{
			global::Unity.VisualScripting.GraphPointer.releaseDebugDataBinding?.Invoke(base.root);
		}

		public void CreateGraphData()
		{
			if (base._data != null)
			{
				throw new global::Unity.VisualScripting.GraphPointerException("Graph data already exists.", this);
			}
			if (base.isRoot)
			{
				if (base.machine == null)
				{
					throw new global::Unity.VisualScripting.GraphPointerException("Root graph data can only be created on machines.", this);
				}
				global::Unity.VisualScripting.IGraphData graphData = (base.machine.graphData = base.graph.CreateData());
				base._data = graphData;
			}
			else
			{
				if (base._parentData == null)
				{
					throw new global::Unity.VisualScripting.GraphPointerException("Child graph data can only be created from parent graph data.", this);
				}
				base._data = base._parentData.CreateChildGraphData(base.parentElement);
			}
		}

		public void FreeGraphData()
		{
			if (base._data == null)
			{
				throw new global::Unity.VisualScripting.GraphPointerException("Graph data does not exist.", this);
			}
			if (base.isRoot)
			{
				if (base.machine == null)
				{
					throw new global::Unity.VisualScripting.GraphPointerException("Root graph data can only be freed on machines.", this);
				}
				global::Unity.VisualScripting.IGraphData graphData = (base.machine.graphData = null);
				base._data = graphData;
			}
			else
			{
				if (base._parentData == null)
				{
					throw new global::Unity.VisualScripting.GraphPointerException("Child graph data can only be freed from parent graph data.", this);
				}
				base._parentData.FreeChildGraphData(base.parentElement);
				base._data = null;
			}
		}

		public override bool Equals(object obj)
		{
			if (!(obj is global::Unity.VisualScripting.GraphReference other))
			{
				return false;
			}
			return InstanceEquals(other);
		}

		private void Hash()
		{
			hashCode = ComputeHashCode();
		}

		public override int GetHashCode()
		{
			return hashCode;
		}

		public static bool operator ==(global::Unity.VisualScripting.GraphReference x, global::Unity.VisualScripting.GraphReference y)
		{
			if ((object)x == y)
			{
				return true;
			}
			if ((object)x == null || (object)y == null)
			{
				return false;
			}
			return x.Equals(y);
		}

		public static bool operator !=(global::Unity.VisualScripting.GraphReference x, global::Unity.VisualScripting.GraphReference y)
		{
			return !(x == y);
		}

		public global::Unity.VisualScripting.GraphReference ParentReference(bool ensureValid)
		{
			if (base.isRoot)
			{
				if (ensureValid)
				{
					throw new global::Unity.VisualScripting.GraphPointerException("Trying to get parent graph reference of a root.", this);
				}
				return null;
			}
			global::Unity.VisualScripting.GraphReference graphReference = Clone();
			graphReference.ExitParentElement();
			graphReference.Hash();
			return graphReference;
		}

		public global::Unity.VisualScripting.GraphReference ChildReference(global::Unity.VisualScripting.IGraphParentElement parentElement, bool ensureValid, int? maxRecursionDepth = null)
		{
			global::Unity.VisualScripting.GraphReference graphReference = Clone();
			if (!graphReference.TryEnterParentElement(parentElement, out var error, maxRecursionDepth))
			{
				if (ensureValid)
				{
					throw new global::Unity.VisualScripting.GraphPointerException(error, this);
				}
				return null;
			}
			graphReference.Hash();
			return graphReference;
		}

		public global::Unity.VisualScripting.GraphReference Revalidate(bool ensureValid)
		{
			try
			{
				return New(base.rootObject, base.parentElementGuids, ensureValid);
			}
			catch (global::System.Exception ex)
			{
				if (ensureValid)
				{
					throw;
				}
				global::UnityEngine.Debug.LogWarning("Failed to revalidate graph pointer: \n" + ex);
				return null;
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.GraphReference> GetBreadcrumbs()
		{
			for (int depth = 0; depth < base.depth; depth++)
			{
				yield return New(base.root, global::System.Linq.Enumerable.Take(parentElementStack, depth), ensureValid: true);
			}
		}

		public static global::Unity.VisualScripting.GraphReference Intern(global::Unity.VisualScripting.GraphPointer pointer)
		{
			int key = pointer.ComputeHashCode();
			if (internPool.TryGetValue(key, out var value))
			{
				foreach (global::Unity.VisualScripting.GraphReference item in value)
				{
					if (item.InstanceEquals(pointer))
					{
						return item;
					}
				}
				global::Unity.VisualScripting.GraphReference graphReference = New(pointer);
				value.Add(graphReference);
				return graphReference;
			}
			global::Unity.VisualScripting.GraphReference graphReference2 = New(pointer);
			internPool.Add(graphReference2.hashCode, new global::System.Collections.Generic.List<global::Unity.VisualScripting.GraphReference> { graphReference2 });
			return graphReference2;
		}

		internal static void ClearIntern(global::Unity.VisualScripting.GraphPointer pointer)
		{
			int key = pointer.ComputeHashCode();
			if (!internPool.TryGetValue(key, out var value))
			{
				return;
			}
			for (int num = value.Count - 1; num >= 0; num--)
			{
				if (value[num].InstanceEquals(pointer))
				{
					value.RemoveAt(num);
					break;
				}
			}
			if (value.Count == 0)
			{
				internPool.Remove(key);
			}
		}

		public static void FreeInvalidInterns()
		{
			global::System.Collections.Generic.List<int> list = global::Unity.VisualScripting.ListPool<int>.New();
			foreach (global::System.Collections.Generic.KeyValuePair<int, global::System.Collections.Generic.List<global::Unity.VisualScripting.GraphReference>> item in internPool)
			{
				int key = item.Key;
				global::System.Collections.Generic.List<global::Unity.VisualScripting.GraphReference> value = item.Value;
				global::System.Collections.Generic.List<global::Unity.VisualScripting.GraphReference> list2 = global::Unity.VisualScripting.ListPool<global::Unity.VisualScripting.GraphReference>.New();
				foreach (global::Unity.VisualScripting.GraphReference item2 in value)
				{
					if (!item2.isValid)
					{
						list2.Add(item2);
					}
				}
				foreach (global::Unity.VisualScripting.GraphReference item3 in list2)
				{
					value.Remove(item3);
				}
				if (value.Count == 0)
				{
					list.Add(key);
				}
				list2.Free();
			}
			foreach (int item4 in list)
			{
				internPool.Remove(item4);
			}
			list.Free();
		}
	}
}
