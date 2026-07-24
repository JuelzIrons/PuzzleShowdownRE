namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SpecialUnit]
	[global::System.Obsolete("Use the new unified variable nodes instead.")]
	public abstract class VariableUnit : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IVariableUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public string defaultName { get; } = string.Empty;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput name { get; private set; }

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected VariableUnit()
		{
		}

		protected VariableUnit(string defaultName)
		{
			global::Unity.VisualScripting.Ensure.That("defaultName").IsNotNull(defaultName);
			this.defaultName = defaultName;
		}

		protected abstract global::Unity.VisualScripting.VariableDeclarations GetDeclarations(global::Unity.VisualScripting.Flow flow);

		protected override void Definition()
		{
			name = ValueInput("name", defaultName);
		}
	}
}
