[global::System.Serializable]
public class GameMenuScreen
{
	public global::UnityEngine.GameObject FirstSelected;

	public global::UnityEngine.GameObject ParentObject;

	public void Activate()
	{
		ParentObject.SetActive(value: true);
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			global::UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(FirstSelected);
		}
	}

	public void Deactivate()
	{
		ParentObject.SetActive(value: false);
	}
}
