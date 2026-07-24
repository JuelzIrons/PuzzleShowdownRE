namespace Unity.VisualScripting
{
	public abstract class EventMachine<TGraph, TMacro> : global::Unity.VisualScripting.Machine<TGraph, TMacro>, global::Unity.VisualScripting.IEventMachine, global::Unity.VisualScripting.IMachine, global::Unity.VisualScripting.IGraphRoot, global::Unity.VisualScripting.IGraphParent, global::Unity.VisualScripting.IGraphNester, global::Unity.VisualScripting.IAotStubbable where TGraph : class, global::Unity.VisualScripting.IGraph, new() where TMacro : global::Unity.VisualScripting.Macro<TGraph>, new()
	{
		protected void TriggerEvent(string name)
		{
			if (base.hasGraph)
			{
				TriggerRegisteredEvent(new global::Unity.VisualScripting.EventHook(name, this), default(global::Unity.VisualScripting.EmptyEventArgs));
			}
		}

		protected void TriggerEvent<TArgs>(string name, TArgs args)
		{
			if (base.hasGraph)
			{
				TriggerRegisteredEvent(new global::Unity.VisualScripting.EventHook(name, this), args);
			}
		}

		protected void TriggerUnregisteredEvent(string name)
		{
			if (base.hasGraph)
			{
				TriggerUnregisteredEvent(name, default(global::Unity.VisualScripting.EmptyEventArgs));
			}
		}

		protected virtual void TriggerRegisteredEvent<TArgs>(global::Unity.VisualScripting.EventHook hook, TArgs args)
		{
			global::Unity.VisualScripting.EventBus.Trigger(hook, args);
		}

		protected virtual void TriggerUnregisteredEvent<TArgs>(global::Unity.VisualScripting.EventHook hook, TArgs args)
		{
			using global::Unity.VisualScripting.GraphStack graphStack = base.reference.ToStackPooled();
			graphStack.TriggerEventHandler((global::Unity.VisualScripting.EventHook _hook) => _hook == hook, args, (global::Unity.VisualScripting.IGraphParentElement parent) => true, force: true);
			graphStack.ClearReference();
		}

		protected override void Awake()
		{
			base.Awake();
			global::Unity.VisualScripting.GlobalMessageListener.Require();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			TriggerEvent("OnEnable");
		}

		protected virtual void Start()
		{
			TriggerEvent("Start");
		}

		protected override void OnInstantiateWhileEnabled()
		{
			base.OnInstantiateWhileEnabled();
			TriggerEvent("OnEnable");
		}

		protected virtual void Update()
		{
			TriggerEvent("Update");
		}

		protected virtual void FixedUpdate()
		{
			TriggerEvent("FixedUpdate");
		}

		protected virtual void LateUpdate()
		{
			TriggerEvent("LateUpdate");
		}

		protected override void OnUninstantiateWhileEnabled()
		{
			TriggerEvent("OnDisable");
			base.OnUninstantiateWhileEnabled();
		}

		protected override void OnDisable()
		{
			TriggerEvent("OnDisable");
			base.OnDisable();
		}

		protected override void OnDestroy()
		{
			try
			{
				TriggerEvent("OnDestroy");
			}
			finally
			{
				base.OnDestroy();
			}
		}

		public override void TriggerAnimationEvent(global::UnityEngine.AnimationEvent animationEvent)
		{
			TriggerEvent("AnimationEvent", animationEvent);
		}

		public override void TriggerUnityEvent(string name)
		{
			TriggerEvent("UnityEvent", name);
		}

		protected virtual void OnDrawGizmos()
		{
			TriggerUnregisteredEvent("OnDrawGizmos");
		}

		protected virtual void OnDrawGizmosSelected()
		{
			TriggerUnregisteredEvent("OnDrawGizmosSelected");
		}
	}
}
