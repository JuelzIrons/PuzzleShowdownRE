public class UiSupressorIfLoading : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.InputSystem.UI.InputSystemUIInputModule m_module;

	private void Update()
	{
		m_module.enabled = !SceneLoader.Instance.IsLoading;
	}
}
