namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitTitle("For Each Loop")]
	[global::Unity.VisualScripting.UnitCategory("Control")]
	[global::Unity.VisualScripting.UnitOrder(10)]
	public class ForEach : global::Unity.VisualScripting.LoopUnit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueInput collection { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Index")]
		public global::Unity.VisualScripting.ValueOutput currentIndex { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Key")]
		public global::Unity.VisualScripting.ValueOutput currentKey { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Item")]
		public global::Unity.VisualScripting.ValueOutput currentItem { get; private set; }

		[global::Unity.VisualScripting.Serialize]
		[global::Unity.VisualScripting.Inspectable]
		[global::Unity.VisualScripting.UnitHeaderInspectable("Dictionary")]
		[global::Unity.VisualScripting.InspectorToggleLeft]
		public bool dictionary { get; set; }

		protected override void Definition()
		{
			base.Definition();
			if (dictionary)
			{
				collection = ValueInput<global::System.Collections.IDictionary>("collection");
			}
			else
			{
				collection = ValueInput<global::System.Collections.IEnumerable>("collection");
			}
			currentIndex = ValueOutput<int>("currentIndex");
			if (dictionary)
			{
				currentKey = ValueOutput<object>("currentKey");
			}
			currentItem = ValueOutput<object>("currentItem");
			Requirement(collection, base.enter);
			Assignment(base.enter, currentIndex);
			Assignment(base.enter, currentItem);
			if (dictionary)
			{
				Assignment(base.enter, currentKey);
			}
		}

		private int Start(global::Unity.VisualScripting.Flow flow, out global::System.Collections.IEnumerator enumerator, out global::System.Collections.IDictionaryEnumerator dictionaryEnumerator, out int currentIndex)
		{
			if (dictionary)
			{
				dictionaryEnumerator = flow.GetValue<global::System.Collections.IDictionary>(collection).GetEnumerator();
				enumerator = dictionaryEnumerator;
			}
			else
			{
				enumerator = flow.GetValue<global::System.Collections.IEnumerable>(collection).GetEnumerator();
				dictionaryEnumerator = null;
			}
			currentIndex = -1;
			return flow.EnterLoop();
		}

		private bool MoveNext(global::Unity.VisualScripting.Flow flow, global::System.Collections.IEnumerator enumerator, global::System.Collections.IDictionaryEnumerator dictionaryEnumerator, ref int currentIndex)
		{
			bool num = enumerator.MoveNext();
			if (num)
			{
				if (dictionary)
				{
					flow.SetValue(currentKey, dictionaryEnumerator.Key);
					flow.SetValue(currentItem, dictionaryEnumerator.Value);
				}
				else
				{
					flow.SetValue(currentItem, enumerator.Current);
				}
				currentIndex++;
				flow.SetValue(this.currentIndex, currentIndex);
			}
			return num;
		}

		protected override global::Unity.VisualScripting.ControlOutput Loop(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IEnumerator enumerator;
			global::System.Collections.IDictionaryEnumerator dictionaryEnumerator;
			int num;
			int loop = Start(flow, out enumerator, out dictionaryEnumerator, out num);
			global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
			try
			{
				while (flow.LoopIsNotBroken(loop) && MoveNext(flow, enumerator, dictionaryEnumerator, ref num))
				{
					flow.Invoke(base.body);
					flow.RestoreStack(stack);
				}
			}
			finally
			{
				(enumerator as global::System.IDisposable)?.Dispose();
			}
			flow.DisposePreservedStack(stack);
			flow.ExitLoop(loop);
			return base.exit;
		}

		protected override global::System.Collections.IEnumerator LoopCoroutine(global::Unity.VisualScripting.Flow flow)
		{
			global::System.Collections.IEnumerator enumerator;
			global::System.Collections.IDictionaryEnumerator dictionaryEnumerator;
			int currentIndex;
			int loop = Start(flow, out enumerator, out dictionaryEnumerator, out currentIndex);
			global::Unity.VisualScripting.GraphStack stack = flow.PreserveStack();
			try
			{
				while (flow.LoopIsNotBroken(loop) && MoveNext(flow, enumerator, dictionaryEnumerator, ref currentIndex))
				{
					yield return base.body;
					flow.RestoreStack(stack);
				}
			}
			finally
			{
				(enumerator as global::System.IDisposable)?.Dispose();
			}
			flow.DisposePreservedStack(stack);
			flow.ExitLoop(loop);
			yield return base.exit;
		}
	}
}
