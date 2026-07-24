namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(14)]
	public sealed class Once : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		public sealed class Data : global::Unity.VisualScripting.IGraphElementData
		{
			public bool executed;
		}

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlInput reset { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput once { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput after { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Enter);
			reset = ControlInput("reset", Reset);
			once = ControlOutput("once");
			after = ControlOutput("after");
			Succession(enter, once);
			Succession(enter, after);
		}

		public global::Unity.VisualScripting.IGraphElementData CreateData()
		{
			return new global::Unity.VisualScripting.Once.Data();
		}

		public global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow flow)
		{
			global::Unity.VisualScripting.Once.Data elementData = flow.stack.GetElementData<global::Unity.VisualScripting.Once.Data>(this);
			if (!elementData.executed)
			{
				elementData.executed = true;
				return once;
			}
			return after;
		}

		public global::Unity.VisualScripting.ControlOutput Reset(global::Unity.VisualScripting.Flow flow)
		{
			flow.stack.GetElementData<global::Unity.VisualScripting.Once.Data>(this).executed = false;
			return null;
		}
	}
}
