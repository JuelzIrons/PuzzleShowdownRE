public class CustomMenuControlSchemeBehaviour : global::UnityEngine.MonoBehaviour
{
	public static CustomMenuControlSchemeBehaviour Instance;

	private void Awake()
	{
		Instance = this;
	}

	public void SetSelected(global::UnityEngine.UI.Selectable sel)
	{
		if (sel != null && global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(sel.gameObject);
		}
	}
}
