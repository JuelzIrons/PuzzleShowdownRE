namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.DisableAnnotation]
	public abstract class Macro<TGraph> : global::Unity.VisualScripting.MacroScriptableObject, global::Unity.VisualScripting.IMacro, global::Unity.VisualScripting.IGraphRoot, global::Unity.VisualScripting.IGraphParent, global::Unity.VisualScripting.ISerializationDependency, global::UnityEngine.ISerializationCallbackReceiver, global::Unity.VisualScripting.IAotStubbable where TGraph : class, global::Unity.VisualScripting.IGraph, new()
	{
		[global::Unity.VisualScripting.SerializeAs("graph")]
		private TGraph _graph = new TGraph();

		[global::Unity.VisualScripting.DoNotSerialize]
		private global::Unity.VisualScripting.GraphReference _reference;

		[global::Unity.VisualScripting.DoNotSerialize]
		public TGraph graph
		{
			get
			{
				return _graph;
			}
			set
			{
				if (value == null)
				{
					throw new global::System.InvalidOperationException("Macros must have a graph.");
				}
				if (value != graph)
				{
					_graph = value;
				}
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IMacro.graph
		{
			get
			{
				return graph;
			}
			set
			{
				graph = (TGraph)value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphParent.childGraph => graph;

		[global::Unity.VisualScripting.DoNotSerialize]
		bool global::Unity.VisualScripting.IGraphParent.isSerializationRoot => true;

		[global::Unity.VisualScripting.DoNotSerialize]
		global::UnityEngine.Object global::Unity.VisualScripting.IGraphParent.serializedObject => this;

		[global::Unity.VisualScripting.DoNotSerialize]
		protected global::Unity.VisualScripting.GraphReference reference
		{
			get
			{
				if (!(_reference == null))
				{
					return _reference;
				}
				return global::Unity.VisualScripting.GraphReference.New(this, ensureValid: false);
			}
		}

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

		bool global::Unity.VisualScripting.ISerializationDependency.IsDeserialized { get; set; }

		public global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			return graph.GetAotStubs(visited);
		}

		protected override void OnBeforeDeserialize()
		{
			base.OnBeforeDeserialize();
			global::Unity.VisualScripting.Serialization.NotifyDependencyDeserializing(this);
		}

		protected override void OnAfterDeserialize()
		{
			base.OnAfterDeserialize();
			global::Unity.VisualScripting.Serialization.NotifyDependencyDeserialized(this);
		}

		public abstract TGraph DefaultGraph();

		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphParent.DefaultGraph()
		{
			return DefaultGraph();
		}

		protected virtual void OnEnable()
		{
			global::Unity.VisualScripting.Serialization.NotifyDependencyAvailable(this);
		}

		protected virtual void OnDisable()
		{
			global::Unity.VisualScripting.Serialization.NotifyDependencyUnavailable(this);
		}

		public global::Unity.VisualScripting.GraphPointer GetReference()
		{
			return reference;
		}
	}
}
