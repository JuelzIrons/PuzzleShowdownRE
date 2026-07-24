namespace Unity.VisualScripting
{
	public sealed class SetMember : global::Unity.VisualScripting.MemberUnit
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.InspectableIf("supportsChaining")]
		public bool chainable { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public bool supportsChaining => base.member.requiresTarget;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.MemberFilter(Fields = true, Properties = true, ReadOnly = false)]
		public global::Unity.VisualScripting.Member setter
		{
			get
			{
				return base.member;
			}
			set
			{
				base.member = value;
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput assign { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Value")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput input { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Value")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput output { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Target")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput targetOutput { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput assigned { get; private set; }

		public SetMember()
		{
		}

		public SetMember(global::Unity.VisualScripting.Member member)
			: base(member)
		{
		}

		protected override void Definition()
		{
			base.Definition();
			assign = ControlInput("assign", Assign);
			assigned = ControlOutput("assigned");
			Succession(assign, assigned);
			if (supportsChaining && chainable)
			{
				targetOutput = ValueOutput(base.member.targetType, "targetOutput");
				Assignment(assign, targetOutput);
			}
			output = ValueOutput(base.member.type, "output");
			Assignment(assign, output);
			if (base.member.requiresTarget)
			{
				Requirement(base.target, assign);
			}
			input = ValueInput(base.member.type, "input");
			Requirement(input, assign);
			if (base.member.allowsNull)
			{
				input.AllowsNull();
			}
			input.SetDefaultValue(base.member.type.PseudoDefault());
		}

		protected override bool IsMemberValid(global::Unity.VisualScripting.Member member)
		{
			if (member.isAccessor)
			{
				return member.isSettable;
			}
			return false;
		}

		private object GetAndChainTarget(global::Unity.VisualScripting.Flow flow)
		{
			if (base.member.requiresTarget)
			{
				object value = flow.GetValue(base.target, base.member.targetType);
				if (supportsChaining && chainable)
				{
					flow.SetValue(targetOutput, value);
				}
				return value;
			}
			return null;
		}

		private global::Unity.VisualScripting.ControlOutput Assign(global::Unity.VisualScripting.Flow flow)
		{
			object andChainTarget = GetAndChainTarget(flow);
			object convertedValue = flow.GetConvertedValue(input);
			flow.SetValue(output, base.member.Set(andChainTarget, convertedValue));
			return assigned;
		}

		public override global::Unity.VisualScripting.AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			global::Unity.VisualScripting.AnalyticsIdentifier obj = new global::Unity.VisualScripting.AnalyticsIdentifier
			{
				Identifier = base.member.targetType.FullName + "." + base.member.name + "(Set)",
				Namespace = base.member.targetType.Namespace
			};
			obj.Hashcode = obj.Identifier.GetHashCode();
			return obj;
		}
	}
}
