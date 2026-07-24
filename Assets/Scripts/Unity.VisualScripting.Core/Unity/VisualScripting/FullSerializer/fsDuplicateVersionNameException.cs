namespace Unity.VisualScripting.FullSerializer
{
	public sealed class fsDuplicateVersionNameException : global::System.Exception
	{
		public fsDuplicateVersionNameException(global::System.Type typeA, global::System.Type typeB, string version)
			: base(typeA?.ToString() + " and " + typeB?.ToString() + " have the same version string (" + version + "); please change one of them.")
		{
		}
	}
}
