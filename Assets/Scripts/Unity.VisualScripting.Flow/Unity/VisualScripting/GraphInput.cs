namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Nesting")]
	[global::Unity.VisualScripting.UnitOrder(1)]
	[global::Unity.VisualScripting.UnitTitle("Input")]
	public sealed class GraphInput : global::Unity.VisualScripting.Unit
	{
		public override bool canDefine => base.graph != null;

		protected override void Definition()
		{
			isControlRoot = true;
			foreach (global::Unity.VisualScripting.ControlInputDefinition item in global::System.Linq.Enumerable.OfType<global::Unity.VisualScripting.ControlInputDefinition>(base.graph.validPortDefinitions))
			{
				ControlOutput(item.key);
			}
			foreach (global::Unity.VisualScripting.ValueInputDefinition item2 in global::System.Linq.Enumerable.OfType<global::Unity.VisualScripting.ValueInputDefinition>(base.graph.validPortDefinitions))
			{
				string key = item2.key;
				global::System.Type type = item2.type;
				ValueOutput(type, key, delegate(global::Unity.VisualScripting.Flow flow)
				{
					global::Unity.VisualScripting.SubgraphUnit parent = flow.stack.GetParent<global::Unity.VisualScripting.SubgraphUnit>();
					if (flow.enableDebug)
					{
						global::Unity.VisualScripting.IUnitDebugData elementDebugData = flow.stack.GetElementDebugData<global::Unity.VisualScripting.IUnitDebugData>(parent);
						elementDebugData.lastInvokeFrame = global::Unity.VisualScripting.EditorTimeBinding.frame;
						elementDebugData.lastInvokeTime = global::Unity.VisualScripting.EditorTimeBinding.time;
					}
					flow.stack.ExitParentElement();
					parent.EnsureDefined();
					object value = flow.GetValue(parent.valueInputs[key], type);
					flow.stack.EnterParentElement(parent);
					return value;
				});
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
