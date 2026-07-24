namespace Unity.VisualScripting
{
	public sealed class ValueOutput : global::Unity.VisualScripting.UnitPort<global::Unity.VisualScripting.ValueInput, global::Unity.VisualScripting.IUnitInputPort, global::Unity.VisualScripting.ValueConnection>, global::Unity.VisualScripting.IUnitValuePort, global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.IUnitOutputPort
	{
		internal readonly global::System.Func<global::Unity.VisualScripting.Flow, object> getValue;

		internal global::System.Func<global::Unity.VisualScripting.Flow, bool> canPredictValue;

		public bool supportsPrediction => canPredictValue != null;

		public bool supportsFetch => getValue != null;

		public global::System.Type type { get; }

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ValueConnection> validConnections => base.unit?.graph?.valueConnections.WithSource(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.ValueConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> invalidConnections => base.unit?.graph?.invalidConnections.WithSource(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.InvalidConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ValueInput> validConnectedPorts => global::System.Linq.Enumerable.Select(validConnections, (global::Unity.VisualScripting.ValueConnection c) => c.destination);

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitInputPort> invalidConnectedPorts => global::System.Linq.Enumerable.Select(invalidConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.destination);

		public ValueOutput(string key, global::System.Type type, global::System.Func<global::Unity.VisualScripting.Flow, object> getValue)
			: base(key)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::Unity.VisualScripting.Ensure.That("getValue").IsNotNull(getValue);
			this.type = type;
			this.getValue = getValue;
		}

		public ValueOutput(string key, global::System.Type type)
			: base(key)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			this.type = type;
		}

		public override bool CanConnectToValid(global::Unity.VisualScripting.ValueInput port)
		{
			return type.IsConvertibleTo(port.type, guaranteed: false);
		}

		public override void ConnectToValid(global::Unity.VisualScripting.ValueInput port)
		{
			port.Disconnect();
			base.unit.graph.valueConnections.Add(new global::Unity.VisualScripting.ValueConnection(this, port));
		}

		public override void ConnectToInvalid(global::Unity.VisualScripting.IUnitInputPort port)
		{
			ConnectInvalid(this, port);
		}

		public override void DisconnectFromValid(global::Unity.VisualScripting.ValueInput port)
		{
			global::Unity.VisualScripting.ValueConnection valueConnection = global::System.Linq.Enumerable.SingleOrDefault(validConnections, (global::Unity.VisualScripting.ValueConnection c) => c.destination == port);
			if (valueConnection != null)
			{
				base.unit.graph.valueConnections.Remove(valueConnection);
			}
		}

		public override void DisconnectFromInvalid(global::Unity.VisualScripting.IUnitInputPort port)
		{
			DisconnectInvalid(this, port);
		}

		public global::Unity.VisualScripting.ValueOutput PredictableIf(global::System.Func<global::Unity.VisualScripting.Flow, bool> condition)
		{
			global::Unity.VisualScripting.Ensure.That("condition").IsNotNull(condition);
			canPredictValue = condition;
			return this;
		}

		public global::Unity.VisualScripting.ValueOutput Predictable()
		{
			canPredictValue = (global::Unity.VisualScripting.Flow flow) => true;
			return this;
		}

		public override global::Unity.VisualScripting.IUnitPort CompatiblePort(global::Unity.VisualScripting.IUnit unit)
		{
			if (unit == base.unit)
			{
				return null;
			}
			return unit.CompatibleValueInput(type);
		}
	}
}
