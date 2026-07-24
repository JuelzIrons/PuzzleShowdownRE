namespace UnityEngine.Rendering.UI
{
	[global::UnityEngine.ExecuteAlways]
	public class UIFoldout : global::UnityEngine.UI.Toggle
	{
		public global::UnityEngine.GameObject content;

		public global::UnityEngine.GameObject arrowOpened;

		public global::UnityEngine.GameObject arrowClosed;

		protected override void Start()
		{
			base.Start();
			onValueChanged.AddListener(SetState);
			SetState(base.isOn);
		}

		private void OnValidate()
		{
			SetState(base.isOn, rebuildLayout: false);
		}

		public void SetState(bool state)
		{
			SetState(state, rebuildLayout: true);
		}

		public void SetState(bool state, bool rebuildLayout)
		{
			if (!(arrowOpened == null) && !(arrowClosed == null) && !(content == null))
			{
				if (arrowOpened.activeSelf != state)
				{
					arrowOpened.SetActive(state);
				}
				if (arrowClosed.activeSelf == state)
				{
					arrowClosed.SetActive(!state);
				}
				if (content.activeSelf != state)
				{
					content.SetActive(state);
				}
				if (rebuildLayout)
				{
					global::UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.parent as global::UnityEngine.RectTransform);
				}
			}
		}
	}
}
