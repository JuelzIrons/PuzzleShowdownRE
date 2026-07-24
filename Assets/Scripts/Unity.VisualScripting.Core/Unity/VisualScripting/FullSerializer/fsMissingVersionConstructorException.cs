namespace Unity.VisualScripting.FullSerializer
{
	public sealed class fsMissingVersionConstructorException : global::System.Exception
	{
		public fsMissingVersionConstructorException(global::System.Type versionedType, global::System.Type constructorType)
			: base(versionedType?.ToString() + " is missing a constructor for previous model type " + constructorType)
		{
		}
	}
}
