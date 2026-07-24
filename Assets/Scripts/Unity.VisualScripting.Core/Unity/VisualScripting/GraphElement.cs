namespace Unity.VisualScripting
{
	public abstract class GraphElement<TGraph> : global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable where TGraph : class, global::Unity.VisualScripting.IGraph
	{
		[global::Unity.VisualScripting.Serialize]
		public global::System.Guid guid { get; set; } = global::System.Guid.NewGuid();

		[global::Unity.VisualScripting.DoNotSerialize]
		public virtual int dependencyOrder => 0;

		[global::Unity.VisualScripting.DoNotSerialize]
		public TGraph graph { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphElement.graph
		{
			get
			{
				return graph;
			}
			set
			{
				global::Unity.VisualScripting.Ensure.That("value").IsOfType<TGraph>(value);
				graph = (TGraph)value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphItem.graph => graph;

		public virtual global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ISerializationDependency> deserializationDependencies => global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.ISerializationDependency>();

		public virtual void Instantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			if (this is global::Unity.VisualScripting.IGraphElementWithData element)
			{
				instance.data.CreateElementData(element);
			}
			if (this is global::Unity.VisualScripting.IGraphNesterElement graphNesterElement && graphNesterElement.nest.graph != null)
			{
				global::Unity.VisualScripting.GraphInstances.Instantiate(instance.ChildReference(graphNesterElement, ensureValid: true));
			}
		}

		public virtual void Uninstantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			if (this is global::Unity.VisualScripting.IGraphNesterElement graphNesterElement && graphNesterElement.nest.graph != null)
			{
				global::Unity.VisualScripting.GraphInstances.Uninstantiate(instance.ChildReference(graphNesterElement, ensureValid: true));
			}
			if (this is global::Unity.VisualScripting.IGraphElementWithData element)
			{
				instance.data.FreeElementData(element);
			}
		}

		public virtual void BeforeAdd()
		{
		}

		public virtual void AfterAdd()
		{
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference> hashSet = global::Unity.VisualScripting.GraphInstances.OfPooled(graph);
			foreach (global::Unity.VisualScripting.GraphReference item in hashSet)
			{
				Instantiate(item);
			}
			hashSet.Free();
		}

		public virtual void BeforeRemove()
		{
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference> hashSet = global::Unity.VisualScripting.GraphInstances.OfPooled(graph);
			foreach (global::Unity.VisualScripting.GraphReference item in hashSet)
			{
				Uninstantiate(item);
			}
			hashSet.Free();
			Dispose();
		}

		public virtual void AfterRemove()
		{
		}

		public virtual void Dispose()
		{
		}

		protected void InstantiateNest()
		{
			global::Unity.VisualScripting.IGraphNesterElement parentElement = (global::Unity.VisualScripting.IGraphNesterElement)this;
			if (graph == null)
			{
				return;
			}
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference> hashSet = global::Unity.VisualScripting.GraphInstances.OfPooled(graph);
			foreach (global::Unity.VisualScripting.GraphReference item in hashSet)
			{
				global::Unity.VisualScripting.GraphInstances.Instantiate(item.ChildReference(parentElement, ensureValid: true));
			}
			hashSet.Free();
		}

		protected void UninstantiateNest()
		{
			global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.GraphReference> hashSet = global::Unity.VisualScripting.GraphInstances.ChildrenOfPooled((global::Unity.VisualScripting.IGraphNesterElement)this);
			foreach (global::Unity.VisualScripting.GraphReference item in hashSet)
			{
				global::Unity.VisualScripting.GraphInstances.Uninstantiate(item);
			}
			hashSet.Free();
		}

		public virtual bool HandleDependencies()
		{
			return true;
		}

		public virtual global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			return global::System.Linq.Enumerable.Empty<object>();
		}

		public virtual void Prewarm()
		{
		}

		protected void CopyFrom(global::Unity.VisualScripting.GraphElement<TGraph> source)
		{
		}

		public override string ToString()
		{
			global::System.Text.StringBuilder stringBuilder = new global::System.Text.StringBuilder();
			stringBuilder.Append(GetType().Name);
			stringBuilder.Append("#");
			stringBuilder.Append(guid.ToString().Substring(0, 5));
			stringBuilder.Append("...");
			return stringBuilder.ToString();
		}

		public virtual global::Unity.VisualScripting.AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			throw new global::System.NotImplementedException();
		}
	}
}
