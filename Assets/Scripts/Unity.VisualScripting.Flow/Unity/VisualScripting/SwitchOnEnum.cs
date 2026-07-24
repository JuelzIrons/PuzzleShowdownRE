namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitTitle("Switch On Enum")]
	[global::Unity.VisualScripting.UnitShortTitle("Switch")]
	[global::Unity.VisualScripting.UnitSubtitle("On Enum")]
	[global::Unity.VisualScripting.UnitOrder(3)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.IBranchUnit))]
	public sealed class SwitchOnEnum : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IBranchUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.Dictionary<global::System.Enum, global::Unity.VisualScripting.ControlOutput> branches { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		[global::Unity.VisualScripting.TypeFilter(new global::System.Type[] { }, Enums = true, Classes = false, Interfaces = false, Structs = false, Primitives = false)]
		public global::System.Type enumType { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput @enum { get; private set; }

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
			branches = new global::System.Collections.Generic.Dictionary<global::System.Enum, global::Unity.VisualScripting.ControlOutput>();
			enter = ControlInput("enter", Enter);
			@enum = ValueInput(enumType, "enum");
			Requirement(@enum, enter);
			foreach (global::System.Collections.Generic.KeyValuePair<string, global::System.Enum> item in global::Unity.VisualScripting.EnumUtility.ValuesByNames(enumType))
			{
				string key = item.Key;
				global::System.Enum value = item.Value;
				if (!branches.ContainsKey(value))
				{
					global::Unity.VisualScripting.ControlOutput controlOutput = ControlOutput("%" + key);
					branches.Add(value, controlOutput);
					Succession(enter, controlOutput);
				}
			}
		}

		public global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Enum key = (global::System.Enum)flow.GetValue(@enum, enumType);
			if (branches.ContainsKey(key))
			{
				return branches[key];
			}
			return null;
		}
	}
}
