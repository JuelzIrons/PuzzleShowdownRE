namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Navigation")]
	public sealed class OnDestinationReached : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		protected override string hookName => "Update";

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput threshold { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput requireSuccess { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			threshold = ValueInput("threshold", 0.05f);
			requireSuccess = ValueInput("requireSuccess", @default: true);
		}

		protected override bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.EmptyEventArgs args)
		{
			global::UnityEngine.AI.NavMeshAgent component = flow.stack.gameObject.GetComponent<global::UnityEngine.AI.NavMeshAgent>();
			if (component != null && component.remainingDistance <= flow.GetValue<float>(threshold))
			{
				if (component.pathStatus != global::UnityEngine.AI.NavMeshPathStatus.PathComplete)
				{
					return !flow.GetValue<bool>(requireSuccess);
				}
				return true;
			}
			return false;
		}
	}
}
