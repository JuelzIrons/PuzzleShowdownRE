namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.SpecialUnit]
	public abstract class NesterUnit<TGraph, TMacro> : global::Unity.VisualScripting.Unit, global::Unity.VisualScripting.INesterUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable, global::Unity.VisualScripting.IGraphNesterElement, global::Unity.VisualScripting.IGraphParentElement, global::Unity.VisualScripting.IGraphParent, global::Unity.VisualScripting.IGraphNester where TGraph : class, global::Unity.VisualScripting.IGraph, new() where TMacro : global::Unity.VisualScripting.Macro<TGraph>
	{
		public override bool canDefine => nest.graph != null;

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

		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		protected NesterUnit()
		{
			nest.nester = this;
		}

		protected NesterUnit(TMacro macro)
		{
			nest.nester = this;
			nest.macro = macro;
			nest.source = global::Unity.VisualScripting.GraphSource.Macro;
		}

		public override global::System.Collections.Generic.IEnumerable<object> GetAotStubs(global::System.Collections.Generic.HashSet<object> visited)
		{
			return global::Unity.VisualScripting.LinqUtility.Concat<object>(new global::System.Collections.IEnumerable[2]
			{
				base.GetAotStubs(visited),
				nest.GetAotStubs(visited)
			});
		}

		protected void CopyFrom(global::Unity.VisualScripting.NesterUnit<TGraph, TMacro> source)
		{
			CopyFrom((global::Unity.VisualScripting.Unit)source);
			nest = source.nest;
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
