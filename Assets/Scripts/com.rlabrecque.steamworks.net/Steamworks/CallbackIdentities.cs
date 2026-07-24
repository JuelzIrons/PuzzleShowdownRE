namespace Steamworks
{
	internal class CallbackIdentities
	{
		public static int GetCallbackIdentity(global::System.Type callbackStruct)
		{
			object[] customAttributes = callbackStruct.GetCustomAttributes(typeof(global::Steamworks.CallbackIdentityAttribute), inherit: false);
			int num = 0;
			if (num < customAttributes.Length)
			{
				return ((global::Steamworks.CallbackIdentityAttribute)customAttributes[num]).Identity;
			}
			throw new global::System.Exception("Callback number not found for struct " + callbackStruct);
		}
	}
}
