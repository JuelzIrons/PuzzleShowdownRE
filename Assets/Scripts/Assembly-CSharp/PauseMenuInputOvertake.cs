public class PauseMenuInputOvertake : global::UnityEngine.MonoBehaviour
{
	private void Update()
	{
		if (base.gameObject.activeSelf)
		{
			global::UnityEngine.InputSystem.InputSystem.Update();
		}
	}
}
