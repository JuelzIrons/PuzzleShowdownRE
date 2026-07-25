public class CharacterSelectGrid : global::UnityEngine.MonoBehaviour
{
	public bool ISMARATHON;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_p1Cursor;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_p2Cursor;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_p1Splash;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_p2Splash;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform[] m_p1SplashPos;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Transform[] m_p2SplashPos;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_p1CharNameText;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_p2CharNameText;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject[] m_playerReadyObjs;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_backBtn;

	private global::DG.Tweening.Tweener m_splashTweenP1;

	private global::DG.Tweening.Tweener m_splashTweenP2;

	public int rows = 2;

	public int cols = 4;

	public global::UnityEngine.GameObject[,] characters;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject[] m_selectableChars;

	private global::UnityEngine.Vector2Int cursor1 = new global::UnityEngine.Vector2Int(0, 0);

	private global::UnityEngine.Vector2Int cursor2 = new global::UnityEngine.Vector2Int(1, 0);

	private CharacterType m_p1SelectedChar;

	private CharacterType m_p2SelectedChar;

	public bool P1_READY;

	public bool P2_READY;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Color m_readySplashColor;

	private global::DG.Tweening.Tweener m_splashColorTweensP1;

	private global::DG.Tweening.Tweener m_splashColorTweensP2;

	[global::UnityEngine.SerializeField]
	private DataHolder m_chardata;

	public global::System.Action ALLREADYACTION;

	public void MoveCursorP1(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.performed && !P1_READY)
		{
			global::UnityEngine.Vector2 vector = context.ReadValue<global::UnityEngine.Vector2>();
			global::UnityEngine.Vector2Int vector2Int = new global::UnityEngine.Vector2Int(-(int)vector.y, (int)vector.x);
			global::UnityEngine.Vector2Int vector2Int2 = ClampToGrid(cursor1 + vector2Int);
			if (vector2Int2 != cursor2 || NetworkServerReciever.Instance != null || ISMARATHON)
			{
				cursor1 = vector2Int2;
				CharacterType p1SelectedChar = m_p1SelectedChar;
				m_p1SelectedChar = GetSelectedCharacter1();
				m_p1Cursor.transform.position = GetP1Pos();
				UpdateSelectedChar(isP1: true, p1SelectedChar);
			}
			if (NetworkServerReciever.Instance != null)
			{
				NetworkServerReciever.Instance.MoveCursorToPosRpc(global::Unity.Netcode.NetworkManager.Singleton.LocalClientId, cursor1.x, cursor1.y);
			}
		}
	}

	public void ForceOpponentCursorToPos(global::UnityEngine.Vector2Int pos)
	{
		cursor2 = pos;
		CharacterType p2SelectedChar = m_p2SelectedChar;
		m_p2SelectedChar = GetSelectedCharacter2();
		m_p2Cursor.transform.position = GetP2Pos();
		UpdateSelectedChar(isP1: false, p2SelectedChar);
	}

	public void ForceOpponentReadyState(bool isRdy)
	{
		if (isRdy)
		{
			P2_READY = true;
			m_playerReadyObjs[1].SetActive(value: true);
			global::DG.Tweening.TweenExtensions.Kill(m_splashColorTweensP2);
			m_splashColorTweensP2 = global::DG.Tweening.DOTweenModuleUI.DOColor(m_p2Splash.GetComponent<global::UnityEngine.UI.Image>(), m_readySplashColor, 0.1f);
			CheckForReady();
		}
		else
		{
			P2_READY = false;
			m_playerReadyObjs[1].SetActive(value: false);
			global::DG.Tweening.TweenExtensions.Kill(m_splashColorTweensP2);
			m_splashColorTweensP2 = global::DG.Tweening.DOTweenModuleUI.DOColor(m_p2Splash.GetComponent<global::UnityEngine.UI.Image>(), global::UnityEngine.Color.white, 0.1f);
		}
	}

	public void MoveCursorP2(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.performed && !P2_READY)
		{
			global::UnityEngine.Vector2 vector = context.ReadValue<global::UnityEngine.Vector2>();
			global::UnityEngine.Vector2Int vector2Int = new global::UnityEngine.Vector2Int(-(int)vector.y, (int)vector.x);
			global::UnityEngine.Vector2Int vector2Int2 = ClampToGrid(cursor2 + vector2Int);
			if (vector2Int2 != cursor1)
			{
				cursor2 = vector2Int2;
				CharacterType p2SelectedChar = m_p2SelectedChar;
				m_p2SelectedChar = GetSelectedCharacter2();
				m_p2Cursor.transform.position = GetP2Pos();
				UpdateSelectedChar(isP1: false, p2SelectedChar);
			}
		}
	}

	public void SelectP2(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			P2_READY = true;
			m_playerReadyObjs[1].SetActive(value: true);
			global::DG.Tweening.TweenExtensions.Kill(m_splashColorTweensP2);
			m_splashColorTweensP2 = global::DG.Tweening.DOTweenModuleUI.DOColor(m_p2Splash.GetComponent<global::UnityEngine.UI.Image>(), m_readySplashColor, 0.1f);
			base.transform.GetComponent<AudioOnDemandPlayer>().PlayAudioOnDemandIndex(0);
			CheckForReady();
		}
	}

	public void DeselectP2(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (!P2_READY && GameManager.Instance.DefinedGameMode == GameModeType.LocalMp)
			{
				LocalMpCtrlManager.Instance.UnsubscribeToAllInputs();
				SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MatchmakeLocally);
			}
			P2_READY = false;
			m_playerReadyObjs[1].SetActive(value: false);
			global::DG.Tweening.TweenExtensions.Kill(m_splashColorTweensP2);
			m_splashColorTweensP2 = global::DG.Tweening.DOTweenModuleUI.DOColor(m_p2Splash.GetComponent<global::UnityEngine.UI.Image>(), global::UnityEngine.Color.white, 0.1f);
		}
	}

	public void SelectP1(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			P1_READY = true;
			m_playerReadyObjs[0].SetActive(value: true);
			global::DG.Tweening.TweenExtensions.Kill(m_splashColorTweensP1);
			m_splashColorTweensP1 = global::DG.Tweening.DOTweenModuleUI.DOColor(m_p1Splash.GetComponent<global::UnityEngine.UI.Image>(), m_readySplashColor, 0.1f);
			if (NetworkServerReciever.Instance != null)
			{
				NetworkServerReciever.Instance.SendReadyUpEventToOpponentRpc(global::Unity.Netcode.NetworkManager.Singleton.LocalClientId, isReady: true);
			}
			base.transform.GetComponent<AudioOnDemandPlayer>().PlayAudioOnDemandIndex(0);
			CheckForReady();
		}
	}

	public void DeselectP1(global::UnityEngine.InputSystem.InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (!P1_READY && GameManager.Instance.DefinedGameMode == GameModeType.LocalMp)
			{
				LocalMpCtrlManager.Instance.UnsubscribeToAllInputs();
				SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MatchmakeLocally);
			}
			if (!P1_READY && GameManager.Instance.DefinedGameMode == GameModeType.Marathon)
			{
				PersistentInputReader.Instance.ClearAllSubscriptions();
				GameManager.Instance.OutOfGamemodeDestroy();
				SceneLoader.Instance.LoadSceneByEnumRegularFadeNoWater(AllGameScenes.MarathonMenu);
			}
			P1_READY = false;
			m_playerReadyObjs[0].SetActive(value: false);
			global::DG.Tweening.TweenExtensions.Kill(m_splashColorTweensP1);
			m_splashColorTweensP1 = global::DG.Tweening.DOTweenModuleUI.DOColor(m_p1Splash.GetComponent<global::UnityEngine.UI.Image>(), global::UnityEngine.Color.white, 0.1f);
			if (NetworkServerReciever.Instance != null)
			{
				NetworkServerReciever.Instance.SendReadyUpEventToOpponentRpc(global::Unity.Netcode.NetworkManager.Singleton.LocalClientId, isReady: false);
			}
		}
	}

	public void CheckForReady()
	{
		if (ISMARATHON && P1_READY)
		{
			GameManager.Instance.LocallySelectedCharacter = m_p1SelectedChar;
			ALLREADYACTION?.Invoke();
			global::DG.Tweening.DOTweenModuleAudio.DOFade(AudioManager.Instance.MusicAS, 0f, 1f);
		}
		else if (P1_READY && P2_READY)
		{
			if (NetworkServerReciever.Instance != null && NetworkServerReciever.Instance.IsHost)
			{
				NetworkServerReciever.Instance.ClientCharacter.Value = (int)m_p2SelectedChar;
				NetworkServerReciever.Instance.HostCharacter.Value = (int)m_p1SelectedChar;
			}
			else
			{
				GameManager.Instance.LocallySelectedCharacter = m_p1SelectedChar;
				GameManager.Instance.LocallySelectedP2Character = m_p2SelectedChar;
			}
			ALLREADYACTION?.Invoke();
			global::DG.Tweening.DOTweenModuleAudio.DOFade(AudioManager.Instance.MusicAS, 0f, 1f);
		}
	}

	private void UpdateSelectedChar(bool isP1, CharacterType prevType = CharacterType.None)
	{
		if (isP1)
		{
			if (m_p1SelectedChar != prevType)
			{
				global::DG.Tweening.TweenExtensions.Kill(m_splashTweenP1);
				m_p1Splash.transform.position = m_p1SplashPos[0].position;
				m_splashTweenP1 = global::DG.Tweening.ShortcutExtensions.DOMove(m_p1Splash.transform, m_p1SplashPos[1].position, 0.166f);
				m_p1Splash.GetComponent<global::UnityEngine.UI.Image>().sprite = GetDataByType(m_p1SelectedChar).CharacterSplashSprite;
				if (m_p1SelectedChar == CharacterType.CoachColby)
				{
					m_p1CharNameText.text = "Coach Colby";
				}
				else
				{
					m_p1CharNameText.text = GetDataByType(m_p1SelectedChar).name;
				}
			}
		}
		else if (GameManager.Instance.DefinedGameMode != GameModeType.Marathon && m_p2SelectedChar != prevType)
		{
			global::DG.Tweening.TweenExtensions.Kill(m_splashTweenP2);
			m_p2Splash.transform.position = m_p2SplashPos[0].position;
			m_splashTweenP2 = global::DG.Tweening.ShortcutExtensions.DOMove(m_p2Splash.transform, m_p2SplashPos[1].position, 0.166f);
			m_p2Splash.GetComponent<global::UnityEngine.UI.Image>().sprite = GetDataByType(m_p2SelectedChar).CharacterSplashSprite;
			if (m_p2SelectedChar == CharacterType.CoachColby)
			{
				m_p2CharNameText.text = "Coach Colby";
			}
			else
			{
				m_p2CharNameText.text = GetDataByType(m_p2SelectedChar).name;
			}
		}
	}

	public bool AreCursorsOnSameCell()
	{
		return cursor1 == cursor2;
	}

	public global::UnityEngine.Vector3 GetP1Pos()
	{
		return characters[cursor1.x, cursor1.y].transform.position;
	}

	public global::UnityEngine.Vector3 GetP2Pos()
	{
		return characters[cursor2.x, cursor2.y].transform.position;
	}

	public CharacterType GetSelectedCharacter1()
	{
		return characters[cursor1.x, cursor1.y].GetComponent<SelectableChar>().CharType;
	}

	public CharacterType GetSelectedCharacter2()
	{
		return characters[cursor2.x, cursor2.y].GetComponent<SelectableChar>().CharType;
	}

	private global::UnityEngine.Vector2Int ClampToGrid(global::UnityEngine.Vector2Int pos)
	{
		pos.x = global::UnityEngine.Mathf.Clamp(pos.x, 0, rows - 1);
		pos.y = global::UnityEngine.Mathf.Clamp(pos.y, 0, cols - 1);
		return pos;
	}

	private void Start()
	{
		characters = new global::UnityEngine.GameObject[rows, cols];
		characters[0, 0] = m_selectableChars[0];
		characters[0, 1] = m_selectableChars[1];
		characters[0, 2] = m_selectableChars[2];
		characters[0, 3] = m_selectableChars[3];
		characters[1, 0] = m_selectableChars[4];
		characters[1, 1] = m_selectableChars[5];
		characters[1, 2] = m_selectableChars[6];
		characters[1, 3] = m_selectableChars[7];
		cursor1 = new global::UnityEngine.Vector2Int(0, 0);
		cursor2 = new global::UnityEngine.Vector2Int(0, 3);
		if (NetworkServerReciever.Instance != null)
		{
			cursor2 = new global::UnityEngine.Vector2Int(0, 0);
		}
		m_p1Cursor.GetComponent<global::UnityEngine.Animator>().SetBool("IsP1", value: true);
		m_p1Cursor.transform.GetChild(0).GetComponent<global::UnityEngine.Animator>().SetBool("IsP1", value: true);
		m_p1SelectedChar = GetSelectedCharacter1();
		m_p1Cursor.transform.position = GetP1Pos();
		if (GameManager.Instance.DefinedGameMode != GameModeType.Marathon)
		{
			m_p2Cursor.GetComponent<global::UnityEngine.Animator>().SetBool("IsP1", value: false);
			m_p2Cursor.transform.GetChild(0).GetComponent<global::UnityEngine.Animator>().SetBool("IsP1", value: false);
			m_p2SelectedChar = GetSelectedCharacter2();
			m_p2Cursor.transform.position = GetP2Pos();
		}
		UpdateSelectedChar(isP1: true);
		UpdateSelectedChar(isP1: false);
		if (m_backBtn != null)
		{
			m_backBtn.SetActive(value: false);
		}
	}

	public CharacterData GetDataByType(CharacterType type)
	{
		foreach (CharacterData allCharacterDatum in m_chardata.AllCharacterData)
		{
			if (allCharacterDatum.Type == type)
			{
				return allCharacterDatum;
			}
		}
		return m_chardata.AllCharacterData[0];
	}
}
