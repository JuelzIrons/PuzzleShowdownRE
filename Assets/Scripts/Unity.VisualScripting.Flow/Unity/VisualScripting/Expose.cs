namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SpecialUnit]
	public sealed class Expose : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IAotStubbable
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.TypeFilter(new global::System.Type[] { }, Enums = false)]
		public global::System.Type type { get; set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Instance")]
		[global::Unity.VisualScripting.InspectorToggleLeft]
		public bool instance { get; set; } = true;

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Static")]
		[global::Unity.VisualScripting.InspectorToggleLeft]
		public bool @static { get; set; } = true;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput target { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.ValueOutput, global::Unity.VisualScripting.Member> members { get; private set; }

		public override bool canDefine => type != null;

		public Expose()
		{
		}

		public Expose(global::System.Type type)
		{
			this.type = type;
		}

		public override global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			if (members == null)
			{
				yield break;
			}
			foreach (global::Unity.VisualScripting.Member value in members.Values)
			{
				if (value != null && value.isReflected)
				{
					yield return value.info;
				}
			}
		}

		protected override void Definition()
		{
			members = new global::System.Collections.Generic.Dictionary<global::Unity.VisualScripting.ValueOutput, global::Unity.VisualScripting.Member>();
			bool flag = false;
			foreach (global::Unity.VisualScripting.Member member in global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(global::System.Linq.Enumerable.Where(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(type.GetMembers(), (global::System.Reflection.MemberInfo m) => m is global::System.Reflection.FieldInfo || m is global::System.Reflection.PropertyInfo), (global::System.Reflection.MemberInfo m) => m.ToManipulator(type)).DistinctBy((global::Unity.VisualScripting.Member m) => m.name), Include), (global::Unity.VisualScripting.Member m) => (!m.requiresTarget) ? 1 : 0), (global::Unity.VisualScripting.Member m) => m.order))
			{
				global::Unity.VisualScripting.ValueOutput valueOutput = ValueOutput(member.type, member.name, (global::Unity.VisualScripting.Flow flow) => GetValue(flow, member));
				if (member.isPredictable)
				{
					valueOutput.Predictable();
				}
				members.Add(valueOutput, member);
				if (member.requiresTarget)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			target = ValueInput(type, "target").NullMeansSelf();
			target.SetDefaultValue(type.PseudoDefault());
			foreach (global::Unity.VisualScripting.ValueOutput key in members.Keys)
			{
				if (members[key].requiresTarget)
				{
					Requirement(target, key);
				}
			}
		}

		private bool Include(global::Unity.VisualScripting.Member member)
		{
			if (!instance && member.requiresTarget)
			{
				return false;
			}
			if (!@static && !member.requiresTarget)
			{
				return false;
			}
			if (!member.isPubliclyGettable)
			{
				return false;
			}
			if (member.info.HasAttribute<global::System.ObsoleteAttribute>())
			{
				return false;
			}
			if (member.isIndexer)
			{
				return false;
			}
			if (member.name == "runInEditMode" && member.declaringType == typeof(global::UnityEngine.MonoBehaviour))
			{
				return false;
			}
			return true;
		}

		private object GetValue(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.Member member)
		{
			object obj = (member.requiresTarget ? flow.GetValue(target, member.targetType) : null);
			return member.Get(obj);
		}
	}
}
