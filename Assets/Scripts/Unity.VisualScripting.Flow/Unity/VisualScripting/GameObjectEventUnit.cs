namespace Unity.VisualScripting
{
	public abstract class GameObjectEventUnit<TArgs> : global::Unity.VisualScripting.EventUnit<TArgs>, global::Unity.VisualScripting.IGameObjectEventUnit, global::Unity.VisualScripting.IEventUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IGraphEventListener
	{
		public new class Data : global::Unity.VisualScripting.EventUnit<TArgs>.Data
		{
			public global::UnityEngine.GameObject target;
		}

		protected sealed override bool register => true;

		public abstract global::System.Type MessageListenerType { get; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.NullMeansSelf]
		[global::Unity.VisualScripting.PortLabel("Target")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput target { get; private set; }

		protected virtual string hookName
		{
			get
			{
				throw new global::Unity.VisualScripting.InvalidImplementationException($"Missing event hook for '{this}'.");
			}
		}

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		public override global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.GameObjectEventUnit<TArgs>.Data();
		}

		protected override void Definition()
		{
			base.Definition();
			target = ValueInput<global::UnityEngine.GameObject>("target", null).NullMeansSelf();
		}

		public override global::Unity.VisualScripting.EventHook GetHook(global::Unity.VisualScripting.GraphReference reference)
		{
			if (!reference.hasData)
			{
				return hookName;
			}
			global::Unity.VisualScripting.GameObjectEventUnit<TArgs>.Data elementData = reference.GetElementData<global::Unity.VisualScripting.GameObjectEventUnit<TArgs>.Data>(this);
			return new global::Unity.VisualScripting.EventHook(hookName, elementData.target);
		}

		private void UpdateTarget(global::Unity.VisualScripting.GraphStack stack)
		{
			global::Unity.VisualScripting.GameObjectEventUnit<TArgs>.Data elementData = stack.GetElementData<global::Unity.VisualScripting.GameObjectEventUnit<TArgs>.Data>(this);
			bool isListening = elementData.isListening;
			global::UnityEngine.GameObject gameObject = global::Unity.VisualScripting.Flow.FetchValue<global::UnityEngine.GameObject>(target, stack.ToReference());
			if (gameObject != elementData.target)
			{
				if (isListening)
				{
					StopListening(stack);
				}
				elementData.target = gameObject;
				if (isListening)
				{
					StartListening(stack, updateTarget: false);
				}
			}
		}

		protected void StartListening(global::Unity.VisualScripting.GraphStack stack, bool updateTarget)
		{
			if (updateTarget)
			{
				UpdateTarget(stack);
			}
			global::Unity.VisualScripting.GameObjectEventUnit<TArgs>.Data elementData = stack.GetElementData<global::Unity.VisualScripting.GameObjectEventUnit<TArgs>.Data>(this);
			if (!(elementData.target == null))
			{
				if (global::Unity.VisualScripting.UnityThread.allowsAPI && MessageListenerType != null)
				{
					global::Unity.VisualScripting.MessageListener.AddTo(MessageListenerType, elementData.target);
				}
				base.StartListening(stack);
			}
		}

		public override void StartListening(global::Unity.VisualScripting.GraphStack stack)
		{
			StartListening(stack, updateTarget: true);
		}
	}
}
