namespace UnityEngine.Rendering
{
	internal class DebugDisplaySettingsRenderGraph : global::UnityEngine.Rendering.IDebugDisplaySettingsData, global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		[global::UnityEngine.Rendering.DisplayInfo(name = "Rendering", order = 10)]
		private class SettingsPanel : global::UnityEngine.Rendering.DebugDisplaySettingsPanel
		{
			public SettingsPanel(global::UnityEngine.Rendering.DebugDisplaySettingsRenderGraph _)
			{
				global::UnityEngine.Rendering.DebugUI.Foldout foldout = new global::UnityEngine.Rendering.DebugUI.Foldout
				{
					displayName = "Render Graph",
					documentationUrl = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.HelpURLAttribute>(typeof(global::UnityEngine.Rendering.DebugDisplaySettingsRenderGraph))?.URL
				};
				AddWidget(foldout);
				bool flag = false;
				foreach (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph registeredRenderGraph in global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.GetRegisteredRenderGraphs())
				{
					flag = true;
					foreach (global::UnityEngine.Rendering.DebugUI.Widget widget in registeredRenderGraph.GetWidgetList())
					{
						foldout.children.Add(widget);
					}
				}
				if (!flag)
				{
					foldout.children.Add(new global::UnityEngine.Rendering.DebugUI.MessageBox
					{
						displayName = "Warning: The current render pipeline does not have Render Graphs Registered",
						style = global::UnityEngine.Rendering.DebugUI.MessageBox.Style.Warning
					});
				}
			}
		}

		public bool AreAnySettingsActive
		{
			get
			{
				foreach (global::System.Collections.Generic.KeyValuePair<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph, global::System.Collections.Generic.List<global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.DebugExecutionItem>> registeredExecution in global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.GetRegisteredExecutions())
				{
					registeredExecution.Deconstruct(out var key, out var _);
					if (key.areAnySettingsActive)
					{
						return true;
					}
				}
				return false;
			}
		}

		public DebugDisplaySettingsRenderGraph()
		{
			foreach (global::UnityEngine.Rendering.RenderGraphModule.RenderGraph registeredRenderGraph in global::UnityEngine.Rendering.RenderGraphModule.RenderGraph.GetRegisteredRenderGraphs())
			{
				registeredRenderGraph.debugParams.Reset();
			}
		}

		global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable global::UnityEngine.Rendering.IDebugDisplaySettingsData.CreatePanel()
		{
			return new global::UnityEngine.Rendering.DebugDisplaySettingsRenderGraph.SettingsPanel(this);
		}
	}
}
