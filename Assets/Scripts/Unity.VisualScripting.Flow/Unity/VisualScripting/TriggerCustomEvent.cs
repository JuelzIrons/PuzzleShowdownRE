namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitSurtitle("Custom Event")]
	[global::Unity.VisualScripting.UnitShortTitle("Trigger")]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.CustomEvent))]
	[global::Unity.VisualScripting.UnitCategory("Events")]
	[global::Unity.VisualScripting.UnitOrder(1)]
	public sealed class TriggerCustomEvent : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.SerializeAs("argumentCount")]
		private int _argumentCount;

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.Generic.List<global::Unity.VisualScripting.ValueInput> arguments { get; private set; }

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
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput name { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput target { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Trigger);
			exit = ControlOutput("exit");
			name = ValueInput("name", string.Empty);
			target = ValueInput<global::UnityEngine.GameObject>("target", null).NullMeansSelf();
			arguments = new global::System.Collections.Generic.List<global::Unity.VisualScripting.ValueInput>();
			for (int i = 0; i < argumentCount; i++)
			{
				global::Unity.VisualScripting.ValueInput valueInput = ValueInput<object>("argument_" + i);
				arguments.Add(valueInput);
				Requirement(valueInput, enter);
			}
			Requirement(name, enter);
			Requirement(target, enter);
			Succession(enter, exit);
		}

		private global::Unity.VisualScripting.ControlOutput Trigger(global::Unity.VisualScripting.Flow flow)
		{
			global::UnityEngine.GameObject value = flow.GetValue<global::UnityEngine.GameObject>(target);
			string value2 = flow.GetValue<string>(name);
			object[] args = global::System.Linq.Enumerable.ToArray(global::System.Linq.Enumerable.Select(arguments, flow.GetConvertedValue));
			global::Unity.VisualScripting.CustomEvent.Trigger(value, value2, args);
			return exit;
		}
	}
}
