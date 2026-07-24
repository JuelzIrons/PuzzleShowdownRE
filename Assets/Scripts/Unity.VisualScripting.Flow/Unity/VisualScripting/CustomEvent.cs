namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Events")]
	[global::Unity.VisualScripting.UnitOrder(0)]
	public sealed class CustomEvent : global::Unity.VisualScripting.GameObjectEventUnit<global::Unity.VisualScripting.CustomEventArgs>
	{
		[global::Unity.VisualScripting.SerializeAs("argumentCount")]
		private int _argumentCount;

		public override global::System.Type MessageListenerType => null;

		protected override string hookName => "Custom";

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Arguments")]
		public int argumentCount
		{
			get
			{
				return _argumentCount;
			}
			set
			{
				_argumentCount = global::UnityEngine.Mathf.Clamp(value, 0, 10);
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput name { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.List<global::Unity.VisualScripting.ValueOutput> argumentPorts { get; } = new global::System.Collections.Generic.List<global::Unity.VisualScripting.ValueOutput>();

		protected override void Definition()
		{
			base.Definition();
			name = ValueInput("name", string.Empty);
			argumentPorts.Clear();
			for (int i = 0; i < argumentCount; i++)
			{
				argumentPorts.Add(ValueOutput<object>("argument_" + i));
			}
		}

		protected override bool ShouldTrigger(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.CustomEventArgs args)
		{
			return global::Unity.VisualScripting.EventUnit<global::Unity.VisualScripting.CustomEventArgs>.CompareNames(flow, name, args.name);
		}

		protected override void AssignArguments(global::Unity.VisualScripting.Flow flow, global::Unity.VisualScripting.CustomEventArgs args)
		{
			for (int i = 0; i < argumentCount; i++)
			{
				flow.SetValue(argumentPorts[i], args.arguments[i]);
			}
		}

		public static void Trigger(global::UnityEngine.GameObject target, string name, params object[] args)
		{
			global::Unity.VisualScripting.EventBus.Trigger("Custom", target, new global::Unity.VisualScripting.CustomEventArgs(name, args));
		}
	}
}
