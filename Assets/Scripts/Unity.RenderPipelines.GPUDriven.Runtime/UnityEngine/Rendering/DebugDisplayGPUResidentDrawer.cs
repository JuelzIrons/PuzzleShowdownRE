namespace UnityEngine.Rendering
{
	public class DebugDisplayGPUResidentDrawer : global::UnityEngine.Rendering.IDebugDisplaySettingsData, global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		private static class Strings
		{
			public const string drawerSettingsContainerName = "GPU Resident Drawer Settings";

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip displayBatcherStats = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Display Culling Stats",
				tooltip = "Enable the checkbox to display stats for instance culling."
			};

			public const string occlusionCullingTitle = "Occlusion Culling";

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip occlusionTestOverlayEnable = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Occlusion Test Overlay",
				tooltip = "Occlusion test visualisation."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip occlusionTestOverlayCountVisible = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Occlusion Test Overlay Count Visible",
				tooltip = "Occlusion test visualisation should count visible instances instead of occluded instances."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip overrideOcclusionTestToAlwaysPass = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Override Occlusion Test To Always Pass",
				tooltip = "Occlusion test always passes."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip occluderContextStats = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Context Stats",
				tooltip = "Show all the active occluder context textures."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip occluderDebugViewEnable = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Debug View",
				tooltip = "Debug view of occluder texture."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip occluderDebugViewIndex = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Debug View Index",
				tooltip = "Index of the view for which the occluder texture is displayed. Use the Occlusion Test Context Stats for a list of the views."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip occluderDebugViewRangeMin = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Debug View Range Min",
				tooltip = "Range in which the occluder debug texture are displayed."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip occluderDebugViewRangeMax = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Occluder Debug View Range Max",
				tooltip = "Range in which the occluder debug texture are displayed."
			};
		}

		[global::UnityEngine.Rendering.DisplayInfo(name = "Rendering", order = 5)]
		private class SettingsPanel : global::UnityEngine.Rendering.DebugDisplaySettingsPanel
		{
			public override global::UnityEngine.Rendering.DebugUI.Flags Flags => global::UnityEngine.Rendering.DebugUI.Flags.EditorForceUpdate;

			public SettingsPanel(global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer data)
			{
				global::UnityEngine.Rendering.DebugUI.Foldout foldout = new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "GPU Resident Drawer Settings",
					documentationUrl = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.HelpURLAttribute>(typeof(global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer))?.URL
				};
				AddWidget(foldout);
				global::UnityEngine.Rendering.DebugUI.MessageBox item = new global::UnityEngine.Rendering.DebugUI.MessageBox
				{
					displayName = "Not Supported",
					style = global::UnityEngine.Rendering.DebugUI.MessageBox.Style.Warning,
					messageCallback = () => (!global::UnityEngine.Rendering.GPUResidentDrawer.IsGPUResidentDrawerSupportedBySRP(global::UnityEngine.Rendering.GPUResidentDrawer.GetGlobalSettingsFromRPAsset(), out var message, out var _)) ? message : string.Empty,
					isHiddenCallback = () => global::UnityEngine.Rendering.GPUResidentDrawer.IsEnabled()
				};
				foldout.children.Add(item);
				foldout.children.Add(new global::UnityEngine.Rendering.DebugUI.Container
				{
					displayName = "Occlusion Culling",
					isHiddenCallback = () => !global::UnityEngine.Rendering.GPUResidentDrawer.IsEnabled(),
					children = 
					{
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.Strings.occlusionTestOverlayEnable,
							getter = () => data.occlusionTestOverlayEnable,
							setter = delegate(bool value)
							{
								data.occlusionTestOverlayEnable = value;
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.Strings.occlusionTestOverlayCountVisible,
							getter = () => data.occlusionTestOverlayCountVisible,
							setter = delegate(bool value)
							{
								data.occlusionTestOverlayCountVisible = value;
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.Strings.overrideOcclusionTestToAlwaysPass,
							getter = () => data.overrideOcclusionTestToAlwaysPass,
							setter = delegate(bool value)
							{
								data.overrideOcclusionTestToAlwaysPass = value;
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.Strings.occluderContextStats,
							getter = () => data.occluderContextStats,
							setter = delegate(bool value)
							{
								data.occluderContextStats = value;
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.Strings.occluderDebugViewEnable,
							getter = () => data.occluderDebugViewEnable,
							setter = delegate(bool value)
							{
								data.occluderDebugViewEnable = value;
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.IntField
						{
							nameAndTooltip = global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.Strings.occluderDebugViewIndex,
							getter = () => data.occluderDebugViewIndex,
							setter = delegate(int value)
							{
								data.occluderDebugViewIndex = value;
							},
							isHiddenCallback = () => !data.occluderDebugViewEnable,
							min = () => 0,
							max = () => global::System.Math.Max(GetOcclusionContextsCounts() - 1, 0)
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.FloatField
						{
							nameAndTooltip = global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.Strings.occluderDebugViewRangeMin,
							getter = () => data.occluderDebugViewRange.x,
							setter = delegate(float value)
							{
								data.occluderDebugViewRange.x = value;
							},
							isHiddenCallback = () => !data.occluderDebugViewEnable
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.FloatField
						{
							nameAndTooltip = global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.Strings.occluderDebugViewRangeMax,
							getter = () => data.occluderDebugViewRange.y,
							setter = delegate(float value)
							{
								data.occluderDebugViewRange.y = value;
							},
							isHiddenCallback = () => !data.occluderDebugViewEnable
						}
					}
				});
				AddOcclusionContextStatsWidget(data);
				foldout.children.Add(new global::UnityEngine.Rendering.DebugUI.BoolField
				{
					nameAndTooltip = global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.Strings.displayBatcherStats,
					getter = () => data.displayBatcherStats,
					setter = delegate(bool value)
					{
						data.displayBatcherStats = value;
					},
					isHiddenCallback = () => !global::UnityEngine.Rendering.GPUResidentDrawer.IsEnabled()
				});
				AddInstanceCullingStatsWidget(data);
			}

			private void AddInstanceCullingStatsWidget(global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer data)
			{
				global::UnityEngine.Rendering.DebugUI.Foldout foldout = new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Instance Culler Stats",
					isHeader = true,
					opened = true,
					isHiddenCallback = () => !data.displayBatcherStats
				};
				foldout.children.Add(new global::UnityEngine.Rendering.DebugUI.ValueTuple
				{
					displayName = "View Count",
					values = new global::UnityEngine.Rendering.DebugUI.Value[1]
					{
						new global::UnityEngine.Rendering.DebugUI.Value
						{
							refreshRate = 0.2f,
							formatString = "{0}",
							getter = () => GetInstanceCullerViewCount()
						}
					}
				});
				foldout.children.Add(new global::UnityEngine.Rendering.DebugUI.ValueTuple
				{
					displayName = "Total Visible Instances (Cameras | Lights | Both)",
					values = new global::UnityEngine.Rendering.DebugUI.Value[3]
					{
						new global::UnityEngine.Rendering.DebugUI.Value
						{
							refreshRate = 0.2f,
							formatString = "{0}",
							getter = delegate
							{
								int num3 = 0;
								for (int i = 0; i < GetInstanceCullerViewCount(); i++)
								{
									global::UnityEngine.Rendering.InstanceCullerViewStats instanceCullerViewStats = GetInstanceCullerViewStats(i);
									if (instanceCullerViewStats.viewType == global::UnityEngine.Rendering.BatchCullingViewType.Camera)
									{
										num3 += instanceCullerViewStats.visibleInstancesOnGPU;
									}
								}
								return num3;
							}
						},
						new global::UnityEngine.Rendering.DebugUI.Value
						{
							refreshRate = 0.2f,
							formatString = "{0}",
							getter = delegate
							{
								int num3 = 0;
								for (int i = 0; i < GetInstanceCullerViewCount(); i++)
								{
									global::UnityEngine.Rendering.InstanceCullerViewStats instanceCullerViewStats = GetInstanceCullerViewStats(i);
									if (instanceCullerViewStats.viewType == global::UnityEngine.Rendering.BatchCullingViewType.Light)
									{
										num3 += instanceCullerViewStats.visibleInstancesOnGPU;
									}
								}
								return num3;
							}
						},
						new global::UnityEngine.Rendering.DebugUI.Value
						{
							refreshRate = 0.2f,
							formatString = "{0}",
							getter = delegate
							{
								int num3 = 0;
								for (int i = 0; i < GetInstanceCullerViewCount(); i++)
								{
									global::UnityEngine.Rendering.InstanceCullerViewStats instanceCullerViewStats = GetInstanceCullerViewStats(i);
									if (instanceCullerViewStats.viewType != global::UnityEngine.Rendering.BatchCullingViewType.Filtering && instanceCullerViewStats.viewType != global::UnityEngine.Rendering.BatchCullingViewType.Picking && instanceCullerViewStats.viewType != global::UnityEngine.Rendering.BatchCullingViewType.SelectionOutline)
									{
										num3 += instanceCullerViewStats.visibleInstancesOnGPU;
									}
								}
								return num3;
							}
						}
					}
				});
				foldout.children.Add(new global::UnityEngine.Rendering.DebugUI.ValueTuple
				{
					displayName = "Total Visible Primitives (Cameras | Lights | Both)",
					values = new global::UnityEngine.Rendering.DebugUI.Value[3]
					{
						new global::UnityEngine.Rendering.DebugUI.Value
						{
							refreshRate = 0.2f,
							formatString = "{0}",
							getter = delegate
							{
								int num3 = 0;
								for (int i = 0; i < GetInstanceCullerViewCount(); i++)
								{
									global::UnityEngine.Rendering.InstanceCullerViewStats instanceCullerViewStats = GetInstanceCullerViewStats(i);
									if (instanceCullerViewStats.viewType == global::UnityEngine.Rendering.BatchCullingViewType.Camera)
									{
										num3 += instanceCullerViewStats.visiblePrimitivesOnGPU;
									}
								}
								return num3;
							}
						},
						new global::UnityEngine.Rendering.DebugUI.Value
						{
							refreshRate = 0.2f,
							formatString = "{0}",
							getter = delegate
							{
								int num3 = 0;
								for (int i = 0; i < GetInstanceCullerViewCount(); i++)
								{
									global::UnityEngine.Rendering.InstanceCullerViewStats instanceCullerViewStats = GetInstanceCullerViewStats(i);
									if (instanceCullerViewStats.viewType == global::UnityEngine.Rendering.BatchCullingViewType.Light)
									{
										num3 += instanceCullerViewStats.visiblePrimitivesOnGPU;
									}
								}
								return num3;
							}
						},
						new global::UnityEngine.Rendering.DebugUI.Value
						{
							refreshRate = 0.2f,
							formatString = "{0}",
							getter = delegate
							{
								int num3 = 0;
								for (int i = 0; i < GetInstanceCullerViewCount(); i++)
								{
									global::UnityEngine.Rendering.InstanceCullerViewStats instanceCullerViewStats = GetInstanceCullerViewStats(i);
									if (instanceCullerViewStats.viewType != global::UnityEngine.Rendering.BatchCullingViewType.Filtering && instanceCullerViewStats.viewType != global::UnityEngine.Rendering.BatchCullingViewType.Picking && instanceCullerViewStats.viewType != global::UnityEngine.Rendering.BatchCullingViewType.SelectionOutline)
									{
										num3 += instanceCullerViewStats.visiblePrimitivesOnGPU;
									}
								}
								return num3;
							}
						}
					}
				});
				global::UnityEngine.Rendering.DebugUI.Table table = new global::UnityEngine.Rendering.DebugUI.Table
				{
					displayName = "",
					isReadOnly = true
				};
				for (int num = 0; num < 32; num++)
				{
					table.children.Add(AddInstanceCullerViewDataRow(num));
				}
				global::UnityEngine.Rendering.DebugUI.Foldout foldout2 = new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Per View Stats",
					isHeader = true,
					opened = false,
					isHiddenCallback = () => !data.displayBatcherStats
				};
				foldout2.children.Add(table);
				foldout.children.Add(foldout2);
				global::UnityEngine.Rendering.DebugUI.Table table2 = new global::UnityEngine.Rendering.DebugUI.Table
				{
					displayName = "",
					isReadOnly = true
				};
				for (int num2 = 0; num2 < 32; num2++)
				{
					table2.children.Add(AddInstanceOcclusionPassDataRow(num2));
				}
				global::UnityEngine.Rendering.DebugUI.Foldout foldout3 = new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Occlusion Culling Events",
					isHeader = true,
					opened = false,
					isHiddenCallback = () => !data.displayBatcherStats
				};
				foldout3.children.Add(table2);
				foldout.children.Add(foldout3);
				AddWidget(foldout);
			}

			private void AddOcclusionContextStatsWidget(global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer data)
			{
				global::UnityEngine.Rendering.DebugUI.Foldout foldout = new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Occlusion Context Stats",
					isHeader = true,
					opened = true,
					isHiddenCallback = () => !data.occluderContextStats
				};
				foldout.children.Add(new global::UnityEngine.Rendering.DebugUI.ValueTuple
				{
					displayName = "Active Occlusion Contexts",
					values = new global::UnityEngine.Rendering.DebugUI.Value[1]
					{
						new global::UnityEngine.Rendering.DebugUI.Value
						{
							refreshRate = 0.2f,
							formatString = "{0}",
							getter = () => GetOcclusionContextsCounts()
						}
					}
				});
				global::UnityEngine.Rendering.DebugUI.Table table = new global::UnityEngine.Rendering.DebugUI.Table
				{
					displayName = "",
					isReadOnly = true
				};
				for (int num = 0; num < 16; num++)
				{
					table.children.Add(AddOcclusionContextDataRow(num));
				}
				foldout.children.Add(table);
				AddWidget(foldout);
			}
		}

		private const string k_FormatString = "{0}";

		private const float k_RefreshRate = 0.2f;

		private const int k_MaxViewCount = 32;

		private const int k_MaxOcclusionPassCount = 32;

		private const int k_MaxContextCount = 16;

		public bool occluderDebugViewEnable;

		internal bool occluderContextStats;

		internal global::UnityEngine.Vector2 occluderDebugViewRange = new global::UnityEngine.Vector2(0f, 1f);

		internal int occluderDebugViewIndex;

		private bool displayBatcherStats
		{
			get
			{
				return global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats()?.enabled ?? false;
			}
			set
			{
				global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats();
				if (debugStats != null)
				{
					debugStats.enabled = value;
				}
			}
		}

		internal bool occlusionTestOverlayEnable
		{
			get
			{
				return global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats()?.occlusionOverlayEnabled ?? false;
			}
			set
			{
				global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats();
				if (debugStats != null)
				{
					debugStats.occlusionOverlayEnabled = value;
				}
			}
		}

		private bool occlusionTestOverlayCountVisible
		{
			get
			{
				return global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats()?.occlusionOverlayCountVisible ?? false;
			}
			set
			{
				global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats();
				if (debugStats != null)
				{
					debugStats.occlusionOverlayCountVisible = value;
				}
			}
		}

		private bool overrideOcclusionTestToAlwaysPass
		{
			get
			{
				return global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats()?.overrideOcclusionTestToAlwaysPass ?? false;
			}
			set
			{
				global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats();
				if (debugStats != null)
				{
					debugStats.overrideOcclusionTestToAlwaysPass = value;
				}
			}
		}

		public bool AreAnySettingsActive => displayBatcherStats;

		public bool IsPostProcessingAllowed => true;

		public bool IsLightingActive => true;

		internal bool GetOccluderViewInstanceID(out int viewInstanceID)
		{
			global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats();
			if (debugStats != null && occluderDebugViewIndex >= 0 && occluderDebugViewIndex < debugStats.occluderStats.Length)
			{
				viewInstanceID = debugStats.occluderStats[occluderDebugViewIndex].viewInstanceID;
				return true;
			}
			viewInstanceID = 0;
			return false;
		}

		private static global::UnityEngine.Rendering.InstanceCullerViewStats GetInstanceCullerViewStats(int viewIndex)
		{
			global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats();
			if (debugStats != null && viewIndex < debugStats.instanceCullerStats.Length)
			{
				return debugStats.instanceCullerStats[viewIndex];
			}
			return default(global::UnityEngine.Rendering.InstanceCullerViewStats);
		}

		private static global::UnityEngine.Rendering.InstanceOcclusionEventStats GetInstanceOcclusionEventStats(int passIndex)
		{
			global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats();
			if (debugStats != null && passIndex < debugStats.instanceOcclusionEventStats.Length)
			{
				return debugStats.instanceOcclusionEventStats[passIndex];
			}
			return default(global::UnityEngine.Rendering.InstanceOcclusionEventStats);
		}

		private static global::UnityEngine.Rendering.DebugOccluderStats GetOccluderStats(int occluderIndex)
		{
			global::UnityEngine.Rendering.DebugRendererBatcherStats debugStats = global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats();
			if (debugStats != null && occluderIndex < debugStats.occluderStats.Length)
			{
				return debugStats.occluderStats[occluderIndex];
			}
			return default(global::UnityEngine.Rendering.DebugOccluderStats);
		}

		private static int GetOcclusionContextsCounts()
		{
			return global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats()?.occluderStats.Length ?? 0;
		}

		private static int GetInstanceCullerViewCount()
		{
			return global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats()?.instanceCullerStats.Length ?? 0;
		}

		private static int GetInstanceOcclusionEventCount()
		{
			return global::UnityEngine.Rendering.GPUResidentDrawer.GetDebugStats()?.instanceOcclusionEventStats.Length ?? 0;
		}

		private static global::UnityEngine.Rendering.DebugUI.Table.Row AddInstanceCullerViewDataRow(int viewIndex)
		{
			return new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = "",
				opened = true,
				isHiddenCallback = () => viewIndex >= GetInstanceCullerViewCount(),
				children = 
				{
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "View Type",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => GetInstanceCullerViewStats(viewIndex).viewType
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "View Instance ID",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => GetInstanceCullerViewStats(viewIndex).viewInstanceID
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Split Index",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => GetInstanceCullerViewStats(viewIndex).splitIndex
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Visible Instances CPU | GPU",
						tooltip = "Visible instances after CPU culling and after GPU culling.",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = delegate
						{
							global::UnityEngine.Rendering.InstanceCullerViewStats instanceCullerViewStats = GetInstanceCullerViewStats(viewIndex);
							return $"{instanceCullerViewStats.visibleInstancesOnCPU} | {instanceCullerViewStats.visibleInstancesOnGPU}";
						}
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Visible Primitives CPU | GPU",
						tooltip = "Visible primitives after CPU culling and after GPU culling.",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = delegate
						{
							global::UnityEngine.Rendering.InstanceCullerViewStats instanceCullerViewStats = GetInstanceCullerViewStats(viewIndex);
							return $"{instanceCullerViewStats.visiblePrimitivesOnCPU} | {instanceCullerViewStats.visiblePrimitivesOnGPU}";
						}
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Draw Commands",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => GetInstanceCullerViewStats(viewIndex).drawCommands
					}
				}
			};
		}

		private static object OccluderVersionString(in global::UnityEngine.Rendering.InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != global::UnityEngine.Rendering.InstanceOcclusionEventType.OccluderUpdate && stats.occlusionTest == global::UnityEngine.Rendering.OcclusionTest.None)
			{
				return "-";
			}
			return stats.occluderVersion;
		}

		private static object OcclusionTestString(in global::UnityEngine.Rendering.InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != global::UnityEngine.Rendering.InstanceOcclusionEventType.OcclusionTest)
			{
				return "-";
			}
			return stats.occlusionTest;
		}

		private static object VisibleInstancesString(in global::UnityEngine.Rendering.InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != global::UnityEngine.Rendering.InstanceOcclusionEventType.OcclusionTest)
			{
				return "-";
			}
			return stats.visibleInstances;
		}

		private static object CulledInstancesString(in global::UnityEngine.Rendering.InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != global::UnityEngine.Rendering.InstanceOcclusionEventType.OcclusionTest)
			{
				return "-";
			}
			return stats.culledInstances;
		}

		private static object VisiblePrimitivesString(in global::UnityEngine.Rendering.InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != global::UnityEngine.Rendering.InstanceOcclusionEventType.OcclusionTest)
			{
				return "-";
			}
			return stats.visiblePrimitives;
		}

		private static object CulledPrimitivesString(in global::UnityEngine.Rendering.InstanceOcclusionEventStats stats)
		{
			if (stats.eventType != global::UnityEngine.Rendering.InstanceOcclusionEventType.OcclusionTest)
			{
				return "-";
			}
			return stats.culledPrimitives;
		}

		private static global::UnityEngine.Rendering.DebugUI.Table.Row AddInstanceOcclusionPassDataRow(int eventIndex)
		{
			return new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = "",
				opened = true,
				isHiddenCallback = () => eventIndex >= GetInstanceOcclusionEventCount(),
				children = 
				{
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "View Instance ID",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => GetInstanceOcclusionEventStats(eventIndex).viewInstanceID
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Event Type",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => $"{GetInstanceOcclusionEventStats(eventIndex).eventType}"
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Occluder Version",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => OccluderVersionString(GetInstanceOcclusionEventStats(eventIndex))
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Subview Mask",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => $"0x{GetInstanceOcclusionEventStats(eventIndex).subviewMask:X}"
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Occlusion Test",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => $"{OcclusionTestString(GetInstanceOcclusionEventStats(eventIndex))}"
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Visible Instances",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => VisibleInstancesString(GetInstanceOcclusionEventStats(eventIndex))
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Culled Instances",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => CulledInstancesString(GetInstanceOcclusionEventStats(eventIndex))
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Visible Primitives",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => VisiblePrimitivesString(GetInstanceOcclusionEventStats(eventIndex))
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Culled Primitives",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => CulledPrimitivesString(GetInstanceOcclusionEventStats(eventIndex))
					}
				}
			};
		}

		private static global::UnityEngine.Rendering.DebugUI.Table.Row AddOcclusionContextDataRow(int index)
		{
			return new global::UnityEngine.Rendering.DebugUI.Table.Row
			{
				displayName = "",
				opened = true,
				isHiddenCallback = () => index >= GetOcclusionContextsCounts(),
				children = 
				{
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "View Instance ID",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => GetOccluderStats(index).viewInstanceID
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Subview Count",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = () => GetOccluderStats(index).subviewCount
					},
					(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Value
					{
						displayName = "Size Per Subview",
						refreshRate = 0.2f,
						formatString = "{0}",
						getter = delegate
						{
							global::UnityEngine.Vector2Int occluderMipLayoutSize = GetOccluderStats(index).occluderMipLayoutSize;
							return $"{occluderMipLayoutSize.x}x{occluderMipLayoutSize.y}";
						}
					}
				}
			};
		}

		public bool TryGetScreenClearColor(ref global::UnityEngine.Color color)
		{
			return false;
		}

		global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable global::UnityEngine.Rendering.IDebugDisplaySettingsData.CreatePanel()
		{
			return new global::UnityEngine.Rendering.DebugDisplayGPUResidentDrawer.SettingsPanel(this);
		}
	}
}
