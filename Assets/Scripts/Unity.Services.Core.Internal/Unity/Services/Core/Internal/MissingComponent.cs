namespace Unity.Services.Core.Internal
{
	internal class MissingComponent : global::Unity.Services.Core.Internal.IServiceComponent
	{
		public global::System.Type IntendedType { get; }

		internal MissingComponent(global::System.Type intendedType)
		{
			IntendedType = intendedType;
		}
	}
}
