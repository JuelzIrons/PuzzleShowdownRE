namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("Visual Scripting/Script Machine")]
	[global::UnityEngine.RequireComponent(typeof(global::Unity.VisualScripting.Variables))]
	[global::Unity.VisualScripting.DisableAnnotation]
	[global::Unity.VisualScripting.RenamedFrom("Bolt.FlowMachine")]
	[global::Unity.VisualScripting.RenamedFrom("Unity.VisualScripting.FlowMachine")]
	public sealed class ScriptMachine : global::Unity.VisualScripting.EventMachine<global::Unity.VisualScripting.FlowGraph, global::Unity.VisualScripting.ScriptGraphAsset>
	{
		public override global::Unity.VisualScripting.FlowGraph DefaultGraph()
		{
			return global::Unity.VisualScripting.FlowGraph.WithStartUpdate();
		}

		protected override void OnEnable()
		{
			if (base.hasGraph)
			{
				base.graph.StartListening(base.reference);
			}
			base.OnEnable();
		}

		protected override void OnInstantiateWhileEnabled()
		{
			if (base.hasGraph)
			{
				base.graph.StartListening(base.reference);
			}
			base.OnInstantiateWhileEnabled();
		}

		protected override void OnUninstantiateWhileEnabled()
		{
			base.OnUninstantiateWhileEnabled();
			if (base.hasGraph)
			{
				base.graph.StopListening(base.reference);
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (base.hasGraph)
			{
				base.graph.StopListening(base.reference);
			}
		}

		[global::UnityEngine.ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}
	}
}
