namespace Unity.VisualScripting
{
	public sealed class ValueConnection : global::Unity.VisualScripting.UnitConnection<global::Unity.VisualScripting.ValueOutput, global::Unity.VisualScripting.ValueInput>, global::Unity.VisualScripting.IUnitConnection, global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		public class DebugData : global::Unity.VisualScripting.UnitConnectionDebugData
		{
			public object lastValue { get; set; }

			public bool assignedLastValue { get; set; }
		}

		public override global::Unity.VisualScripting.ValueOutput source => base.sourceUnit.valueOutputs[base.sourceKey];

		public override global::Unity.VisualScripting.ValueInput destination => base.destinationUnit.valueInputs[base.destinationKey];

		global::Unity.VisualScripting.IUnitOutputPort global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>.source => source;

		global::Unity.VisualScripting.IUnitInputPort global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>.destination => destination;

		public override bool sourceExists => base.sourceUnit.valueOutputs.Contains(base.sourceKey);

		public override bool destinationExists => base.destinationUnit.valueInputs.Contains(base.destinationKey);

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnitConnection.graph => base.graph;

		public override global::Unity.VisualScripting.IGraphElementDebugData CreateDebugData()
		{
			return new global::Unity.VisualScripting.ValueConnection.DebugData();
		}

		[global::System.Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public ValueConnection()
		{
		}

		public ValueConnection(global::Unity.VisualScripting.ValueOutput source, global::Unity.VisualScripting.ValueInput destination)
			: base(source, destination)
		{
			if (destination.hasValidConnection)
			{
				throw new global::Unity.VisualScripting.InvalidConnectionException("Value input ports do not support multiple connections.");
			}
			if (!source.type.IsConvertibleTo(destination.type, guaranteed: false))
			{
				throw new global::Unity.VisualScripting.InvalidConnectionException($"Cannot convert from '{source.type}' to '{destination.type}'.");
			}
		}
	}
}
