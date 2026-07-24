namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Time")]
	[global::Unity.VisualScripting.UnitOrder(6)]
	[global::Unity.VisualScripting.TypeIcon(typeof(global::Unity.VisualScripting.WaitUnit))]
	public sealed class WaitForFlow : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		public sealed class Data : global::Unity.VisualScripting.IGraphElementData
		{
			public bool[] inputsActivated;

			public bool isWaitingCoroutine;
		}

		[global::Unity.VisualScripting.SerializeAs("inputCount")]
		private int _inputCount = 2;

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		public bool resetOnExit { get; set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Inputs")]
		public int inputCount
		{
			get
			{
				return _inputCount;
			}
			set
			{
				_inputCount = global::UnityEngine.Mathf.Clamp(value, 2, 10);
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.ObjectModel.ReadOnlyCollection<global::Unity.VisualScripting.ControlInput> awaitedInputs { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput reset { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			global::System.Collections.Generic.List<global::Unity.VisualScripting.ControlInput> list = new global::System.Collections.Generic.List<global::Unity.VisualScripting.ControlInput>();
			awaitedInputs = list.AsReadOnly();
			exit = ControlOutput("exit");
			for (int i = 0; i < inputCount; i++)
			{
				int _i = i;
				global::Unity.VisualScripting.ControlInput controlInput = ControlInputCoroutine(_i.ToString(), (global::Unity.VisualScripting.Flow flow) => Enter(flow, _i), (global::Unity.VisualScripting.Flow flow) => EnterCoroutine(flow, _i));
				list.Add(controlInput);
				Succession(controlInput, exit);
			}
			reset = ControlInput("reset", Reset);
		}

		public global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.WaitForFlow.Data
			{
				inputsActivated = new bool[inputCount]
			};
		}

		private global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow, int index)
		{
			flow.stack.GetElementData<global::Unity.VisualScripting.WaitForFlow.Data>(this).inputsActivated[index] = true;
			if (CheckActivated(flow))
			{
				if (resetOnExit)
				{
					Reset(flow);
				}
				return exit;
			}
			return null;
		}

		private bool CheckActivated(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.WaitForFlow.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.WaitForFlow.Data>(this);
			for (int i = 0; i < elementData.inputsActivated.Length; i++)
			{
				if (!elementData.inputsActivated[i])
				{
					return false;
				}
			}
			return true;
		}

		private global::System.Collections.IEnumerator EnterCoroutine(global::Unity.VisualScripting.Flow flow, int index)
		{
			global::Unity.VisualScripting.WaitForFlow.Data data = flow.stack.GetElementData<global::Unity.VisualScripting.WaitForFlow.Data>(this);
			data.inputsActivated[index] = true;
			if (data.isWaitingCoroutine)
			{
				yield break;
			}
			if (!CheckActivated(flow))
			{
				data.isWaitingCoroutine = true;
				yield return new global::UnityEngine.WaitUntil(() => CheckActivated(flow));
				data.isWaitingCoroutine = false;
			}
			if (resetOnExit)
			{
				Reset(flow);
			}
			yield return exit;
		}

		private global::Unity.VisualScripting.ControlOutput Reset(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.WaitForFlow.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.WaitForFlow.Data>(this);
			for (int i = 0; i < elementData.inputsActivated.Length; i++)
			{
				elementData.inputsActivated[i] = false;
			}
			return null;
		}
	}
}
