namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitSurtitle("Object")]
	public sealed class IsObjectVariableDefined : global::Unity.VisualScripting.IsVariableDefinedUnit, global::Unity.VisualScripting.IObjectVariableUnit, global::Unity.VisualScripting.IVariableUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput source { get; private set; }

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		public IsObjectVariableDefined()
		{
		}

		public IsObjectVariableDefined(string name)
			: base(name)
		{
		}

		protected override void Definition()
		{
			source = ValueInput<global::UnityEngine.GameObject>("source", null).NullMeansSelf();
			base.Definition();
			Requirement(source, base.isDefined);
		}

		protected override global::Unity.VisualScripting.VariableDeclarations GetDeclarations(global::Unity.VisualScripting.Flow flow)
		{
			return global::Unity.VisualScripting.Variables.Object(flow.GetValue<global::UnityEngine.GameObject>(source));
		}
	}
}
