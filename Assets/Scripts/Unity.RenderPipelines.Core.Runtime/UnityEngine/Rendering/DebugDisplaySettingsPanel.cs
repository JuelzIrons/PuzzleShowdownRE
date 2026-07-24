namespace UnityEngine.Rendering
{
	public abstract class DebugDisplaySettingsPanel : global::UnityEngine.Rendering.IDebugDisplaySettingsPanelDisposable, global::UnityEngine.Rendering.IDebugDisplaySettingsPanel, global::System.IDisposable
	{
		private readonly global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget> m_Widgets = new global::System.Collections.Generic.List<global::UnityEngine.Rendering.DebugUI.Widget>();

		private readonly global::UnityEngine.Rendering.DisplayInfoAttribute m_DisplayInfo;

		public virtual string PanelName => m_DisplayInfo?.name ?? string.Empty;

		public virtual int Order => m_DisplayInfo?.order ?? 0;

		public global::UnityEngine.Rendering.DebugUI.Widget[] Widgets => m_Widgets.ToArray();

		public virtual global::UnityEngine.Rendering.DebugUI.Flags Flags => global::UnityEngine.Rendering.DebugUI.Flags.None;

		protected void AddWidget(global::UnityEngine.Rendering.DebugUI.Widget widget)
		{
			if (widget == null)
			{
				throw new global::System.ArgumentNullException("widget");
			}
			m_Widgets.Add(widget);
		}

		protected void Clear()
		{
			m_Widgets.Clear();
		}

		public virtual void Dispose()
		{
			Clear();
		}

		protected DebugDisplaySettingsPanel()
		{
			m_DisplayInfo = global::System.Reflection.CustomAttributeExtensions.GetCustomAttribute<global::UnityEngine.Rendering.DisplayInfoAttribute>(GetType());
			if (m_DisplayInfo == null)
			{
				global::UnityEngine.Debug.Log(string.Format("Type {0} should specify the attribute {1}", GetType(), "DisplayInfoAttribute"));
			}
		}
	}
	public abstract class DebugDisplaySettingsPanel<T> : global::UnityEngine.Rendering.DebugDisplaySettingsPanel where T : global::UnityEngine.Rendering.IDebugDisplaySettingsData
	{
		internal T m_Data;

		public T data
		{
			get
			{
				return m_Data;
			}
			internal set
			{
				m_Data = value;
			}
		}

		protected DebugDisplaySettingsPanel(T data)
		{
			m_Data = data;
		}
	}
}
