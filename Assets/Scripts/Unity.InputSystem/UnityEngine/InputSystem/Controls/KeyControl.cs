namespace UnityEngine.InputSystem.Controls
{
	public class KeyControl : global::UnityEngine.InputSystem.Controls.ButtonControl
	{
		private int m_ScanCode;

		public global::UnityEngine.InputSystem.Key keyCode { get; set; }

		public int scanCode
		{
			get
			{
				RefreshConfigurationIfNeeded();
				return m_ScanCode;
			}
		}

		protected override void RefreshConfiguration()
		{
			base.displayName = null;
			m_ScanCode = 0;
			global::UnityEngine.InputSystem.LowLevel.QueryKeyNameCommand command = global::UnityEngine.InputSystem.LowLevel.QueryKeyNameCommand.Create(keyCode);
			if (base.device.ExecuteCommand(ref command) <= 0)
			{
				return;
			}
			m_ScanCode = command.scanOrKeyCode;
			string text = command.ReadKeyName();
			if (string.IsNullOrEmpty(text))
			{
				base.displayName = text;
				return;
			}
			string str = text.ToLowerInvariant();
			if (string.IsNullOrEmpty(str))
			{
				base.displayName = text;
				return;
			}
			global::System.Globalization.TextInfo textInfo = global::System.Globalization.CultureInfo.InvariantCulture.TextInfo;
			base.displayName = textInfo.ToTitleCase(str);
		}
	}
}
