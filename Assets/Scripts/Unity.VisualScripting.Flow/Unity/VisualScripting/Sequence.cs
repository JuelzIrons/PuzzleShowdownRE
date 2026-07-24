namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(13)]
	public sealed class Sequence : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.SerializeAs("outputCount")]
		private int _outputCount = 2;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.InspectorLabel("Steps")]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Steps")]
		public int outputCount
		{
			get
			{
				return _outputCount;
			}
			set
			{
				_outputCount = global::UnityEngine.Mathf.Clamp(value, 1, 10);
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.ObjectModel.ReadOnlyCollection<global::Unity.VisualScripting.ControlOutput> multiOutputs { get; private set; }

		protected override void Definition()
		{
			enter = ControlInputCoroutine("enter", Enter, EnterCoroutine);
			global::System.Collections.Generic.List<global::Unity.VisualScripting.ControlOutput> list = new global::System.Collections.Generic.List<global::Unity.VisualScripting.ControlOutput>();
			multiOutputs = list.AsReadOnly();
			for (int i = 0; i < outputCount; i++)
			{
				global::Unity.VisualScripting.ControlOutput controlOutput = ControlOutput(i.ToString());
				Succession(enter, controlOutput);
				list.Add(controlOutput);
			}
		}

		private global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
			foreach (global::Unity.VisualScripting.ControlOutput multiOutput in multiOutputs)
			{
				flow.Invoke(multiOutput);
				flow.RestoreStack(stack);
			}
			flow.DisposePreservedStack(stack);
			return null;
		}

		private global::System.Collections.IEnumerator EnterCoroutine(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
			foreach (global::Unity.VisualScripting.ControlOutput multiOutput in multiOutputs)
			{
				yield return multiOutput;
				flow.RestoreStack(stack);
			}
			flow.DisposePreservedStack(stack);
		}

		public void CopyFrom(global::Unity.VisualScripting.Sequence source)
		{
			CopyFrom((global::Unity.VisualScripting.Unit)source);
			outputCount = source.outputCount;
		}
	}
}
