namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Time")]
	[global::Unity.VisualScripting.UnitOrder(7)]
	public sealed class Timer : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IGraphEventListener
	{
		public sealed class Data : global::Unity.VisualScripting.IGraphElementData
		{
			public float elapsed;

			public float duration;

			public bool active;

			public bool paused;

			public bool unscaled;

			public global::System.Delegate update;

			public bool isListening;
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput start { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput pause { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput resume { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput toggle { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput duration { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Unscaled")]
		public global::Unity.VisualScripting.ValueInput unscaledTime { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput started { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput tick { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput completed { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Elapsed")]
		public global::Unity.VisualScripting.ValueOutput elapsedSeconds { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Elapsed %")]
		public global::Unity.VisualScripting.ValueOutput elapsedRatio { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Remaining")]
		public global::Unity.VisualScripting.ValueOutput remainingSeconds { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Remaining %")]
		public global::Unity.VisualScripting.ValueOutput remainingRatio { get; private set; }

		protected override void Definition()
		{
			isControlRoot = true;
			start = ControlInput("start", Start);
			pause = ControlInput("pause", Pause);
			resume = ControlInput("resume", Resume);
			toggle = ControlInput("toggle", Toggle);
			duration = ValueInput("duration", 1f);
			unscaledTime = ValueInput("unscaledTime", @default: false);
			started = ControlOutput("started");
			tick = ControlOutput("tick");
			completed = ControlOutput("completed");
			elapsedSeconds = ValueOutput<float>("elapsedSeconds");
			elapsedRatio = ValueOutput<float>("elapsedRatio");
			remainingSeconds = ValueOutput<float>("remainingSeconds");
			remainingRatio = ValueOutput<float>("remainingRatio");
		}

		public global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.Timer.Data();
		}

		public void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			global::Unity.VisualScripting.Timer.Data elementData = stack.GetElementData<global::Unity.VisualScripting.Timer.Data>(this);
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
			global::Unity.VisualScripting.Timer.Data elementData = stack.GetElementData<global::Unity.VisualScripting.Timer.Data>(this);
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
			return pointer.GetElementData<global::Unity.VisualScripting.Timer.Data>(this).isListening;
		}

		private void TriggerUpdate(global::Unity.VisualScripting.GraphReference reference)
		{
			using global::Unity.VisualScripting.Flow flow = global::Unity.VisualScripting.Flow.New(reference);
			Update(flow);
		}

		private global::Unity.VisualScripting.ControlOutput Start(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.Timer.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.Timer.Data>(this);
			elementData.elapsed = 0f;
			elementData.duration = flow.GetValue<float>(duration);
			elementData.active = true;
			elementData.paused = false;
			elementData.unscaled = flow.GetValue<bool>(unscaledTime);
			AssignMetrics(flow, elementData);
			return started;
		}

		private global::Unity.VisualScripting.ControlOutput Pause(global::Unity.VisualScripting.Flow flow)
		{
			flow.stack.GetElementData<global::Unity.VisualScripting.Timer.Data>(this).paused = true;
			return null;
		}

		private global::Unity.VisualScripting.ControlOutput Resume(global::Unity.VisualScripting.Flow flow)
		{
			flow.stack.GetElementData<global::Unity.VisualScripting.Timer.Data>(this).paused = false;
			return null;
		}

		private global::Unity.VisualScripting.ControlOutput Toggle(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.Timer.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.Timer.Data>(this);
			if (!elementData.active)
			{
				return Start(flow);
			}
			elementData.paused = !elementData.paused;
			return null;
		}

		private void AssignMetrics(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.Timer.Data data)
		{
			flow.SetValue(elapsedSeconds, data.elapsed);
			flow.SetValue(elapsedRatio, global::UnityEngine.Mathf.Clamp01(data.elapsed / data.duration));
			flow.SetValue(remainingSeconds, global::UnityEngine.Mathf.Max(0f, data.duration - data.elapsed));
			flow.SetValue(remainingRatio, global::UnityEngine.Mathf.Clamp01((data.duration - data.elapsed) / data.duration));
		}

		public void Update(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.Timer.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.Timer.Data>(this);
			if (elementData.active && !elementData.paused)
			{
				elementData.elapsed += (elementData.unscaled ? global::UnityEngine.Time.unscaledDeltaTime : global::UnityEngine.Time.deltaTime);
				elementData.elapsed = global::UnityEngine.Mathf.Min(elementData.elapsed, elementData.duration);
				AssignMetrics(flow, elementData);
				global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
				flow.Invoke(tick);
				if (elementData.elapsed >= elementData.duration)
				{
					elementData.active = false;
					flow.RestoreStack(stack);
					flow.Invoke(completed);
				}
				flow.DisposePreservedStack(stack);
			}
		}
	}
}
