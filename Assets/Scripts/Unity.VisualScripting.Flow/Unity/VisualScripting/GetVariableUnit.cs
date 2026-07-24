namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitShortTitle("Get Variable")]
	public abstract class GetVariableUnit : global::Unity.VisualScripting.VariableUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput value { get; private set; }

		protected GetVariableUnit()
		{
		}

		protected GetVariableUnit(string defaultName)
			: base(defaultName)
		{
		}

		protected override void Definition()
		{
			base.Definition();
			value = ValueOutput("value", Get).PredictableIf(IsDefined);
			Requirement(base.name, value);
		}

		protected virtual bool IsDefined(global::Unity.VisualScripting.Flow flow)
		{
			string variable = flow.GetValue<string>(base.name);
			return GetDeclarations(flow)?.IsDefined(variable) ?? false;
		}

		protected virtual object Get(global::Unity.VisualScripting.Flow flow)
		{
			string variable = flow.GetValue<string>(base.name);
			return GetDeclarations(flow).Get(variable);
		}
	}
}
