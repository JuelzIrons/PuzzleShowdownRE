namespace UnityEngine.Rendering.Universal
{
	public class DebugDisplaySettingsRendering : global::UnityEngine.Rendering.IDebugDisplaySettingsData, global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		public enum TaaDebugMode
		{
			None = 0,
			ShowRawFrame = 1,
			ShowRawFrameNoJitter = 2,
			ShowClampedHistory = 3
		}

		private static class Strings
		{
			public const string RangeValidationSettingsContainerName = "Pixel Range Settings";

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MapOverlays = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Map Overlays",
				tooltip = "Overlays render pipeline textures to validate the scene."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip StpDebugViews = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "STP Debug Views",
				tooltip = "Debug visualizations provided by STP."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MapSize = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Map Size",
				tooltip = "Set the size of the render pipeline texture in the scene."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip AdditionalWireframeModes = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Additional Wireframe Modes",
				tooltip = "Debug the scene with additional wireframe shader views that are different from those in the scene view."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip WireframeNotSupportedWarning = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Warning: This platform might not support wireframe rendering.",
				tooltip = "Some platforms, for example, mobile platforms using OpenGL ES and Vulkan, might not support wireframe rendering."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip OverdrawMode = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Overdraw Mode",
				tooltip = "Debug anywhere materials that overdrawn pixels top of each other."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MaxOverdrawCount = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Max Overdraw Count",
				tooltip = "Maximum overdraw count allowed for a single pixel."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MipMapDisableMipCaching = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Disable Mip Caching",
				tooltip = "By disabling mip caching, the data on GPU accurately reflects what the TextureStreamer calculates. While this can significantly increase CPU-to-GPU traffic, it can be an invaluable tool to validate that the Streamer behaves as expected."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MipMapDebugView = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Debug View",
				tooltip = "Use the drop-down to select a mipmap property to debug."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MipMapDebugOpacity = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Debug Opacity",
				tooltip = "Opacity of texture mipmap streaming debug colors."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MipMapMaterialTextureSlot = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Material Texture Slot",
				tooltip = "Use the drop-down to select the material texture slot to debug (does not affect terrain).\n\nThe slot indices follow the default order by which texture properties appear in the Material Inspector.\nThe default order is itself defined by the order in which (non-hidden) texture properties appear in the shader's \"Properties\" block."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MipMapTerrainTexture = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Terrain Texture",
				tooltip = "Use the drop-down to select the terrain Texture to debug the mipmap for."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MipMapDisplayStatusCodes = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Display Status Codes",
				tooltip = "Show detailed status codes indicating why textures are not streaming or highlighting points of attention."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MipMapActivityTimespan = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Activity Timespan",
				tooltip = "How long a texture should be shown as \"recently updated\"."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MipMapCombinePerMaterial = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Combined per Material",
				tooltip = "Combine the information over all slots per material."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip PostProcessing = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Post-processing",
				tooltip = "Override the controls for Post Processing in the scene."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip MSAA = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "MSAA",
				tooltip = "Use the checkbox to disable MSAA in the scene."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip HDR = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "HDR",
				tooltip = "Use the checkbox to disable High Dynamic Range in the scene."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip TaaDebugMode = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "TAA Debug Mode",
				tooltip = "Choose whether to force TAA to output the raw jittered frame or clamped reprojected history."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip PixelValidationMode = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Pixel Validation Mode",
				tooltip = "Choose between modes that validate pixel on screen."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip Channels = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Channels",
				tooltip = "Choose the texture channel used to validate the scene."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip ValueRangeMin = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Value Range Min",
				tooltip = "Any values set below this field will be considered invalid and will appear red on screen."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip ValueRangeMax = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Value Range Max",
				tooltip = "Any values set above this field will be considered invalid and will appear blue on screen."
			};
		}

		internal static class WidgetFactory
		{
			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMapOverlays(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MapOverlays,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugFullScreenMode),
					getter = () => (int)panel.data.fullScreenDebugMode,
					setter = delegate(int value)
					{
						panel.data.fullScreenDebugMode = (global::UnityEngine.Rendering.Universal.DebugFullScreenMode)value;
					},
					getIndex = () => (int)panel.data.fullScreenDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.fullScreenDebugMode = (global::UnityEngine.Rendering.Universal.DebugFullScreenMode)value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateStpDebugViews(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.StpDebugViews,
					isHiddenCallback = () => panel.data.fullScreenDebugMode != global::UnityEngine.Rendering.Universal.DebugFullScreenMode.STP,
					enumNames = global::UnityEngine.Rendering.STP.debugViewDescriptions,
					enumValues = global::UnityEngine.Rendering.STP.debugViewIndices,
					getter = () => panel.data.stpDebugViewIndex,
					setter = delegate(int value)
					{
						panel.data.stpDebugViewIndex = value;
					},
					getIndex = () => panel.data.stpDebugViewIndex,
					setIndex = delegate(int value)
					{
						panel.data.stpDebugViewIndex = value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMapOverlaySize(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.Container
				{
					children = { (global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.IntField
					{
						nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MapSize,
						getter = () => panel.data.fullScreenDebugModeOutputSizeScreenPercent,
						setter = delegate(int value)
						{
							panel.data.fullScreenDebugModeOutputSizeScreenPercent = value;
						},
						incStep = 10,
						min = () => 0,
						max = () => 100
					} }
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateAdditionalWireframeShaderViews(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.AdditionalWireframeModes,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugWireframeMode),
					getter = () => (int)panel.data.wireframeMode,
					setter = delegate(int value)
					{
						panel.data.wireframeMode = (global::UnityEngine.Rendering.Universal.DebugWireframeMode)value;
					},
					getIndex = () => (int)panel.data.wireframeMode,
					setIndex = delegate(int value)
					{
						panel.data.wireframeMode = (global::UnityEngine.Rendering.Universal.DebugWireframeMode)value;
					},
					onValueChanged = delegate
					{
						global::UnityEngine.Rendering.DebugManager.instance.ReDrawOnScreenDebug();
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateWireframeNotSupportedWarning(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.MessageBox
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.WireframeNotSupportedWarning,
					style = global::UnityEngine.Rendering.DebugUI.MessageBox.Style.Warning,
					isHiddenCallback = delegate
					{
						global::UnityEngine.Rendering.GraphicsDeviceType graphicsDeviceType = global::UnityEngine.SystemInfo.graphicsDeviceType;
						return (graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 && graphicsDeviceType != global::UnityEngine.Rendering.GraphicsDeviceType.Vulkan) || panel.data.wireframeMode == global::UnityEngine.Rendering.Universal.DebugWireframeMode.None;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateOverdrawMode(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.OverdrawMode,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugOverdrawMode),
					getter = () => (int)panel.data.overdrawMode,
					setter = delegate(int value)
					{
						panel.data.overdrawMode = (global::UnityEngine.Rendering.Universal.DebugOverdrawMode)value;
					},
					getIndex = () => (int)panel.data.overdrawMode,
					setIndex = delegate(int value)
					{
						panel.data.overdrawMode = (global::UnityEngine.Rendering.Universal.DebugOverdrawMode)value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMaxOverdrawCount(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.Container
				{
					isHiddenCallback = () => panel.data.overdrawMode == global::UnityEngine.Rendering.Universal.DebugOverdrawMode.None,
					children = { (global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.IntField
					{
						nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MaxOverdrawCount,
						getter = () => panel.data.maxOverdrawCount,
						setter = delegate(int value)
						{
							panel.data.maxOverdrawCount = value;
						},
						incStep = 10,
						min = () => 1,
						max = () => 500
					} }
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMipMapDebugWidget(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.Container
				{
					displayName = "Mipmap Streaming",
					children = 
					{
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MipMapDisableMipCaching,
							getter = () => global::UnityEngine.Texture.streamingTextureDiscardUnusedMips,
							setter = delegate(bool value)
							{
								global::UnityEngine.Texture.streamingTextureDiscardUnusedMips = value;
							}
						},
						CreateMipMapMode(panel),
						CreateMipMapDebugSettings(panel)
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMipMapMode(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MipMapDebugView,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugMipInfoMode),
					getter = () => (int)panel.data.mipInfoMode,
					setter = delegate(int value)
					{
						panel.data.mipInfoMode = (global::UnityEngine.Rendering.Universal.DebugMipInfoMode)value;
					},
					getIndex = () => (int)panel.data.mipInfoMode,
					setIndex = delegate(int value)
					{
						panel.data.mipInfoMode = (global::UnityEngine.Rendering.Universal.DebugMipInfoMode)value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMipMapDebugSettings(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				global::UnityEngine.GUIContent[] array = new global::UnityEngine.GUIContent[64];
				int[] array2 = new int[64];
				for (int i = 0; i < 64; i++)
				{
					array[i] = new global::UnityEngine.GUIContent($"Slot {i}");
					array2[i] = i;
				}
				return new global::UnityEngine.Rendering.DebugUI.Container
				{
					isHiddenCallback = () => panel.data.mipInfoMode == global::UnityEngine.Rendering.Universal.DebugMipInfoMode.None,
					children = 
					{
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.FloatField
						{
							nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MipMapDebugOpacity,
							getter = () => panel.data.mipDebugOpacity,
							setter = delegate(float value)
							{
								panel.data.mipDebugOpacity = value;
							},
							min = () => 0f,
							max = () => 1f
						},
						CreateMipMapDebugSlotSelector(panel, () => panel.data.canAggregateData, array, array2),
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							isHiddenCallback = () => !panel.data.canAggregateData,
							nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MipMapCombinePerMaterial,
							getter = () => panel.data.showInfoForAllSlots,
							setter = delegate(bool value)
							{
								panel.data.showInfoForAllSlots = value;
								panel.data.mipDebugStatusMode = ((!value) ? global::UnityEngine.Rendering.Universal.DebugMipMapStatusMode.Texture : global::UnityEngine.Rendering.Universal.DebugMipMapStatusMode.Material);
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Container
						{
							isHiddenCallback = () => !panel.data.canAggregateData || panel.data.showInfoForAllSlots,
							children = 
							{
								CreateMipMapDebugSlotSelector(panel, () => false, array, array2),
								CreateMipMapShowStatusCodeToggle(panel)
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.EnumField
						{
							nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MipMapTerrainTexture,
							getter = () => (int)panel.data.mipDebugTerrainTexture,
							setter = delegate(int value)
							{
								panel.data.mipDebugTerrainTexture = (global::UnityEngine.Rendering.Universal.DebugMipMapModeTerrainTexture)value;
							},
							autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugMipMapModeTerrainTexture),
							getIndex = () => (int)panel.data.mipDebugTerrainTexture,
							setIndex = delegate(int value)
							{
								panel.data.mipDebugTerrainTexture = (global::UnityEngine.Rendering.Universal.DebugMipMapModeTerrainTexture)value;
							}
						},
						CreateMipMapDebugCooldownSlider(panel)
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMipMapDebugSlotSelector(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel, global::System.Func<bool> hiddenCB, global::UnityEngine.GUIContent[] texSlotStrings, int[] texSlotValues)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					isHiddenCallback = hiddenCB,
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MipMapMaterialTextureSlot,
					getter = () => panel.data.mipDebugMaterialTextureSlot,
					setter = delegate(int value)
					{
						panel.data.mipDebugMaterialTextureSlot = value;
					},
					getIndex = () => panel.data.mipDebugMaterialTextureSlot,
					setIndex = delegate(int value)
					{
						panel.data.mipDebugMaterialTextureSlot = value;
					},
					enumNames = texSlotStrings,
					enumValues = texSlotValues
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMipMapDebugCooldownSlider(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					isHiddenCallback = () => panel.data.mipInfoMode != global::UnityEngine.Rendering.Universal.DebugMipInfoMode.MipStreamingActivity,
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MipMapActivityTimespan,
					getter = () => panel.data.mipDebugRecentUpdateCooldown,
					setter = delegate(float value)
					{
						panel.data.mipDebugRecentUpdateCooldown = value;
					},
					min = () => 0f,
					max = () => 60f
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMipMapShowStatusCodeToggle(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.BoolField
				{
					isHiddenCallback = () => panel.data.mipInfoMode != global::UnityEngine.Rendering.Universal.DebugMipInfoMode.MipStreamingStatus,
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MipMapDisplayStatusCodes,
					getter = () => panel.data.mipDebugStatusShowCode,
					setter = delegate(bool value)
					{
						panel.data.mipDebugStatusShowCode = value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreatePostProcessing(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.PostProcessing,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugPostProcessingMode),
					getter = () => (int)panel.data.postProcessingDebugMode,
					setter = delegate(int value)
					{
						panel.data.postProcessingDebugMode = (global::UnityEngine.Rendering.Universal.DebugPostProcessingMode)value;
					},
					getIndex = () => (int)panel.data.postProcessingDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.postProcessingDebugMode = (global::UnityEngine.Rendering.Universal.DebugPostProcessingMode)value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateMSAA(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.BoolField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.MSAA,
					getter = () => panel.data.enableMsaa,
					setter = delegate(bool value)
					{
						panel.data.enableMsaa = value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateHDR(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.BoolField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.HDR,
					getter = () => panel.data.enableHDR,
					setter = delegate(bool value)
					{
						panel.data.enableHDR = value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreateTaaDebugMode(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.TaaDebugMode,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.TaaDebugMode),
					getter = () => (int)panel.data.taaDebugMode,
					setter = delegate(int value)
					{
						panel.data.taaDebugMode = (global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.TaaDebugMode)value;
					},
					getIndex = () => (int)panel.data.taaDebugMode,
					setIndex = delegate(int value)
					{
						panel.data.taaDebugMode = (global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.TaaDebugMode)value;
					},
					onValueChanged = delegate
					{
						global::UnityEngine.Rendering.DebugManager.instance.ReDrawOnScreenDebug();
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreatePixelValidationMode(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.PixelValidationMode,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.DebugValidationMode),
					getter = () => (int)panel.data.validationMode,
					setter = delegate(int value)
					{
						panel.data.validationMode = (global::UnityEngine.Rendering.Universal.DebugValidationMode)value;
					},
					getIndex = () => (int)panel.data.validationMode,
					setIndex = delegate(int value)
					{
						panel.data.validationMode = (global::UnityEngine.Rendering.Universal.DebugValidationMode)value;
					},
					onValueChanged = delegate
					{
						global::UnityEngine.Rendering.DebugManager.instance.ReDrawOnScreenDebug();
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreatePixelValidationChannels(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.EnumField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.Channels,
					autoEnum = typeof(global::UnityEngine.Rendering.Universal.PixelValidationChannels),
					getter = () => (int)panel.data.validationChannels,
					setter = delegate(int value)
					{
						panel.data.validationChannels = (global::UnityEngine.Rendering.Universal.PixelValidationChannels)value;
					},
					getIndex = () => (int)panel.data.validationChannels,
					setIndex = delegate(int value)
					{
						panel.data.validationChannels = (global::UnityEngine.Rendering.Universal.PixelValidationChannels)value;
					}
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreatePixelValueRangeMin(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.ValueRangeMin,
					getter = () => panel.data.validationRangeMin,
					setter = delegate(float value)
					{
						panel.data.validationRangeMin = value;
					},
					incStep = 0.01f
				};
			}

			internal static global::UnityEngine.Rendering.DebugUI.Widget CreatePixelValueRangeMax(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel panel)
			{
				return new global::UnityEngine.Rendering.DebugUI.FloatField
				{
					nameAndTooltip = global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.Strings.ValueRangeMax,
					getter = () => panel.data.validationRangeMax,
					setter = delegate(float value)
					{
						panel.data.validationRangeMax = value;
					},
					incStep = 0.01f
				};
			}
		}

		[global::UnityEngine.Rendering.DisplayInfo(name = "Rendering", order = 1)]
		internal class SettingsPanel : global::UnityEngine.Rendering.DebugDisplaySettingsPanel<global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering>
		{
			public SettingsPanel(global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering data)
				: base(data)
			{
				AddWidget(new global::UnityEngine.Rendering.DebugUI.RuntimeDebugShadersMessageBox());
				AddWidget(new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Rendering Debug",
					flags = global::UnityEngine.Rendering.DebugUI.Flags.FrequentlyUsed,
					opened = true,
					children = 
					{
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateMapOverlays(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateStpDebugViews(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateMapOverlaySize(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateHDR(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateMSAA(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateTaaDebugMode(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreatePostProcessing(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateAdditionalWireframeShaderViews(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateWireframeNotSupportedWarning(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateOverdrawMode(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateMaxOverdrawCount(this),
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreateMipMapDebugWidget(this)
					}
				});
				AddWidget(new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Pixel Validation",
					opened = true,
					children = 
					{
						global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValidationMode(this),
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Container
						{
							displayName = "Pixel Range Settings",
							isHiddenCallback = () => data.validationMode != global::UnityEngine.Rendering.Universal.DebugValidationMode.HighlightOutsideOfRange,
							children = 
							{
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValidationChannels(this),
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValueRangeMin(this),
								global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValueRangeMax(this)
							}
						}
					}
				});
				AddWidget(new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "HDR Output",
					opened = true,
					children = 
					{
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.MessageBox
						{
							displayName = "The values on the Rendering Debugger editor window might not be accurate. Please use the playmode debug UI (Ctrl+Backspace).",
							style = global::UnityEngine.Rendering.DebugUI.MessageBox.Style.Warning
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)global::UnityEngine.Rendering.DebugDisplaySettingsHDROutput.CreateHDROuputDisplayTable()
					}
				});
			}
		}

		private global::UnityEngine.Rendering.Universal.DebugWireframeMode m_WireframeMode;

		private bool m_Overdraw;

		private global::UnityEngine.Rendering.Universal.DebugOverdrawMode m_OverdrawMode;

		public global::UnityEngine.Rendering.Universal.DebugWireframeMode wireframeMode
		{
			get
			{
				return m_WireframeMode;
			}
			set
			{
				m_WireframeMode = value;
				UpdateDebugSceneOverrideMode();
			}
		}

		[global::System.Obsolete("overdraw has been deprecated. Use overdrawMode instead. #from(2022.2) #breakingFrom(2023.1)", true)]
		public bool overdraw
		{
			get
			{
				return m_Overdraw;
			}
			set
			{
				m_Overdraw = value;
				UpdateDebugSceneOverrideMode();
			}
		}

		public global::UnityEngine.Rendering.Universal.DebugOverdrawMode overdrawMode
		{
			get
			{
				return m_OverdrawMode;
			}
			set
			{
				m_OverdrawMode = value;
				UpdateDebugSceneOverrideMode();
			}
		}

		public int maxOverdrawCount { get; set; } = 10;

		public global::UnityEngine.Rendering.Universal.DebugFullScreenMode fullScreenDebugMode { get; set; }

		internal int stpDebugViewIndex { get; set; }

		public int fullScreenDebugModeOutputSizeScreenPercent { get; set; } = 50;

		internal global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode sceneOverrideMode { get; set; }

		public global::UnityEngine.Rendering.Universal.DebugMipInfoMode mipInfoMode { get; set; }

		public bool mipDebugStatusShowCode { get; set; }

		public global::UnityEngine.Rendering.Universal.DebugMipMapStatusMode mipDebugStatusMode { get; set; }

		public float mipDebugOpacity { get; set; } = 1f;

		public float mipDebugRecentUpdateCooldown { get; set; } = 3f;

		public int mipDebugMaterialTextureSlot { get; set; }

		public bool showInfoForAllSlots { get; set; } = true;

		internal bool canAggregateData
		{
			get
			{
				if (mipInfoMode != global::UnityEngine.Rendering.Universal.DebugMipInfoMode.MipStreamingStatus)
				{
					return mipInfoMode == global::UnityEngine.Rendering.Universal.DebugMipInfoMode.MipStreamingActivity;
				}
				return true;
			}
		}

		public global::UnityEngine.Rendering.Universal.DebugMipMapModeTerrainTexture mipDebugTerrainTexture { get; set; }

		public global::UnityEngine.Rendering.Universal.DebugPostProcessingMode postProcessingDebugMode { get; set; } = global::UnityEngine.Rendering.Universal.DebugPostProcessingMode.Auto;

		public bool enableMsaa { get; set; } = true;

		public bool enableHDR { get; set; } = true;

		public global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.TaaDebugMode taaDebugMode { get; set; }

		public global::UnityEngine.Rendering.Universal.DebugValidationMode validationMode { get; set; }

		public global::UnityEngine.Rendering.Universal.PixelValidationChannels validationChannels { get; set; }

		public float validationRangeMin { get; set; }

		public float validationRangeMax { get; set; } = 1f;

		public bool AreAnySettingsActive
		{
			get
			{
				if (postProcessingDebugMode == global::UnityEngine.Rendering.Universal.DebugPostProcessingMode.Auto && fullScreenDebugMode == global::UnityEngine.Rendering.Universal.DebugFullScreenMode.None && sceneOverrideMode == global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.None && mipInfoMode == global::UnityEngine.Rendering.Universal.DebugMipInfoMode.None && validationMode == global::UnityEngine.Rendering.Universal.DebugValidationMode.None && enableMsaa && enableHDR)
				{
					return taaDebugMode != global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.TaaDebugMode.None;
				}
				return true;
			}
		}

		public bool IsPostProcessingAllowed
		{
			get
			{
				if (postProcessingDebugMode != global::UnityEngine.Rendering.Universal.DebugPostProcessingMode.Disabled && sceneOverrideMode == global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.None)
				{
					return mipInfoMode == global::UnityEngine.Rendering.Universal.DebugMipInfoMode.None;
				}
				return false;
			}
		}

		public bool IsLightingActive
		{
			get
			{
				if (sceneOverrideMode == global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.None)
				{
					return mipInfoMode == global::UnityEngine.Rendering.Universal.DebugMipInfoMode.None;
				}
				return false;
			}
		}

		private void UpdateDebugSceneOverrideMode()
		{
			switch (wireframeMode)
			{
			case global::UnityEngine.Rendering.Universal.DebugWireframeMode.Wireframe:
				sceneOverrideMode = global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Wireframe;
				break;
			case global::UnityEngine.Rendering.Universal.DebugWireframeMode.SolidWireframe:
				sceneOverrideMode = global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.SolidWireframe;
				break;
			case global::UnityEngine.Rendering.Universal.DebugWireframeMode.ShadedWireframe:
				sceneOverrideMode = global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.ShadedWireframe;
				break;
			default:
				sceneOverrideMode = ((overdrawMode != global::UnityEngine.Rendering.Universal.DebugOverdrawMode.None) ? global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Overdraw : global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.None);
				break;
			}
		}

		public bool TryGetScreenClearColor(ref global::UnityEngine.Color color)
		{
			if (mipInfoMode != global::UnityEngine.Rendering.Universal.DebugMipInfoMode.None)
			{
				color = global::UnityEngine.Color.black;
				return true;
			}
			switch (sceneOverrideMode)
			{
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.None:
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.ShadedWireframe:
				return false;
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Overdraw:
				color = global::UnityEngine.Color.black;
				return true;
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.Wireframe:
			case global::UnityEngine.Rendering.Universal.DebugSceneOverrideMode.SolidWireframe:
				color = new global::UnityEngine.Color(0.1f, 0.1f, 0.1f, 1f);
				return true;
			default:
				throw new global::System.ArgumentOutOfRangeException("color");
			}
		}

		global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable global::UnityEngine.Rendering.IDebugDisplaySettingsData.CreatePanel()
		{
			return new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsRendering.SettingsPanel(this);
		}
	}
}
