namespace Unity.VisualScripting
{
	public abstract class GraphPointer
	{
		protected readonly global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphParent> parentStack = new global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphParent>();

		protected readonly global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphParentElement> parentElementStack = new global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphParentElement>();

		protected readonly global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraph> graphStack = new global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraph>();

		protected readonly global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphData> dataStack = new global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphData>();

		protected readonly global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphDebugData> debugDataStack = new global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphDebugData>();

		internal static global::System.Action<global::Unity.VisualScripting.IGraphRoot> releaseDebugDataBinding;

		public global::Unity.VisualScripting.IGraphRoot root { get; protected set; }

		public global::UnityEngine.Object rootObject => root as global::UnityEngine.Object;

		public global::Unity.VisualScripting.IMachine machine => root as global::Unity.VisualScripting.IMachine;

		public global::Unity.VisualScripting.IMacro macro => root as global::Unity.VisualScripting.IMacro;

		public global::UnityEngine.MonoBehaviour component => root as global::UnityEngine.MonoBehaviour;

		public global::UnityEngine.GameObject gameObject { get; private set; }

		public global::UnityEngine.GameObject self => gameObject;

		public global::UnityEngine.ScriptableObject scriptableObject => root as global::UnityEngine.ScriptableObject;

		public global::UnityEngine.SceneManagement.Scene? scene
		{
			get
			{
				if (gameObject == null)
				{
					return null;
				}
				global::UnityEngine.SceneManagement.Scene value = gameObject.scene;
				if (!value.IsValid())
				{
					return null;
				}
				return value;
			}
		}

		public global::UnityEngine.Object serializedObject
		{
			get
			{
				for (int num = depth; num > 0; num--)
				{
					global::Unity.VisualScripting.IGraphParent graphParent = parentStack[num - 1];
					if (graphParent.isSerializationRoot)
					{
						return graphParent.serializedObject;
					}
				}
				throw new global::Unity.VisualScripting.GraphPointerException("Could not find serialized object.", this);
			}
		}

		public global::System.Collections.Generic.IEnumerable<global::System.Guid> parentElementGuids => global::System.Linq.Enumerable.Select(parentElementStack, (global::Unity.VisualScripting.IGraphParentElement parentElement) => parentElement.guid);

		public int depth => parentStack.Count;

		public bool isRoot => depth == 1;

		public bool isChild => depth > 1;

		public global::Unity.VisualScripting.IGraphParent parent => parentStack[parentStack.Count - 1];

		public global::Unity.VisualScripting.IGraphParentElement parentElement
		{
			get
			{
				EnsureChild();
				return parentElementStack[parentElementStack.Count - 1];
			}
		}

		public global::Unity.VisualScripting.IGraph rootGraph => graphStack[0];

		public global::Unity.VisualScripting.IGraph graph => graphStack[graphStack.Count - 1];

		protected global::Unity.VisualScripting.IGraphData _data
		{
			get
			{
				return dataStack[dataStack.Count - 1];
			}
			set
			{
				dataStack[dataStack.Count - 1] = value;
			}
		}

		public global::Unity.VisualScripting.IGraphData data
		{
			get
			{
				EnsureDataAvailable();
				return _data;
			}
		}

		protected global::Unity.VisualScripting.IGraphData _parentData => dataStack[dataStack.Count - 2];

		public bool hasData => _data != null;

		public static global::System.Func<global::Unity.VisualScripting.IGraphRoot, global::Unity.VisualScripting.IGraphDebugData> fetchRootDebugDataBinding { get; set; }

		public bool hasDebugData => _debugData != null;

		protected global::Unity.VisualScripting.IGraphDebugData _debugData
		{
			get
			{
				return debugDataStack[debugDataStack.Count - 1];
			}
			set
			{
				debugDataStack[debugDataStack.Count - 1] = value;
			}
		}

		public global::Unity.VisualScripting.IGraphDebugData debugData
		{
			get
			{
				EnsureDebugDataAvailable();
				return _debugData;
			}
		}

		public bool isValid
		{
			get
			{
				try
				{
					if (rootObject == null)
					{
						return false;
					}
					if (rootGraph != root.childGraph)
					{
						return false;
					}
					if (serializedObject == null)
					{
						return false;
					}
					for (int i = 1; i < depth; i++)
					{
						global::Unity.VisualScripting.IGraphParentElement graphParentElement = parentElementStack[i - 1];
						global::Unity.VisualScripting.IGraph obj = graphStack[i - 1];
						global::Unity.VisualScripting.IGraph graph = graphStack[i];
						if (!obj.elements.Contains(graphParentElement))
						{
							return false;
						}
						if (graphParentElement.childGraph != graph)
						{
							return false;
						}
					}
					return true;
				}
				catch (global::System.Exception ex)
				{
					global::UnityEngine.Debug.LogWarning("Failed to check graph pointer validity: \n" + ex);
					return false;
				}
			}
		}

