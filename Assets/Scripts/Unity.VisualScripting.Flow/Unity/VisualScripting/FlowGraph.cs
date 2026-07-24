namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	[global::System.ComponentModel.DisplayName("Script Graph")]
	public sealed class FlowGraph : global::Unity.VisualScripting.Graph, global::Unity.VisualScripting.IGraphWithVariables, global::Unity.VisualScripting.IGraph, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.ISerializationDepender, global::UnityEngine.ISerializationCallbackReceiver, global::Unity.VisualScripting.IGraphEventListener
	{
		private const string DefinitionRemoveWarningTitle = "Remove Port Definition";

		private const string DefinitionRemoveWarningMessage = "Removing this definition will break any existing connection to this port. Are you sure you want to continue?";

		[global::Unity.VisualScripting.Serialize]
		public global::Unity.VisualScripting.VariableDeclarations variables { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.IUnit> units { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphConnectionCollection<global::Unity.VisualScripting.ControlConnection, global::Unity.VisualScripting.ControlOutput, global::Unity.VisualScripting.ControlInput> controlConnections { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphConnectionCollection<global::Unity.VisualScripting.ValueConnection, global::Unity.VisualScripting.ValueOutput, global::Unity.VisualScripting.ValueInput> valueConnections { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphConnectionCollection<global::Unity.VisualScripting.InvalidConnection, global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort> invalidConnections { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.GraphGroup> groups { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.StickyNote> sticky { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectorLabel("Trigger Inputs")]
		[global::Unity.VisualScripting.InspectorWide(true)]
		[global::Unity.VisualScripting.WarnBeforeRemoving("Remove Port Definition", "Removing this definition will break any existing connection to this port. Are you sure you want to continue?")]
		public global::Unity.VisualScripting.UnitPortDefinitionCollection<global::Unity.VisualScripting.ControlInputDefinition> controlInputDefinitions { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectorLabel("Trigger Outputs")]
		[global::Unity.VisualScripting.InspectorWide(true)]
		[global::Unity.VisualScripting.WarnBeforeRemoving("Remove Port Definition", "Removing this definition will break any existing connection to this port. Are you sure you want to continue?")]
		public global::Unity.VisualScripting.UnitPortDefinitionCollection<global::Unity.VisualScripting.ControlOutputDefinition> controlOutputDefinitions { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectorLabel("Data Inputs")]
		[global::Unity.VisualScripting.InspectorWide(true)]
		[global::Unity.VisualScripting.WarnBeforeRemoving("Remove Port Definition", "Removing this definition will break any existing connection to this port. Are you sure you want to continue?")]
		public global::Unity.VisualScripting.UnitPortDefinitionCollection<global::Unity.VisualScripting.ValueInputDefinition> valueInputDefinitions { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectorLabel("Data Outputs")]
		[global::Unity.VisualScripting.InspectorWide(true)]
		[global::Unity.VisualScripting.WarnBeforeRemoving("Remove Port Definition", "Removing this definition will break any existing connection to this port. Are you sure you want to continue?")]
		public global::Unity.VisualScripting.UnitPortDefinitionCollection<global::Unity.VisualScripting.ValueOutputDefinition> valueOutputDefinitions { get; private set; }

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitPortDefinition> validPortDefinitions => global::System.Linq.Enumerable.Where(global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitPortDefinition>(new global::System.Collections.IEnumerable[4] { controlInputDefinitions, controlOutputDefinitions, valueInputDefinitions, valueOutputDefinitions }), (global::Unity.VisualScripting.IUnitPortDefinition upd) => upd.isValid).DistinctBy((global::Unity.VisualScripting.IUnitPortDefinition upd) => upd.key);

		public event global::System.Action onPortDefinitionsChanged;

		public FlowGraph()
		{
			units = new global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.IUnit>(this);
			controlConnections = new global::Unity.VisualScripting.GraphConnectionCollection<global::Unity.VisualScripting.ControlConnection, global::Unity.VisualScripting.ControlOutput, global::Unity.VisualScripting.ControlInput>(this);
			valueConnections = new global::Unity.VisualScripting.GraphConnectionCollection<global::Unity.VisualScripting.ValueConnection, global::Unity.VisualScripting.ValueOutput, global::Unity.VisualScripting.ValueInput>(this);
			invalidConnections = new global::Unity.VisualScripting.GraphConnectionCollection<global::Unity.VisualScripting.InvalidConnection, global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>(this);
			groups = new global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.GraphGroup>(this);
			sticky = new global::Unity.VisualScripting.GraphElementCollection<global::Unity.VisualScripting.StickyNote>(this);
			base.elements.Include(units);
			base.elements.Include(controlConnections);
			base.elements.Include(valueConnections);
			base.elements.Include(invalidConnections);
			base.elements.Include(groups);
			base.elements.Include(sticky);
			controlInputDefinitions = new global::Unity.VisualScripting.UnitPortDefinitionCollection<global::Unity.VisualScripting.ControlInputDefinition>();
			controlOutputDefinitions = new global::Unity.VisualScripting.UnitPortDefinitionCollection<global::Unity.VisualScripting.ControlOutputDefinition>();
			valueInputDefinitions = new global::Unity.VisualScripting.UnitPortDefinitionCollection<global::Unity.VisualScripting.ValueInputDefinition>();
			valueOutputDefinitions = new global::Unity.VisualScripting.UnitPortDefinitionCollection<global::Unity.VisualScripting.ValueOutputDefinition>();
			variables = new global::Unity.VisualScripting.VariableDeclarations();
		}

		public override global::Unity.VisualScripting.IGraphData CreateData()
		{
			return new global::Unity.VisualScripting.FlowGraphData(this);
		}

		public void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			stack.GetGraphData<global::Unity.VisualScripting.FlowGraphData>().isListening = true;
			foreach (global::Unity.VisualScripting.IUnit unit in units)
			{
				(unit as global::Unity.VisualScripting.IGraphEventListener)?.StartListening(stack);
			}
		}

		public void StopListening(global::Unity.VisualScripting.GraphStack stack)
		{
			foreach (global::Unity.VisualScripting.IUnit unit in units)
			{
				(unit as global::Unity.VisualScripting.IGraphEventListener)?.StopListening(stack);
			}
			stack.GetGraphData<global::Unity.VisualScripting.FlowGraphData>().isListening = false;
		}

		public bool IsListening(global::Unity.VisualScripting.GraphPointer pointer)
		{
			return pointer.GetGraphData<global::Unity.VisualScripting.FlowGraphData>().isListening;
		}

		public global::System.Collections.Generic.IEnumerable<string> GetDynamicVariableNames(global::Unity.VisualScripting.VariableKind kind, global::Unity.VisualScripting.GraphReference reference)
		{
			return global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.Distinct(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.OfType<global::Unity.VisualScripting.IUnifiedVariableUnit>(units), (global::Unity.VisualScripting.IUnifiedVariableUnit v) => v.kind == kind && global::Unity.VisualScripting.Flow.CanPredict(v.name, reference)), (global::Unity.VisualScripting.IUnifiedVariableUnit v) => global::Unity.VisualScripting.Flow.Predict<string>(v.name, reference)), (string name) => !global::Unity.VisualScripting.StringUtility.IsNullOrWhiteSpace(name))), (string name) => name);
		}

		public void PortDefinitionsChanged()
		{
			this.onPortDefinitionsChanged?.Invoke();
		}

		public static global::Unity.VisualScripting.FlowGraph WithInputOutput()
		{
			return new global::Unity.VisualScripting.FlowGraph
			{
				units = 
				{
					(global::Unity.VisualScripting.IUnit)new global::Unity.VisualScripting.GraphInput
					{
						position = new global::UnityEngine.Vector2(-250f, -30f)
					},
					(global::Unity.VisualScripting.IUnit)new global::Unity.VisualScripting.GraphOutput
					{
						position = new global::UnityEngine.Vector2(105f, -30f)
					}
				}
			};
		}

		public static global::Unity.VisualScripting.FlowGraph WithStartUpdate()
		{
			return new global::Unity.VisualScripting.FlowGraph
			{
				units = 
				{
					(global::Unity.VisualScripting.IUnit)new global::Unity.VisualScripting.Start
					{
						position = new global::UnityEngine.Vector2(-204f, -144f)
					},
					(global::Unity.VisualScripting.IUnit)new global::Unity.VisualScripting.Update
					{
						position = new global::UnityEngine.Vector2(-204f, 60f)
					}
				}
			};
		}
	}
}
