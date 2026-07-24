namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("For Loop")]
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(9)]
	public sealed class For : global::Unity.VisualScripting.LoopUnit
	{
		[global::Unity.VisualScripting.PortLabel("First")]
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput firstIndex { get; private set; }

		[global::Unity.VisualScripting.PortLabel("Last")]
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput lastIndex { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput step { get; private set; }

		[global::Unity.VisualScripting.PortLabel("Index")]
		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput currentIndex { get; private set; }

		protected override void Definition()
		{
			firstIndex = ValueInput("firstIndex", 0);
			lastIndex = ValueInput("lastIndex", 10);
			step = ValueInput("step", 1);
			currentIndex = ValueOutput<int>("currentIndex");
			base.Definition();
			Requirement(firstIndex, base.enter);
			Requirement(lastIndex, base.enter);
			Requirement(step, base.enter);
			Assignment(base.enter, currentIndex);
		}

		private int Start(global::Unity.VisualScripting.Flow flow, out int currentIndex, out int lastIndex, out bool ascending)
		{
			int value = flow.GetValue<int>(firstIndex);
			lastIndex = flow.GetValue<int>(this.lastIndex);
			ascending = value <= lastIndex;
			currentIndex = value;
			flow.SetValue(this.currentIndex, currentIndex);
			return flow.EnterLoop();
		}

		private bool CanMoveNext(int currentIndex, int lastIndex, bool ascending)
		{
			if (ascending)
			{
				return currentIndex < lastIndex;
			}
			return currentIndex > lastIndex;
		}

		private void MoveNext(global::Unity.VisualScripting.Flow flow, ref int currentIndex)
		{
			currentIndex += flow.GetValue<int>(step);
			flow.SetValue(this.currentIndex, currentIndex);
		}

		protected override global::Unity.VisualScripting.ControlOutput Loop(global::Unity.VisualScripting.Flow flow)
		{
			int num;
			int num2;
			bool ascending;
			int loop = Start(flow, out num, out num2, out ascending);
			if (!IsStepValueZero())
			{
				global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
				while (flow.LoopIsNotBroken(loop) && CanMoveNext(num, num2, ascending))
				{
					flow.Invoke(base.body);
					flow.RestoreStack(stack);
					MoveNext(flow, ref num);
				}
				flow.DisposePreservedStack(stack);
			}
			flow.ExitLoop(loop);
			return base.exit;
		}

		protected override global::System.Collections.IEnumerator LoopCoroutine(global::Unity.VisualScripting.Flow flow)
		{
			int currentIndex;
			int lastIndex;
			bool ascending;
			int loop = Start(flow, out currentIndex, out lastIndex, out ascending);
			global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
			while (flow.LoopIsNotBroken(loop) && CanMoveNext(currentIndex, lastIndex, ascending))
			{
				yield return base.body;
				flow.RestoreStack(stack);
				MoveNext(flow, ref currentIndex);
			}
			flow.DisposePreservedStack(stack);
			flow.ExitLoop(loop);
			yield return base.exit;
		}

		public bool IsStepValueZero()
		{
			bool num = !step.hasValidConnection && (int)base.defaultValues[step.key] == 0;
			bool flag = false;
			if (step.hasValidConnection && step.connection.source.unit is global::Unity.VisualScripting.Literal literal && global::System.Convert.ToInt32(literal.value) == 0)
			{
				flag = true;
			}
			return num || flag;
		}
	}
}
