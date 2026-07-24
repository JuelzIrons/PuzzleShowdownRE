namespace Unity.VisualScripting
{
	public abstract class Machine<TGraph, TMacro> : global::Unity.VisualScripting.LudiqBehaviour, global::Unity.VisualScripting.IMachine, global::Unity.VisualScripting.IGraphRoot, global::Unity.VisualScripting.IGraphParent, global::Unity.VisualScripting.IGraphNester, global::Unity.VisualScripting.IAotStubbable where TGraph : class, global::Unity.VisualScripting.IGraph, new() where TMacro : global::Unity.VisualScripting.Macro<TGraph>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		private bool _alive;

		[global::Unity.VisualScripting.DoNotSerialize]
		private bool _enabled;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::UnityEngine.GameObject threadSafeGameObject;

		[global::Unity.VisualScripting.DoNotSerialize]
		private bool isReferenceCached;

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::Unity.VisualScripting.GraphReference _reference;

		[global::Unity.VisualScripting.Serialize]
		public global::Unity.VisualScripting.GraphNest<TGraph, TMacro> nest { get; private set; } = new global::Unity.VisualScripting.GraphNest<TGraph, TMacro>();

		[global::Unity.VisualScripting.DoNotSerialize]
		global::Unity.VisualScripting.IGraphNest global::Unity.VisualScripting.IGraphNester.nest => nest;

		[global::Unity.VisualScripting.DoNotSerialize]
		global::UnityEngine.GameObject global::Unity.VisualScripting.IMachine.threadSafeGameObject => threadSafeGameObject;

		[global::Unity.VisualScripting.DoNotSerialize]
		protected global::Unity.VisualScripting.GraphReference reference
		{
			get
			{
				if (!isReferenceCached)
				{
					return global::Unity.VisualScripting.GraphReference.New(this, ensureValid: false);
				}
				return _reference;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		protected bool hasGraph => reference != null;

		[global::Unity.VisualScripting.DoNotSerialize]
		public TGraph graph => nest.graph;

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.IGraphData graphData { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		bool global::Unity.VisualScripting.IGraphParent.isSerializationRoot => true;

		[global::Unity.VisualScripting.DoNotSerialize]
		global::UnityEngine.Object global::Unity.VisualScripting.IGraphParent.serializedObject => nest.source switch
		{
			global::Unity.VisualScripting.GraphSource.Macro => nest.macro, 
			global::Unity.VisualScripting.GraphSource.Embed => this, 
			_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.GraphSource>(nest.source), 
		};

		[global::Unity.VisualScripting.DoNotSerialize]
		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphParent.childGraph => graph;

		public bool isDescriptionValid
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		protected Machine()
		{
			nest.nester = this;
			nest.source = global::Unity.VisualScripting.GraphSource.Macro;
		}

		public global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			return nest.GetAotStubs(visited);
		}

		protected virtual void Awake()
		{
			_alive = true;
			threadSafeGameObject = base.gameObject;
			nest.afterGraphChange += CacheReference;
			nest.beforeGraphChange += ClearCachedReference;
			CacheReference();
			if (graph != null)
			{
				graph.Prewarm();
				InstantiateNest();
			}
		}

		protected virtual void OnEnable()
		{
			_enabled = true;
		}

		protected virtual void OnInstantiateWhileEnabled()
		{
		}

		protected virtual void OnUninstantiateWhileEnabled()
		{
		}

		protected virtual void OnDisable()
		{
			_enabled = false;
		}

		protected virtual void OnDestroy()
		{
			ClearCachedReference();
			if (graph != null)
			{
				UninstantiateNest();
			}
			threadSafeGameObject = null;
			_alive = false;
		}

		protected virtual void OnValidate()
		{
			threadSafeGameObject = base.gameObject;
		}

		public global::Unity.VisualScripting.GraphPointer GetReference()
		{
			return reference;
		}

		private void CacheReference()
		{
			_reference = global::Unity.VisualScripting.GraphReference.New(this, ensureValid: false);
			isReferenceCached = true;
		}

		private void ClearCachedReference()
		{
			if (_reference != null)
			{
				_reference.Release();
				_reference = null;
			}
		}

		public virtual void InstantiateNest()
		{
			if (_alive)
			{
				global::Unity.VisualScripting.GraphInstances.Instantiate(reference);
			}
			if (_enabled)
			{
				if (global::Unity.VisualScripting.UnityThread.allowsAPI)
				{
					OnInstantiateWhileEnabled();
				}
				else
				{
					global::UnityEngine.Debug.LogWarning("Could not run instantiation events on " + this.ToSafeString() + " because the Unity API is not available.\nThis can happen when undoing / redoing a graph source change.", this);
				}
			}
		}

		public virtual void UninstantiateNest()
		{
			if (_enabled)
			{
				if (global::Unity.VisualScripting.UnityThread.allowsAPI)
				{
					OnUninstantiateWhileEnabled();
				}
				else
				{
					global::UnityEngine.Debug.LogWarning("Could not run uninstantiation events on " + this.ToSafeString() + " because the Unity API is not available.\nThis can happen when undoing / redoing a graph source change.", this);
				}
			}
			if (!_alive)
			{
				return;
			}
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference> hashSet = global::Unity.VisualScripting.GraphInstances.ChildrenOfPooled(this);
			foreach (global::Unity.VisualScripting.GraphReference item in hashSet)
			{
				global::Unity.VisualScripting.GraphInstances.Uninstantiate(item);
			}
			hashSet.Free();
		}

		public virtual void TriggerAnimationEvent(global::UnityEngine.AnimationEvent animationEvent)
		{
		}

		public virtual void TriggerUnityEvent(string name)
		{
		}

		public abstract TGraph DefaultGraph();

		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphParent.DefaultGraph()
		{
			return DefaultGraph();
		}
	}
}
