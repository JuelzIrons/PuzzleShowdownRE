namespace Steamworks
{
	[global::System.Runtime.InteropServices.StructLayout(global::System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
	public struct InputAnalogActionData_t
	{
		public global::Steamworks.EInputSourceMode eMode;

		public float x;

		public float y;

		public byte bActive;
	}
}
