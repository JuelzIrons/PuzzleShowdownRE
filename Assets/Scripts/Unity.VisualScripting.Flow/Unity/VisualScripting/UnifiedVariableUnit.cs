namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SpecialUnit]
	public abstract class UnifiedVariableUnit : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IUnifiedVariableUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		public global::Unity.VisualScripting.VariableKind kind { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput name { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput @object { get; private set; }

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected override void Definition()
		{
			name = ValueInput("name", string.Empty);
			if (kind == global::Unity.VisualScripting.VariableKind.Object)
			{
				@object = ValueInput<global::UnityEngine.GameObject>("object", null).NullMeansSelf();
			}
		}
	}
}
