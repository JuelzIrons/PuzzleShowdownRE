namespace UnityEngine.UI
{
	[global::UnityEngine.AddComponentMenu("UI (Canvas)/Toggle Group", 31)]
	[global::UnityEngine.DisallowMultipleComponent]
	public class ToggleGroup : global::UnityEngine.EventSystems.UIBehaviour
	{
		[global::UnityEngine.SerializeField]
		private bool m_AllowSwitchOff;

		protected global::System.Collections.Generic.List<global::UnityEngine.UI.Toggle> m_Toggles = new global::System.Collections.Generic.List<global::UnityEngine.UI.Toggle>();

		public bool allowSwitchOff
		{
			get
			{
				return m_AllowSwitchOff;
			}
			set
			{
				m_AllowSwitchOff = value;
			}
		}

		protected ToggleGroup()
		{
		}

		protected override void Start()
		{
			EnsureValidState();
			base.Start();
		}

		protected override void OnEnable()
		{
			EnsureValidState();
			base.OnEnable();
		}

		private void ValidateToggleIsInGroup(global::UnityEngine.UI.Toggle toggle)
		{
			if (toggle == null || !m_Toggles.Contains(toggle))
			{
				throw new global::System.ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[2] { toggle, this }));
			}
		}

		public void NotifyToggleOn(global::UnityEngine.UI.Toggle toggle, bool sendCallback = true)
		{
			ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < m_Toggles.Count; i++)
			{
				if (!(m_Toggles[i] == toggle))
				{
					if (sendCallback)
					{
						m_Toggles[i].isOn = false;
					}
					else
					{
						m_Toggles[i].SetIsOnWithoutNotify(value: false);
					}
				}
			}
		}

		public void UnregisterToggle(global::UnityEngine.UI.Toggle toggle)
		{
			if (m_Toggles.Contains(toggle))
			{
				m_Toggles.Remove(toggle);
			}
		}

		public void RegisterToggle(global::UnityEngine.UI.Toggle toggle)
		{
			if (!m_Toggles.Contains(toggle))
			{
				m_Toggles.Add(toggle);
			}
		}

		public void EnsureValidState()
		{
			if (!allowSwitchOff && !AnyTogglesOn() && m_Toggles.Count != 0)
			{
				m_Toggles[0].isOn = true;
				NotifyToggleOn(m_Toggles[0]);
			}
			global::System.Collections.Generic.IEnumerable<global::UnityEngine.UI.Toggle> enumerable = ActiveToggles();
			if (global::System.Linq.Enumerable.Count(enumerable) <= 1)
			{
				return;
			}
			global::UnityEngine.UI.Toggle firstActiveToggle = GetFirstActiveToggle();
			foreach (global::UnityEngine.UI.Toggle item in enumerable)
			{
				if (!(item == firstActiveToggle))
				{
					item.isOn = false;
				}
			}
		}

		public bool AnyTogglesOn()
		{
			return m_Toggles.Find((global::UnityEngine.UI.Toggle x) => x.isOn) != null;
		}

		public global::System.Collections.Generic.IEnumerable<global::UnityEngine.UI.Toggle> ActiveToggles()
		{
			return global::System.Linq.Enumerable.Where(m_Toggles, (global::UnityEngine.UI.Toggle x) => x.isOn);
		}

		public global::UnityEngine.UI.Toggle GetFirstActiveToggle()
		{
			global::System.Collections.Generic.IEnumerable<global::UnityEngine.UI.Toggle> source = ActiveToggles();
			if (global::System.Linq.Enumerable.Count(source) <= 0)
			{
				return null;
			}
			return global::System.Linq.Enumerable.First(source);
		}

		public void SetAllTogglesOff(bool sendCallback = true)
		{
			bool flag = m_AllowSwitchOff;
			m_AllowSwitchOff = true;
			if (sendCallback)
			{
				for (int i = 0; i < m_Toggles.Count; i++)
				{
					m_Toggles[i].isOn = false;
				}
			}
			else
			{
				for (int j = 0; j < m_Toggles.Count; j++)
				{
					m_Toggles[j].SetIsOnWithoutNotify(value: false);
				}
			}
			m_AllowSwitchOff = flag;
		}
	}
}
