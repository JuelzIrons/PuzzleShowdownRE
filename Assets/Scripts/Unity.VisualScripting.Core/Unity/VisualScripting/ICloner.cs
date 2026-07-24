namespace Unity.VisualScripting
{
	public interface ICloner
	{
		bool Handles(global::System.Type type);

		object ConstructClone(global::System.Type type, object original);

		void BeforeClone(global::System.Type type, object original);

		void FillClone(global::System.Type type, ref object clone, object original, global::Unity.VisualScripting.CloningContext context);

		void AfterClone(global::System.Type type, object clone);
	}
}
