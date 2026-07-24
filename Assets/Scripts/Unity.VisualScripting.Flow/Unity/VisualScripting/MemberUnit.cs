namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SpecialUnit]
	public abstract class MemberUnit : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IAotStubbable
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.MemberFilter(Fields = true, Properties = true, Methods = true, Constructors = true)]
		public global::Unity.VisualScripting.Member member { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput target { get; private set; }

		public override bool canDefine => member != null;

		protected MemberUnit()
		{
		}

		protected MemberUnit(global::Unity.VisualScripting.Member member)
			: this()
		{
			this.member = member;
		}

		protected override void Definition()
		{
			member.EnsureReflected();
			if (!IsMemberValid(member))
			{
				throw new global::System.NotSupportedException("The member type is not valid for this unit.");
			}
			if (member.requiresTarget)
			{
				target = ValueInput(member.targetType, "target");
				target.SetDefaultValue(member.targetType.PseudoDefault());
				if (typeof(global::UnityEngine.Object).IsAssignableFrom(member.targetType))
				{
					target.NullMeansSelf();
				}
			}
		}

		protected abstract bool IsMemberValid(global::Unity.VisualScripting.Member member);

		public override void Prewarm()
		{
			if (member != null && member.isReflected)
			{
				member.Prewarm();
			}
		}

		public override global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			if (member != null && member.isReflected)
			{
				yield return member.info;
			}
		}
	}
}
