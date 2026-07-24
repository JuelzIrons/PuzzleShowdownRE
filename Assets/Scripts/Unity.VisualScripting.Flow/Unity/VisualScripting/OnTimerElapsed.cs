namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events/Time")]
	[global::System.Obsolete("Use Wait For Seconds or Timer instead.")]
	public sealed class OnTimerElapsed : global::Unity.VisualScripting.MachineEventUnit<global::Unity.VisualScripting.EmptyEventArgs>
	{
		public new class Data : global::Unity.VisualScripting.EventUnit<global::Unity.VisualScripting.EmptyEventArgs>.Data
		{
			public float time;

			public bool triggered;
		}

		protected override string hookName => "Update";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Delay")]
		public global::Unity.VisualScripting.ValueInput seconds { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Unscaled")]
		public global::Unity.VisualScripting.ValueInput unscaledTime { get; private set; }

		public override global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.OnTimerElapsed.Data();
		}

		protected override void Definition()
		{
			base.Definition();
			seconds = ValueInput("seconds", 0f);
			unscaledTime = ValueInput("unscaledTime", @default: false);
		}

		public override void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			base.StartListening(stack);
			global::Unity.VisualScripting.OnTimerElapsed.Data elementData = stack.GetElementData<global::Unity.VisualScripting.OnTimerElapsed.Data>(this);
			elementData.triggered = false;
			elementData.time = 0f;
		}

		protected override bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.EmptyEventArgs args)
		{
			global::Unity.VisualScripting.OnTimerElapsed.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.OnTimerElapsed.Data>(this);
			if (elementData.triggered)
			{
				return false;
			}
			float num = (flow.GetValue<bool>(unscaledTime) ? global::UnityEngine.Time.unscaledDeltaTime : global::UnityEngine.Time.deltaTime);
			float value = flow.GetValue<float>(seconds);
			elementData.time += num;
			if (elementData.time >= value)
			{
				elementData.triggered = true;
				return true;
			}
			return false;
		}
	}
}
