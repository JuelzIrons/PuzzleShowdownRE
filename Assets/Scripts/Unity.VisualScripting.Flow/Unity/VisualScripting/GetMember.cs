namespace Unity.VisualScripting
{
	public sealed class GetMember : global::Unity.VisualScripting.MemberUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.MemberFilter(Fields = true, Properties = true, WriteOnly = false)]
		public global::Unity.VisualScripting.Member getter
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
		public global::Unity.VisualScripting.ValueOutput value { get; private set; }

		public GetMember()
		{
		}

		public GetMember(global::Unity.VisualScripting.Member member)
			: base(member)
		{
		}

		protected override void Definition()
		{
			base.Definition();
			value = ValueOutput(base.member.type, "value", Value);
			if (base.member.isPredictable)
			{
				value.Predictable();
			}
			if (base.member.requiresTarget)
			{
				Requirement(base.target, value);
			}
		}

		protected override bool IsMemberValid(global::Unity.VisualScripting.Member member)
		{
			if (member.isAccessor)
			{
				return member.isGettable;
			}
			return false;
		}

		private object Value(global::Unity.VisualScripting.Flow flow)
		{
			object obj = (base.member.requiresTarget ? flow.GetValue(base.target, base.member.targetType) : null);
			return base.member.Get(obj);
		}

		public override global::Unity.VisualScripting.AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			global::Unity.VisualScripting.AnalyticsIdentifier obj = new global::Unity.VisualScripting.AnalyticsIdentifier
			{
				Identifier = base.member.targetType.FullName + "." + base.member.name + "(Get)",
				Namespace = base.member.targetType.Namespace
			};
			obj.Hashcode = obj.Identifier.GetHashCode();
			return obj;
		}
	}
}
