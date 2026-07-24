namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SpecialUnit]
	public sealed class CreateStruct : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.Serialize]
		public global::System.Type type { get; internal set; }

		public override bool canDefine => type != null;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		[global::System.Obsolete("This parameterless constructor is only made public for serialization. Use another constructor instead.")]
		public CreateStruct()
		{
		}

		public CreateStruct(global::System.Type type)
		{
			global::Unity.VisualScripting.Ensure.That("type").IsNotNull(type);
			if (!type.IsStruct())
			{
				throw new global::System.ArgumentException($"Type {type} must be a struct.", "type");
			}
			this.type = type;
		}

		protected override void Definition()
		{
			enter = ControlInput("enter", Enter);
			exit = ControlOutput("exit");
			output = ValueOutput(type, "output", Create);
			Succession(enter, exit);
		}

		private global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			flow.SetValue(output, global::System.Activator.CreateInstance(type));
			return exit;
		}

		private object Create(global::Unity.VisualScripting.Flow flow)
		{
			return global::System.Activator.CreateInstance(type);
		}
	}
}
