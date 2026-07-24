namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitOrder(202)]
	public abstract class Round<TInput, TOutput> : global::Unity.VisualScripting.Unit
	{
		public enum Rounding
		{
			Floor = 0,
			Ceiling = 1,
			AwayFromZero = 2
		}

		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable]
		[global::Unity.VisualScripting.Serialize]
		public global::Unity.VisualScripting.Round<TInput, TOutput>.Rounding rounding { get; set; } = global::Unity.VisualScripting.Round<TInput, TOutput>.Rounding.AwayFromZero;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		protected override void Definition()
		{
			input = ValueInput<TInput>("input");
			output = ValueOutput("output", Operation).Predictable();
			Requirement(input, output);
		}

		protected abstract TOutput Floor(TInput input);

		protected abstract TOutput AwayFromZero(TInput input);

		protected abstract TOutput Ceiling(TInput input);

		public TOutput Operation(global::Unity.VisualScripting.Flow flow)
		{
			return rounding switch
			{
				global::Unity.VisualScripting.Round<TInput, TOutput>.Rounding.Floor => Floor(flow.GetValue<TInput>(input)), 
				global::Unity.VisualScripting.Round<TInput, TOutput>.Rounding.AwayFromZero => AwayFromZero(flow.GetValue<TInput>(input)), 
				global::Unity.VisualScripting.Round<TInput, TOutput>.Rounding.Ceiling => Ceiling(flow.GetValue<TInput>(input)), 
				_ => throw new global::Unity.VisualScripting.UnexpectedEnumValueException<global::Unity.VisualScripting.Round<TInput, TOutput>.Rounding>(rounding), 
			};
		}
	}
}
