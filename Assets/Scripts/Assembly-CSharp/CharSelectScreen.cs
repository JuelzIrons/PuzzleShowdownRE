public class CharSelectScreen : global::UnityEngine.MonoBehaviour
{
	public global::System.Collections.Generic.List<global::UnityEngine.UI.Button> CharButtons = new global::System.Collections.Generic.List<global::UnityEngine.UI.Button>();

	public global::System.Collections.Generic.List<CharacterType> CharInts = new global::System.Collections.Generic.List<CharacterType>();

	public void EnableAllButtons()
	{
		foreach (global::UnityEngine.UI.Button charButton in CharButtons)
		{
			charButton.enabled = true;
		}
	}

	public void DisableButtonOfCharacter(CharacterType type)
	{
		for (int i = 0; i < CharButtons.Count; i++)
		{
			if (CharInts[i] == type)
			{
				CharButtons[i].enabled = false;
			}
		}
	}
}
