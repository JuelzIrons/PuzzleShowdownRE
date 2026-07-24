namespace Unity.VisualScripting
{
	public sealed class InvalidInput : global::Unity.VisualScripting.UnitPort<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.InvalidConnection>, global::Unity.VisualScripting.IUnitInvalidPort, global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.IUnitInputPort
	{
		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> validConnections => base.unit?.graph?.invalidConnections.WithDestination(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.InvalidConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> invalidConnections => global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.InvalidConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitOutputPort> validConnectedPorts => global::System.Linq.Enumerable.Select(validConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.source);

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitOutputPort> invalidConnectedPorts => global::System.Linq.Enumerable.Select(invalidConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.source);

		public InvalidInput(string key)
			: base(key)
		{
		}

		public override bool CanConnectToValid(global::Unity.VisualScripting.IUnitOutputPort port)
		{
			return false;
		}

		public override void ConnectToValid(global::Unity.VisualScripting.IUnitOutputPort port)
		{
			ConnectInvalid(port, this);
		}

		public override void ConnectToInvalid(global::Unity.VisualScripting.IUnitOutputPort port)
		{
			ConnectInvalid(port, this);
		}

		public override void DisconnectFromValid(global::Unity.VisualScripting.IUnitOutputPort port)
		{
			DisconnectInvalid(port, this);
		}

		public override void DisconnectFromInvalid(global::Unity.VisualScripting.IUnitOutputPort port)
		{
			DisconnectInvalid(port, this);
		}

		public override global::Unity.VisualScripting.IUnitPort CompatiblePort(global::Unity.VisualScripting.IUnit unit)
		{
			return null;
		}
	}
}
