namespace Unity.VisualScripting
{
	public interface IGraphNest : global::Unity.VisualScripting.IAotStubbable
	{
		global::Unity.VisualScripting.IGraphNester nester { get; set; }

		global::Unity.VisualScripting.GraphSource source { get; set; }

		global::Unity.VisualScripting.IGraph embed { get; set; }

		global::Unity.VisualScripting.IMacro macro { get; set; }

		global::Unity.VisualScripting.IGraph graph { get; }

		global::System.Type graphType { get; }

		global::System.Type macroType { get; }

		bool hasBackgroundEmbed { get; }
	}
}
