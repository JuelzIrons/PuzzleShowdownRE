namespace Unity.VisualScripting
{
	public abstract class UnitConnection<TSourcePort, TDestinationPort> : global::Unity.VisualScripting.GraphElement<global::Unity.VisualScripting.FlowGraph>, global::Unity.VisualScripting.IConnection<TSourcePort, TDestinationPort> where TSourcePort : class, global::Unity.VisualScripting.IUnitOutputPort where TDestinationPort : class, global::Unity.VisualScripting.IUnitInputPort
	{
		[global::Unity.VisualScripting.Serialize]
		protected global::Unity.VisualScripting.IUnit sourceUnit { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		protected string sourceKey { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		protected global::Unity.VisualScripting.IUnit destinationUnit { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		protected string destinationKey { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public abstract TSourcePort source { get; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public abstract TDestinationPort destination { get; }

		public override int dependencyOrder => 1;

		public abstract bool sourceExists { get; }

		public abstract bool destinationExists { get; }

		[global::System.Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		protected UnitConnection()
		{
		}

		protected UnitConnection(TSourcePort source, TDestinationPort destination)
		{
			global::Unity.VisualScripting.Ensure.That("source").IsNotNull(source);
			global::Unity.VisualScripting.Ensure.That("destination").IsNotNull(destination);
			if (source.unit.graph != destination.unit.graph)
			{
				throw new global::System.NotSupportedException("Cannot create connections across graphs.");
			}
			if (source.unit == destination.unit)
			{
				throw new global::Unity.VisualScripting.InvalidConnectionException("Cannot create connections on the same unit.");
			}
			sourceUnit = source.unit;
			sourceKey = source.key;
			destinationUnit = destination.unit;
			destinationKey = destination.key;
		}

		public virtual global::Unity.VisualScripting.IGraphElementDebugData CreateDebugData()
		{
			return new global::Unity.VisualScripting.UnitConnectionDebugData();
		}

		protected void CopyFrom(global::Unity.VisualScripting.UnitConnection<TSourcePort, TDestinationPort> source)
		{
			CopyFrom((global::Unity.VisualScripting.GraphElement<global::Unity.VisualScripting.FlowGraph>)source);
		}

		public override bool HandleDependencies()
		{
			bool flag = true;
			global::Unity.VisualScripting.IUnitOutputPort unitOutputPort;
			if (!sourceExists)
			{
				if (!sourceUnit.invalidOutputs.Contains(sourceKey))
				{
					sourceUnit.invalidOutputs.Add(new global::Unity.VisualScripting.InvalidOutput(sourceKey));
				}
				unitOutputPort = sourceUnit.invalidOutputs[sourceKey];
				flag = false;
			}
			else
			{
				unitOutputPort = source;
			}
			global::Unity.VisualScripting.IUnitInputPort unitInputPort;
			if (!destinationExists)
			{
				if (!destinationUnit.invalidInputs.Contains(destinationKey))
				{
					destinationUnit.invalidInputs.Add(new global::Unity.VisualScripting.InvalidInput(destinationKey));
				}
				unitInputPort = destinationUnit.invalidInputs[destinationKey];
				flag = false;
			}
			else
			{
				unitInputPort = destination;
			}
			if (!unitOutputPort.CanValidlyConnectTo(unitInputPort))
			{
				flag = false;
			}
			if (!flag && unitOutputPort.CanInvalidlyConnectTo(unitInputPort))
			{
				unitOutputPort.InvalidlyConnectTo(unitInputPort);
				if (unitOutputPort.unit.GetType() != typeof(global::Unity.VisualScripting.MissingType) && unitInputPort.unit.GetType() != typeof(global::Unity.VisualScripting.MissingType))
				{
					global::UnityEngine.Debug.LogWarning($"Could not load connection between '{unitOutputPort.key}' of '{sourceUnit}' and '{unitInputPort.key}' of '{destinationUnit}'.");
				}
			}
			return flag;
		}

		public override global::Unity.VisualScripting.AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			return null;
		}
	}
}
