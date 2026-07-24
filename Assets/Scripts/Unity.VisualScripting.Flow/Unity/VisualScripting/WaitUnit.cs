namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Time")]
	public abstract class WaitUnit : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInputCoroutine("enter", Await);
			exit = ControlOutput("exit");
			Succession(enter, exit);
		}

		protected abstract global::System.Collections.IEnumerator Await(global::Unity.VisualScripting.Flow flow);
	}
}
