namespace Unity.VisualScripting
{
	[global::Unity.VisualScripting.UnitSurtitle("Scene")]
	public sealed class IsSceneVariableDefined : global::Unity.VisualScripting.IsVariableDefinedUnit, global::Unity.VisualScripting.ISceneVariableUnit, global::Unity.VisualScripting.IVariableUnit, global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		global::Unity.VisualScripting.FlowGraph global::Unity.VisualScripting.IUnit.graph => base.graph;

		public IsSceneVariableDefined()
		{
		}

		public IsSceneVariableDefined(string defaultName)
			: base(defaultName)
		{
		}

		protected override global::Unity.VisualScripting.VariableDeclarations GetDeclarations(global::Unity.VisualScripting.Flow flow)
		{
			global::UnityEngine.SceneManagement.Scene? scene = flow.stack.scene;
			if (!scene.HasValue)
			{
				return null;
			}
			return global::Unity.VisualScripting.Variables.Scene(scene.Value);
		}
	}
}
