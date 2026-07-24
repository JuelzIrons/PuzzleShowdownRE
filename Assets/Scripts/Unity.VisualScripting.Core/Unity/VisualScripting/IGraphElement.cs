namespace Unity.VisualScripting
{
	public interface IGraphElement : global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		new global::Unity.VisualScripting.IGraph graph { get; set; }

		int dependencyOrder { get; }

		new global::System.Guid guid { get; set; }

		global::System.Collections.Generic.IEnumerable<global::Unity.VisualScripting.ISerializationDependency> deserializationDependencies { get; }

		bool HandleDependencies();

		void Instantiate(global::Unity.VisualScripting.GraphReference instance);

		void Uninstantiate(global::Unity.VisualScripting.GraphReference instance);
	}
}
