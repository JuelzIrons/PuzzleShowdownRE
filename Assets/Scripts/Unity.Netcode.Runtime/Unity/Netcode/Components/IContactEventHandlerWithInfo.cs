namespace Unity.Netcode.Components
{
	public interface IContactEventHandlerWithInfo : global::Unity.Netcode.Components.IContactEventHandler
	{
		global::Unity.Netcode.Components.ContactEventHandlerInfo GetContactEventHandlerInfo();
	}
}
