namespace Unity.VisualScripting
{
	public sealed class InvalidConnection : global::Unity.VisualScripting.UnitConnection<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>, global::Unity.VisualScripting.IUnitConnection, global::Unity.VisualScripting.IConnection<global::Unity.VisualScripting.IUnitOutputPort, global::Unity.VisualScripting.IUnitInputPort>, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		public override global::Unity.VisualScripting.IUnitOutputPort source => global::System.Linq.Enumerable.Single(base.sourceUnit.outputs, (global::Unity.VisualScripting.IUnitOutputPort p) => p.key == base.sourceKey);

		public override global::Unity.VisualScripting.IUnitInputPort destination => global::System.Linq.Enumerable.Single(base.destinationUnit.inputs, (global::Unity.VisualScripting.IUnitInputPort p) => p.key == base.destinationKey);

		public global::Unity.VisualScripting.IUnitOutputPort validSource => global::System.Linq.Enumerable.Single(base.sourceUnit.validOutputs, (global::Unity.VisualScripting.IUnitOutputPort p) => p.key == base.sourceKey);

		public global::Unity.VisualScripting.IUnitInputPort validDestination => global::System.Linq.Enumerable.Single(base.destinationUnit.validInputs, (global::Unity.VisualScripting.IUnitInputPort p) => p.key == base.destinationKey);

		public override bool sourceExists => global::System.Linq.Enumerable.Any(base.sourceUnit.outputs, (global::Unity.VisualScripting.IUnitOutputPort p) => p.key == base.sourceKey);

		public override bool destinationExists => global::System.Linq.Enumerable.Any(base.destinationUnit.inputs, (global::Unity.VisualScripting.IUnitInputPort p) => p.key == base.destinationKey);

		public bool validSourceExists => global::System.Linq.Enumerable.Any(base.sourceUnit.validOutputs, (global::Unity.VisualScripting.IUnitOutputPort p) => p.key == base.sourceKey);

		public bool validDestinationExists => global::System.Linq.Enumerable.Any(base.destinationUnit.validInputs, (global::Unity.VisualScripting.IUnitInputPort p) => p.key == base.destinationKey);

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnitConnection.graph => base.graph;

		[global::System.Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public InvalidConnection()
		{
		}

		public InvalidConnection(global::Unity.VisualScripting.IUnitOutputPort source, global::Unity.VisualScripting.IUnitInputPort destination)
			: base(source, destination)
		{
		}

		public override void AfterRemove()
		{
			base.AfterRemove();
			source.unit.RemoveUnconnectedInvalidPorts();
			destination.unit.RemoveUnconnectedInvalidPorts();
		}

		public override bool HandleDependencies()
		{
			if (validSourceExists && validDestinationExists && validSource.CanValidlyConnectTo(validDestination))
			{
				validSource.ValidlyConnectTo(validDestination);
				return false;
			}
			if (!sourceExists)
			{
				base.sourceUnit.invalidOutputs.Add(new global::Unity.VisualScripting.InvalidOutput(base.sourceKey));
			}
			if (!destinationExists)
			{
				base.destinationUnit.invalidInputs.Add(new global::Unity.VisualScripting.InvalidInput(base.destinationKey));
			}
			return true;
		}
	}
}
