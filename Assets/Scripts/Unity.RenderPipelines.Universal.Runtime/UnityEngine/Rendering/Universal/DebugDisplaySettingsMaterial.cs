namespace UnityEngine.Rendering.Universal
{
	public class DebugDisplaySettingsMaterial : global::UnityEngine.Rendering.IDebugDisplaySettingsData, global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		public enum AlbedoDebugValidationPreset
		{
			DefaultLuminance = 0,
			BlackAcrylicPaint = 1,
			DarkSoil = 2,
			WornAsphalt = 3,
			DryClaySoil = 4,
			GreenGrass = 5,
			OldConcrete = 6,
			RedClayTile = 7,
			DrySand = 8,
			NewConcrete = 9,
			WhiteAcrylicPaint = 10,
			FreshSnow = 11,
			BlueSky = 12,
			Foliage = 13,
			Custom = 14
		}

		private struct AlbedoDebugValidationPresetData
		{
			public string name;

			public global::UnityEngine.Color color;

			public float minLuminance;

			public float maxLuminance;
		}

		private static class Strings
		{
			public const string AlbedoSettingsContainerName = "Albedo Settings";

			public const string MetallicSettingsContainerName = "Metallic Settings";

			public const string RenderingLayerMasksSettingsContainerName = "Rendering Layer Masks Settings";

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MaterialOverride = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Material Override",
				tooltip = "Use the drop-down to select a Material property to visualize on every GameObject on screen."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip VertexAttribute = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Vertex Attribute",
				tooltip = "Use the drop-down to select a 3D GameObject attribute, like Texture Coordinates or Vertex Color, to visualize on screen."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MaterialValidationMode = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Material Validation Mode",
				tooltip = "Debug and validate material properties."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip RenderingLayersSelectedLight = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Filter Rendering Layers by Light",
				tooltip = "Highlight Renderers affected by Selected Light"
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip SelectedLightShadowLayerMask = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Use Light's Shadow Layer Mask",
				tooltip = "Highlight Renderers that cast shadows for the Selected Light"
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip FilterRenderingLayerMask = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Filter Layers",
				tooltip = "Use the dropdown to filter Rendering Layers that you want to visualize"
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip ValidationPreset = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Validation Preset",
				tooltip = "Validate using a list of preset surfaces and inputs based on real-world surfaces."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip AlbedoCustomColor = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Target Color",
				tooltip = "Custom target color for albedo validation."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip AlbedoMinLuminance = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Min Luminance",
				tooltip = "Any values set below this field are invalid and appear red on screen."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip AlbedoMaxLuminance = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Max Luminance",
				tooltip = "Any values set above this field are invalid and appear blue on screen."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip AlbedoHueTolerance = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Hue Tolerance",
				tooltip = "Validate a material based on a specific hue."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip AlbedoSaturationTolerance = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Saturation Tolerance",
				tooltip = "Validate a material based on a specific Saturation."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MetallicMinValue = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Min Value",
				tooltip = "Any values set below this field are invalid and appear red on screen."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MetallicMaxValue = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Max Value",
				tooltip = "Any values set above this field are invalid and appear blue on screen."
			};
		}

		internal static class WidgetFactory
		{
			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMaterialOverride(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.MaterialOverride,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugMaterialMode),
					getter = () => (int)panel.data.materialDebugMode,
					setter = delegate(int value)
					{
						panel.data.materialDebugMode = (global::UnityEngine.Rendering.Universal.DebugMaterialMode)value;
					},
					getIndex = () => (int)panel.data.materialDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.materialDebugMode = (global::UnityEngine.Rendering.Universal.DebugMaterialMode)value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateVertexAttribute(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.VertexAttribute,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugVertexAttributeMode),
					getter = () => (int)panel.data.vertexAttributeDebugMode,
					setter = delegate(int value)
					{
						panel.data.vertexAttributeDebugMode = (global::UnityEngine.Rendering.Universal.DebugVertexAttributeMode)value;
					},
					getIndex = () => (int)panel.data.vertexAttributeDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.vertexAttributeDebugMode = (global::UnityEngine.Rendering.Universal.DebugVertexAttributeMode)value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMaterialValidationMode(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.MaterialValidationMode,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode),
					getter = () => (int)panel.data.materialValidationMode,
					setter = delegate(int value)
					{
						panel.data.materialValidationMode = (global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode)value;
					},
					getIndex = () => (int)panel.data.materialValidationMode,
					setIndex = delegate(int value)
					{
						panel.data.materialValidationMode = (global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode)value;
					},
					onValueChanged = delegate
					{
						global::UnityEngine.Rendering.DebugManager.instance.ReDrawOnScreenDebug();
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateRenderingLayersSelectedLight(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.BoolField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.RenderingLayersSelectedLight,
					getter = () => panel.data.renderingLayersSelectedLight,
					setter = delegate(bool value)
					{
						panel.data.renderingLayersSelectedLight = value;
					},
					flags = global::UnityEngine.Rendering.DebugUI.Flags.EditorOnly
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateSelectedLightShadowLayerMask(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.BoolField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.SelectedLightShadowLayerMask,
					getter = () => panel.data.selectedLightShadowLayerMask,
					setter = delegate(bool value)
					{
						panel.data.selectedLightShadowLayerMask = value;
					},
					flags = global::UnityEngine.Rendering.DebugUI.Flags.EditorOnly,
					isHiddenCallback = () => !panel.data.renderingLayersSelectedLight
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.RenderingLayerField CreateFilterRenderingLayerMasks(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.RenderingLayerField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.FilterRenderingLayerMask,
					getter = () => panel.data.renderingLayerMask,
					setter = delegate(global::UnityEngine.RenderingLayerMask value)
					{
						panel.data.renderingLayerMask = value;
					},
					getRenderingLayerColor = (int index) => panel.data.debugRenderingLayersColors[index],
					setRenderingLayerColor = delegate(global::UnityEngine.Vector4 value, int index)
					{
						panel.data.debugRenderingLayersColors[index] = value;
					},
					isHiddenCallback = () => panel.data.renderingLayersSelectedLight
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateAlbedoPreset(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.ValidationPreset,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset),
					getter = () => (int)panel.data.albedoValidationPreset,
					setter = delegate(int value)
					{
						panel.data.albedoValidationPreset = (global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset)value;
					},
					getIndex = () => (int)panel.data.albedoValidationPreset,
					setIndex = delegate(int value)
					{
						panel.data.albedoValidationPreset = (global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset)value;
					},
					onValueChanged = delegate
					{
						global::UnityEngine.Rendering.DebugManager.instance.ReDrawOnScreenDebug();
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateAlbedoCustomColor(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.ColorField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.AlbedoCustomColor,
					getter = () => panel.data.albedoCompareColor,
					setter = delegate(global::UnityEngine.Color value)
					{
						panel.data.albedoCompareColor = value;
					},
					isHiddenCallback = () => panel.data.albedoValidationPreset != global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.Custom
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateAlbedoMinLuminance(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.AlbedoMinLuminance,
					getter = () => panel.data.albedoMinLuminance,
					setter = delegate(float value)
					{
						panel.data.albedoMinLuminance = value;
					},
					incStep = 0.01f
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateAlbedoMaxLuminance(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.AlbedoMaxLuminance,
					getter = () => panel.data.albedoMaxLuminance,
					setter = delegate(float value)
					{
						panel.data.albedoMaxLuminance = value;
					},
					incStep = 0.01f
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateAlbedoHueTolerance(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.AlbedoHueTolerance,
					getter = () => panel.data.albedoHueTolerance,
					setter = delegate(float value)
					{
						panel.data.albedoHueTolerance = value;
					},
					incStep = 0.01f,
					isHiddenCallback = () => panel.data.albedoValidationPreset == global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateAlbedoSaturationTolerance(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.AlbedoSaturationTolerance,
					getter = () => panel.data.albedoSaturationTolerance,
					setter = delegate(float value)
					{
						panel.data.albedoSaturationTolerance = value;
					},
					incStep = 0.01f,
					isHiddenCallback = () => panel.data.albedoValidationPreset == global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMetallicMinValue(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.MetallicMinValue,
					getter = () => panel.data.metallicMinValue,
					setter = delegate(float value)
					{
						panel.data.metallicMinValue = value;
					},
					incStep = 0.01f
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMetallicMaxValue(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.Strings.MetallicMaxValue,
					getter = () => panel.data.metallicMaxValue,
					setter = delegate(float value)
					{
						panel.data.metallicMaxValue = value;
					},
					incStep = 0.01f
				};
			}
		}

		[global::UnityEngine.Rendering.DisplayInfo(name = "Material", order = 2)]
		internal class SettingsPanel : global::UnityEngine.Rendering.DebugDisplaySettingsPanel<global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial>
		{
			public SettingsPanel(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial data)
				: base(data)
			{
				AddWidget(new global::UnityEngine.Rendering.DebugUI.RuntimeDebugShadersMessageBox());
				AddWidget(new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Material Filters",
					flags = global::UnityEngine.Rendering.DebugUI.Flags.FrequentlyUsed,
					opened = true,
					children = 
					{
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateMaterialOverride(this),
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Container
						{
							displayName = "Rendering Layer Masks Settings",
							isHiddenCallback = () => data.materialDebugMode != global::UnityEngine.Rendering.Universal.DebugMaterialMode.RenderingLayerMasks,
							children = 
							{
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateRenderingLayersSelectedLight(this),
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateSelectedLightShadowLayerMask(this),
								(global::UnityEngine.Rendering.DebugUI.Widget)global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateFilterRenderingLayerMasks(this)
							}
						},
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateVertexAttribute(this)
					}
				});
				AddWidget(new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Material Validation",
					opened = true,
					children = 
					{
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateMaterialValidationMode(this),
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Container
						{
							displayName = "Albedo Settings",
							isHiddenCallback = () => data.materialValidationMode != global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode.Albedo,
							children = 
							{
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoPreset(this),
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoCustomColor(this),
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoMinLuminance(this),
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoMaxLuminance(this),
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoHueTolerance(this),
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoSaturationTolerance(this)
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Container
						{
							displayName = "Metallic Settings",
							isHiddenCallback = () => data.materialValidationMode != global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode.Metallic,
							children = 
							{
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateMetallicMinValue(this),
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.WidgetFactory.CreateMetallicMaxValue(this)
							}
						}
					}
				});
			}
		}

		private global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData[] m_AlbedoDebugValidationPresetData = new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData[15]
		{
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Default Luminance",
				color = new global::UnityEngine.Color(0.49803922f, 0.49803922f, 0.49803922f),
				minLuminance = 0.01f,
				maxLuminance = 0.9f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Black Acrylic Paint",
				color = new global::UnityEngine.Color(0.21960784f, 0.21960784f, 0.21960784f),
				minLuminance = 0.03f,
				maxLuminance = 0.07f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Dark Soil",
				color = new global::UnityEngine.Color(1f / 3f, 0.23921569f, 0.19215687f),
				minLuminance = 0.05f,
				maxLuminance = 0.14f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Worn Asphalt",
				color = new global::UnityEngine.Color(0.35686275f, 0.35686275f, 0.35686275f),
				minLuminance = 0.1f,
				maxLuminance = 0.15f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Dry Clay Soil",
				color = new global::UnityEngine.Color(0.5372549f, 0.47058824f, 0.4f),
				minLuminance = 0.15f,
				maxLuminance = 0.35f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Green Grass",
				color = new global::UnityEngine.Color(41f / 85f, 0.5137255f, 0.2901961f),
				minLuminance = 0.16f,
				maxLuminance = 0.26f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Old Concrete",
				color = new global::UnityEngine.Color(0.5294118f, 8f / 15f, 0.5137255f),
				minLuminance = 0.17f,
				maxLuminance = 0.3f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Red Clay Tile",
				color = new global::UnityEngine.Color(0.77254903f, 25f / 51f, 20f / 51f),
				minLuminance = 0.23f,
				maxLuminance = 0.33f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Dry Sand",
				color = new global::UnityEngine.Color(59f / 85f, 0.654902f, 44f / 85f),
				minLuminance = 0.2f,
				maxLuminance = 0.45f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "New Concrete",
				color = new global::UnityEngine.Color(37f / 51f, 0.7137255f, 35f / 51f),
				minLuminance = 0.32f,
				maxLuminance = 0.55f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "White Acrylic Paint",
				color = new global::UnityEngine.Color(0.8901961f, 0.8901961f, 0.8901961f),
				minLuminance = 0.75f,
				maxLuminance = 0.85f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Fresh Snow",
				color = new global::UnityEngine.Color(81f / 85f, 81f / 85f, 81f / 85f),
				minLuminance = 0.85f,
				maxLuminance = 0.95f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Blue Sky",
				color = new global::UnityEngine.Color(31f / 85f, 41f / 85f, 0.6156863f),
				minLuminance = new global::UnityEngine.Color(31f / 85f, 41f / 85f, 0.6156863f).linear.maxColorComponent - 0.05f,
				maxLuminance = new global::UnityEngine.Color(31f / 85f, 41f / 85f, 0.6156863f).linear.maxColorComponent + 0.05f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Foliage",
				color = new global::UnityEngine.Color(0.35686275f, 36f / 85f, 13f / 51f),
				minLuminance = new global::UnityEngine.Color(0.35686275f, 36f / 85f, 13f / 51f).linear.maxColorComponent - 0.05f,
				maxLuminance = new global::UnityEngine.Color(0.35686275f, 36f / 85f, 13f / 51f).linear.maxColorComponent + 0.05f
			},
			new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Custom",
				color = new global::UnityEngine.Color(0.49803922f, 0.49803922f, 0.49803922f),
				minLuminance = 0.01f,
				maxLuminance = 0.9f
			}
		};

		private global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset m_AlbedoValidationPreset;

		private float m_AlbedoHueTolerance = 0.104f;

		private float m_AlbedoSaturationTolerance = 0.214f;

		public global::UnityEngine.Vector4[] debugRenderingLayersColors = new global::UnityEngine.Vector4[32]
		{
			new global::UnityEngine.Vector4(230f, 159f, 0f) / 255f,
			new global::UnityEngine.Vector4(86f, 180f, 233f) / 255f,
			new global::UnityEngine.Vector4(255f, 182f, 291f) / 255f,
			new global::UnityEngine.Vector4(0f, 158f, 115f) / 255f,
			new global::UnityEngine.Vector4(240f, 228f, 66f) / 255f,
			new global::UnityEngine.Vector4(0f, 114f, 178f) / 255f,
			new global::UnityEngine.Vector4(213f, 94f, 0f) / 255f,
			new global::UnityEngine.Vector4(170f, 68f, 170f) / 255f,
			new global::UnityEngine.Vector4(1f, 0.5f, 0.5f),
			new global::UnityEngine.Vector4(0.5f, 1f, 0.5f),
			new global::UnityEngine.Vector4(0.5f, 0.5f, 1f),
			new global::UnityEngine.Vector4(0.5f, 1f, 1f),
			new global::UnityEngine.Vector4(0.75f, 0.25f, 1f),
			new global::UnityEngine.Vector4(0.25f, 1f, 0.75f),
			new global::UnityEngine.Vector4(0.25f, 0.25f, 0.75f),
			new global::UnityEngine.Vector4(0.75f, 0.25f, 0.25f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f),
			new global::UnityEngine.Vector4(0f, 0f, 0f)
		};

		public global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset albedoValidationPreset
		{
			get
			{
				return m_AlbedoValidationPreset;
			}
			set
			{
				m_AlbedoValidationPreset = value;
				global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData albedoDebugValidationPresetData = m_AlbedoDebugValidationPresetData[(int)value];
				albedoMinLuminance = albedoDebugValidationPresetData.minLuminance;
				albedoMaxLuminance = albedoDebugValidationPresetData.maxLuminance;
				albedoCompareColor = albedoDebugValidationPresetData.color;
			}
		}

		public float albedoMinLuminance { get; set; } = 0.01f;

		public float albedoMaxLuminance { get; set; } = 0.9f;

		public float albedoHueTolerance
		{
			get
			{
				if (m_AlbedoValidationPreset != global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance)
				{
					return m_AlbedoHueTolerance;
				}
				return 1f;
			}
			set
			{
				m_AlbedoHueTolerance = value;
			}
		}

		public float albedoSaturationTolerance
		{
			get
			{
				if (m_AlbedoValidationPreset != global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance)
				{
					return m_AlbedoSaturationTolerance;
				}
				return 1f;
			}
			set
			{
				m_AlbedoSaturationTolerance = value;
			}
		}

		public global::UnityEngine.Color albedoCompareColor { get; set; } = new global::UnityEngine.Color(0.49803922f, 0.49803922f, 0.49803922f, 1f);

		public float metallicMinValue { get; set; }

		public float metallicMaxValue { get; set; } = 0.9f;

		public bool renderingLayersSelectedLight { get; set; }

		public bool selectedLightShadowLayerMask { get; set; }

		public uint renderingLayerMask { get; set; }

		public global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode materialValidationMode { get; set; }

		public global::UnityEngine.Rendering.Universal.DebugMaterialMode materialDebugMode { get; set; }

		public global::UnityEngine.Rendering.Universal.DebugVertexAttributeMode vertexAttributeDebugMode { get; set; }

		public bool AreAnySettingsActive
		{
			get
			{
				if (materialDebugMode == global::UnityEngine.Rendering.Universal.DebugMaterialMode.None && vertexAttributeDebugMode == global::UnityEngine.Rendering.Universal.DebugVertexAttributeMode.None)
				{
					return materialValidationMode != global::UnityEngine.Rendering.Universal.DebugMaterialValidationMode.None;
				}
				return true;
			}
		}

		public bool IsPostProcessingAllowed => !AreAnySettingsActive;

		public bool IsLightingActive => !AreAnySettingsActive;

		public uint GetDebugLightLayersMask()
		{
			return 65535u;
		}

		global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable global::UnityEngine.Rendering.IDebugDisplaySettingsData.CreatePanel()
		{
			return new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsMaterial.SettingsPanel(this);
		}
	}
}
