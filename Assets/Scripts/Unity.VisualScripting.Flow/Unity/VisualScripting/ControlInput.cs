namespace Unity.VisualScripting
{
	public sealed class ControlInput : global::Unity.VisualScripting.UnitPort<global::Unity.VisualScripting.ControlOutput, global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.ControlConnection>, global::Unity.VisualScripting.IUnitControlPort, global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.IUnitInputPort
	{
		internal readonly global::System.Func<global::Unity.VisualScripting.Flow, global::Unity.VisualScripting.ControlOutput> action;

		internal readonly global::System.Func<global::Unity.VisualScripting.Flow, global::System.Collections.IEnumerator> coroutineAction;

		public bool supportsCoroutine => coroutineAction != null;

		public bool requiresCoroutine => action == null;

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ControlConnection> validConnections => base.unit?.graph?.controlConnections.WithDestination(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.ControlConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> invalidConnections => base.unit?.graph?.invalidConnections.WithDestination(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.InvalidConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ControlOutput> validConnectedPorts => global::System.Linq.Enumerable.Select(validConnections, (global::Unity.VisualScripting.ControlConnection c) => c.source);

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitOutputPort> invalidConnectedPorts => global::System.Linq.Enumerable.Select(invalidConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.source);

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
				if (!hasValidConnection)
				{
					return false;
				}
				return global::System.Linq.Enumerable.Any(validConnectedPorts, (global::Unity.VisualScripting.ControlOutput cop) => cop.couldBeEntered);
			}
		}

		public ControlInput(string key, global::System.Func<global::Unity.VisualScripting.Flow, global::Unity.VisualScripting.ControlOutput> action)
			: base(key)
		{
			global::Unity.VisualScripting.Ensure.That("action").IsNotNull(action);
			this.action = action;
		}

		public ControlInput(string key, global::System.Func<global::Unity.VisualScripting.Flow, global::System.Collections.IEnumerator> coroutineAction)
			: base(key)
		{
			global::Unity.VisualScripting.Ensure.That("coroutineAction").IsNotNull(coroutineAction);
			this.coroutineAction = coroutineAction;
		}

		public ControlInput(string key, global::System.Func<global::Unity.VisualScripting.Flow, global::Unity.VisualScripting.ControlOutput> action, global::System.Func<global::Unity.VisualScripting.Flow, global::System.Collections.IEnumerator> coroutineAction)
			: base(key)
		{
			global::Unity.VisualScripting.Ensure.That("action").IsNotNull(action);
			global::Unity.VisualScripting.Ensure.That("coroutineAction").IsNotNull(coroutineAction);
			this.action = action;
			this.coroutineAction = coroutineAction;
		}

		public bool IsPredictable(global::Unity.VisualScripting.Recursion recursion)
		{
			if (!hasValidConnection)
			{
				return true;
			}
			global::Unity.VisualScripting.Recursion recursion2 = recursion;
			if (recursion2 != null && !recursion2.TryEnter(this))
			{
				return false;
			}
			bool result = global::System.Linq.Enumerable.All(validConnectedPorts, (global::Unity.VisualScripting.ControlOutput cop) => cop.IsPredictable(recursion));
			global::Unity.VisualScripting.Recursion recursion3 = recursion;
			if (recursion3 != null)
			{
				recursion3.Exit(this);
				return result;
			}
			return result;
		}

		public override bool CanConnectToValid(global::Unity.VisualScripting.ControlOutput port)
		{
			return true;
		}

		public override void ConnectToValid(global::Unity.VisualScripting.ControlOutput port)
		{
			port.Disconnect();
			base.unit.graph.controlConnections.Add(new global::Unity.VisualScripting.ControlConnection(port, this));
		}

		public override void ConnectToInvalid(global::Unity.VisualScripting.IUnitOutputPort port)
		{
			ConnectInvalid(port, this);
		}

		public override void DisconnectFromValid(global::Unity.VisualScripting.ControlOutput port)
		{
			global::Unity.VisualScripting.ControlConnection controlConnection = global::System.Linq.Enumerable.SingleOrDefault(validConnections, (global::Unity.VisualScripting.ControlConnection c) => c.source == port);
			if (controlConnection != null)
			{
				base.unit.graph.controlConnections.Remove(controlConnection);
			}
		}

		public override void DisconnectFromInvalid(global::Unity.VisualScripting.IUnitOutputPort port)
		{
			DisconnectInvalid(port, this);
		}

		public override global::Unity.VisualScripting.IUnitPort CompatiblePort(global::Unity.VisualScripting.IUnit unit)
		{
			if (unit == base.unit)
			{
				return null;
			}
			return global::System.Linq.Enumerable.FirstOrDefault(unit.controlOutputs);
		}
	}
}
