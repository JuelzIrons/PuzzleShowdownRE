namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Graphs/Graph Nodes")]
	public abstract class GetGraph<TGraph, TGraphAsset, TMachine> : global::Unity.VisualScripting.Unit where TGraph : class, global::Unity.VisualScripting.IGraph, new() where TGraphAsset : global::Unity.VisualScripting.Macro<TGraph> where TMachine : global::Unity.VisualScripting.Machine<TGraph, TGraphAsset>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput gameObject { get; protected set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Graph")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput graphOutput { get; protected set; }

		protected override void Definition()
		{
			gameObject = ValueInput<global::UnityEngine.GameObject>("gameObject", null).NullMeansSelf();
			graphOutput = ValueOutput("graphOutput", Get);
		}

		private TGraphAsset Get(global::Unity.VisualScripting.Flow flow)
		{
			return flow.GetValue<global::UnityEngine.GameObject>(gameObject).GetComponent<TMachine>().nest.macro;
		}
	}
}
