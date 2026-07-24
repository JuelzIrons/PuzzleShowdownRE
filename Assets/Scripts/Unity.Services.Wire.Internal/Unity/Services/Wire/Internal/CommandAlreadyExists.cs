namespace Unity.Services.Wire.Internal
{
	internal class CommandAlreadyExists : global::System.Exception
	{
		public CommandAlreadyExists(uint id)
			: base($"Command already exists (id: {id})")
		{
		}
	}
}
