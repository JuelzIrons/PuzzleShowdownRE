namespace UnityEngine.Rendering
{
	public class DebugDisplaySettingsHDROutput
	{
		private static class Strings
		{
			public static readonly string hdrOutputAPI = "HDROutputSettings";

			public static readonly string displayName = "Display ";

			public static readonly string displayMain = " (main)";

			public static readonly string hdrActive = "HDR Output Active";

			public static readonly string hdrAvailable = "HDR Output Available";

			public static readonly string gamut = "Display Color Gamut";

			public static readonly string format = "Display Buffer Graphics Format";

			public static readonly string autoHdrTonemapping = "Automatic HDR Tonemapping";

			public static readonly string paperWhite = "Paper White Nits";

			public static readonly string minLuminance = "Min Tone Map Luminance";

			public static readonly string maxLuminance = "Max Tone Map Luminance";

			public static readonly string maxFullFrameLuminance = "Max Full Frame Tone Map Luminance";

			public static readonly string modeChangeRequested = "HDR Mode Change Requested";

			public static readonly string notAvailable = "N/A";
		}

		public static global::UnityEngine.Rendering.DebugUI.Table CreateHDROuputDisplayTable()
		{
			global::UnityEngine.Rendering.DebugUI.Table table = new global::UnityEngine.Rendering.DebugUI.Table
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.hdrOutputAPI,
				isReadOnly = true
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.hdrActive,
				opened = true
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row2 = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.hdrAvailable,
				opened = true
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row3 = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.gamut,
				opened = false
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row4 = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.format,
				opened = false
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row5 = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.autoHdrTonemapping,
				opened = false
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row6 = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.paperWhite,
				opened = false
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row7 = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.minLuminance,
				opened = false
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row8 = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.maxLuminance,
				opened = false
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row9 = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.maxFullFrameLuminance,
				opened = false
			};
			global::UnityEngine.Rendering.DebugUI.Table.Row row10 = new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.modeChangeRequested,
				opened = false
			};
			global::UnityEngine.HDROutputSettings[] displays = global::UnityEngine.HDROutputSettings.displays;
			for (int i = 0; i < displays.Length; i++)
			{
				global::UnityEngine.HDROutputSettings d = displays[i];
				string text = global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.displayName + (i + 1);
				if (global::UnityEngine.HDROutputSettings.main == d)
				{
					text += global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.displayMain;
				}
				row.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.active
				});
				row2.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.available
				});
				row3.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.available ? ((object)d.displayColorGamut) : global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.notAvailable
				});
				row4.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.available ? ((object)d.graphicsFormat) : global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.notAvailable
				});
				row5.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.available ? ((object)d.automaticHDRTonemapping) : global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.notAvailable
				});
				row6.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.available ? ((object)d.paperWhiteNits) : global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.notAvailable
				});
				row7.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.available ? ((object)d.minToneMapLuminance) : global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.notAvailable
				});
				row8.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.available ? ((object)d.maxToneMapLuminance) : global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.notAvailable
				});
				row9.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.available ? ((object)d.maxFullFrameToneMapLuminance) : global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.notAvailable
				});
				row10.children.Add(new global::UnityEngine.Rendering.DebugUI.Value
				{
					displayName = text,
					getter = () => d.available ? ((object)d.HDRModeChangeRequested) : global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.Strings.notAvailable
				});
			}
			table.children.Add(row);
			table.children.Add(row2);
			table.children.Add(row3);
			table.children.Add(row4);
			table.children.Add(row5);
			table.children.Add(row6);
			table.children.Add(row7);
			table.children.Add(row8);
			table.children.Add(row9);
			table.children.Add(row10);
			return table;
		}
	}
}
