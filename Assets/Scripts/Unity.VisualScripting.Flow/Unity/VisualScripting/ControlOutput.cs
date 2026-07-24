namespace Unity.VisualScripting
{
	public sealed class ControlOutput : global::Unity.VisualScripting.UnitPort<global::Unity.VisualScripting.ControlInput, global::Unity.VisualScripting.IUnitInputPort, global::Unity.VisualScripting.ControlConnection>, global::Unity.VisualScripting.IUnitControlPort, global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.IUnitOutputPort
	{
		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ControlConnection> validConnections => base.unit?.graph?.controlConnections.WithSource(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.ControlConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> invalidConnections => base.unit?.graph?.invalidConnections.WithSource(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.InvalidConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ControlInput> validConnectedPorts => global::System.Linq.Enumerable.Select(validConnections, (global::Unity.VisualScripting.ControlConnection c) => c.destination);

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitInputPort> invalidConnectedPorts => global::System.Linq.Enumerable.Select(invalidConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.destination);

		public bool isPredictable
		{
			get
			{
				using global::Unity.VisualScripting.Recursion recursion = global::Unity.VisualScripting.Recursion.New(1);
				return IsPredictable(recursion);
			}
		}

		public bool couldBeEntered
		{
			get
			{
				if (!isPredictable)
				{
					throw new global::System.NotSupportedException();
				}
				if (base.unit.isControlRoot)
				{
					return true;
				}
				return global::System.Linq.Enumerable.Any(global::System.Linq.Enumerable.Where(base.unit.relations.WithDestination(this), (global::Unity.VisualScripting.IUnitRelation r) => r.source is global::Unity.VisualScripting.ControlInput), (global::Unity.VisualScripting.IUnitRelation r) => ((global::Unity.VisualScripting.ControlInput)r.source).couldBeEntered);
			}
		}

		public global::Unity.VisualScripting.ControlConnection connection => base.unit.graph?.controlConnections.SingleOrDefaultWithSource(this);

		public override bool hasValidConnection => connection != null;

		public ControlOutput(string key)
			: base(key)
		{
		}

		public bool IsPredictable(global::Unity.VisualScripting.Recursion recursion)
		{
			if (base.unit.isControlRoot)
			{
				return true;
			}
			global::Unity.VisualScripting.Recursion recursion2 = recursion;
			if (recursion2 != null && !recursion2.TryEnter(this))
			{
				return false;
			}
			bool result = global::System.Linq.Enumerable.All(global::System.Linq.Enumerable.Where(base.unit.relations.WithDestination(this), (global::Unity.VisualScripting.IUnitRelation r) => r.source is global::Unity.VisualScripting.ControlInput), (global::Unity.VisualScripting.IUnitRelation r) => ((global::Unity.VisualScripting.ControlInput)r.source).IsPredictable(recursion));
			global::Unity.VisualScripting.Recursion recursion3 = recursion;
			if (recursion3 != null)
			{
				recursion3.Exit(this);
				return result;
			}
			return result;
		}

		public override bool CanConnectToValid(global::Unity.VisualScripting.ControlInput port)
		{
			return true;
		}

		public override void ConnectToValid(global::Unity.VisualScripting.ControlInput port)
		{
			Disconnect();
			base.unit.graph.controlConnections.Add(new global::Unity.VisualScripting.ControlConnection(this, port));
		}

		public override void ConnectToInvalid(global::Unity.VisualScripting.IUnitInputPort port)
		{
			ConnectInvalid(this, port);
		}

		public override void DisconnectFromValid(global::Unity.VisualScripting.ControlInput port)
		{
			global::Unity.VisualScripting.ControlConnection controlConnection = global::System.Linq.Enumerable.SingleOrDefault(validConnections, (global::Unity.VisualScripting.ControlConnection c) => c.destination == port);
			if (controlConnection != null)
			{
				base.unit.graph.controlConnections.Remove(controlConnection);
			}
		}

		public override void DisconnectFromInvalid(global::Unity.VisualScripting.IUnitInputPort port)
		{
			DisconnectInvalid(this, port);
		}

		public override global::Unity.VisualScripting.IUnitPort CompatiblePort(global::Unity.VisualScripting.IUnit unit)
		{
			if (unit == base.unit)
			{
				return null;
			}
			return global::System.Linq.Enumerable.FirstOrDefault(unit.controlInputs);
		}
	}
}
