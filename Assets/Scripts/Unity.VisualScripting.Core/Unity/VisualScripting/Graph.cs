namespace Unity.VisualScripting
{
	public abstract class Graph : global::Unity.VisualScripting.IGraph, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.ISerializationDepender, global::UnityEngine.ISerializationCallbackReceiver
	{
		[global::Unity.VisualScripting.SerializeAs("elements")]
		private global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphElement> _elements = new global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphElement>();

		private bool prewarmed;

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.MergedGraphElementCollection elements { get; }

		[global::Unity.VisualScripting.Serialize]
		public string title { get; set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectorTextArea(minLines = 1f, maxLines = 10f)]
		public string summary { get; set; }

		[global::Unity.VisualScripting.Serialize]
		public global::UnityEngine.Vector2 pan { get; set; }

		[global::Unity.VisualScripting.Serialize]
		public float zoom { get; set; } = 1f;

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ISerializationDependency> deserializationDependencies => global::System.Linq.Enumerable.SelectMany(_elements, (global::Unity.VisualScripting.IGraphElement e) => e.deserializationDependencies);

		protected Graph()
		{
			elements = new global::Unity.VisualScripting.MergedGraphElementCollection();
		}

		public override string ToString()
		{
			return global::Unity.VisualScripting.StringUtility.FallbackWhitespace(title, base.ToString());
		}

		public abstract global::Unity.VisualScripting.IGraphData CreateData();

		public virtual global::Unity.VisualScripting.IGraphDebugData CreateDebugData()
		{
			return new global::Unity.VisualScripting.GraphDebugData(this);
		}

		public virtual void Instantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			foreach (global::Unity.VisualScripting.IGraphElement element in elements)
			{
				element.Instantiate(instance);
			}
		}

		public virtual void Uninstantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			foreach (global::Unity.VisualScripting.IGraphElement element in elements)
			{
				element.Uninstantiate(instance);
			}
		}

		public virtual void OnBeforeSerialize()
		{
			_elements.Clear();
			_elements.AddRange(elements);
		}

		public void OnAfterDeserialize()
		{
			global::Unity.VisualScripting.Serialization.AwaitDependencies(this);
		}

		public virtual void OnAfterDependenciesDeserialized()
		{
			elements.Clear();
			global::System.Collections.Generic.List<global::Unity.VisualScripting.IGraphElement> list = global::Unity.VisualScripting.ListPool<global::Unity.VisualScripting.IGraphElement>.New();
			foreach (global::Unity.VisualScripting.IGraphElement element in _elements)
			{
				list.Add(element);
			}
			list.Sort((global::Unity.VisualScripting.IGraphElement a, global::Unity.VisualScripting.IGraphElement b) => a.dependencyOrder.CompareTo(b.dependencyOrder));
			foreach (global::Unity.VisualScripting.IGraphElement item in list)
			{
				try
				{
					if (item.HandleDependencies())
					{
						elements.Add(item);
					}
				}
				catch (global::System.Exception arg)
				{
					global::UnityEngine.Debug.LogWarning($"Failed to add element to graph during deserialization: {item}\n{arg}");
				}
			}
			global::Unity.VisualScripting.ListPool<global::Unity.VisualScripting.IGraphElement>.Free(list);
		}

		public global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			return global::System.Linq.Enumerable.SelectMany(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(elements, (global::Unity.VisualScripting.IGraphElement element) => !visited.Contains(element)), delegate(global::Unity.VisualScripting.IGraphElement element)
			{
				visited.Add(element);
				return element;
			}), (global::Unity.VisualScripting.IGraphElement element) => element.GetAotStubs(visited));
		}

		public void Prewarm()
		{
			if (prewarmed)
			{
				return;
			}
			foreach (global::Unity.VisualScripting.IGraphElement element in elements)
			{
				element.Prewarm();
			}
			prewarmed = true;
		}

		public virtual void Dispose()
		{
			foreach (global::Unity.VisualScripting.IGraphElement element in elements)
			{
				element.Dispose();
			}
		}
	}
}
