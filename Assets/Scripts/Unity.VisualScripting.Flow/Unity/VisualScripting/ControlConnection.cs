namespace Unity.VisualScripting
{
	public sealed class ControlConnection : global::Unity.VisualScripting.UnitConnection<global::Unity.VisualScripting.ControlOutput, global::Unity.VisualScripting.ControlInput>, global::Unity.VisualScripting.IUnitConnection, global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		public override global::Unity.VisualScripting.ControlOutput source => base.sourceUnit.controlOutputs[base.sourceKey];

		public override global::Unity.VisualScripting.ControlInput destination => base.destinationUnit.controlInputs[base.destinationKey];

		global::Unity.VisualScripting.IUnitOutputPort global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>.source => source;

		global::Unity.VisualScripting.IUnitInputPort global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>.destination => destination;

		public override bool sourceExists => base.sourceUnit.controlOutputs.Contains(base.sourceKey);

		public override bool destinationExists => base.destinationUnit.controlInputs.Contains(base.destinationKey);

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnitConnection.graph => base.graph;

		[global::System.Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public ControlConnection()
		{
		}

		public ControlConnection(global::Unity.VisualScripting.ControlOutput source, global::Unity.VisualScripting.ControlInput destination)
			: base(source, destination)
		{
			if (source.hasValidConnection)
			{
				throw new global::Unity.VisualScripting.InvalidConnectionException("Control output ports do not support multiple connections.");
			}
		}
	}
}
