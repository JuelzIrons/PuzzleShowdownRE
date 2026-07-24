namespace UnityEngine.Rendering.Universal
{
	public class DebugDisplaySettingsLighting : global::UnityEngine.Rendering.IDebugDisplaySettingsData, global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		internal static class Strings
		{
			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip LightingDebugMode = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Lighting Debug Mode",
				tooltip = "Use the drop-down to select which lighting and shadow debug information to overlay on the screen."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip LightingFeatures = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Lighting Features",
				tooltip = "Filter and debug selected lighting features in the system."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip HDRDebugMode = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "HDR Debug Mode",
				tooltip = "Select which HDR brightness debug information to overlay on the screen."
			};
		}

		internal static class WidgetFactory
		{
			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateLightingDebugMode(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.Strings.LightingDebugMode,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugLightingMode),
					getter = () => (int)panel.data.lightingDebugMode,
					setter = delegate(int value)
					{
						panel.data.lightingDebugMode = (global::UnityEngine.Rendering.Universal.DebugLightingMode)value;
					},
					getIndex = () => (int)panel.data.lightingDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.lightingDebugMode = (global::UnityEngine.Rendering.Universal.DebugLightingMode)value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateLightingFeatures(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.BitField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.Strings.LightingFeatures,
					getter = () => panel.data.lightingFeatureFlags,
					setter = delegate(global::System.Enum value)
					{
						panel.data.lightingFeatureFlags = (global::UnityEngine.Rendering.Universal.DebugLightingFeatureFlags)(object)value;
					},
					enumType = typeof(global::UnityEngine.Rendering.Universal.DebugLightingFeatureFlags)
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateHDRDebugMode(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.Strings.HDRDebugMode,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.HDRDebugMode),
					getter = () => (int)panel.data.hdrDebugMode,
					setter = delegate(int value)
					{
						panel.data.hdrDebugMode = (global::UnityEngine.Rendering.Universal.HDRDebugMode)value;
					},
					getIndex = () => (int)panel.data.hdrDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.hdrDebugMode = (global::UnityEngine.Rendering.Universal.HDRDebugMode)value;
					}
				};
			}
		}

		[global::UnityEngine.Rendering.DisplayInfo(name = "Lighting", order = 3)]
		internal class SettingsPanel : global::UnityEngine.Rendering.DebugDisplaySettingsPanel<global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting>
		{
			public SettingsPanel(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting data)
				: base(data)
			{
				AddWidget(new global::UnityEngine.Rendering.DebugUI.RuntimeDebugShadersMessageBox());
				AddWidget(new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Lighting Debug Modes",
					flags = global::UnityEngine.Rendering.DebugUI.Flags.FrequentlyUsed,
					opened = true,
					children = 
					{
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.WidgetFactory.CreateLightingDebugMode(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.WidgetFactory.CreateHDRDebugMode(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.WidgetFactory.CreateLightingFeatures(this)
					},
					documentationUrl = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.HelpURLAttribute>(typeof(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting))?.URL
				});
			}
		}

		public global::UnityEngine.Rendering.Universal.DebugLightingMode lightingDebugMode { get; set; }

		public global::UnityEngine.Rendering.Universal.DebugLightingFeatureFlags lightingFeatureFlags { get; set; }

		public global::UnityEngine.Rendering.Universal.HDRDebugMode hdrDebugMode { get; set; }

		public bool AreAnySettingsActive
		{
			get
			{
				if (lightingDebugMode == global::UnityEngine.Rendering.Universal.DebugLightingMode.None && lightingFeatureFlags == global::UnityEngine.Rendering.Universal.DebugLightingFeatureFlags.None)
				{
					return hdrDebugMode != global::UnityEngine.Rendering.Universal.HDRDebugMode.None;
				}
				return true;
			}
		}

		public bool IsPostProcessingAllowed
		{
			get
			{
				if (lightingDebugMode != global::UnityEngine.Rendering.Universal.DebugLightingMode.Reflections)
				{
					return lightingDebugMode != global::UnityEngine.Rendering.Universal.DebugLightingMode.ReflectionsWithSmoothness;
				}
				return false;
			}
		}

		public bool IsLightingActive => true;

		global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable global::UnityEngine.Rendering.IDebugDisplaySettingsData.CreatePanel()
		{
			return new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsLighting.SettingsPanel(this);
		}
	}
}