		protected static bool IsValidRoot(global::Unity.VisualScripting.IGraphRoot root)
		{
			if (root?.childGraph != null)
			{
				return root as global::UnityEngine.Object != null;
			}
			return false;
		}

		protected static bool IsValidRoot(global::UnityEngine.Object rootObject)
		{
			if (rootObject != null)
			{
				return (rootObject as global::Unity.VisualScripting.IGraphRoot)?.childGraph != null;
			}
			return false;
		}

		internal GraphPointer()
		{
		}

		protected void Initialize(global::Unity.VisualScripting.IGraphRoot root)
		{
			if (!IsValidRoot(root))
			{
				throw new global::System.ArgumentException("Graph pointer root must be a valid Unity object with a non-null child graph.", "root");
			}
			if ((!(root is global::Unity.VisualScripting.IMachine) || !(root is global::UnityEngine.MonoBehaviour)) && (!(root is global::Unity.VisualScripting.IMacro) || !(root is global::UnityEngine.ScriptableObject)))
			{
				throw new global::System.ArgumentException("Graph pointer root must be either a machine or a macro.", "root");
			}
			this.root = root;
			parentStack.Add(root);
			graphStack.Add(root.childGraph);
			dataStack.Add(machine?.graphData);
			debugDataStack.Add(fetchRootDebugDataBinding?.Invoke(root));
			if (machine != null)
			{
				if (machine.threadSafeGameObject != null)
				{
					gameObject = machine.threadSafeGameObject;
					return;
				}
				if (!global::Unity.VisualScripting.UnityThread.allowsAPI)
				{
					throw new global::Unity.VisualScripting.GraphPointerException("Could not fetch graph pointer root game object.", this);
				}
				gameObject = component.gameObject;
			}
			else
			{
				gameObject = null;
			}
		}

		protected void Initialize(global::Unity.VisualScripting.IGraphRoot root, global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IGraphParentElement> parentElements, bool ensureValid)
		{
			Initialize(root);
			global::Unity.VisualScripting.Ensure.That("parentElements").IsNotNull(parentElements);
			foreach (global::Unity.VisualScripting.IGraphParentElement parentElement in parentElements)
			{
				if (!TryEnterParentElement(parentElement, out var error))
				{
					if (ensureValid)
					{
						throw new global::Unity.VisualScripting.GraphPointerException(error, this);
					}
					break;
				}
			}
		}

		protected void Initialize(global::UnityEngine.Object rootObject, global::System.Collections.Generic.IEnumerable<global::System.Guid> parentElementGuids, bool ensureValid)
		{
			Initialize(rootObject as global::Unity.VisualScripting.IGraphRoot);
			global::Unity.VisualScripting.Ensure.That("parentElementGuids").IsNotNull(parentElementGuids);
			foreach (global::System.Guid parentElementGuid in parentElementGuids)
			{
				if (!TryEnterParentElement(parentElementGuid, out var error))
				{
					if (ensureValid)
					{
						throw new global::Unity.VisualScripting.GraphPointerException(error, this);
					}
					break;
				}
			}
		}

		public abstract global::Unity.VisualScripting.GraphReference AsReference();

		public virtual void CopyFrom(global::Unity.VisualScripting.GraphPointer other)
		{
			root = other.root;
			gameObject = other.gameObject;
			parentStack.Clear();
			parentElementStack.Clear();
			graphStack.Clear();
			dataStack.Clear();
			debugDataStack.Clear();
			foreach (global::Unity.VisualScripting.IGraphParent item in other.parentStack)
			{
				parentStack.Add(item);
			}
			foreach (global::Unity.VisualScripting.IGraphParentElement item2 in other.parentElementStack)
			{
				parentElementStack.Add(item2);
			}
			foreach (global::Unity.VisualScripting.IGraph item3 in other.graphStack)
			{
				graphStack.Add(item3);
			}
			foreach (global::Unity.VisualScripting.IGraphData item4 in other.dataStack)
			{
				dataStack.Add(item4);
			}
			foreach (global::Unity.VisualScripting.IGraphDebugData item5 in other.debugDataStack)
			{
				debugDataStack.Add(item5);
			}
		}

		public void EnsureDepthValid(int depth)
		{
			global::Unity.VisualScripting.Ensure.That("depth").IsGte(depth, 1);
			if (depth > this.depth)
			{
				throw new global::Unity.VisualScripting.GraphPointerException($"Trying to fetch a graph pointer level above depth: {depth} > {this.depth}", this);
			}
		}

