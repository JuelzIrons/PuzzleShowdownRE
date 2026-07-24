namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitShortTitle("Is Variable Defined")]
	public abstract class IsVariableDefinedUnit : global::Unity.VisualScripting.VariableUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Defined")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public new global::Unity.VisualScripting.ValueOutput isDefined { get; private set; }

		protected IsVariableDefinedUnit()
		{
		}

		protected IsVariableDefinedUnit(string defaultName)
			: base(defaultName)
		{
		}

		protected override void Definition()
		{
			base.Definition();
			isDefined = ValueOutput("isDefined", IsDefined);
			Requirement(base.name, isDefined);
		}

		protected virtual bool IsDefined(global::Unity.VisualScripting.Flow flow)
		{
			string value = flow.GetValue<string>(base.name);
			return GetDeclarations(flow).IsDefined(value);
		}
	}
}
