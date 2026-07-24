namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Time")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.Timer))]
	[global::Unity.VisualScripting.UnitOrder(8)]
	public sealed class Cooldown : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IGraphEventListener
	{
		public sealed class Data : global::Unity.VisualScripting.IGraphElementData
		{
			public float remaining;

			public float duration;

			public bool unscaled;

			public global::System.Delegate update;

			public bool isListening;

			public bool isReady => remaining <= 0f;
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput reset { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput duration { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Unscaled")]
		public global::Unity.VisualScripting.ValueInput unscaledTime { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Ready")]
		public global::Unity.VisualScripting.ControlOutput exitReady { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Not Ready")]
		public global::Unity.VisualScripting.ControlOutput exitNotReady { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput tick { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Completed")]
		public global::Unity.VisualScripting.ControlOutput becameReady { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Remaining")]
		public global::Unity.VisualScripting.ValueOutput remainingSeconds { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Remaining %")]
		public global::Unity.VisualScripting.ValueOutput remainingRatio { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Enter);
			reset = ControlInput("reset", Reset);
			duration = ValueInput("duration", 1f);
			unscaledTime = ValueInput("unscaledTime", @default: false);
			exitReady = ControlOutput("exitReady");
			exitNotReady = ControlOutput("exitNotReady");
			tick = ControlOutput("tick");
			becameReady = ControlOutput("becameReady");
			remainingSeconds = ValueOutput<float>("remainingSeconds");
			remainingRatio = ValueOutput<float>("remainingRatio");
			Requirement(duration, enter);
			Requirement(unscaledTime, enter);
			Succession(enter, exitReady);
			Succession(enter, exitNotReady);
			Succession(enter, tick);
			Succession(enter, becameReady);
			Assignment(enter, remainingSeconds);
			Assignment(enter, remainingRatio);
		}

		public global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.Cooldown.Data();
		}

		public void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			global::Unity.VisualScripting.Cooldown.Data elementData = stack.GetElementData<global::Unity.VisualScripting.Cooldown.Data>(this);
			if (!elementData.isListening)
			{
				global::Unity.VisualScripting.GraphReference reference = stack.ToReference();
				global::Unity.VisualScripting.EventHook hook = new global::Unity.VisualScripting.EventHook("Update", stack.machine);
				global::System.Action<global::Unity.VisualScripting.EmptyEventArgs> action = delegate
				{
					TriggerUpdate(reference);
				};
				global::Unity.VisualScripting.EventBus.Register(hook, action);
				elementData.update = action;
				elementData.isListening = true;
			}
		}

		public void StopListening(global::Unity.VisualScripting.GraphStack stack)
		{
			global::Unity.VisualScripting.Cooldown.Data elementData = stack.GetElementData<global::Unity.VisualScripting.Cooldown.Data>(this);
			if (elementData.isListening)
			{
				global::Unity.VisualScripting.EventBus.Unregister(new global::Unity.VisualScripting.EventHook("Update", stack.machine), elementData.update);
				stack.ClearReference();
				elementData.update = null;
				elementData.isListening = false;
			}
		}

		public bool IsListening(global::Unity.VisualScripting.GraphPointer pointer)
		{
			return pointer.GetElementData<global::Unity.VisualScripting.Cooldown.Data>(this).isListening;
		}

		private void TriggerUpdate(global::Unity.VisualScripting.GraphReference reference)
		{
			using global::Unity.VisualScripting.Flow flow = global::Unity.VisualScripting.Flow.New(reference);
			Update(flow);
		}

		private global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.stack.GetElementData<global::Unity.VisualScripting.Cooldown.Data>(this).isReady)
			{
				return Reset(flow);
			}
			return exitNotReady;
		}

		private global::Unity.VisualScripting.ControlOutput Reset(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.Cooldown.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.Cooldown.Data>(this);
			elementData.duration = flow.GetValue<float>(duration);
			elementData.remaining = elementData.duration;
			elementData.unscaled = flow.GetValue<bool>(unscaledTime);
			return exitReady;
		}

		private void AssignMetrics(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.Cooldown.Data data)
		{
			flow.SetValue(remainingSeconds, data.remaining);
			flow.SetValue(remainingRatio, global::UnityEngine.Mathf.Clamp01(data.remaining / data.duration));
		}

		public void Update(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.Cooldown.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.Cooldown.Data>(this);
			if (!elementData.isReady)
			{
				elementData.remaining -= (elementData.unscaled ? global::UnityEngine.Time.unscaledDeltaTime : global::UnityEngine.Time.deltaTime);
				elementData.remaining = global::UnityEngine.Mathf.Max(0f, elementData.remaining);
				AssignMetrics(flow, elementData);
				global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
				flow.Invoke(tick);
				if (elementData.isReady)
				{
					flow.RestoreStack(stack);
					flow.Invoke(becameReady);
				}
				flow.DisposePreservedStack(stack);
			}
		}
	}
}
