namespace Unity.VisualScripting
{
	public interface IUnit : global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		new global::Unity.VisualScripting.FlowGraph graph { get; }

		bool canDefine { get; }

		bool isDefined { get; }

		bool failedToDefine { get; }

		global::System.Exception definitionException { get; }

		global::System.Collections.Generic.Dictionary<string, object> defaultValues { get; }

		global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.ControlInput> controlInputs { get; }

		global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.ControlOutput> controlOutputs { get; }

		global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.ValueInput> valueInputs { get; }

		global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.ValueOutput> valueOutputs { get; }

		global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.InvalidInput> invalidInputs { get; }

		global::Unity.VisualScripting.IUnitPortCollection<global::Unity.VisualScripting.InvalidOutput> invalidOutputs { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitInputPort> inputs { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitOutputPort> outputs { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitInputPort> validInputs { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitOutputPort> validOutputs { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitPort> ports { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitPort> invalidPorts { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitPort> validPorts { get; }

		global::Unity.VisualScripting.IConnectionCollection<global::Unity.VisualScripting.IUnitRelation, global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IUnitPort> relations { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitConnection> connections { get; }

		bool isControlRoot { get; }

		global::UnityEngine.Vector2 position { get; set; }

		event global::System.Action onPortsChanged;

		void Define();

		void EnsureDefined();

		void RemoveUnconnectedInvalidPorts();

		void PortsChanged();
	}
}
