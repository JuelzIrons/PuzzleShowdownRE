namespace Unity.VisualScripting
{
	public abstract class UnitPort<TValidOther, TInvalidOther, TExternalConnection> : global::Unity.VisualScripting.IUnitPort, global::Unity.VisualScripting.IGraphItem where TValidOther : global::Unity.VisualScripting.IUnitPort where TInvalidOther : global::Unity.VisualScripting.IUnitPort where TExternalConnection : global::Unity.VisualScripting.IUnitConnection
	{
		public global::Unity.VisualScripting.IUnit unit { get; set; }

		public string key { get; }

		public global::Unity.VisualScripting.IGraph graph => unit?.graph;

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitRelation> relations => global::System.Linq.Enumerable.Distinct(global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitRelation>(new global::System.Collections.IEnumerable[2]
		{
			unit.relations.WithSource(this),
			unit.relations.WithDestination(this)
		}));

		public abstract global::System.Collections.Generic.IEnumerable<TExternalConnection> validConnections { get; }

		public abstract global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.InvalidConnection> invalidConnections { get; }

		public abstract global::System.Collections.Generic.IEnumerable<TValidOther> validConnectedPorts { get; }

		public abstract global::System.Collections.Generic.IEnumerable<TInvalidOther> invalidConnectedPorts { get; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitConnection> global::Unity.VisualScripting.IUnitPort.validConnections => global::System.Linq.Enumerable.Cast<global::Unity.VisualScripting.IUnitConnection>(validConnections);

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitConnection> connections => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitConnection>(new global::System.Collections.IEnumerable[2] { validConnections, invalidConnections });

		public global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.IUnitPort> connectedPorts => global::Unity.VisualScripting.LinqUtility.Concat<global::Unity.VisualScripting.IUnitPort>(new global::System.Collections.IEnumerable[2] { validConnectedPorts, invalidConnectedPorts });

		public bool hasAnyConnection
		{
			get
			{
				if (!hasValidConnection)
				{
					return hasInvalidConnection;
				}
				return true;
			}
		}

		public virtual bool hasValidConnection => global::System.Linq.Enumerable.Any(validConnections);

		public virtual bool hasInvalidConnection => global::System.Linq.Enumerable.Any(invalidConnections);

		protected UnitPort(string key)
		{
			global::Unity.VisualScripting.Ensure.That("key").IsNotNull(key);
			this.key = key;
		}

		private bool CanConnectTo(global::Unity.VisualScripting.IUnitPort port)
		{
			global::Unity.VisualScripting.Ensure.That("port").IsNotNull(port);
			if (unit != null && port.unit != null && port.unit != unit)
			{
				return port.unit.graph == unit.graph;
			}
			return false;
		}

		public bool CanValidlyConnectTo(global::Unity.VisualScripting.IUnitPort port)
		{
			if (CanConnectTo(port) && port is TValidOther)
			{
				return CanConnectToValid((TValidOther)port);
			}
			return false;
		}

		public bool CanInvalidlyConnectTo(global::Unity.VisualScripting.IUnitPort port)
		{
			if (CanConnectTo(port) && port is TInvalidOther)
			{
				return CanConnectToInvalid((TInvalidOther)port);
			}
			return false;
		}

		public void ValidlyConnectTo(global::Unity.VisualScripting.IUnitPort port)
		{
			global::Unity.VisualScripting.Ensure.That("port").IsNotNull(port);
			if (!(port is TValidOther))
			{
				throw new global::Unity.VisualScripting.InvalidConnectionException();
			}
			ConnectToValid((TValidOther)port);
		}

		public void InvalidlyConnectTo(global::Unity.VisualScripting.IUnitPort port)
		{
			global::Unity.VisualScripting.Ensure.That("port").IsNotNull(port);
			if (!(port is TInvalidOther))
			{
				throw new global::Unity.VisualScripting.InvalidConnectionException();
			}
			ConnectToInvalid((TInvalidOther)port);
		}

		public void Disconnect()
		{
			while (global::System.Linq.Enumerable.Any(validConnectedPorts))
			{
				DisconnectFromValid(global::System.Linq.Enumerable.First(validConnectedPorts));
			}
			while (global::System.Linq.Enumerable.Any(invalidConnectedPorts))
			{
				DisconnectFromInvalid(global::System.Linq.Enumerable.First(invalidConnectedPorts));
			}
		}

		public abstract bool CanConnectToValid(TValidOther port);

		public bool CanConnectToInvalid(TInvalidOther port)
		{
			return true;
		}

		public abstract void ConnectToValid(TValidOther port);

		public abstract void ConnectToInvalid(TInvalidOther port);

		public abstract void DisconnectFromValid(TValidOther port);

		public abstract void DisconnectFromInvalid(TInvalidOther port);

		public abstract global::Unity.VisualScripting.IUnitPort CompatiblePort(global::Unity.VisualScripting.IUnit unit);

		protected void ConnectInvalid(global::Unity.VisualScripting.IUnitOutputPort source, global::Unity.VisualScripting.IUnitInputPort destination)
		{
			if (global::System.Linq.Enumerable.SingleOrDefault(unit.graph.invalidConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.source == source && c.destination == destination) == null)
			{
				unit.graph.invalidConnections.Add(new global::Unity.VisualScripting.InvalidConnection(source, destination));
			}
		}

		protected void DisconnectInvalid(global::Unity.VisualScripting.IUnitOutputPort source, global::Unity.VisualScripting.IUnitInputPort destination)
		{
			global::Unity.VisualScripting.InvalidConnection invalidConnection = global::System.Linq.Enumerable.SingleOrDefault(unit.graph.invalidConnections, (global::Unity.VisualScripting.InvalidConnection c) => c.source == source && c.destination == destination);
			if (invalidConnection != null)
			{
				unit.graph.invalidConnections.Remove(invalidConnection);
			}
		}
	}
}
