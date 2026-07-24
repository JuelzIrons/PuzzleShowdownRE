namespace Unity.VisualScripting
{
	[global::UnityEngine.AddComponentMenu("Visual Scripting/State Machine")]
	[global::UnityEngine.RequireComponent(typeof(global::Unity.VisualScripting.Variables))]
	[global::Unity.VisualScripting.DisableAnnotation]
	public sealed class StateMachine : global::Unity.VisualScripting.EventMachine<global::Unity.VisualScripting.StateGraph, global::Unity.VisualScripting.StateGraphAsset>
	{
		protected override void OnEnable()
		{
			if (base.hasGraph)
			{
				using global::Unity.VisualScripting.Flow flow = global::Unity.VisualScripting.Flow.New(base.reference);
				base.graph.Start(flow);
			}
			base.OnEnable();
		}

		protected override void OnInstantiateWhileEnabled()
		{
			if (base.hasGraph)
			{
				using global::Unity.VisualScripting.Flow flow = global::Unity.VisualScripting.Flow.New(base.reference);
				base.graph.Start(flow);
			}
			base.OnInstantiateWhileEnabled();
		}

		protected override void OnUninstantiateWhileEnabled()
		{
			base.OnUninstantiateWhileEnabled();
			if (base.hasGraph)
			{
				using (global::Unity.VisualScripting.Flow flow = global::Unity.VisualScripting.Flow.New(base.reference))
				{
					base.graph.Stop(flow);
				}
			}
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if (base.hasGraph)
			{
				using (global::Unity.VisualScripting.Flow flow = global::Unity.VisualScripting.Flow.New(base.reference))
				{
					base.graph.Stop(flow);
				}
			}
		}

		[global::UnityEngine.ContextMenu("Show Data...")]
		protected override void ShowData()
		{
			base.ShowData();
		}

		public override global::Unity.VisualScripting.StateGraph DefaultGraph()
		{
			return global::Unity.VisualScripting.StateGraph.WithStart();
		}
	}
}
