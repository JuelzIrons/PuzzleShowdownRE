namespace Unity.Services.Multiplayer
{
	public interface IMigrationDataHandler
	{
		byte[] Generate();

		void Apply(byte[] migrationData);
	}
}
