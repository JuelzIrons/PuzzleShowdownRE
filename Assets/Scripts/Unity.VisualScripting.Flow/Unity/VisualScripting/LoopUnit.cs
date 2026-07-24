namespace Unity.VisualScripting
{
	public abstract class LoopUnit : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		public global::Unity.VisualScripting.ControlOutput body { get; private set; }

		protected override void Definition()
		{
			enter = ControlInputCoroutine("enter", Loop, LoopCoroutine);
			exit = ControlOutput("exit");
			body = ControlOutput("body");
			Succession(enter, body);
			Succession(enter, exit);
		}

		protected abstract global::Unity.VisualScripting.ControlOutput Loop(global::Unity.VisualScripting.Flow flow);

		protected abstract global::System.Collections.IEnumerator LoopCoroutine(global::Unity.VisualScripting.Flow flow);
	}
}
