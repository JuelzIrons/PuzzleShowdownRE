namespace Unity.VisualScripting
{
	public abstract class MultiInputUnit<T> : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IMultiInputUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		[global::Unity.VisualScripting.SerializeAs("inputCount")]
		private int _inputCount = 2;

		[global::Unity.VisualScripting.DoNotSerialize]
		protected virtual int minInputCount => 2;

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Inputs")]
		public virtual int inputCount
		{
			get
			{
				return _inputCount;
			}
			set
			{
				_inputCount = global::UnityEngine.Mathf.Clamp(value, minInputCount, 10);
			}
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::System.Collections.ObjectModel.ReadOnlyCollection<global::Unity.VisualScripting.ValueInput> multiInputs { get; protected set; }

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected override void Definition()
		{
			global::System.Collections.Generic.List<global::Unity.VisualScripting.ValueInput> list = new global::System.Collections.Generic.List<global::Unity.VisualScripting.ValueInput>();
			multiInputs = list.AsReadOnly();
			for (int i = 0; i < inputCount; i++)
			{
				list.Add(ValueInput<T>(i.ToString()));
			}
		}

		protected void InputsAllowNull()
		{
			foreach (global::Unity.VisualScripting.ValueInput multiInput in multiInputs)
			{
				multiInput.AllowsNull();
			}
		}
	}
}
