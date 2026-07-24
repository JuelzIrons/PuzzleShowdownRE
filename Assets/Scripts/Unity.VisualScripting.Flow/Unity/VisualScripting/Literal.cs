namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SpecialUnit]
	public sealed class Literal : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.SerializeAs("value")]
		private object _value;

		public override bool canDefine => type != null;

		[global::Unity.VisualScripting.Serialize]
		public global::System.Type type { get; internal set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public object value
		{
			get
			{
				return _value;
			}
			set
			{
				global::Unity.VisualScripting.Ensure.That("value").IsOfType(value, type);
				_value = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		[global::System.Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public Literal()
		{
		}

		public Literal(global::System.Type type)
			: this(type, type.PseudoDefault())
		{
		}

		public Literal(global::System.Type type, object value)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			global::Unity.VisualScripting.Ensure.That("value").IsOfType(value, type);
			this.type = type;
			this.value = value;
		}

		protected override void Definition()
		{
			output = ValueOutput(type, "output", (global::Unity.VisualScripting.Flow flow) => value).Predictable();
		}

		public override global::Unity.VisualScripting.AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			global::Unity.VisualScripting.AnalyticsIdentifier obj = new global::Unity.VisualScripting.AnalyticsIdentifier
			{
				Identifier = GetType().FullName + "(" + type.Name + ")",
				Namespace = type.Namespace
			};
			obj.Hashcode = obj.Identifier.GetHashCode();
			return obj;
		}
	}
}
