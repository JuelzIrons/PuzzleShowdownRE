public class GameLoop : global::UnityEngine.MonoBehaviour
{
	private static global::System.Collections.Generic.Dictionary<int, int> ComboExtraScore = new global::System.Collections.Generic.Dictionary<int, int>
	{
		{ 4, 30 },
		{ 5, 50 },
		{ 6, 150 },
		{ 7, 190 },
		{ 8, 230 },
		{ 9, 270 },
		{ 10, 310 },
		{ 11, 400 },
		{ 12, 450 },
		{ 13, 500 },
		{ 14, 550 },
		{ 15, 700 },
		{ 16, 760 },
		{ 17, 850 },
		{ 18, 970 },
		{ 19, 1120 },
		{ 20, 1300 },
		{ 21, 1510 },
		{ 22, 1750 },
		{ 23, 2020 },
		{ 24, 2320 },
		{ 25, 2650 },
		{ 26, 3010 },
		{ 27, 3400 },
		{ 28, 3820 },
		{ 29, 4270 },
		{ 30, 4750 },
		{ 31, 5260 },
		{ 32, 15000 },
		{ 33, 15570 },
		{ 34, 16170 },
		{ 35, 16800 },
		{ 36, 17460 },
		{ 37, 18150 },
		{ 38, 18870 },
		{ 39, 19620 },
		{ 40, 20400 }
	};

	private static global::System.Collections.Generic.Dictionary<int, int> ChainExtraScore = new global::System.Collections.Generic.Dictionary<int, int>
	{
		{ 1, 0 },
		{ 2, 50 },
		{ 3, 80 },
		{ 4, 150 },
		{ 5, 300 },
		{ 6, 400 },
		{ 7, 500 },
		{ 8, 700 },
		{ 9, 900 },
		{ 10, 1100 },
		{ 11, 1300 },
		{ 12, 1500 },
		{ 13, 1800 },
		{ 14, 1800 }
	};

	private static global::System.Collections.Generic.Dictionary<int, float> FreezeTimesForCombos = new global::System.Collections.Generic.Dictionary<int, float>
	{
		{ 4, 0.8f },
		{ 5, 1.033f },
		{ 6, 1.266f },
		{ 7, 1.5f },
		{ 8, 1.733f },
		{ 9, 1.966f },
		{ 10, 2.2f },
		{ 11, 2.433f },
		{ 12, 2.66f }
	};

	private static global::System.Collections.Generic.Dictionary<int, float> FreezeTimesForChain = new global::System.Collections.Generic.Dictionary<int, float>
	{
		{ 2, 1.65f },
		{ 3, 1.88f },
		{ 4, 2.12f },
		{ 5, 2.35f },
		{ 6, 2.58f },
		{ 7, 2.81f },
		{ 8, 3.05f },
		{ 9, 3.28f },
		{ 10, 3.51f },
		{ 11, 3.75f },
		{ 12, 3.98f },
		{ 13, 4.21f }
	};

	public PodManager MyPodManager;

	public global::UnityEngine.GameObject ChainCounterEffect;

	public global::UnityEngine.GameObject ComboCounterEffect;

	public VoiceBoxSO VoiceBox;

	public int SpeedLv;

	public float OffsetValue;

	public bool Freeze;

	public bool Clearing;

	public bool ShouldAnythingFall;

	public bool HandlingFall;

	public global::UnityEngine.Events.UnityEvent Rise;

	public bool Lost;

	public bool PostDeathEvaluation;

	public bool HasLossChecked;

	public float TimePassed;

	public int Score;

	public global::System.Collections.Generic.List<int> SlottedChainCounter = new global::System.Collections.Generic.List<int>(16);

	public global::System.Collections.Generic.List<int> SlottedChainBlockCounter = new global::System.Collections.Generic.List<int>(16);

	public global::System.Collections.Generic.List<ListWrapper<int>> SlottedComboCounter = new global::System.Collections.Generic.List<ListWrapper<int>>(16);

	[global::UnityEngine.HideInInspector]
	public int[] ComboCounters = new int[51];

	[global::UnityEngine.HideInInspector]
	public int[] ChainCounters = new int[51];

	public bool ShowDebugText;

	public int DanceSyncIndex;

	public int GarbageOverlaySyncIndex;

	private bool shouldSlowRise;

	public int ScoreForMaxLvl;

	public bool IsPaused;

	public bool IsAnythingDancing;

	public bool IsAnythingTopRow;

	[global::UnityEngine.SerializeField]
	public CursorController m_cursor;

	[global::UnityEngine.SerializeField]
	private bool m_classicMatching;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationCurve m_speedLevelCurve;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationCurve m_riseCurve;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animation m_lightAnim;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.Animation m_blockShake;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationClip m_lightDimmerBig;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AnimationClip m_lightDimmerLight;

	public global::UnityEngine.GameObject[] m_starSparkle;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_starSwirlChain;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_starSwirlCombo;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_blockLite;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip DropClip;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip DeathConk;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip[] CombosSound;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip ChainSound;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip m_wooshLight;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.AudioClip m_wooshHeavy;

	public global::UnityEngine.Animator PodAnim;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_scoreMenu;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_onlineRematchBtnHost;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_rematchButton;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_rematchDescClient;

	[global::UnityEngine.SerializeField]
	private bool m_noPreplacedBlocks;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.SpriteRenderer m_lineClearline;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_lineClearObj;

	public global::UnityEngine.SpriteRenderer GameOverLogo;

	public global::UnityEngine.SpriteRenderer LoseLogo;

	public global::UnityEngine.GameObject m_pressAnyKeyToContinueObj;

	public global::UnityEngine.SpriteRenderer WinLogo;

	public global::UnityEngine.GameObject LineClearWinLogo;

	public global::UnityEngine.GameObject LineClearLossLogo;

	[global::UnityEngine.SerializeField]
	private global::UnityEngine.GameObject m_winSparksObj;

	public bool m_isForcingUp;

	public bool HoldingForceButton;

	private global::System.Collections.Generic.List<global::System.Collections.Generic.List<SquareComponent>> matches = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<SquareComponent>>();

	private bool m_canRise;

	private int m_smoothRiseCounter;

	public float m_totalBonusFreeze;

	public float UnfreezeTimestamp;

	private float m_unclearTimeStamp;

	public bool shouldKill;

	private bool prevShouldKill;

	public int GameOverTS = -1;

	private int LineClearWinFrameCounter;

	public static readonly int GRACETIME = 70;

	public static readonly int GRACETIMEMARATHON = 6;

	private int m_startingSpeedLv;

	private float m_hasClearedTS = -1f;

	[global::UnityEngine.SerializeField]
	private bool m_allowOneRaiseAfterClear;

	public global::System.Collections.Generic.List<global::UnityEngine.Vector2> ShouldLandAllPositions = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();

	public global::System.Collections.Generic.List<global::UnityEngine.Vector2> ShouldTsConnectedBlocksPositions = new global::System.Collections.Generic.List<global::UnityEngine.Vector2>();

	public global::System.Collections.Generic.HashSet<SquareComponent> ShouldClearChainIds = new global::System.Collections.Generic.HashSet<SquareComponent>();

	public int GameLoopFrameCounter;

	public int HangingFrames = 10;

	public int SwappingFrames = 4;

	public int LineClearWinConLevels = -1;

	private bool m_hasCalledTimeout;

	private float m_forcingUpTimer;

	private global::System.Collections.Generic.Dictionary<int, int> m_lastVoiceLineIndex = new global::System.Collections.Generic.Dictionary<int, int>();

	private const int HIGH_CHAIN_THRESHOLD = 7;

	private const int HIGH_CHAIN_POOL_KEY = -1;

	[global::UnityEngine.SerializeField]
	private AudioOnDemandPlayer m_lineClearDemandPlayerPopBlocks;

	public static int GetComboExtraScore(int comboLen)
	{
		if (comboLen > 40)
		{
			return ComboExtraScore[comboLen] + (comboLen - 40) * 800;
		}
		return ComboExtraScore[comboLen];
	}

	public static int GetExtraChainScore(int chainAmount)
	{
		if (chainAmount == 1)
		{
			return 0;
		}
		if (chainAmount >= 14)
		{
			return 1800;
		}
		return ChainExtraScore[chainAmount];
	}

	public float GetExtraComboTime(int comboSize)
	{
		if (comboSize <= 3)
		{
			return 0f;
		}
		return FreezeTimesForCombos[global::UnityEngine.Mathf.Clamp(comboSize, 4, 12)];
	}

	public float GetExtraChainTime(int chainLen)
	{
		return FreezeTimesForChain[global::UnityEngine.Mathf.Clamp(chainLen, 2, 13)];
	}

	private void Awake()
	{
		StartCoroutine(DanceSyncerLoop());
		StartCoroutine(GbgOverlaySyncerLoop());
	}

	public void Setup()
	{
		global::UnityEngine.Debug.Log("called setup in gameloop");
		GameLoopFrameCounter = 0;
		MyPodManager.GridManager.BakeGrid();
		MyPodManager.GridManager.SpawnNewRow();
		OffsetValue = 0f;
		m_smoothRiseCounter = 0;
		TimePassed = 0f;
		AudioManager.Instance.SetMusicSpeed(1f);
		if (!MyPodManager.ISScenarioCreator)
		{
			switch (GameManager.Instance.DefinedGameMode)
			{
			case GameModeType.Campaign:
				SpeedLv = GameManager.Instance.SelectedSpeedLevel;
				SetHangingFramesFromDiff(GameManager.Instance.SelectedDifficulty);
				m_startingSpeedLv = SpeedLv;
				if (!m_noPreplacedBlocks)
				{
					MyPodManager.GridManager.SpawnPrePlacedBlocks(7);
				}
				break;
			case GameModeType.Marathon:
				SpeedLv = GameManager.Instance.SelectedSpeedLevel;
				SetHangingFramesFromDiff(GameManager.Instance.SelectedDifficulty);
				m_startingSpeedLv = SpeedLv;
				if (!m_noPreplacedBlocks)
				{
					MyPodManager.GridManager.SpawnPrePlacedBlocks(7);
				}
				break;
			case GameModeType.LineClear:
				SpeedLv = GameManager.Instance.SelectedSpeedLevel;
				SetHangingFramesFromDiff(GameManager.Instance.SelectedDifficulty);
				m_startingSpeedLv = SpeedLv;
				LineClearWinConLevels = GameManager.Instance.SelectedDepthLevel;
				MyPodManager.GridManager.SpawnPrePlacedBlocks(9);
				m_lineClearline.enabled = true;
				m_lineClearObj.SetActive(value: true);
				break;
			case GameModeType.LocalMp:
				SpeedLv = GameManager.Instance.SelectedSpeedLevel;
				SetHangingFramesFromDiff(GameManager.Instance.SelectedDifficulty);
				m_startingSpeedLv = SpeedLv;
				if (!m_noPreplacedBlocks)
				{
					MyPodManager.GridManager.SpawnPrePlacedBlocks(7);
				}
				break;
			case GameModeType.Online:
				SpeedLv = 1;
				MyPodManager.GridManager.SpawnPrePlacedBlocks(7);
				break;
			}
		}
		MyPodManager.GridManager.TimeTickAllBlocks();
		MyPodManager.CursorController.enabled = false;
		Score = 0;
	}

	private void SetCPULevelFromDiff(DifficultyType type)
	{
		switch (type)
		{
		case DifficultyType.Easy:
			MyPodManager.CPUAct.CPULevel = 1;
			break;
		case DifficultyType.Medium:
			MyPodManager.CPUAct.CPULevel = 2;
			break;
		case DifficultyType.Hard:
			MyPodManager.CPUAct.CPULevel = 3;
			break;
		case DifficultyType.Harder:
			MyPodManager.CPUAct.CPULevel = 4;
			break;
		case DifficultyType.Hardest:
			MyPodManager.CPUAct.CPULevel = 5;
			break;
		}
	}

	private void SetHangingFramesFromDiff(DifficultyType type)
	{
		switch (type)
		{
		case DifficultyType.Easy:
			HangingFrames = 11;
			break;
		case DifficultyType.Medium:
			HangingFrames = 8;
			break;
		case DifficultyType.Hard:
			HangingFrames = 5;
			break;
		case DifficultyType.Harder:
			HangingFrames = 5;
			break;
		case DifficultyType.Hardest:
			HangingFrames = 5;
			break;
		}
	}

	private void Update()
	{
		if (!(MyPodManager == null) && MyPodManager.IsControlledByCPU)
		{
			if (m_isForcingUp)
			{
				m_forcingUpTimer += global::UnityEngine.Time.deltaTime;
			}
			else
			{
				m_forcingUpTimer = 0f;
			}
		}
	}

	private void FixedUpdate()
	{
		if (MyPodManager == null || !MyPodManager.HasStarted)
		{
			return;
		}
		if (MyPodManager.IsOnlineMp)
		{
			NetworkServerReciever instance = NetworkServerReciever.Instance;
			if ((object)instance != null && !instance.HasLoadedInLocally)
			{
				return;
			}
			NetworkServerReciever instance2 = NetworkServerReciever.Instance;
			if ((object)instance2 == null || instance2.NetworkReadyCounter.Value != 4)
			{
				return;
			}
			if (GameLoopFrameCounter == 1)
			{
				NetworkServerReciever.Instance.ServerInit();
			}
			GameLoopFrameCounter++;
			if (GameLoopFrameCounter < 240 && !MyPodManager.ISScenarioCreator)
			{
				MyPodManager.HandleCountDown(GameLoopFrameCounter);
			}
			else
			{
				if (GameLoopFrameCounter < 240 && !MyPodManager.ISScenarioCreator)
				{
					return;
				}
				if (MyPodManager.IsOnlineMp && MyPodManager.ISPLAYER1)
				{
					NetworkServerReciever.Instance.RecieverFixedUpdate();
				}
				if (MyPodManager.TheOneThatExecutesInputSystemUpdates)
				{
					global::UnityEngine.InputSystem.InputSystem.Update();
				}
				MyPodManager.CursorController.MoveContinously();
				if (MyPodManager.IsOnlineMp && MyPodManager.ISPLAYER1)
				{
					if (global::Unity.Netcode.NetworkManager.Singleton.IsHost)
					{
						NetworkServerReciever.Instance.LocalOpponentPodManager.GameLoop.LocalFixedUpdate();
						LocalFixedUpdate();
					}
					else
					{
						LocalFixedUpdate();
						NetworkServerReciever.Instance.LocalOpponentPodManager.GameLoop.LocalFixedUpdate();
					}
				}
			}
			return;
		}
		GameLoopFrameCounter++;
		if (GameLoopFrameCounter < 240 && !MyPodManager.ISScenarioCreator)
		{
			MyPodManager.HandleCountDown(GameLoopFrameCounter);
			return;
		}
		if (MyPodManager.IsControlledByCPU)
		{
			MyPodManager.CPUAct.ImprovedActionLoop();
		}
		if (MyPodManager.TheOneThatExecutesInputSystemUpdates)
		{
			global::UnityEngine.InputSystem.InputSystem.Update();
		}
		if (MyPodManager.DebugInputReader != null)
		{
			MyPodManager.DebugInputReader.ReadDebugInput(GameLoopFrameCounter);
		}
		if (!Lost || !HasLossChecked)
		{
			MyPodManager.CursorController.MoveContinously();
			LocalFixedUpdate();
		}
	}

	public void LocalFixedUpdate()
	{
		if (MyPodManager.IsOnlineMp && !MyPodManager.ISPLAYER1 && (NetworkServerReciever.Instance.LocalTS < NetworkServerReciever.Instance.MOVE_LATENCY || PostDeathEvaluation))
		{
			return;
		}
		SmoothMoveLogic();
		m_cursor.UseOffset();
		if (GameManager.Instance != null && GameManager.Instance.DefinedGameMode == GameModeType.LineClear)
		{
			m_lineClearline.transform.position = new global::UnityEngine.Vector2(m_lineClearline.transform.position.x, (float)LineClearWinConLevels - 5.5f) + new global::UnityEngine.Vector2(0f, MyPodManager.GameLoop.OffsetValue);
		}
		if (CheckForLineClearWin())
		{
			if (Lost)
			{
				return;
			}
			if (LineClearWinFrameCounter == 2)
			{
				HasLossChecked = true;
				Lost = true;
				EndGame(isWinner: true);
				return;
			}
		}
		else
		{
			LineClearWinFrameCounter = 0;
		}
		CheckForLoss();
		if (!MyPodManager.ISPLAYER1 && NetworkServerReciever.Instance != null && NetworkServerReciever.Instance.LocalOpponentHasDied)
		{
			Lost = true;
		}
		ShouldAnythingFall = MyPodManager.GridManager.ShouldAnythingFall();
		if (shouldKill && GameOverTS == -1 && !Freeze && !Clearing && !ShouldAnythingFall && !MyPodManager.GridManager.IsAnythingFalling())
		{
			if (MyPodManager.IsVersus)
			{
				GameOverTS = GameLoopFrameCounter + GRACETIME;
			}
			else
			{
				GameOverTS = GameLoopFrameCounter + GRACETIMEMARATHON;
			}
		}
		else if (shouldKill != prevShouldKill)
		{
			if (!shouldKill)
			{
				GameOverTS = -1;
			}
		}
		else if (GameOverTS > -1 && (Freeze || Clearing || ShouldAnythingFall || MyPodManager.GridManager.IsAnythingFalling()))
		{
			if (MyPodManager.IsVersus)
			{
				GameOverTS = GameLoopFrameCounter + GRACETIME;
			}
			else
			{
				GameOverTS = GameLoopFrameCounter + GRACETIMEMARATHON;
			}
		}
		if (shouldKill && GameOverTS > 0 && GameLoopFrameCounter >= GameOverTS && !Freeze && !Clearing)
		{
			CheckTopLayer();
			if (shouldKill && !MyPodManager.GridManager.IsAnythingFalling() && !MyPodManager.GridManager.ShouldAnythingFall() && !Clearing)
			{
				if (MyPodManager.IsOnlineMp)
				{
					if (!MyPodManager.ISPLAYER1)
					{
						if (NetworkServerReciever.Instance.LocalOpponentHasDied && !NetworkServerReciever.Instance.DRAW.Value)
						{
							Lost = true;
						}
					}
					else
					{
						CallMpLoss();
					}
				}
				else
				{
					Lost = true;
				}
			}
		}
		if (!Lost || MyPodManager.GameLoop.PostDeathEvaluation)
		{
			TimePassed += 1f / 60f;
		}
		if (!MyPodManager.ISScenarioCreator && GameManager.Instance.DefinedGameMode == GameModeType.LocalMp && TimePassed >= 600f && !Lost && !m_hasCalledTimeout)
		{
			ExtraElementsManager.Instance.TimeBuzzer?.Play();
			Lost = true;
		}
		if (m_totalBonusFreeze > 0f && !Clearing)
		{
			m_totalBonusFreeze -= 1f / 60f;
			Freeze = true;
		}
		if (m_totalBonusFreeze <= 0f)
		{
			Freeze = false;
		}
		prevShouldKill = shouldKill;
		GameLoopTick();
	}

	public void CallMpLoss()
	{
		if (PostDeathEvaluation)
		{
			return;
		}
		NetworkServerReciever instance = NetworkServerReciever.Instance;
		uint localTS = NetworkServerReciever.Instance.LocalTS;
		ulong localClientId = global::Unity.Netcode.NetworkManager.Singleton.LocalClientId;
		int moveOrderNumberThisFrame = MyPodManager.CursorController.moveOrderNumberThisFrame;
		int score = Score;
		instance.SendMoveToServerRpc(localTS, 3, localClientId, moveOrderNumberThisFrame, default(global::UnityEngine.Vector2), score);
		MyPodManager.CursorController.moveOrderNumberThisFrame++;
		PostDeathEvaluation = true;
		
		{
			PostDeathEvaluation = false;
			if (!NetworkServerReciever.Instance.DRAW.Value && !NetworkServerReciever.Instance.LocalOpponentHasDied)
			{
				Lost = true;
				ulong clientId = global::System.Linq.Enumerable.First(global::Unity.Netcode.NetworkManager.Singleton.ConnectedClientsList, (global::Unity.Netcode.NetworkClient c) => c.ClientId == 0).ClientId;
				ulong clientId2 = global::System.Linq.Enumerable.First(global::Unity.Netcode.NetworkManager.Singleton.ConnectedClientsList, (global::Unity.Netcode.NetworkClient c) => c.ClientId != 0).ClientId;
				if (clientId != global::Unity.Netcode.NetworkManager.Singleton.LocalClientId)
				{
					NetworkServerReciever.Instance.BroadcastTimeoutResultRpc(clientId);
				}
				else
				{
					NetworkServerReciever.Instance.BroadcastTimeoutResultRpc(clientId2);
				}
			}
		}), 
	}

	private bool CheckForLineClearWin()
	{
		if (Clearing || ShouldAnythingFall)
		{
			return false;
		}
		if (MyPodManager.ISScenarioCreator)
		{
			return false;
		}
		if (GameManager.Instance.DefinedGameMode != GameModeType.LineClear || HandlingFall || MyPodManager.GridManager.ShouldAnythingFall())
		{
			return false;
		}
		if (LineClearWinConLevels < 0)
		{
			return false;
		}
		for (int i = 0; i < 6; i++)
		{
			for (int j = LineClearWinConLevels; j < 11; j++)
			{
				if (MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(i, j), out var _))
				{
					return false;
				}
			}
		}
		LineClearWinFrameCounter++;
		return true;
	}

	public void SqueezeAllBlocks()
	{
		if (shouldKill)
		{
			return;
		}
		foreach (SquareComponent aliveSquare in MyPodManager.GridManager.AliveSquares)
		{
			if (!aliveSquare.IsGarbage && !aliveSquare.IsBeingDestroyed && !aliveSquare.IsFalling && !aliveSquare.IsLanding)
			{
				aliveSquare.SetDownSqueeze(isSqueeze: true);
			}
		}
	}

	public void UnSqueezeAllBlocks()
	{
		if (!shouldKill)
		{
			return;
		}
		foreach (SquareComponent aliveSquare in MyPodManager.GridManager.AliveSquares)
		{
			if (!aliveSquare.IsGarbage && !aliveSquare.IsBeingDestroyed && !aliveSquare.IsFalling && !aliveSquare.IsLanding)
			{
				aliveSquare.SetDownSqueeze();
			}
		}
	}

	public void GameLoopTick()
	{
		if (HasLossChecked)
		{
			return;
		}
		MyPodManager.GridManager.AliveSquares = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.ThenBy(global::System.Linq.Enumerable.OrderBy(MyPodManager.GridManager.AliveSquares, (SquareComponent b) => b.Coords.y), (SquareComponent b) => b.Coords.x));
		MyPodManager.GridManager.TimeTickAllBlocks();
		MyPodManager.GridManager.FallTickAllBlocks();
		ClearTimeTicker();
		shouldSlowRise = false;
		if (!Clearing && !shouldKill && !ShouldAnythingFall)
		{
			ManualMoveUp();
		}
		if (!Freeze && !ShouldAnythingFall && !Clearing)
		{
			if (!m_isForcingUp && !shouldKill)
			{
				MoveGridUp();
			}
			CheckForDifficultyIncrease();
		}
		if (!Clearing && !shouldKill && !ShouldAnythingFall && !MyPodManager.GridManager.IsAnythingFalling())
		{
			MoveLogic();
		}
		else
		{
			m_canRise = false;
		}
		CheckTopLayer();
		DetectDancing();
		DetectMusicSpeedUp();
		if (MyPodManager.ISPLAYER1)
		{
			if (MyPodManager.IsVersus && (IsAnythingTopRow || MyPodManager.OpponentManager.GameLoop.IsAnythingTopRow))
			{
				AudioManager.Instance.FadeMusicSpeed(1.18f, 1.2f);
			}
			else if (MyPodManager.IsVersus && !IsAnythingTopRow && !MyPodManager.OpponentManager.GameLoop.IsAnythingTopRow)
			{
				AudioManager.Instance.FadeMusicSpeed(1f, 0.4f);
			}
			if (!MyPodManager.IsVersus && IsAnythingTopRow)
			{
				AudioManager.Instance.FadeMusicSpeed(1.18f, 1.2f);
			}
			else if (!MyPodManager.IsVersus && !IsAnythingTopRow)
			{
				AudioManager.Instance.FadeMusicSpeed(1f, 0.4f);
			}
		}
		CheckForMatches();
		MyPodManager.CursorController.hasSwappedThisFrame = false;
		MyPodManager.CursorController.moveOrderNumberThisFrame = 0;
		MyPodManager.GridManager.CheckSendGarbageQueue();
		if (ShouldClearChainIds.Count > 0)
		{
			MyPodManager.GridManager.ClearChainTicks();
			ShouldClearChainIds.Clear();
		}
		ClearCheckForMatches();
		if (ShouldLandAllPositions.Count > 0)
		{
			MyPodManager.GridManager.LandTicks();
			ShouldLandAllPositions.Clear();
		}
		if (ShouldTsConnectedBlocksPositions.Count > 0)
		{
			MyPodManager.GridManager.TsAllConnectedBlocksAboveTick();
			ShouldTsConnectedBlocksPositions.Clear();
		}
	}

	private void SmoothMoveLogic()
	{
		if (!m_canRise || ShouldAnythingFall || MyPodManager.GridManager.IsAnythingFalling() || Clearing || (!shouldSlowRise && !m_isForcingUp))
		{
			return;
		}
		int num = global::UnityEngine.Mathf.RoundToInt(16f / m_speedLevelCurve.Evaluate(SpeedLv) * 100f);
		int num2 = global::UnityEngine.Mathf.RoundToInt(5333.333f);
		bool flag = m_isForcingUp && (m_allowOneRaiseAfterClear || m_hasClearedTS <= (float)GameLoopFrameCounter);
		m_smoothRiseCounter += (flag ? num2 : num);
		if (m_smoothRiseCounter >= 96000)
		{
			m_smoothRiseCounter = 0;
			m_isForcingUp = false;
			m_allowOneRaiseAfterClear = false;
			if (!ShouldAnythingFall && !MyPodManager.GridManager.IsAnythingFalling() && !Clearing)
			{
				Rise.Invoke();
				if (GameManager.Instance.DefinedGameMode == GameModeType.LineClear)
				{
					LineClearWinConLevels++;
				}
				Score++;
				MyPodManager.GridManager.SpawnNewRow();
				m_smoothRiseCounter = 0;
			}
			else
			{
				global::UnityEngine.Debug.LogError("NO RISE ALLOWED BUT REACHED THIS?!");
			}
		}
		m_smoothRiseCounter %= 96000;
		OffsetValue = m_riseCurve.Evaluate((float)m_smoothRiseCounter / 96000f);
	}

	private void MoveLogic()
	{
		m_canRise = true;
	}

	public void SubtractChainBlockCount(int chainID)
	{
		SlottedChainBlockCounter[chainID]--;
		global::UnityEngine.Debug.Log($"Subtracting 1 from {chainID}");
		if (SlottedChainBlockCounter[chainID] != 0)
		{
			return;
		}
		if (MyPodManager.IsVersus)
		{
			if (MyPodManager.IsOnlineMp && MyPodManager.ISPLAYER1)
			{
				int height = SlottedChainCounter[chainID];
				if (NetworkServerReciever.Instance.IsHost)
				{
					MyPodManager.OpponentManager.GridManager.ReceiveGarbageInstruction(6, height, (float)NetworkServerReciever.Instance.LocalTS + (float)NetworkServerReciever.Instance.MOVE_LATENCY * 2f + 1f);
					MyPodManager.OpponentManager.VisualGarbageQueue.RemoveItemFromQueue(chainID, -1);
				}
				else
				{
					MyPodManager.OpponentManager.GridManager.ReceiveGarbageInstruction(6, height, (float)NetworkServerReciever.Instance.LocalTS + (float)NetworkServerReciever.Instance.MOVE_LATENCY * 2f);
					MyPodManager.OpponentManager.VisualGarbageQueue.RemoveItemFromQueue(chainID, -1);
				}
			}
			else if (MyPodManager.IsOnlineMp && !MyPodManager.ISPLAYER1)
			{
				int height2 = SlottedChainCounter[chainID];
				MyPodManager.OpponentManager.GridManager.ReceiveGarbageInstruction(6, height2, NetworkServerReciever.Instance.LocalTS);
				MyPodManager.OpponentManager.VisualGarbageQueue.RemoveItemFromQueue(chainID, -1);
			}
			else
			{
				MyPodManager.OpponentManager.GridManager.ReceiveGarbageInstruction(6, SlottedChainCounter[chainID], GameLoopFrameCounter);
				MyPodManager.OpponentManager.VisualGarbageQueue.RemoveItemFromQueue(chainID, -1);
			}
			if (SlottedComboCounter != null && global::System.Linq.Enumerable.Count(SlottedComboCounter[chainID].list) > 0)
			{
				foreach (int item in SlottedComboCounter[chainID].list)
				{
					if (MyPodManager.IsOnlineMp && MyPodManager.ISPLAYER1)
					{
						if (NetworkServerReciever.Instance.IsHost)
						{
							MyPodManager.OpponentManager.GridManager.ReceiveGarbageInstruction(global::UnityEngine.Mathf.Clamp(item - 1, 3, 5), 1, (float)NetworkServerReciever.Instance.LocalTS + (float)NetworkServerReciever.Instance.MOVE_LATENCY * 2f + 1f);
							MyPodManager.OpponentManager.VisualGarbageQueue.RemoveItemFromQueue(-1, item);
						}
						else
						{
							MyPodManager.OpponentManager.GridManager.ReceiveGarbageInstruction(global::UnityEngine.Mathf.Clamp(item - 1, 3, 5), 1, (float)NetworkServerReciever.Instance.LocalTS + (float)NetworkServerReciever.Instance.MOVE_LATENCY * 2f);
							MyPodManager.OpponentManager.VisualGarbageQueue.RemoveItemFromQueue(-1, item);
						}
					}
					else if (MyPodManager.IsOnlineMp && !MyPodManager.ISPLAYER1)
					{
						MyPodManager.OpponentManager.GridManager.ReceiveGarbageInstruction(global::UnityEngine.Mathf.Clamp(item - 1, 3, 5), 1, NetworkServerReciever.Instance.LocalTS);
						MyPodManager.OpponentManager.VisualGarbageQueue.RemoveItemFromQueue(-1, item);
					}
					else
					{
						MyPodManager.OpponentManager.GridManager.ReceiveGarbageInstruction(global::UnityEngine.Mathf.Clamp(item - 1, 3, 5), 1, GameLoopFrameCounter);
						MyPodManager.OpponentManager.VisualGarbageQueue.RemoveItemFromQueue(-1, item);
					}
				}
			}
		}
		else
		{
			int num = SlottedChainCounter[chainID];
			if (num >= ChainCounters.Length)
			{
				global::System.Array.Resize(ref ChainCounters, num + 1);
			}
			ChainCounters[num]++;
			global::UnityEngine.Debug.Log($"{SlottedChainCounter[chainID] + 1} chain");
		}
		SlottedChainCounter[chainID] = 0;
		SlottedComboCounter[chainID].list.Clear();
	}

	public void CheckTopLayer()
	{
		for (int i = 0; i < 6; i++)
		{
			if (MyPodManager.GridManager.TryGetSquareAtTile(new global::UnityEngine.Vector2(i, 10f), out var _))
			{
				MyPodManager.GameLoop.m_isForcingUp = false;
				MyPodManager.GameLoop.SqueezeAllBlocks();
				shouldKill = true;
				return;
			}
		}
		UnSqueezeAllBlocks();
		shouldKill = false;
	}

	private void CheckForDifficultyIncrease()
	{
		if (!MyPodManager.IsVersus)
		{
			int value = global::UnityEngine.Mathf.FloorToInt((float)Score / (float)ScoreForMaxLvl * 50f);
			SpeedLv = global::UnityEngine.Mathf.Clamp(value, m_startingSpeedLv, 50);
		}
	}

	private void DetectDancing()
	{
		if (Lost || MyPodManager.GameLoop.PostDeathEvaluation)
		{
			return;
		}
		IsAnythingDancing = false;
		for (int i = 0; i < 6; i++)
		{
			SquareComponent topSquareInRow = MyPodManager.GridManager.GetTopSquareInRow(i);
			if (topSquareInRow != null && topSquareInRow.Coords.y > 7f)
			{
				if (!MyPodManager.GridManager.GetAllSquaresInCollumn(i, out var list))
				{
					continue;
				}
				foreach (SquareComponent item in list)
				{
					if (item.IsBeingDestroyed)
					{
						item.SetDance(shouldDance: false);
						continue;
					}
					IsAnythingDancing = true;
					item.SetDance(shouldDance: true);
				}
			}
			else
			{
				if (!MyPodManager.GridManager.GetAllSquaresInCollumn(i, out var list2))
				{
					continue;
				}
				foreach (SquareComponent item2 in list2)
				{
					item2.SetDance(shouldDance: false);
				}
			}
		}
	}

	private void DetectMusicSpeedUp()
	{
		if (Lost || MyPodManager.GameLoop.PostDeathEvaluation)
		{
			return;
		}
		IsAnythingTopRow = false;
		for (int i = 0; i < 6; i++)
		{
			SquareComponent topSquareInRow = MyPodManager.GridManager.GetTopSquareInRow(i);
			if (!(topSquareInRow != null) || !(topSquareInRow.Coords.y > 8f) || !MyPodManager.GridManager.GetAllSquaresInCollumn(i, out var list))
			{
				continue;
			}
			foreach (SquareComponent item in list)
			{
				if (!item.IsBeingDestroyed)
				{
					IsAnythingTopRow = true;
				}
			}
		}
	}

	private void ClearCheckForMatches()
	{
		for (int i = 0; i < MyPodManager.GridManager.AliveSquares.Count; i++)
		{
			MyPodManager.GridManager.AliveSquares[i].ShouldCheckForMatches = false;
		}
	}

	public void TryCancelUpForce()
	{
		if (!HoldingForceButton)
		{
			m_isForcingUp = false;
		}
	}

	private void ManualMoveUp()
	{
		if (MyPodManager.IsOnlineMp && !MyPodManager.ISPLAYER1)
		{
			HoldingForceButton = NetworkServerReciever.Instance.ForceTrueThisFrame;
		}
		if (HoldingForceButton)
		{
			if (MyPodManager.IsOnlineMp && MyPodManager.ISPLAYER1)
			{
				NetworkServerReciever.Instance.SendMoveToServerRpc(NetworkServerReciever.Instance.LocalTS, 2, global::Unity.Netcode.NetworkManager.Singleton.LocalClientId, MyPodManager.CursorController.moveOrderNumberThisFrame);
				MyPodManager.CursorController.moveOrderNumberThisFrame++;
			}
			m_isForcingUp = true;
			if (!Clearing)
			{
				m_totalBonusFreeze = 0f;
				Freeze = false;
			}
		}
	}

	private void MoveGridUp()
	{
		shouldSlowRise = true;
	}

	private void CheckForMatches()
	{
		matches.Clear();
		for (int i = 0; i < MyPodManager.GridManager.AliveSquares.Count; i++)
		{
			SquareComponent squareComponent = MyPodManager.GridManager.AliveSquares[i];
			if (!squareComponent.IsGarbage && squareComponent.ShouldCheckForMatches && !squareComponent.ShouldBeDestroyed && !squareComponent.IsFalling && !squareComponent.IsSwapping && squareComponent.IsMatchable)
			{
				if (MyPodManager.GridManager.AliveSquares[i].CheckForHorizontalMatches(out var horizontalMatchingBlocks))
				{
					matches.Add(horizontalMatchingBlocks);
				}
				if (MyPodManager.GridManager.AliveSquares[i].CheckForVerticalMatches(out var verticalMatchingBlocks))
				{
					matches.Add(verticalMatchingBlocks);
				}
			}
		}
		global::System.Collections.Generic.List<global::System.Collections.Generic.List<SquareComponent>> list = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<SquareComponent>>();
		foreach (global::System.Collections.Generic.List<SquareComponent> match in matches)
		{
			foreach (global::System.Collections.Generic.List<SquareComponent> match2 in matches)
			{
				if (match != match2 && global::System.Linq.Enumerable.Any(global::System.Linq.Enumerable.Intersect(match, match2)) && !list.Contains(match))
				{
					match.AddRange(match2);
					list.Add(match2);
				}
			}
		}
		foreach (global::System.Collections.Generic.List<SquareComponent> item in list)
		{
			matches.Remove(item);
		}
		list.Clear();
		foreach (global::System.Collections.Generic.List<SquareComponent> match3 in matches)
		{
			foreach (global::System.Collections.Generic.List<SquareComponent> match4 in matches)
			{
				if (global::System.Linq.Enumerable.All(match3, match4.Contains) && match3.Count == match4.Count && !list.Contains(match3) && !list.Contains(match4) && match3 != match4)
				{
					list.Add(match4);
				}
			}
		}
		foreach (global::System.Collections.Generic.List<SquareComponent> item2 in list)
		{
			matches.Remove(item2);
		}
		foreach (global::System.Collections.Generic.List<SquareComponent> match5 in matches)
		{
			global::System.Collections.Generic.List<SquareComponent> collection = global::System.Linq.Enumerable.ToList(global::System.Linq.Enumerable.Distinct(match5));
			match5.Clear();
			match5.AddRange(collection);
		}
		int num = 0;
		global::System.Collections.Generic.List<SquareComponent> list2 = new global::System.Collections.Generic.List<SquareComponent>();
		foreach (global::System.Collections.Generic.List<SquareComponent> match6 in matches)
		{
			match6.Sort((SquareComponent b2, SquareComponent b1) => b1.Coords.y.CompareTo(b2.Coords.y));
			match6.Sort((SquareComponent b1, SquareComponent b2) => b1.Coords.x.CompareTo(b2.Coords.x));
		}
		matches.Sort((global::System.Collections.Generic.List<SquareComponent> r1, global::System.Collections.Generic.List<SquareComponent> r2) => r1[0].Coords.x.CompareTo(r2[0].Coords.x));
		foreach (global::System.Collections.Generic.List<SquareComponent> match7 in matches)
		{
			list2.AddRange(match7);
		}
		matches.Clear();
		if (list2.Count > 0)
		{
			matches.Add(list2);
		}
		global::System.Collections.Generic.HashSet<GarbageGroup> hashSet = new global::System.Collections.Generic.HashSet<GarbageGroup>();
		float num2 = 0f;
		foreach (global::System.Collections.Generic.List<SquareComponent> match8 in matches)
		{
			if (match8.Count > num)
			{
				num = match8.Count;
			}
			m_isForcingUp = false;
			int num3 = 0;
			bool flag = false;
			foreach (SquareComponent item3 in match8)
			{
				if (item3.ChainID != 0)
				{
					num3 = item3.ChainID;
				}
				ShouldClearChainIds.Remove(item3);
			}
			if (num3 <= 0)
			{
				for (int num4 = 1; num4 < SlottedChainCounter.Count; num4++)
				{
					if (SlottedChainCounter[num4] == 0)
					{
						num3 = num4;
						break;
					}
				}
				flag = true;
			}
			else
			{
				SlottedChainCounter[num3]++;
				if (MyPodManager.IsVersus)
				{
					if (SlottedChainCounter[num3] < 2)
					{
						MyPodManager.OpponentManager.VisualGarbageQueue.AddChainItemToQueue(match8[0].transform.position + global::UnityEngine.Vector3.up, num3);
					}
					else
					{
						MyPodManager.OpponentManager.VisualGarbageQueue.IncrementGarbageVisualChain(match8[0].transform.position + global::UnityEngine.Vector3.up, num3);
					}
				}
			}
			for (int num5 = 0; num5 < match8.Count; num5++)
			{
				match8[num5].ShouldBeDestroyed = true;
				if (MyPodManager.GridManager.TryGetSquareSurronding(match8[num5].Coords, out var sqrs))
				{
					foreach (SquareComponent item4 in sqrs)
					{
						if (item4.IsGarbage && item4.GarbageGroup != null && !item4.GarbageGroup.IsRevealing)
						{
							hashSet.Add(item4.GarbageGroup);
						}
					}
				}
				int num6 = 0;
				if (flag && match8.Count < 4)
				{
					num6 = 0;
				}
				else if (!flag)
				{
					num6 = 2;
					global::UnityEngine.GameObject obj = global::UnityEngine.Object.Instantiate(m_blockLite);
					obj.transform.SetParent(MyPodManager.GridManager.transform);
					obj.transform.localPosition = match8[num5].Coords + new global::UnityEngine.Vector2(0f, OffsetValue);
				}
				else
				{
					num6 = 1;
					global::UnityEngine.GameObject obj2 = global::UnityEngine.Object.Instantiate(m_blockLite);
					obj2.transform.SetParent(MyPodManager.GridManager.transform);
					obj2.transform.localPosition = match8[num5].Coords + new global::UnityEngine.Vector2(0f, OffsetValue);
				}
				global::UnityEngine.GameObject obj3 = global::UnityEngine.Object.Instantiate(m_starSparkle[num6]);
				obj3.transform.SetParent(MyPodManager.GridManager.transform);
				global::UnityEngine.ParticleSystem.MainModule main = obj3.GetComponent<global::UnityEngine.ParticleSystem>().main;
				main.startDelay = 1.08f + (float)num5 * 0.2f;
				obj3.transform.localPosition = match8[num5].Coords + new global::UnityEngine.Vector2(0f, OffsetValue);
				match8[num5].TempChainIDFrames = -1;
				if (num3 != match8[num5].ChainID)
				{
					match8[num5].ChainID = num3;
					if (num3 != 0)
					{
						SlottedChainBlockCounter[num3]++;
						global::UnityEngine.Debug.Log($"Adding 1 to {num3}");
					}
				}
				int chainNum = ((!flag) ? SlottedChainCounter[num3] : 0);
				match8[num5].TryDestroyBlock(58 + match8.Count * 10, num5, chainNum);
			}
			if (hashSet.Count > 0)
			{
				global::System.Collections.Generic.List<GarbageGroup> list3 = new global::System.Collections.Generic.List<GarbageGroup>();
				foreach (GarbageGroup item5 in hashSet)
				{
					if (!item5.FindConnectedGbgs(out var gbgs))
					{
						continue;
					}
					foreach (GarbageGroup item6 in gbgs)
					{
						list3.Add(item6);
					}
				}
				hashSet.UnionWith(list3);
				global::System.Collections.Generic.List<GarbageGroup> list4 = global::System.Linq.Enumerable.ToList(hashSet);
				list4.Sort((GarbageGroup b, GarbageGroup a) => a.LowestRightmostCoord.x.CompareTo(b.LowestRightmostCoord.x));
				list4.Sort((GarbageGroup a, GarbageGroup b) => a.LowestRightmostCoord.y.CompareTo(b.LowestRightmostCoord.y));
				int num7 = 0;
				foreach (GarbageGroup item7 in list4)
				{
					foreach (SquareComponent block in item7.Blocks)
					{
						if (block.Coords.y <= 11f)
						{
							num7++;
						}
					}
					item7.IsRevealing = true;
					item7.SetSeparatedRevealSprite();
				}
				foreach (GarbageGroup item8 in list4)
				{
					item8.FlashBlocks();
				}
				int num8 = 0;
				int num9 = 0;
				int num10 = 0;
				for (int num11 = 0; num11 < list4.Count; num11++)
				{
					list4[num11].GenerateGarbageShapeSprites(6);
					list4[num11].revealChainID = num3;
					for (int num12 = 0; num12 < list4[num11].Blocks.Count; num12++)
					{
						if (list4[num11].Blocks[num12].Coords.y >= 11f)
						{
							if (num10 == 0)
							{
								num10 = num8;
							}
							list4[num11].Blocks[num12].IndividualRevealTs = GameLoopFrameCounter + 60 + match8.Count * 8 + num10;
							list4[num11].Blocks[num12].TotalGBGRevealTs = GameLoopFrameCounter + 60 + match8.Count * 8 + num7 * 10;
							list4[num11].Blocks[num12].GarbageRevealIndex = num9;
						}
						else
						{
							list4[num11].Blocks[num12].IndividualRevealTs = GameLoopFrameCounter + 60 + match8.Count * 8 + num8 + num12 * 10;
							list4[num11].Blocks[num12].TotalGBGRevealTs = GameLoopFrameCounter + 60 + match8.Count * 8 + num7 * 10;
							list4[num11].Blocks[num12].GarbageRevealIndex = num9;
						}
						if (num12 >= 6)
						{
							list4[num11].Blocks[num12].FakeReveal = true;
						}
						else
						{
							list4[num11].Blocks[num12].FakeReveal = false;
							list4[num11].Blocks[num12].TotalGBGChainID = num3;
							MyPodManager.GameLoop.SlottedChainBlockCounter[num3]++;
						}
						num9++;
					}
					num8 += list4[num11].Blocks.Count * 10;
				}
				ClearTime(60 + match8.Count * 12 + num9 * 10);
			}
			if (match8.Count >= ComboCounters.Length)
			{
				global::System.Array.Resize(ref ComboCounters, match8.Count + 1);
			}
			ComboCounters[match8.Count]++;
			if (match8.Count > 3)
			{
				Score += GetComboExtraScore(match8.Count);
				global::UnityEngine.GameObject obj4 = global::UnityEngine.Object.Instantiate(ComboCounterEffect);
				obj4.transform.position = match8[0].transform.position;
				obj4.GetComponentInChildren<global::TMPro.TextMeshPro>().text = match8.Count.ToString() ?? "";
				if (flag)
				{
					global::UnityEngine.GameObject obj5 = global::UnityEngine.Object.Instantiate(m_starSwirlCombo);
					m_lightAnim.clip = m_lightDimmerLight;
					m_lightAnim.Play();
					obj5.transform.position = match8[0].transform.position;
					EmberAmountAdjuster componentInChildren = obj5.transform.GetComponentInChildren<EmberAmountAdjuster>();
					if (componentInChildren != null)
					{
						componentInChildren.SetParams(match8.Count);
					}
				}
				num2 += GetExtraComboTime(match8.Count);
				global::UnityEngine.Debug.Log($"{match8.Count} combo");
				if (flag)
				{
					MyPodManager.AudioManager.PlaySfx(CombosSound[global::UnityEngine.Mathf.Clamp(match8.Count - 3, 0, 4)]);
				}
				if (MyPodManager.IsVersus)
				{
					if (SlottedComboCounter[num3].list == null)
					{
						SlottedComboCounter[num3] = default(ListWrapper<int>);
					}
					SlottedComboCounter[num3].list.Add(match8.Count);
					MyPodManager.OpponentManager.VisualGarbageQueue.AddComboItemToQueue(match8[0].transform.position + global::UnityEngine.Vector3.up, match8.Count);
				}
			}
			if (flag)
			{
				continue;
			}
			int num13 = SlottedChainCounter[num3] + 1;
			Score += GetExtraChainScore(num13);
			global::UnityEngine.GameObject obj6 = global::UnityEngine.Object.Instantiate(ChainCounterEffect);
			obj6.transform.position = match8[0].transform.position + new global::UnityEngine.Vector3(0f, 1f);
			obj6.GetComponentInChildren<global::TMPro.TextMeshPro>().text = num13.ToString() ?? "";
			global::UnityEngine.GameObject obj7 = global::UnityEngine.Object.Instantiate(m_starSwirlChain);
			obj7.transform.position = match8[0].transform.position + new global::UnityEngine.Vector3(0f, 1f);
			m_lightAnim.clip = m_lightDimmerLight;
			m_lightAnim.Play();
			EmberAmountAdjuster componentInChildren2 = obj7.transform.GetComponentInChildren<EmberAmountAdjuster>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.SetParams(num13 + 2);
			}
			num2 += GetExtraChainTime(num13);
			MyPodManager.AudioManager.PlaySfx(ChainSound, 1f, antiOverlap: true, global::UnityEngine.Mathf.Clamp(1f + (0.18f * (float)num13 - 1f), 1f, 1.6f));
			if (VoiceBox != null)
			{
				global::System.Collections.Generic.List<global::UnityEngine.AudioClip> list5 = VoiceBox.GetGroup(num13);
				if (list5 != null && list5.Count > 0)
				{
					int desiredIndex = MyPodManager.RandomManager.Next(0, list5.Count);
					int nextVoiceLineIndex = GetNextVoiceLineIndex(num13, list5.Count, desiredIndex);
					MyPodManager.AudioManager.PlayGameVoiceLine(list5[nextVoiceLineIndex], num13, 1f, antiOverlap: true, 1f, MyPodManager.ISPLAYER1);
				}
			}
		}
		if (num > 0)
		{
			m_hasClearedTS = (float)GameLoopFrameCounter + 1f + (float)num * 0.2f + 0.35f;
			m_allowOneRaiseAfterClear = true;
			FreezeTime(num2);
			ClearTime(60 + num * 12);
		}
	}

	private int GetNextVoiceLineIndex(int multiplier, int poolCount, int desiredIndex)
	{
		int key = ((multiplier > 7) ? (-1) : multiplier);
		int num = desiredIndex % poolCount;
		if (num < 0)
		{
			num += poolCount;
		}
		if (poolCount > 1 && m_lastVoiceLineIndex.TryGetValue(key, out var value) && num == value)
		{
			num = (num + 1) % poolCount;
		}
		m_lastVoiceLineIndex[key] = num;
		return num;
	}

	private void ClearTimeTicker()
	{
		if ((float)GameLoopFrameCounter >= m_unclearTimeStamp)
		{
			Clearing = false;
		}
	}

	public void FreezeTime(float bonusTime)
	{
		if (MyPodManager.IsVersus)
		{
			m_totalBonusFreeze = 0f;
		}
		else
		{
			m_totalBonusFreeze += bonusTime;
		}
	}

	public void ClearTime(int duration)
	{
		if (!((float)(GameLoopFrameCounter + duration) < m_unclearTimeStamp))
		{
			Clearing = true;
			m_unclearTimeStamp = GameLoopFrameCounter + duration;
		}
	}

	public void CheckForLoss(bool fromTwoPlayerManager = false)
	{
		if (HasLossChecked || !Lost)
		{
			return;
		}
		if (TwoPlayerManager.Instance != null && !fromTwoPlayerManager)
		{
			TwoPlayerManager.Instance.OnPlayerLose(MyPodManager);
		}
		else
		{
			if (!fromTwoPlayerManager)
			{
				GameOverLogo.enabled = true;
			}
			if (GameManager.Instance.DefinedGameMode == GameModeType.Marathon)
			{
				bool num = SaveSystem.SubmitHighscore(Score);
				if (Score > 20000)
				{
					SteamAchievementsHandler.Instance.GrantAchievement("MARATHON_20k");
				}
				if (Score > 50000)
				{
					SteamAchievementsHandler.Instance.GrantAchievement("MARATHON_50k");
				}
				if (Score > 100000)
				{
					SteamAchievementsHandler.Instance.GrantAchievement("MARATHON_100k");
				}
				if (num)
				{
					MyPodManager.ShowNewHighscoreUI();
				}
				EndGame();
			}
			if (GameManager.Instance.DefinedGameMode == GameModeType.LineClear)
			{
				EndGame();
			}
		}
		Freeze = true;
		HasLossChecked = true;
	}

	private global::System.Collections.IEnumerator DanceSyncerLoop()
	{
		DanceSyncIndex = (int)global::UnityEngine.Mathf.Repeat(global::UnityEngine.Mathf.Clamp(DanceSyncIndex + 1, 0, 5), 5f);
		for (int i = 0; i < 3; i++)
		{
			yield return new global::UnityEngine.WaitForFixedUpdate();
		}
		if (DanceSyncIndex == 0 || DanceSyncIndex == 3)
		{
			for (int i = 0; i < 3; i++)
			{
				yield return new global::UnityEngine.WaitForFixedUpdate();
			}
		}
		StartCoroutine(DanceSyncerLoop());
	}

	private global::System.Collections.IEnumerator GbgOverlaySyncerLoop()
	{
		GarbageOverlaySyncIndex = (int)global::UnityEngine.Mathf.Repeat(global::UnityEngine.Mathf.Clamp(GarbageOverlaySyncIndex + 1, 0, 3), 3f);
		for (int i = 0; i < 60; i++)
		{
			yield return new global::UnityEngine.WaitForFixedUpdate();
		}
		StartCoroutine(GbgOverlaySyncerLoop());
	}

	public void EndGame(bool isWinner = false, bool isDraw = false)
	{
		AudioManager.Instance.FadeMusicSpeed(0f);
		if (MyPodManager.VisualGarbageQueue != null)
		{
			MyPodManager.VisualGarbageQueue.PurgeAllQueues();
		}
		PauseManager component = global::UnityEngine.EventSystems.EventSystem.current.gameObject.GetComponent<PauseManager>();
		if (component != null)
		{
			component.enabled = false;
		}
		PersistentInputReader.Instance.AllowPausing = false;
		switch (GameManager.Instance.DefinedGameMode)
		{
		case GameModeType.Campaign:
			StartCoroutine(EndGameRoutineCampaign(isWinner));
			break;
		case GameModeType.Marathon:
			StartCoroutine(EndGameRoutineDefault(isWinner));
			break;
		case GameModeType.LineClear:
			StartCoroutine(EndGameLineClearRoutine(isWinner));
			break;
		case GameModeType.LocalMp:
			StartCoroutine(EndGameRoutineDefault(isWinner, isDraw));
			AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
			if (!isDraw)
			{
				if (isWinner && MyPodManager.ISPLAYER1)
				{
					TallyManager.Instance.IncreaseP1Score();
				}
				else if (isWinner && !MyPodManager.ISPLAYER1)
				{
					TallyManager.Instance.IncreaseP2Score();
				}
			}
			break;
		case GameModeType.Online:
			StartCoroutine(EndGameRoutineDefault(isWinner, isDraw));
			AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
			if (!isDraw)
			{
				if (isWinner && MyPodManager.ISPLAYER1)
				{
					TallyManager.Instance.IncreaseP1Score();
					SteamAchievementsHandler.Instance.GrantAchievement("WIN_ONLINE_MATCH");
				}
				else if (isWinner && !MyPodManager.ISPLAYER1)
				{
					TallyManager.Instance.IncreaseP2Score();
				}
			}
			NetworkServerReciever.Instance.HasEnded = true;
			break;
		}
	}

	private void SetAllBlocksLose()
	{
		global::System.Collections.Generic.List<SquareComponent> list = new global::System.Collections.Generic.List<SquareComponent>();
		foreach (SquareComponent aliveSquare in MyPodManager.GridManager.AliveSquares)
		{
			if (!aliveSquare.IsBeingDestroyed)
			{
				list.Add(aliveSquare);
				OffsetValue = 0.25f;
			}
		}
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			if (list[i] != null)
			{
				list[i].SetDance(shouldDance: false);
				list[i].LoseBlock();
			}
		}
	}

	private void DestroyAllBlocks()
	{
		global::System.Collections.Generic.List<SquareComponent> list = new global::System.Collections.Generic.List<SquareComponent>();
		foreach (SquareComponent aliveSquare in MyPodManager.GridManager.AliveSquares)
		{
			if (!aliveSquare.IsBeingDestroyed)
			{
				list.Add(aliveSquare);
				OffsetValue = 0.25f;
			}
		}
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			if (list[i] != null)
			{
				global::UnityEngine.Object.Destroy(list[i].gameObject);
			}
		}
	}

	private global::System.Collections.IEnumerator PopBlocksAscending()
	{
		global::System.Collections.Generic.List<SquareComponent> popSqrs = new global::System.Collections.Generic.List<SquareComponent>();
		foreach (SquareComponent aliveSquare in MyPodManager.GridManager.AliveSquares)
		{
			if (!aliveSquare.IsBeingDestroyed)
			{
				popSqrs.Add(aliveSquare);
				OffsetValue = 0.25f;
			}
		}
		int len = popSqrs.Count;
		popSqrs.Sort((SquareComponent b1, SquareComponent b2) => b1.Coords.y.CompareTo(b2.Coords.y));
		float prevY = 0f;
		float waitLim = 0f;
		for (int i = 0; i < len; i++)
		{
			if (prevY < popSqrs[i].Coords.y)
			{
				prevY = popSqrs[i].Coords.y;
				if (waitLim < 0.95f)
				{
					yield return new global::UnityEngine.WaitForSeconds(0.1f);
					waitLim += 0.1f;
				}
			}
			if (popSqrs[i] != null)
			{
				popSqrs[i].SetDance(shouldDance: false);
				popSqrs[i].LoseBlock();
			}
		}
		if (waitLim < 1f)
		{
			yield return new global::UnityEngine.WaitForSeconds(1f - waitLim);
		}
	}

	private global::System.Collections.IEnumerator EndGameLineClearRoutine(bool isWinner = false)
	{
		if (isWinner)
		{
			AudioManager.Instance.ChangeSong(MusicTrackType.NoMusic);
			yield return new global::UnityEngine.WaitForFixedUpdate();
			SetAllBlocksLose();
			m_lineClearDemandPlayerPopBlocks?.PlayAudioOnDemand();
			m_winSparksObj.SetActive(value: true);
			m_lineClearline.enabled = false;
			m_lineClearObj.SetActive(value: false);
			m_cursor.SetInvis();
			yield return new global::UnityEngine.WaitForSeconds(2f);
			LineClearWinLogo.SetActive(value: true);
			DestroyAllBlocks();
			yield return new global::UnityEngine.WaitForSeconds(1f);
			LineClearingManager.Instance.SectionWon();
			if (MyPodManager.VisualGarbageQueue != null)
			{
				MyPodManager.VisualGarbageQueue.PurgeAllQueues();
			}
			yield break;
		}
		StartBlockShake();
		MyPodManager.AudioManager.PlaySfx(DeathConk, 0.25f);
		yield return StartCoroutine(PopBlocksAscending());
		yield return new global::UnityEngine.WaitForSeconds(1.3f);
		yield return new global::UnityEngine.WaitForFixedUpdate();
		yield return new global::UnityEngine.WaitForSeconds(2f);
		yield return new global::UnityEngine.WaitForFixedUpdate();
		DestroyAllBlocks();
		MyPodManager.Lose();
		m_cursor.SetInvis();
		m_lineClearline.enabled = false;
		m_lineClearObj.SetActive(value: false);
		LineClearLossLogo.SetActive(value: true);
		LineClearingManager.Instance.SectionLose();
		if (MyPodManager.VisualGarbageQueue != null)
		{
			MyPodManager.VisualGarbageQueue.PurgeAllQueues();
		}
	}

	private global::System.Collections.IEnumerator EndGameRoutineCampaign(bool isWin = false)
	{
		if (isWin)
		{
			yield return new global::UnityEngine.WaitForFixedUpdate();
			yield return new global::UnityEngine.WaitForSeconds(2.3f);
			yield return new global::UnityEngine.WaitForFixedUpdate();
			PodAnim.SetBool("isClosed", value: true);
			yield return new global::UnityEngine.WaitForSeconds(1.5f);
			yield return new global::UnityEngine.WaitForFixedUpdate();
			m_cursor.SetInvis();
			MyPodManager.Win();
			DestroyAllBlocks();
			yield return new global::UnityEngine.WaitForSeconds(0.1f);
			PodAnim.SetBool("isClosed", value: false);
			yield return new global::UnityEngine.WaitForSeconds(1.5f);
			if (!MyPodManager.IsControlledByCPU)
			{
				CampaignManager.Instance.OnLevelWon();
			}
			if (MyPodManager.VisualGarbageQueue != null)
			{
				MyPodManager.VisualGarbageQueue.PurgeAllQueues();
			}
			yield break;
		}
		StartBlockShake();
		MyPodManager.AudioManager.PlaySfx(DeathConk, 0.25f);
		yield return StartCoroutine(PopBlocksAscending());
		yield return new global::UnityEngine.WaitForSeconds(1.3f);
		yield return new global::UnityEngine.WaitForFixedUpdate();
		PodAnim.SetBool("isClosed", value: true);
		yield return new global::UnityEngine.WaitForSeconds(1.5f);
		yield return new global::UnityEngine.WaitForFixedUpdate();
		m_cursor.SetInvis();
		MyPodManager.Lose();
		DestroyAllBlocks();
		yield return new global::UnityEngine.WaitForSeconds(0.1f);
		PodAnim.SetBool("isClosed", value: false);
		yield return new global::UnityEngine.WaitForSeconds(1.5f);
		if (!MyPodManager.IsControlledByCPU)
		{
			CampaignManager.Instance.OnLevelLost();
		}
		if (MyPodManager.VisualGarbageQueue != null)
		{
			MyPodManager.VisualGarbageQueue.PurgeAllQueues();
		}
	}

	private global::System.Collections.IEnumerator EndGameRoutineDefault(bool isWin = false, bool isDraw = false)
	{
		if (isDraw)
		{
			yield return new global::UnityEngine.WaitForFixedUpdate();
			PodAnim.SetBool("isClosed", value: true);
			yield return new global::UnityEngine.WaitForSeconds(1.9f);
			yield return new global::UnityEngine.WaitForFixedUpdate();
			m_scoreMenu.SetActive(value: true);
			m_cursor.SetInvis();
			MyPodManager.Draw();
			DestroyAllBlocks();
			yield return new global::UnityEngine.WaitForSeconds(0.1f);
			PodAnim.SetBool("isClosed", value: false);
			if (MyPodManager.IsOnlineMp && NetworkServerReciever.Instance != null && NetworkServerReciever.Instance.IsHost && m_rematchButton != null)
			{
				m_rematchButton.SetActive(value: true);
			}
			if (MyPodManager.IsOnlineMp && NetworkServerReciever.Instance != null && !NetworkServerReciever.Instance.IsHost && m_rematchDescClient != null)
			{
				m_rematchDescClient.SetActive(value: true);
			}
			if (MyPodManager.VisualGarbageQueue != null)
			{
				MyPodManager.VisualGarbageQueue.PurgeAllQueues();
			}
			yield break;
		}
		if (isWin)
		{
			yield return new global::UnityEngine.WaitForFixedUpdate();
			yield return new global::UnityEngine.WaitForSeconds(2.3f);
			yield return new global::UnityEngine.WaitForFixedUpdate();
			PodAnim.SetBool("isClosed", value: true);
			yield return new global::UnityEngine.WaitForSeconds(1.9f);
			yield return new global::UnityEngine.WaitForFixedUpdate();
			m_scoreMenu.SetActive(value: true);
			m_cursor.SetInvis();
			MyPodManager.Win();
			DestroyAllBlocks();
			yield return new global::UnityEngine.WaitForSeconds(0.1f);
			PodAnim.SetBool("isClosed", value: false);
			if (MyPodManager.IsOnlineMp && NetworkServerReciever.Instance != null && NetworkServerReciever.Instance.IsHost && m_rematchButton != null)
			{
				m_rematchButton.SetActive(value: true);
			}
			if (MyPodManager.IsOnlineMp && NetworkServerReciever.Instance != null && !NetworkServerReciever.Instance.IsHost && m_rematchDescClient != null)
			{
				m_rematchDescClient.SetActive(value: true);
			}
			if (MyPodManager.VisualGarbageQueue != null)
			{
				MyPodManager.VisualGarbageQueue.PurgeAllQueues();
			}
			yield break;
		}
		StartBlockShake();
		MyPodManager.AudioManager.PlaySfx(DeathConk, 0.25f);
		yield return StartCoroutine(PopBlocksAscending());
		yield return new global::UnityEngine.WaitForSeconds(1.3f);
		yield return new global::UnityEngine.WaitForFixedUpdate();
		PodAnim.SetBool("isClosed", value: true);
		yield return new global::UnityEngine.WaitForSeconds(1.9f);
		yield return new global::UnityEngine.WaitForFixedUpdate();
		m_cursor.SetInvis();
		m_scoreMenu.SetActive(value: true);
		if (MyPodManager.IsOnlineMp && NetworkServerReciever.Instance != null && NetworkServerReciever.Instance.IsHost && m_rematchButton != null)
		{
			m_rematchButton.SetActive(value: true);
		}
		if (MyPodManager.IsOnlineMp && NetworkServerReciever.Instance != null && !NetworkServerReciever.Instance.IsHost && m_rematchDescClient != null)
		{
			m_rematchDescClient.SetActive(value: true);
		}
		MyPodManager.Lose();
		DestroyAllBlocks();
		yield return new global::UnityEngine.WaitForSeconds(0.1f);
		PodAnim.SetBool("isClosed", value: false);
		if (MyPodManager.VisualGarbageQueue != null)
		{
			MyPodManager.VisualGarbageQueue.PurgeAllQueues();
		}
	}

	public void StartBlockShake()
	{
		if (!m_blockShake.IsPlaying("blocksShake"))
		{
			m_blockShake.Play();
		}
	}
}
