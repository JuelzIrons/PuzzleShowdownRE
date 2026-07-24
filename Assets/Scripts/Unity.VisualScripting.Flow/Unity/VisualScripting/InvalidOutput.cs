namespace Unity.VisualScripting
{
	public sealed class InvalidOutput : global::Unity.VisualScripting.UnitPort<global::Unity.VisualScripting.IUnitInputPort, global::Unity.VisualScripting.IUnitInputPort, global::Unity.VisualScripting.InvalidConnection>, global::Unity.VisualScripting.IUnitInvalidPort, global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.IUnitOutputPort
	{
		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> validConnections => base.unit?.graph?.invalidConnections.WithSource(this) ?? global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.InvalidConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> invalidConnections => global::System.Linq.Enumerable.Empty<global::Unity.VisualScripting.InvalidConnection>();

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitInputPort> validConnectedPorts => global::System.Linq.Enumerable.Select(validConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.destination);

		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitInputPort> invalidConnectedPorts => global::System.Linq.Enumerable.Select(invalidConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.destination);

		public InvalidOutput(string key)
			: base(key)
		{
		}

		public override bool CanConnectToValid(global::Unity.VisualScripting.IUnitInputPort port)
		{
			return false;
		}

		public override void ConnectToValid(global::Unity.VisualScripting.IUnitInputPort port)
		{
			ConnectInvalid(this, port);
		}

		public override void ConnectToInvalid(global::Unity.VisualScripting.IUnitInputPort port)
		{
			ConnectInvalid(this, port);
		}

		public override void DisconnectFromValid(global::Unity.VisualScripting.IUnitInputPort port)
		{
			DisconnectInvalid(this, port);
		}

		public override void DisconnectFromInvalid(global::Unity.VisualScripting.IUnitInputPort port)
		{
			DisconnectInvalid(this, port);
		}

		public override global::Unity.VisualScripting.IUnitPort CompatiblePort(global::Unity.VisualScripting.IUnit unit)
		{
			return null;
		}
	}
}
