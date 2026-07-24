namespace Unity.VisualScripting
{
	public interface IUnitPort : global::Unity.VisualScripting.IGraphItem
	{
		global::Unity.VisualScripting.IUnit unit { get; set; }

		string key { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitRelation> relations { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitConnection> validConnections { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> invalidConnections { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitConnection> connections { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitPort> connectedPorts { get; }

		bool hasAnyConnection { get; }

		bool hasValidConnection { get; }

		bool hasInvalidConnection { get; }

		bool CanInvalidlyConnectTo(global::Unity.VisualScripting.IUnitPort port);

		bool CanValidlyConnectTo(global::Unity.VisualScripting.IUnitPort port);

		void InvalidlyConnectTo(global::Unity.VisualScripting.IUnitPort port);

		void ValidlyConnectTo(global::Unity.VisualScripting.IUnitPort port);

		void Disconnect();

		global::Unity.VisualScripting.IUnitPort CompatiblePort(global::Unity.VisualScripting.IUnit unit);
	}
}
