namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitTitle("Select On Enum")]
	[global::Unity.VisualScripting.UnitShortTitle("Select")]
	[global::Unity.VisualScripting.UnitSubtitle("On Enum")]
	[global::Unity.VisualScripting.UnitOrder(7)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.ISelectUnit))]
	public sealed class SelectOnEnum : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.ISelectUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.Dictionary<object, global::Unity.VisualScripting.ValueInput> branches { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput selector { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput selection { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		[global::Unity.VisualScripting.TypeFilter(new global::System.Type[] { }, Enums = true, Classes = false, Interfaces = false, Structs = false, Primitives = false)]
		public global::System.Type enumType { get; set; }

		public override bool canDefine
		{
			get
			{
				if (enumType != null)
				{
					return enumType.IsEnum;
				}
				return false;
			}
		}

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected override void Definition()
		{
			branches = new global::System.Collections.Generic.Dictionary<object, global::Unity.VisualScripting.ValueInput>();
			selection = ValueOutput("selection", Branch).Predictable();
			selector = ValueInput(enumType, "selector");
			Requirement(selector, selection);
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Enum> item in global::Unity.VisualScripting.EnumUtility.ValuesByNames(enumType))
			{
				global::System.Enum value = item.Value;
				if (!branches.ContainsKey(value))
				{
					global::Unity.VisualScripting.ValueInput valueInput = ValueInput<object>("%" + item.Key).AllowsNull();
					branches.Add(value, valueInput);
					Requirement(valueInput, selection);
				}
			}
		}

		public object Branch(global::Unity.VisualScripting.Flow flow)
		{
			object value = flow.GetValue(selector, enumType);
			return flow.GetValue(branches[value]);
		}
	}
}
