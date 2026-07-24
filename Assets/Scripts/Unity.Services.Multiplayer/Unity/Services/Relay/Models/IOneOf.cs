namespace Unity.Services.Relay.Models
{
	public interface IOneOf
	{
		global::System.Type Type { get; }

		object Value { get; }
	}
}
