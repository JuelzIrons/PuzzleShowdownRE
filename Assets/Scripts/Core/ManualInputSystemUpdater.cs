public class ManualInputSystemUpdater : global::UnityEngine.MonoBehaviour
{
	private void FixedUpdate()
	{
		global::UnityEngine.InputSystem.InputSystem.Update();
	}
}
