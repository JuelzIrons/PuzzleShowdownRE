public class BackgroundChanger : global::UnityEngine.MonoBehaviour
{
	[global::UnityEngine.SerializeField]
	private global::UnityEngine.SpriteRenderer m_bgRend;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.Sprite> m_bgs;

	private void Start()
	{
		if (GameManager.Instance != null && GameManager.Instance.DefinedGameMode == GameModeType.Marathon)
		{
			m_bgRend.sprite = m_bgs[(int)GameManager.Instance.LocallySelectedCharacter];
		}
		else if (GameManager.Instance != null && GameManager.Instance.DefinedGameMode == GameModeType.LocalMp)
		{
			m_bgRend.sprite = m_bgs[(int)GameManager.Instance.LocallySelectedCharacter];
		}
		else if (GameManager.Instance != null && GameManager.Instance.DefinedGameMode == GameModeType.Online)
		{
			m_bgRend.sprite = m_bgs[NetworkServerReciever.Instance.HostCharacter.Value];
		}
	}
}