		public void EnsureChild()
		{
			if (!isChild)
			{
				throw new global::Unity.VisualScripting.GraphPointerException("Graph pointer does not point to a child graph.", this);
			}
		}

		public bool IsWithin<T>() where T : global::Unity.VisualScripting.IGraphParent
		{
			return parent is T;
		}

		public void EnsureWithin<T>() where T : global::Unity.VisualScripting.IGraphParent
		{
			if (!IsWithin<T>())
			{
				throw new global::Unity.VisualScripting.GraphPointerException($"Graph pointer must be within a {typeof(T)} for this operation.", this);
			}
		}

		public T GetParent<T>() where T : global::Unity.VisualScripting.IGraphParent
		{
			EnsureWithin<T>();
			return (T)parent;
		}

		public void EnsureDataAvailable()
		{
			if (!hasData)
			{
				throw new global::Unity.VisualScripting.GraphPointerException("Graph data is not available.", this);
			}
		}

		public T GetGraphData<T>() where T : global::Unity.VisualScripting.IGraphData
		{
			global::Unity.VisualScripting.IGraphData graphData = data;
			if (graphData is T)
			{
				return (T)graphData;
			}
			throw new global::Unity.VisualScripting.GraphPointerException($"Graph data type mismatch. Found {graphData.GetType()}, expected {typeof(T)}.", this);
		}

		public T GetElementData<T>(global::Unity.VisualScripting.IGraphElementWithData element) where T : global::Unity.VisualScripting.IGraphElementData
		{
			if (_data.TryGetElementData(element, out var graphElementData))
			{
				if (graphElementData is T)
				{
					return (T)graphElementData;
				}
				throw new global::Unity.VisualScripting.GraphPointerException($"Graph element data type mismatch. Found {graphElementData.GetType()}, expected {typeof(T)}.", this);
			}
			throw new global::Unity.VisualScripting.GraphPointerException($"Missing graph element data for {element}.", this);
		}

		public void EnsureDebugDataAvailable()
		{
			if (!hasDebugData)
			{
				throw new global::Unity.VisualScripting.GraphPointerException("Graph debug data is not available.", this);
			}
		}

		public T GetGraphDebugData<T>() where T : global::Unity.VisualScripting.IGraphDebugData
		{
			global::Unity.VisualScripting.IGraphDebugData graphDebugData = debugData;
			if (graphDebugData is T)
			{
				return (T)graphDebugData;
			}
			throw new global::Unity.VisualScripting.GraphPointerException($"Graph debug data type mismatch. Found {graphDebugData.GetType()}, expected {typeof(T)}.", this);
		}

		public T GetElementDebugData<T>(global::Unity.VisualScripting.IGraphElementWithDebugData element)
		{
			global::Unity.VisualScripting.IGraphElementDebugData orCreateElementData = debugData.GetOrCreateElementData(element);
			if (orCreateElementData is T)
			{
				return (T)orCreateElementData;
			}
			throw new global::Unity.VisualScripting.GraphPointerException($"Graph element runtime debug data type mismatch. Found {orCreateElementData.GetType()}, expected {typeof(T)}.", this);
		}

		protected bool TryEnterParentElement(global::System.Guid parentElementGuid, out string error, int? maxRecursionDepth = null)
		{
			if (!graph.elements.TryGetValue(parentElementGuid, out var value))
			{
				error = "Trying to enter a graph parent element with a GUID that is not within the current graph.";
				return false;
			}
			if (!(value is global::Unity.VisualScripting.IGraphParentElement))
			{
				error = "Provided element GUID does not point to a graph parent element.";
				return false;
			}
			global::Unity.VisualScripting.IGraphParentElement graphParentElement = (global::Unity.VisualScripting.IGraphParentElement)value;
			return TryEnterParentElement(graphParentElement, out error, maxRecursionDepth);
		}

		protected bool TryEnterParentElement(global::Unity.VisualScripting.IGraphParentElement parentElement, out string error, int? maxRecursionDepth = null, bool skipContainsCheck = false)
		{
			if (!skipContainsCheck && !graph.elements.Contains(parentElement))
			{
				error = "Trying to enter a graph parent element that is not within the current graph.";
				return false;
			}
			global::Unity.VisualScripting.IGraph childGraph = parentElement.childGraph;
			if (childGraph == null)
			{
				error = "Trying to enter a graph parent element without a child graph.";
				return false;
			}
			if (global::Unity.VisualScripting.Recursion.safeMode)
			{
				int num = 0;
				int num2 = maxRecursionDepth ?? global::Unity.VisualScripting.Recursion.defaultMaxDepth;
				foreach (global::Unity.VisualScripting.IGraph item in graphStack)
				{
					if (item == childGraph)
					{
						num++;
					}
				}
				if (num > num2)
				{
					error = string.Format("Max recursion depth of {0} has been exceeded. Are you nesting a graph within itself?\nIf not, consider increasing '{1}.{2}'.", num2, "Recursion", "defaultMaxDepth");
					return false;
				}
			}
			EnterValidParentElement(parentElement);
			error = null;
			return true;
		}

