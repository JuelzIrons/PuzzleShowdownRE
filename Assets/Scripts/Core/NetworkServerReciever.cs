public class NetworkServerReciever : global::Unity.Netcode.NetworkBehaviour
{
	public readonly uint MOVE_LATENCY = 72u;

	public global::System.Collections.Generic.List<OpponentMove> m_opponentMoves = new global::System.Collections.Generic.List<OpponentMove>();

	public global::System.Collections.Generic.List<OpponentMove> m_opponentSwaps = new global::System.Collections.Generic.List<OpponentMove>();

	public global::System.Collections.Generic.List<OpponentMove> m_opponentForceRaises = new global::System.Collections.Generic.List<OpponentMove>();

	public global::System.Collections.Generic.List<OpponentMove> m_opponentLostMoves = new global::System.Collections.Generic.List<OpponentMove>();

	public static NetworkServerReciever Instance;

	public global::Unity.Netcode.NetworkVariable<int> HostDifficultyLevel = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<bool> HostHasLaunchedScene = new global::Unity.Netcode.NetworkVariable<bool>(value: false);

	public global::Unity.Netcode.NetworkVariable<int> NetworkedRandomSeed = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<int> NetworkReadyCounter = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<int> HostCharacter = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<int> ClientCharacter = new global::Unity.Netcode.NetworkVariable<int>(0);

	public global::Unity.Netcode.NetworkVariable<bool> LetClientSelectChar = new global::Unity.Netcode.NetworkVariable<bool>(value: false);

	public global::Unity.Netcode.NetworkVariable<bool> DRAW = new global::Unity.Netcode.NetworkVariable<bool>(value: false);

	public PodManager LocalPlayerPodManager;

	public PodManager LocalOpponentPodManager;

	public uint LocalTS;

	public bool HasLoadedInLocally;

	public bool LocalOpponentHasDied;

	public bool ForceTrueThisFrame;

	private int HostsDeathTS = -1;

	private int ClientsDeathTS = -1;

	private int ClientScoreAtDeath = -1;

	private int HostScoreAtDeath = -1;

	public bool HasEnded;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_hostWins;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_clientWins;

	[global::UnityEngine.SerializeField]
	private global::TMPro.TextMeshProUGUI m_localTSDebug;

	private bool m_gameEndBroadcasted;

	public void ClearAllQueues()
	{
		HasEnded = false;
		if (base.IsHost)
		{
			DRAW.Value = false;
		}
		HostsDeathTS = -1;
		ClientsDeathTS = -1;
		ClientScoreAtDeath = -1;
		HostScoreAtDeath = -1;
		m_gameEndBroadcasted = false;
		m_opponentMoves.Clear();
		m_opponentForceRaises.Clear();
		m_opponentLostMoves.Clear();
		m_opponentSwaps.Clear();
	}

	private void Awake()
	{
		Instance = this;
		LocalTS = 0u;
		if (global::Unity.Netcode.NetworkManager.Singleton.IsHost)
		{
			ClientCharacter.Value = -1;
			HostCharacter.Value = -1;
		}
		global::Unity.Netcode.NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
		global::Unity.Netcode.NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectCallback;
		HostSetDifficultyLevel(1);
	}

	private void OnClientDisconnectCallback(ulong clientId)
	{
		RelayManager.Instance.ReturnToMainMenu();
	}

	private void OnClientConnectCallback(ulong clientId)
	{
		RelayManager.Instance.SetupCharSelectScreenLocally();
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void MoveCursorToPosRpc(ulong id, int x, int y)
	{
		if (base.NetworkManager.LocalClientId != id)
		{
			RelayManager.Instance.CharGrid.ForceOpponentCursorToPos(new global::UnityEngine.Vector2Int(x, y));
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void SendReadyUpEventToOpponentRpc(ulong id, bool isReady)
	{
		if (base.NetworkManager.LocalClientId != id)
		{
			RelayManager.Instance.CharGrid.ForceOpponentReadyState(isReady);
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void DisconnectAsClientRpc(ulong id)
	{
		global::Unity.Netcode.NetworkManager.Singleton.DisconnectClient(id);
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void CloseConnectionScreenForAllClientRpc()
	{
		RelayManager.Instance.LocallyCloseLobbyCanvas(base.IsHost);
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void SendReadyToServerRpc()
	{
		if (base.IsServer)
		{
			NetworkReadyCounter.Value++;
		}
	}

	public void HostSetDifficultyLevel(int Level)
	{
		HostDifficultyLevel.Value = Level;
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.Server, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void SendMoveToServerRpc(uint TS, int moveType, ulong playerId, int moveOrder, global::UnityEngine.Vector2 dir = default(global::UnityEngine.Vector2), int scoreAtTimeOfDeath = -1)
	{
		global::UnityEngine.Debug.Log($"Broadcasting move of type {(Moves)moveType} with TS: {TS} From player with id: {playerId}");
		BroadcastOpponentMovesRpc(TS + MOVE_LATENCY, moveType, playerId, moveOrder, dir);
		if (moveType != 3)
		{
			return;
		}
		if (global::Unity.Netcode.NetworkManager.Singleton.LocalClientId == playerId && HostsDeathTS == -1)
		{
			HostsDeathTS = (int)TS;
			HostScoreAtDeath = scoreAtTimeOfDeath;
		}
		if (global::Unity.Netcode.NetworkManager.Singleton.LocalClientId != playerId && ClientsDeathTS == -1)
		{
			ClientsDeathTS = (int)TS;
			ClientScoreAtDeath = scoreAtTimeOfDeath;
		}
		if (ClientsDeathTS == HostsDeathTS && ClientsDeathTS != -1 && HostsDeathTS != -1)
		{
			DRAW.Value = true;
			ulong hostId = global::Unity.Netcode.NetworkManager.Singleton.LocalClientId;
			ulong winnerClientId = global::System.Linq.Enumerable.First(global::Unity.Netcode.NetworkManager.Singleton.ConnectedClientsIds, (ulong id) => id != hostId);
			if (HostScoreAtDeath > ClientScoreAtDeath)
			{
				BroadcastTimeoutResultRpc(hostId);
			}
			else if (HostScoreAtDeath < ClientScoreAtDeath)
			{
				BroadcastTimeoutResultRpc(winnerClientId);
			}
			else
			{
				BroadcastTimeoutResultRpc(hostId, isDraw: true);
			}
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void BroadcastTimeoutResultRpc(ulong winnerClientId, bool isDraw = false)
	{
		if (!m_gameEndBroadcasted)
		{
			m_gameEndBroadcasted = true;
			bool flag = global::Unity.Netcode.NetworkManager.Singleton.LocalClientId == winnerClientId;
			LocalPlayerPodManager.GameLoop.Lost = true;
			LocalOpponentPodManager.GameLoop.Lost = true;
			LocalPlayerPodManager.GameLoop.HasLossChecked = false;
			LocalOpponentPodManager.GameLoop.HasLossChecked = true;
			LocalPlayerPodManager.GameLoop.EndGame(flag, isDraw);
			LocalPlayerPodManager.GameLoop.HasLossChecked = true;
			LocalOpponentPodManager.GameLoop.EndGame(!flag, isDraw);
			LocalOpponentPodManager.GameLoop.HasLossChecked = true;
		}
	}

	[global::Unity.Netcode.Rpc(global::Unity.Netcode.SendTo.ClientsAndHost, InvokePermission = global::Unity.Netcode.RpcInvokePermission.Everyone)]
	public void BroadcastOpponentMovesRpc(uint TS, int moveType, ulong playerId, int moveOrder, global::UnityEngine.Vector2 dir = default(global::UnityEngine.Vector2))
	{
		if (base.NetworkManager.LocalClientId == playerId)
		{
			return;
		}
		global::UnityEngine.Debug.Log($"MOVE ADDED WITH TS: {TS} MoveType: {(Moves)moveType} Dir = {dir}");
		switch (moveType)
		{
		case 2:
			m_opponentForceRaises.Add(new OpponentMove(TS, (Moves)moveType, moveOrder, dir));
			m_opponentForceRaises.Sort((OpponentMove m1, OpponentMove m2) => m1.TS.CompareTo(m2.TS));
			break;
		case 3:
			m_opponentLostMoves.Add(new OpponentMove(TS, (Moves)moveType, moveOrder, dir));
			m_opponentLostMoves.Sort((OpponentMove m1, OpponentMove m2) => m1.TS.CompareTo(m2.TS));
			break;
		case 1:
			m_opponentSwaps.Add(new OpponentMove(TS, (Moves)moveType, moveOrder, dir));
			m_opponentSwaps.Sort((OpponentMove m1, OpponentMove m2) => m1.TS.CompareTo(m2.TS));
			break;
		case 0:
			m_opponentMoves.Add(new OpponentMove(TS, (Moves)moveType, moveOrder, dir));
			m_opponentMoves.Sort((OpponentMove m1, OpponentMove m2) => m1.TS.CompareTo(m2.TS));
			break;
		}
		if ((float)(TS - LocalTS) <= 0f)
		{
			global::UnityEngine.Debug.LogError($"Err Recieved Move diff: {TS - LocalTS}");
			RelayManager.Instance.ReturnToMainMenu();
		}
		else if ((float)(TS - LocalTS) < 0.5f)
		{
			global::UnityEngine.Debug.LogWarning($"Warning Recieved Move diff: {TS - LocalTS}");
		}
		else
		{
			global::UnityEngine.Debug.Log($"Recieved Move diff: {TS - LocalTS}");
		}
	}

	public void ServerInit()
	{
		LocalPlayerPodManager.GameLoop.HangingFrames = 7;
		LocalOpponentPodManager.GameLoop.HangingFrames = 7;
		LocalPlayerPodManager.GameLoop.SpeedLv = HostDifficultyLevel.Value;
		LocalOpponentPodManager.GameLoop.SpeedLv = HostDifficultyLevel.Value;
		if (global::Unity.Netcode.NetworkManager.Singleton.IsHost)
		{
			LocalPlayerPodManager.SetSelectedCharacter((CharacterType)Instance.HostCharacter.Value);
			LocalOpponentPodManager.SetSelectedCharacter((CharacterType)Instance.ClientCharacter.Value);
		}
		else
		{
			LocalPlayerPodManager.SetSelectedCharacter((CharacterType)Instance.ClientCharacter.Value);
			LocalOpponentPodManager.SetSelectedCharacter((CharacterType)Instance.HostCharacter.Value);
		}
	}

	public void RecieverFixedUpdate()
	{
		ForceTrueThisFrame = false;
		m_localTSDebug.text = LocalTS.ToString() ?? "";
		if ((!HasLoadedInLocally && NetworkReadyCounter.Value >= 2) || HasEnded)
		{
			return;
		}
		LocalTS++;
		if (LocalTS == 36000)
		{
			LocalPlayerPodManager.GameLoop.CallMpLoss();
			ExtraElementsManager.Instance.TimeBuzzer?.Play();
		}
		global::System.Collections.Generic.List<OpponentMove> list = new global::System.Collections.Generic.List<OpponentMove>();
		if (m_opponentForceRaises.Count > 0 && m_opponentForceRaises[0].TS == (float)LocalTS)
		{
			list.Add(m_opponentForceRaises[0]);
			m_opponentForceRaises.RemoveAt(0);
		}
		if (m_opponentLostMoves.Count > 0 && m_opponentLostMoves[0].TS == (float)LocalTS)
		{
			list.Add(m_opponentLostMoves[0]);
			m_opponentLostMoves.RemoveAt(0);
		}
		if (m_opponentMoves.Count > 0 && m_opponentMoves[0].TS == (float)LocalTS)
		{
			global::System.Collections.Generic.List<OpponentMove> list2 = new global::System.Collections.Generic.List<OpponentMove>();
			for (int i = 0; i < m_opponentMoves.Count; i++)
			{
				if (m_opponentMoves[i].TS == (float)LocalTS)
				{
					list2.Add(m_opponentMoves[i]);
				}
			}
			foreach (OpponentMove item in list2)
			{
				list.Add(item);
				m_opponentMoves.RemoveAt(0);
			}
		}
		if (m_opponentSwaps.Count > 0 && m_opponentSwaps[0].TS == (float)LocalTS)
		{
			list.Add(m_opponentSwaps[0]);
			m_opponentSwaps.RemoveAt(0);
		}
		list.Sort((OpponentMove m1, OpponentMove m2) => m1.MoveFrameOrder.CompareTo(m2.MoveFrameOrder));
		foreach (OpponentMove item2 in list)
		{
			PerformMoveLocally(item2);
		}
	}

	public void StartTimer()
	{
		HasLoadedInLocally = true;
		SendReadyToServerRpc();
	}

	private void PerformMoveLocally(OpponentMove move)
	{
		switch (move.MoveType)
		{
		case Moves.Move:
			LocalOpponentPodManager.CursorController.ApplyNetworkMove(move.Dir);
			break;
		case Moves.Swap:
			LocalOpponentPodManager.CursorController.NetworkedSwap();
			break;
		case Moves.ForceTrue:
			ForceTrueThisFrame = true;
			break;
		case Moves.LOST:
			LocalOpponentHasDied = true;
			break;
		}
	}
}
