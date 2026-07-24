namespace Steamworks
{
	public class InteropHelp
	{
		public class UTF8StringHandle : global::Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid
		{
			public UTF8StringHandle(string str)
				: base(ownsHandle: true)
			{
				if (str == null)
				{
					SetHandle(global::System.IntPtr.Zero);
					return;
				}
				byte[] array = new byte[global::System.Text.Encoding.UTF8.GetByteCount(str) + 1];
				global::System.Text.Encoding.UTF8.GetBytes(str, 0, str.Length, array, 0);
				global::System.IntPtr destination = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(array.Length);
				global::System.Runtime.InteropServices.Marshal.Copy(array, 0, destination, array.Length);
				SetHandle(destination);
			}

			protected override bool ReleaseHandle()
			{
				if (!IsInvalid)
				{
					global::System.Runtime.InteropServices.Marshal.FreeHGlobal(handle);
				}
				return true;
			}
		}

		public class SteamParamStringArray
		{
			private global::System.IntPtr[] m_Strings;

			private global::System.IntPtr m_ptrStrings;

			private global::System.IntPtr m_pSteamParamStringArray;

			public SteamParamStringArray(global::System.Collections.Generic.IList<string> strings)
			{
				if (strings == null)
				{
					m_pSteamParamStringArray = global::System.IntPtr.Zero;
					return;
				}
				m_Strings = new global::System.IntPtr[strings.Count];
				for (int i = 0; i < strings.Count; i++)
				{
					byte[] array = new byte[global::System.Text.Encoding.UTF8.GetByteCount(strings[i]) + 1];
					global::System.Text.Encoding.UTF8.GetBytes(strings[i], 0, strings[i].Length, array, 0);
					m_Strings[i] = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(array.Length);
					global::System.Runtime.InteropServices.Marshal.Copy(array, 0, m_Strings[i], array.Length);
				}
				m_ptrStrings = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::System.IntPtr)) * m_Strings.Length);
				global::Steamworks.SteamParamStringArray_t structure = new global::Steamworks.SteamParamStringArray_t
				{
					m_ppStrings = m_ptrStrings,
					m_nNumStrings = m_Strings.Length
				};
				global::System.Runtime.InteropServices.Marshal.Copy(m_Strings, 0, structure.m_ppStrings, m_Strings.Length);
				m_pSteamParamStringArray = global::System.Runtime.InteropServices.Marshal.AllocHGlobal(global::System.Runtime.InteropServices.Marshal.SizeOf(typeof(global::Steamworks.SteamParamStringArray_t)));
				global::System.Runtime.InteropServices.Marshal.StructureToPtr(structure, m_pSteamParamStringArray, fDeleteOld: false);
			}

			~SteamParamStringArray()
			{
				if (m_Strings != null)
				{
					global::System.IntPtr[] strings = m_Strings;
					int i = 0;
					for (; i < strings.Length; i++)
					{
						global::System.Runtime.InteropServices.Marshal.FreeHGlobal(strings[i]);
					}
				}
				if (m_ptrStrings != global::System.IntPtr.Zero)
				{
					global::System.Runtime.InteropServices.Marshal.FreeHGlobal(m_ptrStrings);
				}
				if (m_pSteamParamStringArray != global::System.IntPtr.Zero)
				{
					global::System.Runtime.InteropServices.Marshal.FreeHGlobal(m_pSteamParamStringArray);
				}
			}

			public static implicit operator global::System.IntPtr(global::Steamworks.InteropHelp.SteamParamStringArray that)
			{
				return that.m_pSteamParamStringArray;
			}
		}

		public static void TestIfPlatformSupported()
		{
		}

		public static void TestIfAvailableClient()
		{
			TestIfPlatformSupported();
			if (global::Steamworks.CSteamAPIContext.GetSteamClient() == global::System.IntPtr.Zero && !global::Steamworks.CSteamAPIContext.Init())
			{
				throw new global::System.InvalidOperationException("Steamworks is not initialized.");
			}
		}

		public static void TestIfAvailableGameServer()
		{
			TestIfPlatformSupported();
			if (global::Steamworks.CSteamGameServerAPIContext.GetSteamClient() == global::System.IntPtr.Zero && !global::Steamworks.CSteamGameServerAPIContext.Init())
			{
				throw new global::System.InvalidOperationException("Steamworks GameServer is not initialized.");
			}
		}

		public static string PtrToStringUTF8(global::System.IntPtr nativeUtf8)
		{
			if (nativeUtf8 == global::System.IntPtr.Zero)
			{
				return null;
			}
			int i;
			for (i = 0; global::System.Runtime.InteropServices.Marshal.ReadByte(nativeUtf8, i) != 0; i++)
			{
			}
			if (i == 0)
			{
				return string.Empty;
			}
			byte[] array = new byte[i];
			global::System.Runtime.InteropServices.Marshal.Copy(nativeUtf8, array, 0, array.Length);
			return global::System.Text.Encoding.UTF8.GetString(array);
		}

		public static string ByteArrayToStringUTF8(byte[] buffer)
		{
			int i;
			for (i = 0; i < buffer.Length && buffer[i] != 0; i++)
			{
			}
			return global::System.Text.Encoding.UTF8.GetString(buffer, 0, i);
		}

		public static void StringToByteArrayUTF8(string str, byte[] outArrayBuffer, int outArrayBufferSize)
		{
			outArrayBuffer = new byte[outArrayBufferSize];
			int bytes = global::System.Text.Encoding.UTF8.GetBytes(str, 0, str.Length, outArrayBuffer, 0);
			outArrayBuffer[bytes] = 0;
		}
	}
}
