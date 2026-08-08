public class DifficultyManager : global::UnityEngine.MonoBehaviour
{
	public int SelectedLocalDifficulty;

	[global::UnityEngine.SerializeField]
	private global::System.Collections.Generic.List<global::UnityEngine.UI.Image> m_difficultyButtonHoverOutlines;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject NextButton;

	public void Setup()
	{
		if (global::UnityEngine.PlayerPrefs.GetInt("SELECTED_DIFFICULTY_LVL") == 0)
		{
			SelectDifficulty(1);
		}
		else
		{
			SelectDifficulty(global::UnityEngine.PlayerPrefs.GetInt("SELECTED_DIFFICULTY_LVL"));
		}
	}

	public void SaveSelectedDifficulty()
	{
		global::UnityEngine.PlayerPrefs.SetInt("SELECTED_DIFFICULTY_LVL", SelectedLocalDifficulty);
		GameManager.Instance.SelectedDifficulty = (DifficultyType)SelectedLocalDifficulty;
	}

	public void SelectedGoToSpeed(int difficulty)
	{
		SelectDifficulty(difficulty);
	}

	public void SelectDifficulty(int diff)
	{
		SelectedLocalDifficulty = diff;
		if (NetworkServerReciever.Instance != null)
		{
			NetworkServerReciever.Instance.HostSetDifficultyLevel(SelectedLocalDifficulty);
		}
		if (m_difficultyButtonHoverOutlines.Count != 0 && m_difficultyButtonHoverOutlines[0] != null)
		{
			m_difficultyButtonHoverOutlines[0].color = new global::UnityEngine.Color(255f, 255f, 255f, 0f);
			m_difficultyButtonHoverOutlines[1].color = new global::UnityEngine.Color(255f, 255f, 255f, 0f);
			m_difficultyButtonHoverOutlines[2].color = new global::UnityEngine.Color(255f, 255f, 255f, 0f);
			m_difficultyButtonHoverOutlines[3].color = new global::UnityEngine.Color(255f, 255f, 255f, 0f);
			m_difficultyButtonHoverOutlines[4].color = new global::UnityEngine.Color(255f, 255f, 255f, 0f);
			switch (diff)
			{
			case 1:
				m_difficultyButtonHoverOutlines[0].color = new global::UnityEngine.Color(255f, 255f, 255f, 1f);
				break;
			case 2:
				m_difficultyButtonHoverOutlines[1].color = new global::UnityEngine.Color(255f, 255f, 255f, 1f);
				break;
			case 3:
				m_difficultyButtonHoverOutlines[2].color = new global::UnityEngine.Color(255f, 255f, 255f, 1f);
				break;
			case 4:
				m_difficultyButtonHoverOutlines[3].color = new global::UnityEngine.Color(255f, 255f, 255f, 1f);
				break;
			case 5:
				m_difficultyButtonHoverOutlines[4].color = new global::UnityEngine.Color(255f, 255f, 255f, 1f);
				break;
			}
		}
		SaveSelectedDifficulty();
	}
}