		protected void EnterParentElement(global::Unity.VisualScripting.IGraphParentElement parentElement)
		{
			if (!TryEnterParentElement(parentElement, out var error))
			{
				throw new global::Unity.VisualScripting.GraphPointerException(error, this);
			}
		}

		protected void EnterParentElement(global::System.Guid parentElementGuid)
		{
			if (!TryEnterParentElement(parentElementGuid, out var error))
			{
				throw new global::Unity.VisualScripting.GraphPointerException(error, this);
			}
		}

		private void EnterValidParentElement(global::Unity.VisualScripting.IGraphParentElement parentElement)
		{
			global::Unity.VisualScripting.IGraph childGraph = parentElement.childGraph;
			parentStack.Add(parentElement);
			parentElementStack.Add(parentElement);
			graphStack.Add(childGraph);
			global::Unity.VisualScripting.IGraphData item = null;
			_data?.TryGetChildGraphData(parentElement, out item);
			dataStack.Add(item);
			global::Unity.VisualScripting.IGraphDebugData item2 = _debugData?.GetOrCreateChildGraphData(parentElement);
			debugDataStack.Add(item2);
		}

		protected void ExitParentElement()
		{
			if (!isChild)
			{
				throw new global::Unity.VisualScripting.GraphPointerException("Trying to exit the root graph.", this);
			}
			parentStack.RemoveAt(parentStack.Count - 1);
			parentElementStack.RemoveAt(parentElementStack.Count - 1);
			graphStack.RemoveAt(graphStack.Count - 1);
			dataStack.RemoveAt(dataStack.Count - 1);
			debugDataStack.RemoveAt(debugDataStack.Count - 1);
		}

		public void EnsureValid()
		{
			if (!isValid)
			{
				throw new global::Unity.VisualScripting.GraphPointerException("Graph pointer is invalid.", this);
			}
		}

		public bool InstanceEquals(global::Unity.VisualScripting.GraphPointer other)
		{
			if (this == other)
			{
				return true;
			}
			if (!global::Unity.VisualScripting.UnityObjectUtility.TrulyEqual(rootObject, other.rootObject))
			{
				return false;
			}
			if (!DefinitionEquals(other))
			{
				return false;
			}
			int num = depth;
			for (int i = 0; i < num; i++)
			{
				global::Unity.VisualScripting.IGraphData graphData = dataStack[i];
				global::Unity.VisualScripting.IGraphData graphData2 = other.dataStack[i];
				if (graphData != graphData2)
				{
					return false;
				}
			}
			return true;
		}

		public bool DefinitionEquals(global::Unity.VisualScripting.GraphPointer other)
		{
			if (other == null)
			{
				return false;
			}
			if (rootGraph != other.rootGraph)
			{
				return false;
			}
			int num = depth;
			if (num != other.depth)
			{
				return false;
			}
			for (int i = 1; i < num; i++)
			{
				global::Unity.VisualScripting.IGraphParentElement graphParentElement = parentElementStack[i - 1];
				global::Unity.VisualScripting.IGraphParentElement graphParentElement2 = other.parentElementStack[i - 1];
				if (graphParentElement != graphParentElement2)
				{
					return false;
				}
			}
			return true;
		}

		public int ComputeHashCode()
		{
			int num = 17;
			num = num * 23 + (rootObject.AsUnityNull()?.GetHashCode() ?? 0);
			num = num * 23 + (rootGraph?.GetHashCode() ?? 0);
			int num2 = depth;
			for (int i = 1; i < num2; i++)
			{
				num = num * 23 + parentElementStack[i - 1].guid.GetHashCode();
			}
			return num;
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append("[ ");
			stringBuilder.Append(rootObject.ToSafeString());
			for (int i = 1; i < depth; i++)
			{
				stringBuilder.Append(" > ");
				int num = i - 1;
				if (num >= parentElementStack.Count)
				{
					stringBuilder.Append("?");
					break;
				}
				global::Unity.VisualScripting.IGraphParentElement value = parentElementStack[num];
				stringBuilder.Append(value);
			}
			stringBuilder.Append(" ]");
			return stringBuilder.ToString();
		}
	}
}
