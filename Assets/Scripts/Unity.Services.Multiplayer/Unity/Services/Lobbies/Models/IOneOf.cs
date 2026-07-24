namespace Unity.Services.Lobbies.Models
{
	public interface IOneOf
	{
		global::System.Type Type { get; }

		object Value { get; }
	}
}
