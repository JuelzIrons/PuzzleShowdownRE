namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitCategory("Graphs/Graph Nodes")]
	public abstract class GetGraphs<TGraph, TGraphAsset, TMachine> : global::Unity.VisualScripting.Unit where TGraph : class, global::Unity.VisualScripting.IGraph, new() where TGraphAsset : global::Unity.VisualScripting.Macro<TGraph> where TMachine : global::Unity.VisualScripting.Machine<TGraph, TGraphAsset>
	{
		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabelHidden]
		[global::Unity.VisualScripting.NullMeansSelf]
		public global::Unity.VisualScripting.ValueInput gameObject { get; protected set; }

		[global::Unity.VisualScripting.DoNotSerialize]
		[global::Unity.VisualScripting.PortLabel("Graphs")]
		[global::Unity.VisualScripting.PortLabelHidden]
		public global::Unity.VisualScripting.ValueOutput graphList { get; protected set; }

		protected override void Definition()
		{
			gameObject = ValueInput<global::UnityEngine.GameObject>("gameObject", null).NullMeansSelf();
			graphList = ValueOutput("graphList", Get);
		}

		private global::System.Collections.Generic.List<TGraphAsset> Get(global::Unity.VisualScripting.Flow flow)
		{
			global::UnityEngine.GameObject go = flow.GetValue<global::UnityEngine.GameObject>(gameObject);
			return global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Select(global::System.Linq.Enumerable.Where(go.GetComponents<TMachine>(), (TMachine machine) => go.GetComponent<TMachine>().nest.macro != null), (TMachine machine) => machine.nest.macro));
		}
	}
}
