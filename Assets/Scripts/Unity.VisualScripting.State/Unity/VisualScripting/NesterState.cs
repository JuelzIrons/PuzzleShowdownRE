namespace Unity.VisualScripting
{
	public abstract class NesterState<TGraph, TMacro> : global::Unity.VisualScripting.State, global::Unity.VisualScripting.INesterState, global::Unity.VisualScripting.IState, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IGraphElementWithData, global::Unity.VisualScripting.IGraphNesterElement, global::Unity.VisualScripting.IGraphParentElement, global::Unity.VisualScripting.IGraphParent, global::Unity.VisualScripting.IGraphNester where TGraph : class, global::Unity.VisualScripting.IGraph, new() where TMacro : global::Unity.VisualScripting.Macro<TGraph>
	{
		[global::Unity.VisualScripting.Serialize]
		public global::Unity.VisualScripting.GraphNest<TGraph, TMacro> nest { get; private set; } = new global::Unity.VisualScripting.GraphNest<TGraph, TMacro>();

		[global::Unity.VisualScripting.DoNotSerialize]
		global::Unity.VisualScripting.IGraphNest global::Unity.VisualScripting.IGraphNester.nest => nest;

		[global::Unity.VisualScripting.DoNotSerialize]
		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphParent.childGraph => nest.graph;

		[global::Unity.VisualScripting.DoNotSerialize]
		bool global::Unity.VisualScripting.IGraphParent.isSerializationRoot => nest.source == global::Unity.VisualScripting.GraphSource.Macro;

		[global::Unity.VisualScripting.DoNotSerialize]
		global::UnityEngine.Object global::Unity.VisualScripting.IGraphParent.serializedObject => nest.macro;

		[global::Unity.VisualScripting.DoNotSerialize]
		public override global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ISerializationDependency> deserializationDependencies => nest.deserializationDependencies;

		global::Unity.VisualScripting.StateGraph global::Unity.VisualScripting.IState.graph => base.graph;

		protected NesterState()
		{
			nest.nester = this;
		}

		protected NesterState(TMacro macro)
		{
			nest.nester = this;
			nest.macro = macro;
			nest.source = global::Unity.VisualScripting.GraphSource.Macro;
		}

		protected void CopyFrom(global::Unity.VisualScripting.NesterState<TGraph, TMacro> source)
		{
			CopyFrom((global::Unity.VisualScripting.State)source);
			nest = source.nest;
		}

		public override global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			return global::Unity.VisualScripting.LinqUtility.Concat<object>(new global::System.Collections.IEnumerable[2]
			{
				base.GetAotStubs(visited),
				nest.GetAotStubs(visited)
			});
		}

		public abstract TGraph DefaultGraph();

		global::Unity.VisualScripting.IGraph global::Unity.VisualScripting.IGraphParent.DefaultGraph()
		{
			return DefaultGraph();
		}

		void global::Unity.VisualScripting.IGraphNester.InstantiateNest()
		{
			InstantiateNest();
		}

		void global::Unity.VisualScripting.IGraphNester.UninstantiateNest()
		{
			UninstantiateNest();
		}
	}
}
