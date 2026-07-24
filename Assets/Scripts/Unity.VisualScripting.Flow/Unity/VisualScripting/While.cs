namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("While Loop")]
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(11)]
	public class While : global::Unity.VisualScripting.LoopUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput condition { get; private set; }

		protected override void Definition()
		{
			base.Definition();
			condition = ValueInput<bool>("condition");
			Requirement(condition, base.enter);
		}

		private int Start(global::Unity.VisualScripting.Flow flow)
		{
			return flow.EnterLoop();
		}

		private bool CanMoveNext(global::Unity.VisualScripting.Flow flow)
		{
			return flow.GetValue<bool>(condition);
		}

		protected override global::Unity.VisualScripting.ControlOutput Loop(global::Unity.VisualScripting.Flow flow)
		{
			int loop = Start(flow);
			global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
			while (flow.LoopIsNotBroken(loop) && CanMoveNext(flow))
			{
				flow.Invoke(base.body);
				flow.RestoreStack(stack);
			}
			flow.DisposePreservedStack(stack);
			flow.ExitLoop(loop);
			return base.exit;
		}

		protected override global::System.Collections.IEnumerator LoopCoroutine(global::Unity.VisualScripting.Flow flow)
		{
			int loop = Start(flow);
			global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
			while (flow.LoopIsNotBroken(loop) && CanMoveNext(flow))
			{
				yield return base.body;
				flow.RestoreStack(stack);
			}
			flow.DisposePreservedStack(stack);
			flow.ExitLoop(loop);
			yield return base.exit;
		}
	}
}
