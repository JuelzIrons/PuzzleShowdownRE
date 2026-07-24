namespace UnityEngine.Rendering.RenderGraphModule
{
	internal class RenderGraphDebugParams : global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		private static class Strings
		{
			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip ClearRenderTargetsAtCreation = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Clear Render Targets At Creation",
				tooltip = "Enable to clear all render textures before any rendergraph passes to check if some clears are missing."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip ClearRenderTargetsAtFree = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Clear Render Targets When Freed",
				tooltip = "Enable to clear all render textures when textures are freed by the graph to detect use after free of textures."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip DisablePassCulling = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Disable Pass Culling",
				tooltip = "Enable to temporarily disable culling to assess if a pass is culled."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip DisablePassMerging = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Disable Pass Merging",
				tooltip = "Enable to temporarily disable pass merging to diagnose issues or analyze performance."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip ImmediateMode = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Immediate Mode",
				tooltip = "Enable to force render graph to execute all passes in the order you registered them."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip EnableLogging = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Enable Logging",
				tooltip = "Enable to allow HDRP to capture information in the log."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip LogFrameInformation = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Log Frame Information",
				tooltip = "Enable to log information output from each frame."
			};

			public static readonly global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip LogResources = new global::UnityEngine.Rendering.DebugUI.Widget.NameAndTooltip
			{
				name = "Log Resources",
				tooltip = "Enable to log the current render graph's global resource usage."
			};
		}

		private global::UnityEngine.Rendering.DebugUI.Widget[] m_DebugItems;

		private global::UnityEngine.Rendering.DebugUI.Panel m_DebugPanel;

		public bool clearRenderTargetsAtCreation;

		public bool clearRenderTargetsAtRelease;

		public bool disablePassCulling;

		public bool disablePassMerging;

		public bool immediateMode;

		public bool logFrameInformation;

		public bool logResources;

		public bool enableLogging
		{
			get
			{
				if (!logFrameInformation)
				{
					return logResources;
				}
				return true;
			}
		}

		public bool AreAnySettingsActive
		{
			get
			{
				if (!clearRenderTargetsAtCreation && !clearRenderTargetsAtRelease && !disablePassCulling && !disablePassMerging && !immediateMode)
				{
					return enableLogging;
				}
				return true;
			}
		}

		public void ResetLogging()
		{
			logFrameInformation = false;
			logResources = false;
		}

		internal void Reset()
		{
			clearRenderTargetsAtCreation = false;
			clearRenderTargetsAtRelease = false;
			disablePassCulling = false;
			disablePassMerging = false;
			immediateMode = false;
			ResetLogging();
		}

		internal global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> GetWidgetList(string name)
		{
			return new global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget>
			{
				new global::UnityEngine.Rendering.DebugUI.Container
				{
					displayName = name + " Render Graph",
					children = 
					{
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams.Strings.ClearRenderTargetsAtCreation,
							getter = () => clearRenderTargetsAtCreation,
							setter = delegate(bool value)
							{
								clearRenderTargetsAtCreation = value;
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams.Strings.ClearRenderTargetsAtFree,
							getter = () => clearRenderTargetsAtRelease,
							setter = delegate(bool value)
							{
								clearRenderTargetsAtRelease = value;
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams.Strings.DisablePassCulling,
							getter = () => disablePassCulling,
							setter = delegate(bool value)
							{
								disablePassCulling = value;
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams.Strings.DisablePassMerging,
							getter = () => disablePassMerging,
							setter = delegate(bool value)
							{
								disablePassMerging = value;
							},
							isHiddenCallback = () => !global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.hasAnyRenderGraphWithNativeRenderPassesEnabled
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.BoolField
						{
							nameAndTooltip = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams.Strings.ImmediateMode,
							getter = () => immediateMode,
							setter = delegate(bool value)
							{
								immediateMode = value;
							},
							isHiddenCallback = () => !IsImmediateModeSupported()
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Button
						{
							nameAndTooltip = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams.Strings.LogFrameInformation,
							action = delegate
							{
								logFrameInformation = true;
							}
						},
						(global::UnityEngine.Rendering.DebugUI.Widget)new global::UnityEngine.Rendering.DebugUI.Button
						{
							nameAndTooltip = global::UnityEngine.Rendering.RenderGraphModule.RenderGraphDebugParams.Strings.LogResources,
							action = delegate
							{
								logResources = true;
							}
						}
					}
				}
			};
		}

		private bool IsImmediateModeSupported()
		{
			if (global::UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline is global::UnityEngine.Rendering.RenderGraphModule.IRenderGraphEnabledRenderPipeline renderGraphEnabledRenderPipeline)
			{
				return renderGraphEnabledRenderPipeline.isImmediateModeSupported;
			}
			return false;
		}

		public void RegisterDebug(string name, global::UnityEngine.Rendering.DebugUI.Panel debugPanel = null)
		{
			global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> widgetList = GetWidgetList(name);
			m_DebugItems = widgetList.ToArray();
			m_DebugPanel = ((debugPanel != null) ? debugPanel : global::UnityEngine.Rendering.DebugManager.instance.GetPanel((name.Length == 0) ? "Rendering" : name, createIfNull: true));
			global::UnityEngine.Rendering.DebugUI.Foldout foldout = new global::UnityEngine.Rendering.DebugUI.Foldout
			{
				displayName = name
			};
			foldout.children.Add(m_DebugItems);
			m_DebugPanel.children.Add(foldout);
		}

		public void UnRegisterDebug(string name)
		{
			if (m_DebugPanel != null)
			{
				m_DebugPanel.children.Remove(m_DebugItems);
			}
			m_DebugPanel = null;
			m_DebugItems = null;
		}
	}
}
