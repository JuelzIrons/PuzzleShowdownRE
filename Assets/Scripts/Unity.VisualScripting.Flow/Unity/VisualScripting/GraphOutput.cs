namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Nesting")]
	[global::Unity.VisualScripting.UnitOrder(2)]
	[global::Unity.VisualScripting.UnitTitle("Output")]
	public sealed class GraphOutput : global::Unity.VisualScripting.Unit
	{
		public override bool canDefine => base.graph != null;

		protected override void Definition()
		{
			isControlRoot = true;
			foreach (global::Unity.VisualScripting.ControlOutputDefinition item in global::System.Linq.Enumerable.OfType<global::Unity.VisualScripting.ControlOutputDefinition>(base.graph.validPortDefinitions))
			{
				string key = item.key;
				ControlInput(key, delegate(global::Unity.VisualScripting.Flow flow)
				{
					global::Unity.VisualScripting.SubgraphUnit parent = flow.stack.GetParent<global::Unity.VisualScripting.SubgraphUnit>();
					flow.stack.ExitParentElement();
					parent.EnsureDefined();
					return parent.controlOutputs[key];
				});
			}
			foreach (global::Unity.VisualScripting.ValueOutputDefinition item2 in global::System.Linq.Enumerable.OfType<global::Unity.VisualScripting.ValueOutputDefinition>(base.graph.validPortDefinitions))
			{
				string key2 = item2.key;
				global::System.Type type = item2.type;
				ValueInput(type, key2);
			}
		}

		protected override void AfterDefine()
		{
			base.graph.onPortDefinitionsChanged += Define;
		}

		protected override void BeforeUndefine()
		{
			base.graph.onPortDefinitionsChanged -= Define;
		}
	}
}
