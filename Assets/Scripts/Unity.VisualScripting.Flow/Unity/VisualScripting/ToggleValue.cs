namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(19)]
	[global::Unity.VisualScripting.UnitFooterPorts(ControlInputs = true, ControlOutputs = true)]
	public sealed class ToggleValue : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		public class Data : global::Unity.VisualScripting.IGraphElementData
		{
			public bool isOn;
		}

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Start On")]
		[global::Unity.VisualScripting.InspectorToggleLeft]
		public bool startOn { get; set; } = true;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("On")]
		public global::Unity.VisualScripting.ControlInput turnOn { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Off")]
		public global::Unity.VisualScripting.ControlInput turnOff { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput toggle { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput onValue { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueInput offValue { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput turnedOn { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput turnedOff { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput isOn { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ValueOutput value { get; private set; }

		protected override void Definition()
		{
			turnOn = ControlInput("turnOn", TurnOn);
			turnOff = ControlInput("turnOff", TurnOff);
			toggle = ControlInput("toggle", Toggle);
			onValue = ValueInput<object>("onValue");
			offValue = ValueInput<object>("offValue");
			turnedOn = ControlOutput("turnedOn");
			turnedOff = ControlOutput("turnedOff");
			isOn = ValueOutput("isOn", IsOn);
			value = ValueOutput("value", Value);
			Requirement(onValue, value);
			Requirement(offValue, value);
			Succession(turnOn, turnedOn);
			Succession(turnOff, turnedOff);
			Succession(toggle, turnedOn);
			Succession(toggle, turnedOff);
		}

		public global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.ToggleValue.Data
			{
				isOn = startOn
			};
		}

		private bool IsOn(global::Unity.VisualScripting.Flow flow)
		{
			return flow.stack.GetElementData<global::Unity.VisualScripting.ToggleValue.Data>(this).isOn;
		}

		private global::Unity.VisualScripting.ControlOutput TurnOn(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.ToggleValue.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.ToggleValue.Data>(this);
			if (elementData.isOn)
			{
				return null;
			}
			elementData.isOn = true;
			return turnedOn;
		}

		private global::Unity.VisualScripting.ControlOutput TurnOff(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.ToggleValue.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.ToggleValue.Data>(this);
			if (!elementData.isOn)
			{
				return null;
			}
			elementData.isOn = false;
			return turnedOff;
		}

		private global::Unity.VisualScripting.ControlOutput Toggle(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.ToggleValue.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.ToggleValue.Data>(this);
			elementData.isOn = !elementData.isOn;
			if (!elementData.isOn)
			{
				return turnedOff;
			}
			return turnedOn;
		}

		private object Value(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.ToggleValue.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.ToggleValue.Data>(this);
			return flow.GetValue(elementData.isOn ? onValue : offValue);
		}
	}
}
