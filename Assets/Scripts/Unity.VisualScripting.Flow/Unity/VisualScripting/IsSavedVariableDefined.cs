namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitSurtitle("Save")]
	public sealed class IsSavedVariableDefined : global::Unity.VisualScripting.IsVariableDefinedUnit, global::Unity.VisualScripting.ISavedVariableUnit, global::Unity.VisualScripting.IVariableUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		public IsSavedVariableDefined()
		{
		}

		public IsSavedVariableDefined(string defaultName)
			: base(defaultName)
		{
		}

		protected override global::Unity.VisualScripting.VariableDeclarations GetDeclarations(global::Unity.VisualScripting.Flow flow)
		{
			return global::Unity.VisualScripting.Variables.Saved;
		}
	}
}
