namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(16)]
	public sealed class Throw : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Custom")]
		[global::Unity.VisualScripting.InspectorToggleLeft]
		public bool custom { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput message { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput exception { get; private set; }

		protected override void Definition()
		{
			if (custom)
			{
				enter = ControlInput("enter", ThrowCustom);
				exception = ValueInput<global::System.Exception>("exception");
				Requirement(exception, enter);
			}
			else
			{
				enter = ControlInput("enter", ThrowMessage);
				message = ValueInput("message", string.Empty);
				Requirement(message, enter);
			}
		}

		private global::Unity.VisualScripting.ControlOutput ThrowCustom(global::Unity.VisualScripting.Flow flow)
		{
			throw flow.GetValue<global::System.Exception>(exception);
		}

		private global::Unity.VisualScripting.ControlOutput ThrowMessage(global::Unity.VisualScripting.Flow flow)
		{
			throw new global::System.Exception(flow.GetValue<string>(message));
		}
	}
}
