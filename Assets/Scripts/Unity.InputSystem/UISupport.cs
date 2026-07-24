internal static class UISupport
{
	public static void Initialize()
	{
		global::UnityEngine.InputSystem.InputSystem.RegisterLayout("\n            {\n                \"name\" : \"VirtualMouse\",\n                \"extend\" : \"Mouse\"\n            }\n        ");
	}
}
