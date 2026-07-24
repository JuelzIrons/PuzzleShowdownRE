namespace Unity.VisualScripting
{
	public interface IMultiInputUnit : global::Unity.VisualScripting.IUnit, global::Unity.VisualScripting.IGraphElementWithDebugData, global::Unity.VisualScripting.IGraphElement, global::Unity.VisualScripting.IGraphItem, global::Unity.VisualScripting.INotifiedCollectionItem, global::System.IDisposable, global::Unity.VisualScripting.IPrewarmable, global::Unity.VisualScripting.IAotStubbable, global::Unity.VisualScripting.IIdentifiable, global::Unity.VisualScripting.IAnalyticsIdentifiable
	{
		int inputCount { get; set; }

		global::System.Collections.ObjectModel.ReadOnlyCollection<global::Unity.VisualScripting.ValueInput> multiInputs { get; }
	}
}
