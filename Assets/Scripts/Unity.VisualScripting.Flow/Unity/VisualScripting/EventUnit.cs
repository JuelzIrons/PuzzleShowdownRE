namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SerializationVersion("A", new global::System.Type[] { })]
	[global::Unity.VisualScripting.SpecialUnit]
	public abstract class EventUnit<TArgs> : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IEventUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IGraphEventListener, global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphEventHandler<TArgs>
	{
		public class Data : global::Unity.VisualScripting.IGraphElementData
		{
			public global::Unity.VisualScripting.EventHook hook;

			public global::System.Delegate handler;

			public bool isListening;

			public global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.Flow> activeCoroutines = new global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.Flow>();
		}

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.InspectorExpandTooltip]
		public bool coroutine { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput trigger { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		protected abstract bool register { get; }

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		public virtual global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.EventUnit<TArgs>.Data();
		}

		protected override void Definition()
		{
			isControlRoot = true;
			trigger = ControlOutput("trigger");
		}

		public virtual global::Unity.VisualScripting.EventHook GetHook(global::Unity.VisualScripting.GraphReference reference)
		{
			throw new global::Unity.VisualScripting.InvalidImplementationException($"Missing event hook for '{this}'.");
		}

		public virtual void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			global::Unity.VisualScripting.EventUnit<TArgs>.Data elementData = stack.GetElementData<global::Unity.VisualScripting.EventUnit<TArgs>.Data>(this);
			if (elementData.isListening)
			{
				return;
			}
			if (register)
			{
				global::Unity.VisualScripting.GraphReference reference = stack.ToReference();
				global::Unity.VisualScripting.EventHook hook = GetHook(reference);
				global::System.Action<TArgs> handler = delegate(TArgs args)
				{
					Trigger(reference, args);
				};
				global::Unity.VisualScripting.EventBus.Register(hook, handler);
				elementData.hook = hook;
				elementData.handler = handler;
			}
			elementData.isListening = true;
		}

		public virtual void StopListening(global::Unity.VisualScripting.GraphStack stack)
		{
			global::Unity.VisualScripting.EventUnit<TArgs>.Data elementData = stack.GetElementData<global::Unity.VisualScripting.EventUnit<TArgs>.Data>(this);
			if (!elementData.isListening)
			{
				return;
			}
			foreach (global::Unity.VisualScripting.Flow activeCoroutine in elementData.activeCoroutines)
			{
				activeCoroutine.StopCoroutine(disposeInstantly: false);
			}
			if (register)
			{
				global::Unity.VisualScripting.EventBus.Unregister(elementData.hook, elementData.handler);
				stack.ClearReference();
				elementData.handler = null;
			}
			elementData.isListening = false;
		}

		public override void Uninstantiate(global::Unity.VisualScripting.GraphReference instance)
		{
			StopAllCoroutines(instance.GetElementData<global::Unity.VisualScripting.EventUnit<TArgs>.Data>(this).activeCoroutines.ToHashSetPooled());
			base.Uninstantiate(instance);
		}

		private static void StopAllCoroutines(global::System.Collections.Generic.HashSet<global::Unity.VisualScripting.Flow> activeCoroutines)
		{
			foreach (global::Unity.VisualScripting.Flow activeCoroutine in activeCoroutines)
			{
				activeCoroutine.StopCoroutineImmediate();
			}
			activeCoroutines.Free();
		}

		public bool IsListening(global::Unity.VisualScripting.GraphPointer pointer)
		{
			if (!pointer.hasData)
			{
				return false;
			}
			return pointer.GetElementData<global::Unity.VisualScripting.EventUnit<TArgs>.Data>(this).isListening;
		}

		public void Trigger(global::Unity.VisualScripting.GraphReference reference, TArgs args)
		{
			InternalTrigger(reference, args);
		}

		private protected virtual void InternalTrigger(global::Unity.VisualScripting.GraphReference reference, TArgs args)
		{
			global::Unity.VisualScripting.Flow flow = global::Unity.VisualScripting.Flow.New(reference);
			if (!ShouldTrigger(flow, args))
			{
				flow.Dispose();
				return;
			}
			AssignArguments(flow, args);
			Run(flow);
		}

		protected virtual bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, TArgs args)
		{
			return true;
		}

		protected virtual void AssignArguments(global::Unity.VisualScripting.Flow flow, TArgs args)
		{
		}

		private void Run(global::Unity.VisualScripting.Flow flow)
		{
			if (flow.enableDebug)
			{
				global::Unity.VisualScripting.IUnitDebugData elementDebugData = flow.stack.GetElementDebugData<global::Unity.VisualScripting.IUnitDebugData>(this);
				elementDebugData.lastInvokeFrame = global::Unity.VisualScripting.EditorTimeBinding.frame;
				elementDebugData.lastInvokeTime = global::Unity.VisualScripting.EditorTimeBinding.time;
			}
			if (coroutine)
			{
				flow.StartCoroutine(trigger, flow.stack.GetElementData<global::Unity.VisualScripting.EventUnit<TArgs>.Data>(this).activeCoroutines);
			}
			else
			{
				flow.Run(trigger);
			}
		}

		protected static bool CompareNames(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.ValueInput namePort, string calledName)
		{
			global::Unity.VisualScripting.Ensure.That("calledName").IsNotNull(calledName);
			return calledName.Trim().Equals(flow.GetValue<string>(namePort)?.Trim(), global::System.StringComparison.OrdinalIgnoreCase);
		}
	}
}
