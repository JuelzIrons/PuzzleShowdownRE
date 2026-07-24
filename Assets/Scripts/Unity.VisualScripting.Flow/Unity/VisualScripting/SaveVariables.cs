namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Variables")]
	public sealed class SaveVariables : global::Unity.VisualScripting.Unit
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlInput enter { get; private set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ControlOutput exit { get; private set; }

		protected override void Definition()
		{
			enter = ControlInput("enter", Enter);
			exit = ControlOutput("exit");
			Succession(enter, exit);
		}

		private global::Unity.VisualScripting.ControlOutput Enter(global::Unity.VisualScripting.Flow arg)
		{
			global::Unity.VisualScripting.SavedVariables.SaveDeclarations(global::Unity.VisualScripting.SavedVariables.merged);
			return exit;
		}
	}
}
