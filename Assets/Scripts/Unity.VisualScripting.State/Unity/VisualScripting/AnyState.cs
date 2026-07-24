namespace Unity.VisualScripting
{
	public sealed class AnyState : global::Unity.VisualScripting.State
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		public override bool canBeDestination => false;

		public AnyState()
		{
			base.isStart = true;
		}

		public override void OnExit(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.StateExitReason reason)
		{
			if (reason != global::Unity.VisualScripting.StateExitReason.Branch)
			{
				base.OnExit(flow, reason);
			}
		}

		public override void OnBranchTo(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.IState destination)
		{
			foreach (global::Unity.VisualScripting.IStateTransition item in base.outgoingTransitionsNoAlloc)
			{
				if (item.destination != destination)
				{
					item.destination.OnExit(flow, global::Unity.VisualScripting.StateExitReason.AnyBranch);
				}
			}
		}
	}
}
