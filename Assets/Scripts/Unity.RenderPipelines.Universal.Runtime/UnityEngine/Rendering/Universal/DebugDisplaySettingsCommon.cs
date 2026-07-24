namespace UnityEngine.Rendering.Universal
{
	internal class DebugDisplaySettingsCommon : global::UnityEngine.Rendering.IDebugDisplaySettingsData, global::UnityEngine.Rendering.IDebugDisplaySettingsQuery
	{
		[global::UnityEngine.Rendering.DisplayInfo(name = "Frequently Used", order = -1)]
		private class SettingsPanel : global::UnityEngine.Rendering.DebugDisplaySettingsPanel
		{
			private const string k_GoToSectionString = "Go to Section...";

			public override global::UnityEngine.Rendering.DebugUI.Flags Flags => global::UnityEngine.Rendering.DebugUI.Flags.FrequentlyUsed;

			public SettingsPanel()
			{
				AddWidget(new global::UnityEngine.Rendering.DebugUI.RuntimeDebugShadersMessageBox());
				global::UnityEngine.Rendering.DebugUI.Widget[] items = global::UnityEngine.Rendering.DebugManager.instance.GetItems(global::UnityEngine.Rendering.DebugUI.Flags.FrequentlyUsed);
				foreach (global::UnityEngine.Rendering.DebugUI.Widget widget in items)
				{
					global::UnityEngine.Rendering.DebugUI.Foldout foldout = widget as global::UnityEngine.Rendering.DebugUI.Foldout;
					if (foldout != null)
					{
						if (foldout.contextMenuItems == null)
						{
							foldout.contextMenuItems = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Foldout.ContextMenuItem>();
						}
						foldout.contextMenuItems.Add(new global::UnityEngine.Rendering.DebugUI.Foldout.ContextMenuItem
						{
							displayName = "Go to Section...",
							action = delegate
							{
								int num = global::UnityEngine.Rendering.DebugManager.instance.PanelIndex(foldout.panel.displayName);
								if (num >= 0)
								{
									global::UnityEngine.Rendering.DebugManager.instance.RequestEditorWindowPanelIndex(num);
								}
							}
						});
					}
					AddWidget(widget);
				}
			}
		}

		public bool AreAnySettingsActive => false;

		public global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new global::UnityEngine.Rendering.Universal.DebugDisplaySettingsCommon.SettingsPanel();
		}
	}
}
