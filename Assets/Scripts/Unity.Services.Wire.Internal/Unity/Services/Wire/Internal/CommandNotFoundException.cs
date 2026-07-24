namespace Unity.Services.Wire.Internal
{
	internal class CommandNotFoundException : global::System.Exception
	{
		public CommandNotFoundException(uint id)
			: base($"Command not found (id: {id})")
		{
		}
	}
}
